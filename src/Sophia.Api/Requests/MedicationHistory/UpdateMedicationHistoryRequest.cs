namespace Sophia.Api.Requests.MedicationHistory;

using System.ComponentModel.DataAnnotations;

public record UpdateMedicationHistoryRequest
{
    [Required]
    public required decimal Amount { get; init; }

    public string? Note { get; init; }

    public string? MedicationDate { get; init; }
}
