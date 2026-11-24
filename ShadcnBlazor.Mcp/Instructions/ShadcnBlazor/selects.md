---
title: Select
description: Select dropdown with trigger button, portal-rendered content, groups, labels, separators and disabled items.
tags: [select, dropdown, form, input, select-trigger, select-item]
---

# Select

A composable select component for choosing a single value from a list of options. Use it whenever a user must pick one item from a predefined set: forms, filters, language or format pickers, and any single-choice control that should save vertical space compared to a set of radio buttons. The root `<Select>` holds the selected value and supports two-way binding through `Value`/`ValueChanged`; `<SelectTrigger>` is the button that opens the dropdown, while `<SelectContent>` renders the items in a portal next to the trigger with a full-viewport backdrop that closes the dropdown on outside click. Because the content is rendered through a portal, the hosting layout must contain a `<PortalOutlet />`. Available building blocks are `<SelectItem>` (with optional `Disabled` state and an automatic check indicator on the selected item), `<SelectGroup>`, `<SelectLabel>`, and `<SelectSeparator>` for structuring the list.

## Usage

```razor
@using ShadcnBlazor.Selects

<Select>
    <SelectTrigger ClassName="w-[180px]">
        <SelectValue Placeholder="Select a fruit" />
    </SelectTrigger>
    <SelectContent ClassName="w-[180px]">
        <SelectGroup>
            <SelectLabel>Fruits</SelectLabel>
            <SelectItem Value="apple">Apple</SelectItem>
            <SelectItem Value="banana">Banana</SelectItem>
            <SelectItem Value="grapes">Grapes</SelectItem>
        </SelectGroup>
    </SelectContent>
</Select>
```

## Parameters

| Name | Type | Default | Description |
|---|---|---|---|
| `ChildContent` (Select) | `RenderFragment` | `null` | The root content, normally one `<SelectTrigger>` and one `<SelectContent>`. Cascades the select state so the trigger can open it and the items can update the value. |
| `Value` (Select) | `string` | `null` | The currently selected value. When it matches a `<SelectItem>`'s `Value`, the item shows a check indicator and its content is displayed in the trigger. |
| `ValueChanged` (Select) | `EventCallback<string>` | `default` | Invoked with the new value every time an item is clicked; the dropdown closes automatically afterwards. Supports two-way binding via `@bind-Value`. |
| `DefaultValue` (Select) | `string?` | `null` | Initial `Value` applied during initialization; pass an empty string to start with nothing selected (the placeholder shows instead). |
| `UseAutoWidth` (SelectContent) | `bool` | `true` | When `true`, the content width is set to the trigger's measured width in pixels. |
| `ChildContent` (SelectContent) | `RenderFragment` | `null` | The items and structural blocks of the dropdown. Rendered in the `PortalNames.Select` portal near the trigger, with a full-viewport backdrop that closes the dropdown on click. |
| `Align` (SelectContent) | `PositionAlignment` | `PositionAlignment.Start` | How the content is aligned to the trigger: `PositionAlignment.Start`, `PositionAlignment.Center` or `PositionAlignment.End`. |
| `ChildContent` (SelectGroup) | `RenderFragment` | `null` | The content of the group container that wraps related items (typically a `<SelectLabel>` followed by `<SelectItem>`s). |
| `ChildContent` (SelectItem) | `RenderFragment` | `null` | The item's content. Registered in the select's item map and shown in the trigger when the item's `Value` is selected; a check icon is rendered automatically while selected. |
| `Value` (SelectItem) | `string` | `null` | The value this item represents. Clicking the item sets this as the select's `Value`. |
| `Disabled` (SelectItem) | `bool` | `false` | Makes the item unclickable and dims it. |
| `ChildContent` (SelectLabel) | `RenderFragment` | `null` | The label text, rendered as a non-interactive section heading above the group's items. |
| `ChildContent` (SelectTrigger) | `RenderFragment` | `null` | The trigger button's content. Use a `<SelectValue>` to display the placeholder or the selected item's content. |
| `Size` (SelectTrigger) | `SelectSize` | `SelectSize.Default` | The trigger height: `SelectSize.Default` for the standard height or `SelectSize.Sm` for the smaller variant. |
| `Placeholder` (SelectValue) | `string` | `"Select item..."` | The text shown while nothing is selected. An empty-value item with this text is also registered so the placeholder can be picked inside the dropdown. |

The `Select` root is a plain component; every other component in this folder inherits from `ShadcnBase`, so the base parameters are also available on each one. `ClassName` (a `string`) is merged into the element's Tailwind classes, and `Props` (a `Dictionary<string, object>?` with `CaptureUnmatchedValues = true`) collects any unmatched attributes and spreads them onto the rendered element.

## Examples

Basic select with a labeled group of items, adapted from the repo's `SelectSc1` showcase block:

```razor
@using ShadcnBlazor.Selects

<Select>
    <SelectTrigger ClassName="w-[180px]">
        <SelectValue Placeholder="Select a fruit" />
    </SelectTrigger>
    <SelectContent ClassName="w-[180px]">
        <SelectGroup>
            <SelectLabel>Fruits</SelectLabel>
            <SelectItem Value="apple">Apple</SelectItem>
            <SelectItem Value="banana">Banana</SelectItem>
            <SelectItem Value="blueberry">Blueberry</SelectItem>
            <SelectItem Value="grapes">Grapes</SelectItem>
            <SelectItem Value="pineapple">Pineapple</SelectItem>
        </SelectGroup>
    </SelectContent>
</Select>
```

Language picker with two-way binding, adapted from the repo's `LanguageSelector` partial, where the selected value drives the app state and `@bind-Value:after` runs an action after the change:

```razor
@using ShadcnBlazor.Selects

<Select @bind-Value="_currentLanguage" @bind-Value:after="OnLanguageChangedAsync">
    <SelectTrigger ClassName="w-full [&>span]:w-full [&>span]:justify-center">
        <SelectValue Placeholder="Select language" />
    </SelectTrigger>
    <SelectContent ClassName="w-[230px] text-center">
        <SelectGroup>
            @foreach (var language in _languages)
            {
                <SelectItem ClassName="text-center" Value="@language.Key">@language.Value</SelectItem>
            }
        </SelectGroup>
    </SelectContent>
</Select>

@code
{
    private Dictionary<string, string> _languages = new()
    {
        { "en", "🇬🇧 English" },
        { "de", "🇩🇪 Deutsch" },
    };

    private string _currentLanguage;

    protected override void OnInitialized()
    {
        _currentLanguage = "en";
    }

    private Task OnLanguageChangedAsync()
    {
        Console.WriteLine($"language changed to {_currentLanguage}");
        return Task.CompletedTask;
    }
}
```

Checkout-style month select that starts with nothing selected using `DefaultValue=""`, adapted from the repo's `FieldSc1` showcase block:

```razor
@using ShadcnBlazor.Selects

<Select DefaultValue="">
    <SelectTrigger>
        <SelectValue Placeholder="MM"/>
    </SelectTrigger>
    <SelectContent>
        <SelectGroup>
            <SelectItem Value="01">01</SelectItem>
            <SelectItem Value="02">02</SelectItem>
            <SelectItem Value="03">03</SelectItem>
            <SelectItem Value="04">04</SelectItem>
            <SelectItem Value="05">05</SelectItem>
            <SelectItem Value="06">06</SelectItem>
            <SelectItem Value="07">07</SelectItem>
            <SelectItem Value="08">08</SelectItem>
            <SelectItem Value="09">09</SelectItem>
            <SelectItem Value="10">10</SelectItem>
            <SelectItem Value="11">11</SelectItem>
            <SelectItem Value="12">12</SelectItem>
        </SelectGroup>
    </SelectContent>
</Select>
```

Combined usage: a smaller `SelectSize.Sm` trigger, a disabled item, a `<SelectSeparator>` between option groups, and dynamically rendered items from a data source with `@key`, adapted from the repo's `ArchiveDialog` format picker:

```razor
@using ShadcnBlazor.Selects

<Select @bind-Value="SelectedFormat" @bind-Value:after="OnFormatChangedAsync">
    <SelectTrigger Size="SelectSize.Sm" ClassName="w-full">
        <SelectValue Placeholder="Select a format"/>
    </SelectTrigger>
    <SelectContent>
        <SelectGroup>
            <SelectLabel>Archives</SelectLabel>
            @foreach (var format in Formats)
            {
                <SelectItem @key="format" Value="@format">
                    @format
                </SelectItem>
            }
        </SelectGroup>
        <SelectSeparator />
        <SelectGroup>
            <SelectItem Value="zip" Disabled>.zip (read-only)</SelectItem>
        </SelectGroup>
    </SelectContent>
</Select>

@code
{
    private string[] Formats = { "tar", "tar.gz", "7z" };
    private string SelectedFormat;

    private Task OnFormatChangedAsync()
    {
        Console.WriteLine($"selected format: {SelectedFormat}");
        return Task.CompletedTask;
    }
}
```
