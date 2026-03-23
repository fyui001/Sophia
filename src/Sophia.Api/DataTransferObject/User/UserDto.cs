namespace Sophia.Api.DataTransferObject.User;

public record UserDto(
    long? Id,
    string? Name,
    string? IconUrl,
    bool IsRegistered,
    string? DiscordUserId
);
