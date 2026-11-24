---
title: Combobox
description: Searchable dropdown that lazily loads items from an async source, with debounced search and a toggleable bound value.
tags: [combobox, select, search, lazy-loading, dropdown, form]
---

# Combobox

The Comboboxes folder provides `Combobox<TItem>`, a searchable dropdown built on `Popover` and the `Command` component. It wraps an outline `Button` (rendered with `role="combobox"`) whose content is either a `FieldPlaceholder` or the `ValueSelector` output of the current `Value`, plus a `ChevronsUpDownIcon`. When opened, a `PopoverContent` shows a `ComboboxInput` search field and a `LazyLoader` that calls an async `Source` delegate with the debounced search term, then lists the results in a `CommandList` with a `CheckIcon` on the selected item. Use `Combobox` when the option list is too large or too dynamic for a plain `Select` and needs asynchronous, server-side or API-backed searching with typeahead.

Clicking an item sets the bound value and closes the popover; clicking the already-selected item again clears it (`Value` becomes `default(TItem)`). When the popover closes, the internal search term and loaded items are reset, so the next open reloads through `Source`. `ComboboxInput` is also exposed as a standalone component: a `SearchIcon` plus an `<input data-slot="command-input" cmdk-input>` styled for the `Command` menu.

## Usage

```razor
@using ShadcnBlazor.Extras.Comboboxes

<Combobox TItem="string"
          Source="GetFruitsAsync"
          Value="Selected"
          ValueChanged="OnChanged" />

@code
{
    private string? Selected;

    private async Task<string[]> GetFruitsAsync(string? searchTerm)
    {
        // Return items for the search term (null when the popover first opens).
        return ["Apple", "Banana", "Cherry", "Date", "Elderberry"];
    }

    private void OnChanged(string? value)
    {
        // The bound Value is updated by the component before ValueChanged fires.
    }
}
```

## Parameters

### Combobox

| Name | Type | Default | Description |
| --- | --- | --- | --- |
| `Value` | `TItem?` | `null` | The currently selected item. The trigger button shows `ValueSelector(Value)` when set, or `FieldPlaceholder` otherwise. |
| `ValueChanged` | `EventCallback<TItem?>` | — | Invoked after an item is clicked. The same item clicked again clears the selection and invokes this with `default(TItem)`. |
| `DefaultValue` | `TItem?` | `null` | Initial value applied to `Value` in `OnInitialized`. |
| `Alignment` | `PositionAlignment` | `PositionAlignment.Center` | Horizontal alignment of the `PopoverContent`. Enum values: `Start`, `Center`, `End`. |
| `Side` | `PositionSide` | `PositionSide.Bottom` | Which side of the trigger the `PopoverContent` opens on. Enum values: `Right`, `Left`, `Bottom`, `Top`. |
| `Source` | `Combobox<TItem>.ItemLoader` | — *(required)* | The async item source: `public delegate Task<TItem[]> ItemLoader(string? searchTerm)`. The search term is `null` on the initial load and debounced by `DebounceMs` while typing. |
| `ValueSelector` | `Func<TItem, string>` | `item => item.ToString()` | Maps an item to the text shown in the trigger button and in each `CommandItem`. |
| `DebounceMs` | `int` | `250` | Debounce delay in milliseconds applied to search term changes before `Source` is re-invoked. |
| `SearchPlaceholder` | `string` | `"Search items..."` | Placeholder of the `ComboboxInput` inside the popover. |
| `FieldPlaceholder` | `string` | `"Select item..."` | Placeholder shown in the trigger button while no item is selected. |
| `ClassName` | `string` | `null` | *(inherited from `ShadcnBase`)* Merged via `Cn(ClassName)` into the classes of the trigger `<Button>`. |
| `Props` | `Dictionary<string, object>?` | `null` | *(inherited from `ShadcnBase`)* Extra attributes applied to the trigger `<Button>` alongside its built-in attributes. |

### ComboboxInput

`ComboboxInput` declares no parameters of its own; it inherits from `ShadcnBase` and is normally used inside `Combobox`, where `Combobox` passes `placeholder="@SearchPlaceholder"`, `ClassName="h-9"` and `@oninput="OnSearchTermChanged"`.

| Name | Type | Default | Description |
| --- | --- | --- | --- |
| `ClassName` | `string` | `null` | *(inherited from `ShadcnBase`)* Merged via `Cn` with the input's base styling (`placeholder:text-muted-foreground flex h-10 w-full rounded-md bg-transparent py-3 text-sm outline-hidden disabled:cursor-not-allowed disabled:opacity-50`). |
| `Props` | `Dictionary<string, object>?` | `null` | *(inherited from `ShadcnBase`)* Spread onto the `<input type="text" data-slot="command-input" cmdk-input>` element. |

## Examples

### Basic string list with a custom ValueSelector

```razor
@using ShadcnBlazor.Extras.Comboboxes

<Combobox TItem="string"
          Source="LoadColorsAsync"
          Value="Color"
          ValueChanged="OnColorChanged" />

@code
{
    private string? Color;

    private async Task<string[]> LoadColorsAsync(string? searchTerm)
    {
        var all = ["Red", "Green", "Blue", "Yellow", "Black"];
        if (string.IsNullOrEmpty(searchTerm))
            return all;

        return all.Where(c => c.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)).ToArray();
    }

    private void OnColorChanged(string? value)
        => Console.WriteLine($"Selected: {value}");
}
```

### Typed items with DefaultValue, ValueSelector and placement

```razor
@using ShadcnBlazor.Extras.Comboboxes

<Combobox TItem="Employee"
          Source="LoadEmployeesAsync"
          Value="Employee"
          ValueChanged="OnEmployeeChanged"
          DefaultValue="DefaultEmployee"
          ValueSelector="e => $'{e.Name} ({e.Department})'"
          Alignment="PositionAlignment.Start"
          Side="PositionSide.Top" />

@code
{
    private class Employee
    {
        public required string Name { get; init; }
        public required string Department { get; init; }
    }

    private Employee DefaultEmployee = new() { Name = "Ada Lovelace", Department = "Engineering" };
    private Employee? Employee;

    private async Task<Employee[]> LoadEmployeesAsync(string? searchTerm)
    {
        await Task.Delay(300); // simulate an API call
        var all = new[]
        {
            new Employee { Name = "Ada Lovelace", Department = "Engineering" },
            new Employee { Name = "Alan Turing", Department = "Research" },
            new Employee { Name = "Grace Hopper", Department = "Compiler" },
            new Employee { Name = "Katherine Johnson", Department = "Computations" }
        };

        return searchTerm is null
            ? all
            : all.Where(e => e.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)).ToArray();
    }

    private void OnEmployeeChanged(Employee? value)
    {
        // value is Employee? (default(Employee) when the current item is toggled off)
    }
}
```

### Custom placeholders and debouncing

```razor
@using ShadcnBlazor.Extras.Comboboxes

<Combobox TItem="string"
          Source="LoadCountriesAsync"
          Value="Country"
          ValueChanged="OnCountryChanged"
          SearchPlaceholder="Type a country name..."
          FieldPlaceholder="Pick a country..."
          DebounceMs="500" />

@code
{
    private string? Country;

    private async Task<string[]> LoadCountriesAsync(string? searchTerm)
    {
        await Task.Delay(100);
        var all = ["Germany", "France", "Italy", "Spain", "Switzerland"];
        return searchTerm is null
            ? all
            : all.Where(c => c.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)).ToArray();
    }

    private void OnCountryChanged(string? value)
    {
        // DebounceMs delays reloading by 500 ms after each keystroke.
    }
}
```

### Standalone ComboboxInput

`ComboboxInput` can be rendered on its own as a styled search field; the `cmdk-input` attribute makes it work inside any `Command` menu, and its value can be read from the `ChangeEventArgs` of `@oninput`.

```razor
@using ShadcnBlazor.Extras.Comboboxes

<ComboboxInput placeholder="Search..." @oninput="OnInput" />

@code
{
    private string? Query;

    private void OnInput(ChangeEventArgs args)
    {
        Query = args?.Value?.ToString();
    }
}
```
