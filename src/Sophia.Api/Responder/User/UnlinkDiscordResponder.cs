namespace Sophia.Api.Responder.User;

using Sophia.Api.DataTransferObject.User;

public record UnlinkDiscordResponder : BaseResponder<UserDto>
{
    private UnlinkDiscordResponder(UserDto data)
        : base(true, "", null, data) { }

    public static UnlinkDiscordResponder Create(UserDto data) => new(data);
}
