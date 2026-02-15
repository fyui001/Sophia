namespace Sophia.Domain.Lily;

public record LilyResponder<T>(
    bool Status,
    string Message,
    object? Errors,
    T? Data
);
