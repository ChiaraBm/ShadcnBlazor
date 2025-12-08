namespace ShadcnBlazor.DataGrids;

public class DataGridRequest<TGridItem>
{
    public int StartIndex { get; set; }
    public int Length { get; set; }
    public Dictionary<string, string> Filters { get; set; }
    public string? SearchTerm { get; set; }
}

public class DataGridResponse<TGridItem>
{
    public TGridItem[] Data { get; set; }
    public int TotalLength { get; set; }
}