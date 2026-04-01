namespace Sophia.Api.Controllers.MedicationHistory;

using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sophia.Api.Responder;
using Sophia.Api.Responder.MedicationHistory;
using Sophia.Api.Services;

public class UpdateMedicationHistoryAction(MedicationHistoryService medicationHistoryService) : ControllerBase
{
    [HttpPut("/api/medication-histories/{id}")]
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    [ProducesResponseType<GetMedicationHistoryDetailResponder>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Invoke(int id, [FromBody] Requests.MedicationHistory.UpdateMedicationHistoryRequest request)
    {
        var result = await medicationHistoryService.UpdateAsync(id, request.Amount, request.Note, request.MedicationDate);
        return Ok(GetMedicationHistoryDetailResponder.Create(result));
    }
}
