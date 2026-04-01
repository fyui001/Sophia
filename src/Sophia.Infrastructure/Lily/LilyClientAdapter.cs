namespace Sophia.Infrastructure.Lily;

using Sophia.Domain.Lily;
using Sophia.Infrastructure.Lily.Generated;
using DrugDetailDomain = Sophia.Domain.Lily.DrugDetail;
using MedicationHistoryDetailDomain = Sophia.Domain.Lily.MedicationHistoryDetail;

public sealed class LilyClientAdapter(ILilyGeneratedClient client) : ILilyClient
{
    public async Task<DrugListResult> GetDrugsAsync(int? page = null, int? perPage = null)
    {
        var result = await client.ApiDrugsGetAsync(page, perPage);
        var drugs = result.Data?.Drugs;
        return new DrugListResult(
            drugs?.CurrentPage ?? 0,
            drugs?.LastPage ?? 0,
            drugs?.PerPage ?? 0,
            drugs?.Total ?? 0,
            drugs?.Data?.Select(d => new DrugDetailDomain(d.Id, d.Name, d.Url, d.Note)).ToList() ?? []
        );
    }

    public async Task<DrugDetailDomain> GetDrugAsync(int id)
    {
        var result = await client.ApiDrugsGetAsync(id);
        var drug = result.Data?.Drug;
        return new DrugDetailDomain(drug?.Id ?? 0, drug?.Name ?? "", drug?.Url ?? "", drug?.Note);
    }

    public async Task CreateDrugAsync(string drugName, string url)
    {
        await client.ApiDrugsPostAsync(new Create_drug_request
        {
            Drug_name = drugName,
            Url = url,
        });
    }

    public async Task DeleteDrugAsync(int id)
    {
        await client.ApiDrugsDeleteAsync(id);
    }

    public async Task UpdateDrugAsync(int id, string drugName, string url, string? note)
    {
        await client.ApiDrugsPutAsync(id, new Update_drug_request
        {
            Drug_name = drugName,
            Url = url,
            Note = note,
        });
    }

    public async Task<MedicationHistoryListResult> GetMedicationHistoriesAsync(
        long userId, int? page = null, int? perPage = null)
    {
        var result = await client.ApiMedicationHistoriesGetAsync(userId, page, perPage);
        var histories = result.Data?.MedicationHistories;
        return new MedicationHistoryListResult(
            histories?.CurrentPage ?? 0,
            histories?.LastPage ?? 0,
            histories?.PerPage ?? 0,
            histories?.Total ?? 0,
            histories?.Data?.Select(MapMedicationHistory).ToList() ?? []
        );
    }

    public async Task<MedicationHistoryDetailDomain> GetMedicationHistoryAsync(int id)
    {
        var result = await client.ApiMedicationHistoriesGetAsync(id);
        return MapMedicationHistory(result.Data?.MedicationHistory!);
    }

    public async Task<MedicationHistoryDetailDomain> CreateMedicationHistoryAsync(
        int drugId, long userId, decimal amount, string medicationDate)
    {
        var result = await client.ApiMedicationHistoriesPostAsync(
            new Create_medication_history_request
            {
                Drug_id = drugId,
                User_id = userId,
                Amount = (double)amount,
                Medication_date = medicationDate,
            });
        var history = result.Data?.Drug
            ?? throw new InvalidOperationException("Lily returned null for created medication history");
        return MapMedicationHistory(history);
    }

    public async Task<MedicationHistoryDetailDomain> UpdateMedicationHistoryAsync(int id, decimal amount, string? note, string? medicationDate = null)
    {
        var result = await client.ApiMedicationHistoriesPutAsync(id, new Update_medication_history_request
        {
            Amount = (double)amount,
            Note = note,
            Medication_date = medicationDate is not null ? DateTimeOffset.Parse(medicationDate) : null,
        });
        return MapMedicationHistory(result.Data!);
    }

    public async Task DeleteMedicationHistoryAsync(int id)
    {
        await client.ApiMedicationHistoriesDeleteAsync(id);
    }

    private static MedicationHistoryDetailDomain MapMedicationHistory(Medication_history_detail h) =>
        new(h.Id, h.UserId ?? 0, (decimal)h.Amount, h.DrugId ?? 0,
            h.DrugName, h.DrugUrl, h.Note, h.CreatedAt, h.UpdatedAt);
}
