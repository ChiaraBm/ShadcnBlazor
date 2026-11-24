---
title: Toggle Group
description: Toggle buttons grouped in a row with single- or multi-select, per-item size/variant overrides, and configurable spacing.
tags: [toggle, group, button, selection, variant]
---

# Toggle Group

A row of linked toggle buttons that act as one control for choosing one or more options at a time — think a text-format toolbar (bold/italic/underline), a layout-density switcher, or a set of quick filters the user can turn on and off together. `ToggleGroup` owns the shared look-and-feel (`Variant`, `Size`, `Spacing`) and the selection state (`Values`), while each `ToggleGroupItem` renders as a native `<button type="button">` that flips its `data-state` between `on` and `off`. With `Type="ToggleGroupType.Single"` the group behaves like a radio group (picking one item clears the others, tapping a pressed item turns it off); with `ToggleGroupType.Multiple` every item toggles independently. Setting `Spacing="0"` fuses the items into a segmented control with square inner edges.

## Usage

```razor
@using ShadcnBlazor.ToggleGroups
@using ShadcnBlazor.Toggles

<ToggleGroup Type="ToggleGroupType.Single">
    <ToggleGroupItem Value="bold">Bold</ToggleGroupItem>
    <ToggleGroupItem Value="italic">Italic</ToggleGroupItem>
    <ToggleGroupItem Value="underline">Underline</ToggleGroupItem>
</ToggleGroup>
```

## Parameters

`ToggleGroup`:

| Name | Type | Default | Description |
|---|---|---|---|
| `ChildContent` | `RenderFragment` | `null` | Content rendered inside a `CascadingValue` of type `ToggleGroup`. Typically one or more `ToggleGroupItem` elements. |
| `Size` | `ToggleSize` | `ToggleSize.Default` | Default size applied to items that do not override it. `ToggleSize` is `Default`, `Sm`, `Lg`. Drives `data-size` and the item height/padding. |
| `Variant` | `ToggleVariant` | `ToggleVariant.Default` | Default variant applied to items that do not override it. `ToggleVariant` is `Default` (transparent) and `Outline` (bordered). Drives `data-variant`. |
| `Type` | `ToggleGroupType` | `ToggleGroupType.Single` | Selection mode. `Single` selects at most one value; `Multiple` allows any combination. |
| `Spacing` | `int` | `0` | Gap between items in px, exposed as `--gap` and `data-spacing`. `0` fuses the items into a segmented control (square inner corners, merged borders). |
| `Values` | `List<string>` | `new()` | The values that are currently pressed. An item is `on` when its `Value` is contained in this list. The group edits this list in place. |
| `OnValuesChanged` | `EventCallback` | `null` | Non-generic callback that signals a change to `Values`; the handler reads the updated `Values` list, which is not passed as an argument. |

`ToggleGroupItem`:

| Name | Type | Default | Description |
|---|---|---|---|
| `ChildContent` | `RenderFragment` | `null` | The button label — text and/or an icon. Rendered inside the `<button>`. |
| `Disabled` | `bool` | `false` | Disables this item. A disabled item ignores clicks and is rendered with reduced opacity. |
| `Value` | `string` | `null` | The value this item represents. The item is pressed when the group's `Values` contains this; clicking calls `ToggleGroup.OnPressedAsync(Value)`. |
| `Variant` | `ToggleVariant?` | `null` | Optional per-item variant override. Falls back to the group's `Variant` when null. |
| `Size` | `ToggleSize?` | `null` | Optional per-item size override. Falls back to the group's `Size` when null. |

The item receives the group through a `[CascadingParameter]` named `ToggleGroup`, so it inherits from `ShadcnBase` and the base parameters are also available: `ClassName` (a `string`) is merged with the component's built-in classes, and `Props` (a `Dictionary<string, object>?`) captures any unmatched attributes such as `aria-label` and merges them into the rendered `<button>`.

## Examples

A multiple-selection outline group with icons, as in the packaged showcase. Each item overrides the pressed state with its own colored fill:

```razor
@using LucideBlazor
@using ShadcnBlazor.ToggleGroups
@using ShadcnBlazor.Toggles

<ToggleGroup Type="ToggleGroupType.Multiple" Variant="ToggleVariant.Outline" Spacing="2" Size="ToggleSize.Sm">
    <ToggleGroupItem
        Value="star"
        aria-label="Toggle star"
        ClassName="data-[state=on]:bg-transparent data-[state=on]:*:[svg]:fill-yellow-500 data-[state=on]:*:[svg]:stroke-yellow-500">
        <StarIcon/>
        Star
    </ToggleGroupItem>
    <ToggleGroupItem
        Value="heart"
        aria-label="Toggle heart"
        ClassName="data-[state=on]:bg-transparent data-[state=on]:*:[svg]:fill-red-500 data-[state=on]:*:[svg]:stroke-red-500">
        <HeartIcon/>
        Heart
    </ToggleGroupItem>
    <ToggleGroupItem
        Value="bookmark"
        aria-label="Toggle bookmark"
        ClassName="data-[state=on]:bg-transparent data-[state=on]:*:[svg]:fill-blue-500 data-[state=on]:*:[svg]:stroke-blue-500">
        <BookmarkIcon/>
        Bookmark
    </ToggleGroupItem>
</ToggleGroup>
```

A single-selection, segmented (`Spacing="0"`) toolbar. With `Type="ToggleGroupType.Single"`, selecting a tool deselects the others and tapping the active tool clears it:

```razor
@using LucideBlazor
@using ShadcnBlazor.ToggleGroups
@using ShadcnBlazor.Toggles

<ToggleGroup Type="ToggleGroupType.Single" Spacing="0">
    <ToggleGroupItem Value="bold" aria-label="Bold">
        <BoldIcon/>
    </ToggleGroupItem>
    <ToggleGroupItem Value="italic" aria-label="Italic">
        <ItalicIcon/>
    </ToggleGroupItem>
    <ToggleGroupItem Value="underline" aria-label="Underline">
        <UnderlineIcon/>
    </ToggleGroupItem>
</ToggleGroup>
```

A controlled multi-select where the page owns the selection. The group edits the shared `Values` list in place, so the same reference on the page always reflects the current selection:

```razor
@using ShadcnBlazor.ToggleGroups
@using ShadcnBlazor.Toggles

<ToggleGroup Type="ToggleGroupType.Multiple"
             Variant="ToggleVariant.Outline"
             Values="Selected"
             OnValuesChanged="HandleValuesChanged">
    <ToggleGroupItem Value="bold">Bold</ToggleGroupItem>
    <ToggleGroupItem Value="italic">Italic</ToggleGroupItem>
    <ToggleGroupItem Value="underline">Underline</ToggleGroupItem>
</ToggleGroup>
<p class="mt-3 text-sm text-muted-foreground">
    Selected: @(Selected.Count == 0 ? "none" : string.Join(", ", Selected))
</p>

@code
{
    private List<string> Selected = new();

    private void HandleValuesChanged() => StateHasChanged();
}
```
