using Microsoft.AspNetCore.Components;

namespace ShadcnBlazor.DataGrids;

public partial class DataGrid<TGridItem>
{
    [Parameter] public Func<DataGridRequest<TGridItem>, Task<DataGridResponse<TGridItem>>> Loader { get; set; }

    [Parameter] public int PageSize { get; set; } = 25;
    
    public int CurrentPage { get; private set; }
    public int MaximumPage { get; private set; }
    
    public int StartIndex { get; private set; }
    public int TotalItems { get; private set; }

    public bool IsLoading { get; private set; }
    public TGridItem[] Items { get; private set; } = [];

    public async Task RefreshAsync(bool isSilent = false)
    {
        if (!isSilent)
        {
            IsLoading = true;
            await InvokeAsync(StateHasChanged);
        }

        var filters = Columns
            .Where(x => x.IsFilterable && !string.IsNullOrWhiteSpace(x.Filter))
            .ToDictionary(x => x.Identifier, x => x.Filter!);

        StartIndex = CurrentPage * PageSize;

        var response = await Loader.Invoke(new DataGridRequest<TGridItem>()
        {
            StartIndex = CurrentPage * PageSize,
            Length = PageSize,
            Filters = filters,
            SearchTerm = SearchTerm
        });

        Items = response.Data;
        TotalItems = response.TotalLength;

        MaximumPage = (int)Math.Ceiling((double)TotalItems / PageSize) - 1;

        foreach (var column in Columns)
            await column.ResetAsync();

        foreach (var row in Rows)
            await row.ResetAsync();

        IsLoading = false;
        await InvokeAsync(StateHasChanged);
    }

    public async Task NavigateAsync(int diff)
    {
        CurrentPage += diff;

        if (CurrentPage < 0)
            CurrentPage = 0;
        else if (CurrentPage > MaximumPage)
            CurrentPage = MaximumPage;

        await RefreshAsync();
    }

    public async Task NavigateToStartAsync()
    {
        CurrentPage = 0;
        await RefreshAsync();
    }

    public async Task NavigateToEndAsync()
    {
        CurrentPage = MaximumPage;
        await RefreshAsync();
    }
}