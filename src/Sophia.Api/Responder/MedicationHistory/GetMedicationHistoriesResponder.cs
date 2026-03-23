namespace Sophia.Api.Responder.MedicationHistory;

using Sophia.Infrastructure.Lily.Generated;

public record GetMedicationHistoriesResponder : BaseResponder<Get_medication_history_list_responder>
{
    private GetMedicationHistoriesResponder(Get_medication_history_list_responder data)
        : base(true, "", null, data) { }

    public static GetMedicationHistoriesResponder Create(Get_medication_history_list_responder data) => new(data);
}
