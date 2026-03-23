namespace Sophia.Api.Controllers.User;

using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sophia.Api.DataTransferObject.User;
using Sophia.Api.Responder;
using Sophia.Api.Responder.User;
using Sophia.Infrastructure.DbContext;
using Sophia.Infrastructure.Models;

public class RegisterUserAction(SophiaContext dbContext) : ControllerBase
{
    [HttpPost("/api/user/register")]
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    [ProducesResponseType<RegisterUserResponder>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Invoke([FromBody] Requests.User.RegisterUserRequest request)
    {
        var sub = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "";
        var picture = User.FindFirstValue("picture") ?? "";

        var existingUser = await dbContext.Users
            .FirstOrDefaultAsync(u => u.Auth0Sub == sub);

        if (existingUser is not null)
        {
            return Conflict(new BaseResponder<object>(false, "User already registered", null, null));
        }

        // Extract Discord UserId if authenticated via Discord social connection
        // sub format: "oauth2|discord|<user_id>" or "discord|<user_id>"
        string? discordUserId = null;
        var parts = sub.Split('|');
        var discordIndex = Array.IndexOf(parts, "discord");
        if (discordIndex >= 0 && discordIndex + 1 < parts.Length)
        {
            discordUserId = parts[discordIndex + 1];
        }

        var now = DateTime.UtcNow;
        var newUser = new Infrastructure.Models.User
        {
            Auth0Sub = sub,
            Name = request.Name,
            IconUrl = picture,
            DiscordUserId = discordUserId,
            Status = UserStatus.Valid,
            CreatedAt = now,
            UpdatedAt = now,
        };

        dbContext.Users.Add(newUser);
        await dbContext.SaveChangesAsync();

        var data = new UserDto(
            Id: newUser.Id,
            Name: newUser.Name,
            IconUrl: newUser.IconUrl,
            IsRegistered: true,
            DiscordUserId: newUser.DiscordUserId
        );

        return Ok(RegisterUserResponder.Create(data));
    }
}
