namespace Sophia.Domain.Lily;

public record DrugDetail(
    int Id,
    string Name,
    string Url
);

public record DrugListData(
    LilyPaginator<DrugDetail> Drugs
);

public record DrugData(
    DrugDetail Drug
);

public record CreateDrugRequest(
    string DrugName,
    string Url
);
