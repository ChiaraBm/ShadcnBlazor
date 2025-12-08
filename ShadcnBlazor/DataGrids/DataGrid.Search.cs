using Microsoft.AspNetCore.Components;

namespace ShadcnBlazor.DataGrids;

public partial class DataGrid<TGridItem>
{
    [Parameter] public bool EnableSearch { get; set; }
    [Parameter] public bool EnableLiveSearch { get; set; } = true;

    private readonly Debouncer SearchDebouncer = new(500);
    
    private string? SearchTerm;
    
    private async Task OnSearchChangedAsync()
    {
        if (EnableLiveSearch)
            SearchDebouncer.Debounce(() => RefreshAsync());
        else
            await RefreshAsync();
    }
}