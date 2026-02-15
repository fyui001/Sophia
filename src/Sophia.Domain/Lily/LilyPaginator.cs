namespace Sophia.Domain.Lily;

public record LilyPaginator<T>(
    int CurrentPage,
    int LastPage,
    int PerPage,
    int Total,
    List<T> Data
);
