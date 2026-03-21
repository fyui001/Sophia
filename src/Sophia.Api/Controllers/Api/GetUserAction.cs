namespace Sophia.Api.Controllers.Api;

using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sophia.Api.Responder;
using Sophia.Infrastructure.DbContext;
using Sophia.Infrastructure.Models;

public class GetUserAction(SophiaContext dbContext) : ControllerBase
{
    [HttpGet("/api/user")]
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    [ProducesResponseType(typeof(BaseResponder<UserResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Invoke()
    {
        var sub = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "";

        var dbUser = await dbContext.Users
            .FirstOrDefaultAsync(u => u.Auth0Sub == sub && u.Status == UserStatus.Valid);

        if (dbUser is not null)
        {
            var data = new UserResponse(
                Id: dbUser.Id,
                Name: dbUser.Name,
                IconUrl: dbUser.IconUrl,
                IsRegistered: true
            );
            return Ok(BaseResponder<UserResponse>.Success(data));
        }

        var unregistered = new UserResponse(
            Id: null,
            Name: null,
            IconUrl: null,
            IsRegistered: false
        );
        return Ok(BaseResponder<UserResponse>.Success(unregistered));
    }
}

public record UserResponse(long? Id, string? Name, string? IconUrl, bool IsRegistered);
