using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace ShadcnBlazor.DataGrids.Abstractions;

public abstract partial class RowBase<TGridItem>
{
    [CascadingParameter] public DataGrid<TGridItem> Grid { get; set; }
    
    public abstract void RenderRow(RenderTreeBuilder __builder);

    protected override void OnInitialized()
    {
        Grid.RegisterRow(this);
    }
    
    public virtual Task ResetAsync() => Task.CompletedTask;
}