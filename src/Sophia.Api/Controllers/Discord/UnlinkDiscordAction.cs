namespace Sophia.Api.Controllers.Discord;

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

public class UnlinkDiscordAction(SophiaContext dbContext) : ControllerBase
{
    [HttpDelete("/api/user/discord")]
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    [ProducesResponseType<UnlinkDiscordResponder>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Invoke()
    {
        var sub = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "";

        var user = await dbContext.Users
            .FirstOrDefaultAsync(u => u.Auth0Sub == sub && u.Status == UserStatus.Valid);

        if (user is null)
        {
            return NotFound(new BaseResponder<object>(false, "User not found", null, null));
        }

        user.DiscordUserId = null;
        user.UpdatedAt = DateTime.UtcNow;
        await dbContext.SaveChangesAsync();

        var data = new UserDto(
            Id: user.Id,
            Name: user.Name,
            IconUrl: user.IconUrl,
            IsRegistered: true,
            DiscordUserId: null
        );

        return Ok(UnlinkDiscordResponder.Create(data));
    }
}
