namespace Sophia.Api.Controllers.Discord;

using System.Security.Cryptography;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

public class DiscordLinkAction(IConfiguration configuration) : ControllerBase
{
    [HttpGet("/api/discord/link")]
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    [ApiExplorerSettings(IgnoreApi = true)]
    public IActionResult Invoke()
    {
        var discordClientId = configuration["Discord:ClientId"]!;
        var graceOrigin = configuration["Grace:Origin"]!;
        var redirectUri = $"{graceOrigin}/api/discord/callback";

        var state = Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));
        Response.Cookies.Append("discord_link_state", state, new CookieOptions
        {
            HttpOnly = true,
            SameSite = SameSiteMode.Lax,
            Secure = Request.IsHttps,
            MaxAge = TimeSpan.FromMinutes(10),
        });

        var authorizeUrl = "https://discord.com/oauth2/authorize"
            + $"?response_type=code"
            + $"&client_id={Uri.EscapeDataString(discordClientId)}"
            + $"&redirect_uri={Uri.EscapeDataString(redirectUri)}"
            + $"&scope=identify"
            + $"&state={Uri.EscapeDataString(state)}";

        return Redirect(authorizeUrl);
    }
}
