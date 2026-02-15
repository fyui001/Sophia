namespace Sophia.Api.Controllers.Api;

using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sophia.Api.Responder;

public class GetUserAction : ControllerBase
{
    [HttpGet("/api/user")]
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    [ProducesResponseType(typeof(BaseResponder<UserResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public IActionResult Invoke()
    {
        var data = new UserResponse(
            Id: User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "",
            Name: User.FindFirstValue("name") ?? "",
            Email: User.FindFirstValue(ClaimTypes.Email) ?? ""
        );

        return Ok(BaseResponder<UserResponse>.Success(data));
    }
}

public record UserResponse(string Id, string Name, string Email);
