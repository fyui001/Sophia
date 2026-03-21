namespace Sophia.Api.Controllers.Api;

using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sophia.Api.Responder;
using Sophia.Infrastructure.DbContext;
using Sophia.Infrastructure.Models;

public class RegisterUserAction(SophiaContext dbContext) : ControllerBase
{
    [HttpPost("/api/user/register")]
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    [ProducesResponseType(typeof(BaseResponder<UserResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Invoke([FromBody] RegisterUserRequest request)
    {
        var sub = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "";
        var picture = User.FindFirstValue("picture") ?? "";

        var existingUser = await dbContext.Users
            .FirstOrDefaultAsync(u => u.Auth0Sub == sub);

        if (existingUser is not null)
        {
            return Conflict(new BaseResponder<object>(false, "User already registered", null, null));
        }

        var now = DateTime.UtcNow;
        var newUser = new Infrastructure.Models.User
        {
            Auth0Sub = sub,
            Name = request.Name,
            IconUrl = picture,
            Status = UserStatus.Valid,
            CreatedAt = now,
            UpdatedAt = now,
        };

        dbContext.Users.Add(newUser);
        await dbContext.SaveChangesAsync();

        var data = new UserResponse(
            Id: newUser.Id,
            Name: newUser.Name,
            IconUrl: newUser.IconUrl,
            IsRegistered: true
        );

        return Ok(BaseResponder<UserResponse>.Success(data));
    }
}

public record RegisterUserRequest
{
    [Required]
    [MaxLength(255)]
    public required string Name { get; init; }
}
