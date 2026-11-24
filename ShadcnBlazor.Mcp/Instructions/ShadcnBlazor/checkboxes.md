---
title: Checkbox
description: An accessible, toggleable checkbox rendered as a button with a checkmark, supporting two-way binding, a default value and custom classes.
tags: [checkbox, form, input, selection, toggle]
---

# Checkbox

A small, square, clickable checkbox used to let users select one or more options in a form or list (for example accepting terms, choosing items, or toggling a setting). It is rendered as a native `<button type="button">` element that flips its `data-state` between `checked` and `unchecked` and updates `aria-checked` for accessibility. When checked, a Lucide checkmark icon appears inside the box. The component supports two-way binding via `Value`/`ValueChanged`, a `DefaultValue` for one-time initialization, and the inherited `ClassName` and `Props` from `ShadcnBase` so you can restyle the box or merge in extra attributes.

## Usage

```razor
@using ShadcnBlazor.Checkboxes

<Checkbox @bind-Value="FirstCheckboxValue"/>
```

```csharp
@code
{
    private bool FirstCheckboxValue;
}
```

## Parameters

| Name | Type | Default | Description |
|---|---|---|---|
| `Value` | `bool` | `false` | The current checked/unchecked state of the checkbox. When `true`, the box renders a checkmark and `data-state="checked"`. |
| `ValueChanged` | `EventCallback<bool>` | `null` | Invoked with the new state every time the checkbox is toggled. Pair it with `@bind-Value` or supply a `ValueChanged="handler"` callback. |
| `DefaultValue` | `bool?` | `null` | Optional one-time seed value. If a value is provided, `OnInitialized` copies it into `Value`; subsequent changes are driven by interaction, not by re-setting this parameter. |

The component inherits from `ShadcnBase`, so the base parameters are also available: `ClassName` (a `string`) is merged with the component's built-in box classes, and `Props` (a `Dictionary<string, object>?`) captures any unmatched attributes and merges them into the rendered `<button>`.

## Examples

Checkbox paired with a label, using two-way binding:

```razor
@using ShadcnBlazor.Checkboxes
@using ShadcnBlazor.Labels

<div class="flex items-center gap-3">
    <Checkbox @bind-Value="FirstCheckboxValue" id="terms"/>
    <Label for="terms">Accept terms and conditions</Label>
</div>

@code
{
    private bool FirstCheckboxValue;
}
```

One-way controlled state using the `ValueChanged` callback, with a default initial value:

```razor
@using ShadcnBlazor.Checkboxes
@using ShadcnBlazor.Labels

<div class="flex items-center gap-3">
    <Checkbox id="terms-2" Value="true"/>
    <div class="grid gap-2">
        <Label for="terms-2">Accept terms and conditions</Label>
        <p class="text-muted-foreground text-sm">
            By clicking this checkbox, you agree to the terms and conditions.
        </p>
    </div>
</div>
```

A disabled checkbox, plus a custom-styled checkbox (blue when checked) inside a wrapping label:

```razor
@using ShadcnBlazor.Checkboxes
@using ShadcnBlazor.Labels

<div class="flex items-center gap-3">
    <Checkbox id="toggle" disabled="disabled"/>
    <Label for="toggle">Enable notifications</Label>
</div>

<label for="toggle-2"
       class="label hover:bg-accent/50 flex items-start gap-3 rounded-lg border p-3 has-[[aria-checked=true]]:border-blue-600 has-[[aria-checked=true]]:bg-blue-50 dark:has-[[aria-checked=true]]:border-blue-900 dark:has-[[aria-checked=true]]:bg-blue-950">
    <Checkbox
        id="toggle-2"
        ClassName="data-[state=checked]:border-blue-600 data-[state=checked]:bg-blue-600 data-[state=checked]:text-white dark:data-[state=checked]:border-blue-700 dark:data-[state=checked]:bg-blue-700"/>
    <div class="grid gap-1.5 font-normal">
        <p class="text-sm leading-none font-medium">Enable notifications</p>
        <p class="text-muted-foreground text-sm">
            You can enable or disable notifications at any time.
        </p>
    </div>
</label>
```

Checkbox used to select rows in a grid, where each row's state is controlled and a `ValueChanged` handler mutates a shared collection:

```razor
@using ShadcnBlazor.Checkboxes

<Checkbox ClassName="ms-2" Value="isSelected" ValueChanged="b => ToggleAsync(item, b)" />

@code
{
    private bool isSelected;

    private Task ToggleAsync(object item, bool value)
    {
        if (value)
            SelectedItems.Add(item);
        else
            SelectedItems.Remove(item);
        return Task.CompletedTask;
    }
}
```
