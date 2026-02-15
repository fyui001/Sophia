namespace Sophia.Api.Controllers.Auth0;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Mvc;

public class Auth0LoginAction(IConfiguration configuration) : ControllerBase
{
    [HttpGet("/auth0/login")]
    [ProducesResponseType(StatusCodes.Status302Found)]
    public IActionResult Invoke([FromQuery] string? return_to)
    {
        var graceOrigin = configuration["Grace:Origin"]!;

        var redirectPath = return_to is not null && return_to.StartsWith('/')
            ? return_to
            : "/";

        var properties = new AuthenticationProperties
        {
            RedirectUri = $"{graceOrigin}{redirectPath}"
        };

        return Challenge(properties, OpenIdConnectDefaults.AuthenticationScheme);
    }
}
