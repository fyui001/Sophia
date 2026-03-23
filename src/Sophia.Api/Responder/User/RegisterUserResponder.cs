namespace Sophia.Api.Responder.User;

using Sophia.Api.DataTransferObject.User;

public record RegisterUserResponder : BaseResponder<UserDto>
{
    private RegisterUserResponder(UserDto data)
        : base(true, "", null, data) { }

    public static RegisterUserResponder Create(UserDto data) => new(data);
}
