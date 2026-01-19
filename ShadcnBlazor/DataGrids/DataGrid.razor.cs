using Microsoft.AspNetCore.Components;
using ShadcnBlazor.DataGrids.Abstractions;
using ShadcnBlazor.Popovers;

namespace ShadcnBlazor.DataGrids;

[CascadingTypeParameter(nameof(TGridItem))]
public partial class DataGrid<TGridItem>
{
    [Parameter] public bool ColumnVisibility { get; set; } = true;
    [Parameter] public RenderFragment? ToolbarSlot { get; set; }
    
    private readonly List<ColumnBase<TGridItem>> Columns = new();
    private readonly List<RowBase<TGridItem>> Rows = new();

    private RenderFragment RowFragment;
    private RenderFragment HeaderFragment;
    private RenderFragment<TGridItem> CellFragment;
    
    private bool IsInitialized = false;

    private Popover FilterPopover;

    internal void RegisterColumn(ColumnBase<TGridItem> column)
        => Columns.Add(column);
    
    internal void RegisterRow(RowBase<TGridItem> row)
        => Rows.Add(row);

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if(!firstRender)
            return;
        
        Columns.Sort((x, y) => x.Order - y.Order);
        Rows.Sort((x, y) => x.Order - y.Order);
        
        HeaderFragment = builder =>
        {
            foreach (var column in Columns.Where(x => x.IsVisible))
                column.RenderHead(builder);
        };

        CellFragment = value =>
        {
            return builder =>
            {
                foreach (var column in Columns.Where(x => x.IsVisible))
                    column.RenderCell(builder, value);
            };
        };

        RowFragment = builder =>
        {
            foreach (var row in Rows)
                row.RenderRow(builder);
        };

        IsInitialized = true;
        await InvokeAsync(StateHasChanged);

        await RefreshAsync();
    }

    private async Task SetColumnVisibleAsync(ColumnBase<TGridItem> column, bool toggle)
    {
        column.IsVisible = toggle;
        await InvokeAsync(StateHasChanged);
    }

    private async Task ApplyFilterAsync()
    {
        await FilterPopover.CloseAsync();
        
        CurrentPage = 0;
        await RefreshAsync();
    }

    // Call for columns and rows to rerender the whole table
    public async Task RenderAsync() => await InvokeAsync(StateHasChanged);
}