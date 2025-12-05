using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace ShadcnBlazor.DataGrids.Abstractions;

public abstract partial class ColumnBase<TGridItem>
{
    [CascadingParameter] public DataGrid<TGridItem> Grid { get; set; }
    
    [Parameter] public int Order { get; set; }

    protected override void OnInitialized()
    {
        Grid.RegisterColumn(this);
    }
    
    public abstract void RenderCell(RenderTreeBuilder __builder, TGridItem item);

    protected virtual Task ResetAsync() => Task.CompletedTask;
}