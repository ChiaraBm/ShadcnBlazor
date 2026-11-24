---
title: Toggle
description: A clickable toggle button for switching an option on or off, with two-way binding, default/outline variants, sm/lg sizing and a disabled state.
tags: [toggle, button, switch, control, two-way-binding]
---

# Toggle

A small, rectangular, pressable button used to switch a single option on or off, such as bold or italic text in a toolbar, or enabling a feature in a settings list. It is rendered as a native `<button>` element whose `data-state` flips between `on` and `off` on every click, and the button dims and stops responding while `disabled`. The component supports two-way binding via `Value`/`ValueChanged` or one-time initialization via `DefaultValue`, and it can be styled as `ToggleVariant.Default` or `ToggleVariant.Outline` in `ToggleSize.Sm`, `ToggleSize.Default` or `ToggleSize.Lg`. As with all ShadcnBlazor components it inherits from `ShadcnBase`, so `ClassName` and arbitrary attributes flow straight into the rendered button.

## Usage

```razor
@using ShadcnBlazor.Toggles

<Toggle @bind-Value="IsBold"/>
```

```csharp
@code
{
    private bool IsBold;
}
```

## Parameters

| Name | Type | Default | Description |
|---|---|---|---|
| `Value` | `bool` | `false` | The current on/off state of the toggle. When `true`, the button renders with `data-state="on"` and the pressed styling. |
| `ValueChanged` | `EventCallback<bool>` | `null` | Invoked with the new state every time the toggle is clicked. Pair it with `@bind-Value` or supply a `ValueChanged="handler"` callback. |
| `ChildContent` | `RenderFragment` | `null` | The content rendered inside the button, typically an icon or a short label. |
| `Variant` | `ToggleVariant` | `ToggleVariant.Default` | The visual style of the button. `ToggleVariant.Default` renders a flat, borderless button; `ToggleVariant.Outline` adds a visible border and input-style background. |
| `Size` | `ToggleSize` | `ToggleSize.Default` | The height and minimum width of the button. `ToggleSize.Default` renders `h-9` and `ToggleSize.Sm`/`ToggleSize.Lg` render `h-8`/`h-10` respectively. |
| `Disabled` | `bool` | `false` | When `true`, the button is rendered with the `disabled` attribute and ignores clicks. |
| `DefaultValue` | `bool` | `false` | A one-time seed value. `OnInitialized` copies it into `Value`; after that, state changes are driven by user interaction. |

The component inherits from `ShadcnBase`, so the base parameters are also available: `ClassName` (a `string`) is merged with the component's built-in button classes, and `Props` (a `Dictionary<string, object>?`) captures any unmatched attributes and merges them into the rendered `<button>`.

## Examples

Toggle button with an icon in a toolbar, using two-way binding and the `Outline` variant:

```razor
@using ShadcnBlazor.Toggles

<div class="flex items-center gap-1 rounded-lg border p-1">
    <Toggle
        @bind-Value="IsBold"
        Variant="ToggleVariant.Outline"
        Size="ToggleSize.Sm"
        class="bg-transparent shadow-none"
        title="Make text bold"
        aria-label="Make text bold">
        <LucideIcon IconName="lucide:bold" class="size-4"/>
    </Toggle>
    <Toggle
        @bind-Value="IsItalic"
        Variant="ToggleVariant.Outline"
        Size="ToggleSize.Sm"
        class="bg-transparent shadow-none"
        title="Make text italic"
        aria-label="Make text italic">
        <LucideIcon IconName="lucide:italic" class="size-4"/>
    </Toggle>
</div>

@code
{
    private bool IsBold;
    private bool IsItalic;
}
```

A list of formatting options where a `ValueChanged` handler tracks which feature is active, using the `Default` variant:

```razor
@using ShadcnBlazor.Toggles

<div class="flex items-center gap-1 rounded-lg border p-1">
    <Toggle
        Value='ActiveFeature == "bold"'
        ValueChanged='b => { ActiveFeature = b ? "bold" : null; }'
        Size="ToggleSize.Default"
        class="px-2"
        aria-label="Make text bold">
        <LucideIcon IconName="lucide:bold" class="size-4"/>
        Bold
    </Toggle>
    <Toggle
        Value='ActiveFeature == "italic"'
        ValueChanged='b => { ActiveFeature = b ? "italic" : null; }'
        Size="ToggleSize.Default"
        class="px-2"
        aria-label="Make text italic">
        <LucideIcon IconName="lucide:italic" class="size-4"/>
        Italic
    </Toggle>
</div>

@code
{
    private string? ActiveFeature;
}
```

Disabled toggle plus a large-sized toggle with a custom `ClassName`:

```razor
@using ShadcnBlazor.Toggles

<Toggle Disabled Size="ToggleSize.Lg">
    <LucideIcon IconName="lucide:heart" class="size-4"/>
    Favorite
</Toggle>

<Toggle
    @bind-Value="Subscribed"
    Size="ToggleSize.Default"
    Variant="ToggleVariant.Default"
    DefaultValue="true"
    ClassName="bg-blue-100 dark:bg-blue-900"
    aria-label="Subscribe to updates">
    Subscribe
</Toggle>

@code
{
    private bool Subscribed;
}
```
