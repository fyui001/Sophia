namespace Sophia.Api.Controllers.Discord;

using System.Security.Claims;
using System.Text.Json;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sophia.Infrastructure.DbContext;
using Sophia.Infrastructure.Models;

public class DiscordCallbackAction(
    IConfiguration configuration,
    SophiaContext dbContext,
    IHttpClientFactory httpClientFactory
) : ControllerBase
{
    [HttpGet("/api/discord/callback")]
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    [ApiExplorerSettings(IgnoreApi = true)]
    public async Task<IActionResult> Invoke(
        [FromQuery] string code,
        [FromQuery] string state)
    {
        var graceOrigin = configuration["Grace:Origin"]!;
        var redirectUrl = $"{graceOrigin}/settings";

        // CSRF state validation
        var storedState = Request.Cookies["discord_link_state"];
        Response.Cookies.Delete("discord_link_state");

        if (string.IsNullOrEmpty(storedState) || storedState != state)
        {
            return BadRequest("Invalid state parameter");
        }

        // Exchange code for access token via Discord API
        var discordClientId = configuration["Discord:ClientId"]!;
        var discordClientSecret = configuration["Discord:ClientSecret"]!;
        var callbackUri = $"{graceOrigin}/api/discord/callback";

        var httpClient = httpClientFactory.CreateClient();
        var tokenResponse = await httpClient.PostAsync(
            "https://discord.com/api/oauth2/token",
            new FormUrlEncodedContent(new Dictionary<string, string>
            {
                ["grant_type"] = "authorization_code",
                ["client_id"] = discordClientId,
                ["client_secret"] = discordClientSecret,
                ["code"] = code,
                ["redirect_uri"] = callbackUri,
            })
        );

        if (!tokenResponse.IsSuccessStatusCode)
        {
            return Redirect(redirectUrl);
        }

        var tokenJson = await tokenResponse.Content.ReadFromJsonAsync<JsonElement>();
        var accessToken = tokenJson.GetProperty("access_token").GetString();

        if (accessToken is null)
        {
            return Redirect(redirectUrl);
        }

        // Fetch Discord user info
        var userRequest = new HttpRequestMessage(HttpMethod.Get, "https://discord.com/api/users/@me");
        userRequest.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
        var userResponse = await httpClient.SendAsync(userRequest);

        if (!userResponse.IsSuccessStatusCode)
        {
            return Redirect(redirectUrl);
        }

        var discordUser = await userResponse.Content.ReadFromJsonAsync<JsonElement>();
        var discordUserId = discordUser.GetProperty("id").GetString();

        if (discordUserId is null)
        {
            return Redirect(redirectUrl);
        }

        // Save Discord UserId to the authenticated user's record
        var auth0Sub = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "";
        var user = await dbContext.Users
            .FirstOrDefaultAsync(u => u.Auth0Sub == auth0Sub && u.Status == UserStatus.Valid);

        if (user is not null)
        {
            user.DiscordUserId = discordUserId;
            user.UpdatedAt = DateTime.UtcNow;
            await dbContext.SaveChangesAsync();
        }

        return Redirect(redirectUrl);
    }
}
