namespace Sophia.Api.Responder;

public record BaseResponder<T>(
    bool Status,
    string Message,
    object? Errors,
    T? Data
)
{
    public static BaseResponder<T> Success(T data)
        => new(true, "", null, data);
}
