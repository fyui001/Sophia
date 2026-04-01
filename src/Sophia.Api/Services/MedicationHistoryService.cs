namespace Sophia.Api.Services;

using Microsoft.EntityFrameworkCore;
using Sophia.Domain.Lily;
using Sophia.Infrastructure.DbContext;
using Sophia.Infrastructure.Models;

public sealed class MedicationHistoryService(
    ILilyClient lilyClient,
    SophiaContext dbContext
)
{
    public async Task<long?> ResolveDiscordUserIdAsync(string auth0Sub)
    {
        var user = await dbContext.Users
            .FirstOrDefaultAsync(u => u.Auth0Sub == auth0Sub && u.Status == UserStatus.Valid);

        if (user?.DiscordUserId is null) return null;

        return long.TryParse(user.DiscordUserId, out var discordId) ? discordId : null;
    }

    public async Task<MedicationHistoryListResult> GetListAsync(
        long discordUserId, int? page = null, int? perPage = null)
    {
        return await lilyClient.GetMedicationHistoriesAsync(discordUserId, page, perPage);
    }

    public async Task<MedicationHistoryDetail> GetDetailAsync(int id)
    {
        return await lilyClient.GetMedicationHistoryAsync(id);
    }

    public async Task<MedicationHistoryDetail> CreateAsync(
        long discordUserId, int drugId, decimal amount, string medicationDate)
    {
        return await lilyClient.CreateMedicationHistoryAsync(drugId, discordUserId, amount, medicationDate);
    }

    public async Task<MedicationHistoryDetail> UpdateAsync(int id, decimal amount, string? note, string? medicationDate = null)
    {
        return await lilyClient.UpdateMedicationHistoryAsync(id, amount, note, medicationDate);
    }

    public async Task DeleteAsync(int id)
    {
        await lilyClient.DeleteMedicationHistoryAsync(id);
    }
}
