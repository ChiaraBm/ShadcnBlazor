namespace ShadcnBlazor.DataGrids;

public class DataGridRequest<TGridItem>
{
    public int StartIndex { get; set; }
    public int Length { get; set; }
}

public class DataGridResponse<TGridItem>
{
    public TGridItem[] Data { get; set; }
    public int TotalLength { get; set; }
}