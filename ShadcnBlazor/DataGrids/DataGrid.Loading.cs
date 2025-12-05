using Microsoft.AspNetCore.Components;

namespace ShadcnBlazor.DataGrids;

public partial class DataGrid<TGridItem>
{
    [Parameter]
    public Func<DataGridRequest<TGridItem>, Task<DataGridResponse<TGridItem>>> Loader { get; set; }
    
    public bool IsLoading { get; private set; }
    public TGridItem[] Items { get; private set; } = [];
    public int TotalItems { get; private set; }
    
    public async Task RefreshAsync(bool isSilent = false)
    {
        if (!isSilent)
        {
            IsLoading = true;
            await InvokeAsync(StateHasChanged);
        }

        var response = await Loader.Invoke(new DataGridRequest<TGridItem>()
        {
            StartIndex = 0,
            Length = 5
        });

        Items = response.Data;
        TotalItems = response.TotalLength;

        IsLoading = false;
        await InvokeAsync(StateHasChanged);
    }
}