namespace Sophia.Api.Controllers.Auth0;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

public class Auth0LogoutAction(IConfiguration configuration) : ControllerBase
{
    [HttpPost("/auth0/logout")]
    [ProducesResponseType(StatusCodes.Status302Found)]
    public async Task<IActionResult> Invoke()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        var auth0Domain = configuration["Auth0:Domain"]!;
        var clientId = configuration["Auth0:ClientId"]!;
        var graceOrigin = configuration["Grace:Origin"]!;

        var logoutUrl = $"https://{auth0Domain}/v2/logout?client_id={clientId}&returnTo={Uri.EscapeDataString(graceOrigin)}";

        return Redirect(logoutUrl);
    }
}
