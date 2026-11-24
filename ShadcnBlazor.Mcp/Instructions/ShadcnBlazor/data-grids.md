---
title: Data Grid
description: A data table that handles server-driven pagination, search, and filtering via a user-supplied Loader function.
tags: [data-grid, table, pagination, filtering, search, columns]
---

# Data Grid

The `<DataGrid>` is a generic, server-driven data table used to display large, paged collections of records. Instead of passing the whole dataset in, you supply a `Loader` function that receives a `DataGridRequest` (start index, page length, filters, and search term) and returns a `DataGridResponse` (a page of items plus the total count); the grid then renders the rows and manages all paging for you. On top of plain paging it provides a toolbar with an optional search box, per-column filtering, and a column-visibility toggle, plus built-in select-all checkboxes and slots for templated columns and rows. Reach for it whenever you need a professional, paginated table with filtering and search, and you are willing to implement the data-fetching yourself.

## Usage

```razor
@using ShadcnBlazor.DataGrids
@using ShadcnBlazor.Tabels

<DataGrid TGridItem="Payment" Loader="LoadAsync" PageSize="10">
    <ChildContent>
        <PropertyColumn Field="payment => payment.Id"/>
        <PropertyColumn Field="payment => payment.Email"/>
        <PropertyColumn Field="payment => payment.Amount"/>
    </ChildContent>
</DataGrid>

@code
{
    private readonly List<Payment> Payments = new();

    protected override void OnInitialized()
    {
        for (var i = 1; i <= 40; i++)
            Payments.Add(new Payment(i, $"user{i}@example.com", 99.99));
    }

    record Payment(int Id, string Email, double Amount);

    private Task<DataGridResponse<Payment>> LoadAsync(DataGridRequest<Payment> request)
    {
        var data = Payments
            .Skip(request.StartIndex)
            .Take(request.Length)
            .ToArray();

        return Task.FromResult(new DataGridResponse<Payment>(data, Payments.Count));
    }
}
```

## Parameters

### DataGrid\<TGridItem\>

| Name | Type | Default | Description |
|---|---|---|---|
| `ChildContent` | `RenderFragment` | `null` | The inner content of the grid. Hosts the column components (and optional row components) that define the table structure; these register themselves with the grid via cascading. |
| `Loader` | `Func<DataGridRequest<TGridItem>, Task<DataGridResponse<TGridItem>>>` | `null` | The async data-access function the grid invokes on init and on every page change, search, or filter. It receives a request (`StartIndex`, `Length`, `Filters`, `SearchTerm`) and must return a response (`Data` array plus `TotalLength`). |
| `PageSize` | `int` | `25` | The number of rows requested per page. |
| `EnableSearch` | `bool` | `false` | When `true`, adds a search box to the toolbar. |
| `EnableLiveSearch` | `bool` | `true` | Only meaningful when `EnableSearch` is `true`. When `true`, the search term is applied with a 500 ms debounce on every input; when `false`, it is applied on submit. |
| `ColumnVisibility` | `bool` | `true` | When `true`, shows a "Columns" dropdown in the toolbar that lets the user toggle the visibility of each titled column. |
| `ToolbarSlot` | `RenderFragment?` | `null` | Extra content rendered on the right side of the toolbar, next to the search, filter, and column-visibility controls. |
| `ClassName` | `string` | `null` | Inherited from `ShadcnBase`. Tailwind classes merged into the grid's outer wrapper element. |
| `Props` | `Dictionary<string, object>?` | `null` | Inherited from `ShadcnBase` (`CaptureUnmatchedValues = true`). Unmatched attributes are merged into the rendered `<Table>` element. |

### ColumnBase\<TGridItem\> (abstract base of `PropertyColumn`, `SelectColumn`, `TemplateColumn`)

| Name | Type | Default | Description |
|---|---|---|---|
| `Title` | `string?` | `null` | The header text of the column. Also the label used by the toolbar's filter and column-visibility UI. |
| `Identifier` | `string` | `null` | The key this column's filter value is stored under in the `Filters` dictionary of the `DataGridRequest`. |
| `Order` | `int` | `0` | Sort order of the column; columns are sorted by this value before rendering. |
| `DefaultVisible` | `bool` | `true` | The initial visibility of the column. The user can later toggle this from the "Columns" dropdown. |
| `IsFilterable` | `bool` | `false` | When `true`, the column contributes an input to the toolbar's filter popover. |
| `DefaultFilter` | `string?` | `null` | The initial filter value for a filterable column. |
| `HeadClassName` | `string?` | `null` | Tailwind classes merged into the column's header `<TableHead>` element. |

Each column also receives a cascading `Grid` parameter of type `DataGrid<TGridItem>` (`[CascadingParameter]`), which is how it registers itself with the grid — not a `[Parameter]` you set yourself.

### RowBase\<TGridItem\> (abstract base of `TemplateRow`)

| Name | Type | Default | Description |
|---|---|---|---|
| `Order` | `int` | `0` | Sort order of the custom row; rows are sorted by this value before rendering. |

Like columns, rows receive a cascading `Grid` parameter (`[CascadingParameter]`) used to register themselves with the grid.

### PropertyColumn\<TGridItem\> (inherits `ColumnBase<TGridItem>`)

| Name | Type | Default | Description |
|---|---|---|---|
| `Field` | `Expression<Func<TGridItem, object>>` | `null` | An expression selecting the member to display. The member name is used automatically as the column `Title`, and the value is rendered as its string form. |
| `CellClassName` | `string` | `null` | Tailwind classes merged into the column's data cell `<TableCell>` element. |
| *All `ColumnBase` parameters* | | | Inherits `Title`, `Identifier`, `Order`, `DefaultVisible`, `IsFilterable`, `DefaultFilter`, and `HeadClassName`. |

### SelectColumn\<TGridItem\> (inherits `ColumnBase<TGridItem>`)

| Name | Type | Default | Description |
|---|---|---|---|
| `Items` | `List<TGridItem>` | `new List<TGridItem>()` | The collection holding the currently selected items. The grid appends/removes items from it as the user toggles checkboxes; the header checkbox selects/deselects all. |
| `OnChanged` | `EventCallback` | `EventCallback.None` | Invoked whenever the selection changes, so the owning component can react. |
| *All `ColumnBase` parameters* | | | Inherits the `ColumnBase` parameters (usually a `Title` is not needed here). |

### TemplateColumn\<TGridItem\> (inherits `ColumnBase<TGridItem>`)

| Name | Type | Default | Description |
|---|---|---|---|
| `HeadTemplate` | `RenderFragment?` | `null` | Optional custom header. When `null`, falls back to the standard `Title`-based header. |
| `CellTemplate` | `RenderFragment<TGridItem>` | `required` | The content rendered for each data row; it receives the current `TGridItem` as its argument. |
| *All `ColumnBase` parameters* | | | Inherits the `ColumnBase` parameters (`Title`, `Order`, `DefaultVisible`, etc.). |

### TemplateRow\<TGridItem\> (inherits `RowBase<TGridItem>`)

| Name | Type | Default | Description |
|---|---|---|---|
| `ChildContent` | `RenderFragment` | `required` | The custom row content rendered above the data rows (e.g. a banner or summary row). |
| *All `RowBase` parameters* | | | Inherits `Order` and the cascading `Grid`. |

All components inherit from `ShadcnBase`, so the base `ClassName` and `Props` parameters are available. Column and row components additionally resolve their parent grid through the cascading `Grid` parameter rather than through `[Parameter]`.

## Examples

Filterable and searchable grid. `EnableSearch` adds the search box, and the `IsFilterable` column contributes an input to the filter popover; the `Identifier` is the key the filter value is passed back under in `request.Filters`:

```razor
@using ShadcnBlazor.DataGrids
@using ShadcnBlazor.Tabels

<DataGrid TGridItem="Payment" Loader="LoadAsync" PageSize="5" EnableSearch="true">
    <ChildContent>
        <PropertyColumn Field="payment => payment.Id"/>
        <PropertyColumn IsFilterable="true" Identifier="Email"
                        Field="payment => payment.Email"/>
        <PropertyColumn Field="payment => payment.Amount"/>
    </ChildContent>
</DataGrid>

@code
{
    private readonly List<Payment> Payments = new();

    protected override void OnInitialized()
    {
        for (var i = 1; i <= 40; i++)
            Payments.Add(new Payment(i, $"user{i}@example.com", 99.99));
    }

    record Payment(int Id, string Email, double Amount);

    private async Task<DataGridResponse<Payment>> LoadAsync(DataGridRequest<Payment> request)
    {
        IEnumerable<Payment> items = Payments;

        foreach (var filter in request.Filters)
        {
            items = filter.Key switch
            {
                nameof(Payment.Email) => items.Where(x => x.Email.Contains(filter.Value, StringComparison.OrdinalIgnoreCase)),
                _ => items
            };
        }

        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
            items = items.Where(x => x.Email.Contains(request.SearchTerm, StringComparison.OrdinalIgnoreCase));

        var data = items.Skip(request.StartIndex).Take(request.Length).ToArray();
        return new DataGridResponse<Payment>(data, Payments.Count);
    }
}
```

Select-all and per-row selection. A `SelectColumn` binds to a `List<TGridItem>` and raises `OnChanged`, so the owner can react to selection changes (and the selection is reset on every refresh):

```razor
@using ShadcnBlazor.DataGrids
@using ShadcnBlazor.Tabels

<div>
    Selected @Selected.Count
</div>

<DataGrid TGridItem="Payment" Loader="LoadAsync" PageSize="5">
    <ChildContent>
        <SelectColumn Items="Selected" OnChanged="() => InvokeAsync(StateHasChanged)"/>
        <PropertyColumn Field="payment => payment.Id"/>
        <PropertyColumn Field="payment => payment.Email"/>
        <PropertyColumn Field="payment => payment.Amount"/>
    </ChildContent>
</DataGrid>

@code
{
    private readonly List<Payment> Payments = new();
    private readonly List<Payment> Selected = new();

    protected override void OnInitialized()
    {
        for (var i = 1; i <= 40; i++)
            Payments.Add(new Payment(i, $"user{i}@example.com", 99.99));
    }

    record Payment(int Id, string Email, double Amount);

    private Task<DataGridResponse<Payment>> LoadAsync(DataGridRequest<Payment> request)
    {
        var data = Payments.Skip(request.StartIndex).Take(request.Length).ToArray();
        return Task.FromResult(new DataGridResponse<Payment>(data, Payments.Count));
    }
}
```

Action column with a template. A `TemplateColumn` renders arbitrary markup for each row via its required `CellTemplate`; `DefaultVisible="false"` hides it by default so it can be re-enabled from the "Columns" dropdown:

```razor
@using LucideBlazor
@using ShadcnBlazor.Buttons
@using ShadcnBlazor.DataGrids
@using ShadcnBlazor.Tabels

<DataGrid TGridItem="Payment" Loader="LoadAsync" PageSize="5">
    <ChildContent>
        <PropertyColumn Field="payment => payment.Id"/>
        <PropertyColumn Field="payment => payment.Email"/>
        <PropertyColumn Field="payment => payment.Amount"/>
        <TemplateColumn Title="Actions" DefaultVisible="false">
            <CellTemplate>
                <TableCell ClassName="flex justify-end items-center">
                    <ButtonGroup>
                        <Button Variant="ButtonVariant.Outline" Size="ButtonSize.IconSm">
                            <PlayIcon/>
                        </Button>
                        <Button Variant="ButtonVariant.Destructive" Size="ButtonSize.IconSm">
                            <SquareIcon/>
                        </Button>
                    </ButtonGroup>
                </TableCell>
            </CellTemplate>
        </TemplateColumn>
    </ChildContent>
</DataGrid>

@code
{
    private readonly List<Payment> Payments = new();

    protected override void OnInitialized()
    {
        for (var i = 1; i <= 40; i++)
            Payments.Add(new Payment(i, $"user{i}@example.com", 99.99));
    }

    record Payment(int Id, string Email, double Amount);

    private Task<DataGridResponse<Payment>> LoadAsync(DataGridRequest<Payment> request)
    {
        var data = Payments.Skip(request.StartIndex).Take(request.Length).ToArray();
        return Task.FromResult(new DataGridResponse<Payment>(data, Payments.Count));
    }
}
```

Custom toolbar button and a custom row. `ToolbarSlot` adds a button to the right of the toolbar, and a `TemplateRow` injects an extra custom row above the data rows:

```razor
@using LucideBlazor
@using ShadcnBlazor.Buttons
@using ShadcnBlazor.DataGrids
@using ShadcnBlazor.Tabels

<DataGrid TGridItem="Payment" Loader="LoadAsync" PageSize="5">
    <ChildContent>
        <PropertyColumn Field="payment => payment.Id"/>
        <PropertyColumn Field="payment => payment.Email"/>
        <PropertyColumn Field="payment => payment.Amount"/>
        <TemplateRow Order="-1">
            <TableRow>
                <TableCell colspan="3" class="bg-muted font-semibold">
                    Summary row rendered before the data
                </TableCell>
            </TableRow>
        </TemplateRow>
    </ChildContent>
    <ToolbarSlot>
        <Button Variant="ButtonVariant.Default">
            <PlusIcon/>
            <span class="hidden md:block">Create</span>
        </Button>
    </ToolbarSlot>
</DataGrid>

@code
{
    private readonly List<Payment> Payments = new();

    protected override void OnInitialized()
    {
        for (var i = 1; i <= 40; i++)
            Payments.Add(new Payment(i, $"user{i}@example.com", 99.99));
    }

    record Payment(int Id, string Email, double Amount);

    private Task<DataGridResponse<Payment>> LoadAsync(DataGridRequest<Payment> request)
    {
        var data = Payments.Skip(request.StartIndex).Take(request.Length).ToArray();
        return Task.FromResult(new DataGridResponse<Payment>(data, Payments.Count));
    }
}
```
