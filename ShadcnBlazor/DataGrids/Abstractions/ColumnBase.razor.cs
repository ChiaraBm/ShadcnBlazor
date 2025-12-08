using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace ShadcnBlazor.DataGrids.Abstractions;

public abstract partial class ColumnBase<TGridItem>
{
    [CascadingParameter] public DataGrid<TGridItem> Grid { get; set; }
    
    [Parameter] public int Order { get; set; }
    [Parameter] public string Identifier { get; set; }

    [Parameter] public bool DefaultVisible { get; set; } = true;
    public bool IsVisible { get; set; }
    
    [Parameter] public bool IsFilterable { get; set; }
    [Parameter] public string? DefaultFilter { get; set; }
    public string? Filter { get; set; }

    protected override void OnInitialized()
    {
        IsVisible = DefaultVisible;
        Filter = DefaultFilter;
        
        Grid.RegisterColumn(this);
    }
    
    public abstract void RenderCell(RenderTreeBuilder __builder, TGridItem item);

    public virtual Task ResetAsync() => Task.CompletedTask;
}