namespace Sophia.Api.Controllers.User;

using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sophia.Api.DataTransferObject.User;
using Sophia.Api.Responder.User;
using Sophia.Infrastructure.DbContext;
using Sophia.Infrastructure.Models;

public class GetUserAction(SophiaContext dbContext) : ControllerBase
{
    [HttpGet("/api/user")]
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    [ProducesResponseType<GetUserResponder>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Invoke()
    {
        var sub = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "";

        var dbUser = await dbContext.Users
            .FirstOrDefaultAsync(u => u.Auth0Sub == sub && u.Status == UserStatus.Valid);

        if (dbUser is not null)
        {
            var data = new UserDto(
                Id: dbUser.Id,
                Name: dbUser.Name,
                IconUrl: dbUser.IconUrl,
                IsRegistered: true,
                DiscordUserId: dbUser.DiscordUserId
            );
            return Ok(GetUserResponder.Create(data));
        }

        var unregistered = new UserDto(
            Id: null,
            Name: null,
            IconUrl: null,
            IsRegistered: false,
            DiscordUserId: null
        );
        return Ok(GetUserResponder.Create(unregistered));
    }
}
