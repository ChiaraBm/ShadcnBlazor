using Microsoft.AspNetCore.Components;
using ShadcnBlazor.DataGrids.Abstractions;

namespace ShadcnBlazor.DataGrids;

[CascadingTypeParameter(nameof(TGridItem))]
public partial class DataGrid<TGridItem>
{
    private readonly List<ColumnBase<TGridItem>> RegisteredColumns = new();
    private Dictionary<ColumnBase<TGridItem>, bool> VisibleColumns = new();
    private Dictionary<ColumnBase<TGridItem>, string> FilteredColumns = new();

    private RenderFragment HeaderFragment;
    private RenderFragment<TGridItem> CellFragment;
    
    private bool IsInitialized = false;

    internal void RegisterColumn(ColumnBase<TGridItem> column)
        => RegisteredColumns.Add(column);

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if(!firstRender)
            return;
        
        VisibleColumns = RegisteredColumns
            .OrderBy(x => x.Order)
            .ToDictionary(x => x, x => x.DefaultVisible);

        FilteredColumns = RegisteredColumns
            .OrderBy(x => x.Order)
            .ToDictionary(x => x, x => string.Empty);

        HeaderFragment = builder =>
        {
            foreach (var column in VisibleColumns.Where(x => x.Value))
                column.Key.RenderHead(builder);
        };

        CellFragment = value =>
        {
            return builder =>
            {
                foreach (var column in VisibleColumns.Where(x => x.Value))
                    column.Key.RenderCell(builder, value);
            };
        };

        IsInitialized = true;
        await InvokeAsync(StateHasChanged);

        await RefreshAsync();
    }

    private async Task SetColumnVisibleAsync(ColumnBase<TGridItem> column, bool toggle)
    {
        VisibleColumns[column] = toggle;
        await InvokeAsync(StateHasChanged);
    }

    private async Task SetColumnFilterAsync(ColumnBase<TGridItem> column, string filter)
    {
        FilteredColumns[column] = filter;
        await InvokeAsync(StateHasChanged);
    }
}