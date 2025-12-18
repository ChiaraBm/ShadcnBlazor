namespace ShadcnBlazor.DataGrids;

public record DataGridRequest<TGridItem>(
    int StartIndex,
    int Length,
    Dictionary<string, string> Filters,
    string? SearchTerm
);

public record DataGridResponse<TGridItem>(TGridItem[] Data, int TotalLength);