namespace Sophia.Infrastructure.Lily;

using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using Sophia.Domain.Lily;

public class LilyClient(HttpClient httpClient) : ILilyClient
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
    };

    // Drug

    public async Task<LilyResponder<DrugListData>> GetDrugsAsync(int? page = null, int? perPage = null)
    {
        var query = BuildQuery(("page", page), ("per_page", perPage));
        return (await httpClient.GetFromJsonAsync<LilyResponder<DrugListData>>($"/api/drugs{query}", JsonOptions))!;
    }

    public async Task<LilyResponder<DrugData>> GetDrugAsync(int id)
    {
        return (await httpClient.GetFromJsonAsync<LilyResponder<DrugData>>($"/api/drugs/{id}", JsonOptions))!;
    }

    public async Task<LilyResponder<object>> CreateDrugAsync(CreateDrugRequest request)
    {
        var body = new { drug_name = request.DrugName, url = request.Url };
        var response = await httpClient.PostAsJsonAsync("/api/drugs", body, JsonOptions);
        response.EnsureSuccessStatusCode();
        return (await response.Content.ReadFromJsonAsync<LilyResponder<object>>(JsonOptions))!;
    }

    public async Task<LilyResponder<object>> DeleteDrugAsync(int id)
    {
        var response = await httpClient.DeleteAsync($"/api/drugs/{id}");
        response.EnsureSuccessStatusCode();
        return (await response.Content.ReadFromJsonAsync<LilyResponder<object>>(JsonOptions))!;
    }

    // MedicationHistory

    public async Task<LilyResponder<MedicationHistoryListData>> GetMedicationHistoriesAsync(
        int userId, int? page = null, int? perPage = null)
    {
        var query = BuildQuery(("user_id", userId), ("page", page), ("per_page", perPage));
        return (await httpClient.GetFromJsonAsync<LilyResponder<MedicationHistoryListData>>(
            $"/api/medication_histories{query}", JsonOptions))!;
    }

    public async Task<LilyResponder<MedicationHistoryData>> GetMedicationHistoryAsync(int id)
    {
        return (await httpClient.GetFromJsonAsync<LilyResponder<MedicationHistoryData>>(
            $"/api/medication_histories/{id}", JsonOptions))!;
    }

    public async Task<LilyResponder<MedicationHistoryData>> CreateMedicationHistoryAsync(
        CreateMedicationHistoryRequest request)
    {
        var body = new
        {
            drug_id = request.DrugId,
            user_id = request.UserId,
            amount = request.Amount,
            medication_date = request.MedicationDate
        };
        var response = await httpClient.PostAsJsonAsync("/api/medication_histories", body, JsonOptions);
        response.EnsureSuccessStatusCode();
        return (await response.Content.ReadFromJsonAsync<LilyResponder<MedicationHistoryData>>(JsonOptions))!;
    }

    private static string BuildQuery(params (string key, int? value)[] parameters)
    {
        var pairs = parameters
            .Where(p => p.value.HasValue)
            .Select(p => $"{p.key}={p.value}");

        var query = string.Join("&", pairs);
        return query.Length > 0 ? $"?{query}" : "";
    }
}
