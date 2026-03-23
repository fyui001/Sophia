namespace Sophia.Api.Controllers.MedicationHistory;

using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sophia.Api.Responder;
using Sophia.Api.Responder.MedicationHistory;
using Sophia.Infrastructure.DbContext;
using Sophia.Infrastructure.Lily.Generated;
using Sophia.Infrastructure.Models;

public class GetMedicationHistoriesAction(
    SophiaContext dbContext,
    ILilyGeneratedClient lilyClient
) : ControllerBase
{
    [HttpGet("/api/medication-histories")]
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    [ProducesResponseType<GetMedicationHistoriesResponder>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Invoke(
        [FromQuery] int? page = null,
        [FromQuery] int? per_page = null)
    {
        var auth0Sub = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "";
        var user = await dbContext.Users
            .FirstOrDefaultAsync(u => u.Auth0Sub == auth0Sub && u.Status == UserStatus.Valid);

        if (user?.DiscordUserId is null)
        {
            return BadRequest(new BaseResponder<object>(false, "Discord account not linked", null, null));
        }

        if (!long.TryParse(user.DiscordUserId, out var discordId))
        {
            return BadRequest(new BaseResponder<object>(false, "Invalid Discord UserId", null, null));
        }

        var result = await lilyClient.ApiMedicationHistoriesGetAsync(discordId, page, per_page);

        return Ok(GetMedicationHistoriesResponder.Create(result));
    }
}
