namespace Sophia.Domain.Lily;

public interface ILilyClient
{
    // Drug
    Task<LilyResponder<DrugListData>> GetDrugsAsync(int? page = null, int? perPage = null);
    Task<LilyResponder<DrugData>> GetDrugAsync(int id);
    Task<LilyResponder<object>> CreateDrugAsync(CreateDrugRequest request);
    Task<LilyResponder<object>> DeleteDrugAsync(int id);

    // MedicationHistory
    Task<LilyResponder<MedicationHistoryListData>> GetMedicationHistoriesAsync(int userId, int? page = null, int? perPage = null);
    Task<LilyResponder<MedicationHistoryData>> GetMedicationHistoryAsync(int id);
    Task<LilyResponder<MedicationHistoryData>> CreateMedicationHistoryAsync(CreateMedicationHistoryRequest request);
}
