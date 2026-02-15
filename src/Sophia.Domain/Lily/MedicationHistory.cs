namespace Sophia.Domain.Lily;

public record MedicationHistoryDetail(
    int Id,
    int UserId,
    int Amount,
    int DrugId,
    string DrugName,
    string DrugUrl,
    string CreatedAt,
    string UpdatedAt
);

public record MedicationHistoryListData(
    LilyPaginator<MedicationHistoryDetail> MedicationHistories
);

public record MedicationHistoryData(
    MedicationHistoryDetail MedicationHistory
);

public record CreateMedicationHistoryRequest(
    int DrugId,
    int UserId,
    int Amount,
    string MedicationDate
);
