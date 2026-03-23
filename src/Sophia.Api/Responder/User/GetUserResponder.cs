namespace Sophia.Api.Responder.User;

using Sophia.Api.DataTransferObject.User;

public record GetUserResponder : BaseResponder<UserDto>
{
    private GetUserResponder(UserDto data)
        : base(true, "", null, data) { }

    public static GetUserResponder Create(UserDto data) => new(data);
}
