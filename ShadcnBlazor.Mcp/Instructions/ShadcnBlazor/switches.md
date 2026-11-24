---
title: Switch
description: An accessible on/off switch rendered as a button with a sliding thumb, supporting two-way binding, a default value, a size, a disabled state and custom classes.
tags: [switch, toggle, form, input, boolean]
---

# Switch

A small, clickable on/off switch used to let users toggle a single boolean setting on or off (for example enabling notifications, dark mode, or any binary preference). It is rendered as a native `<button type="button">` element with an inner `div` thumb that slides to reflect the current state, and it exposes `data-state`, `data-checked` and `data-unchecked` attributes for accessibility and styling. The component supports two-way binding via `Value`/`ValueChanged`, a one-time seed through `DefaultValue`, a `Size` of either `Default` or `Sm`, a `Disabled` state, and the inherited `ClassName` and `Props` from `ShadcnBase` so you can restyle the track or merge in extra attributes.

## Usage

```razor
@using ShadcnBlazor.Switches

<Switch @bind-Value="IsChecked"/>
```

```csharp
@code
{
    private bool IsChecked;
}
```

## Parameters

| Name | Type | Default | Description |
|---|---|---|---|
| `Value` | `bool` | `false` | The current on/off state of the switch. When `true`, the track renders `data-state="checked"` and the thumb slides to the right; when `false` it renders `data-state="unchecked"`. |
| `ValueChanged` | `EventCallback<bool>` | `null` | Invoked with the new state every time the switch is toggled. Pair it with `@bind-Value` or supply a `ValueChanged="handler"` callback. |
| `DefaultValue` | `bool?` | `null` | Optional one-time seed value. If a value is provided, `OnInitialized` copies it into `Value`; subsequent changes are driven by interaction, not by re-setting this parameter. |
| `Size` | `SwitchSize` | `SwitchSize.Default` | The rendered size of the switch. `SwitchSize.Default` is `18.4px` tall by `32px` wide; `SwitchSize.Sm` is `14px` tall by `24px` wide. |
| `Disabled` | `bool` | `false` | When `true`, the `<button>` receives the `disabled` attribute and the switch shows `data-disabled` styling (reduced opacity and a `not-allowed` cursor) without responding to clicks. |

The component inherits from `ShadcnBase`, so the base parameters are also available: `ClassName` (a `string`) is merged with the component's built-in track classes, and `Props` (a `Dictionary<string, object>?`) captures any unmatched attributes and merges them into the rendered `<button>`.

The `SwitchSize` enum (namespace `ShadcnBlazor.Switches`) has exactly two members: `Default = 0` and `Sm = 1`.

## Examples

Switch paired with a label, using two-way binding:

```razor
@using ShadcnBlazor.Switches
@using ShadcnBlazor.Labels

<div class="flex items-center gap-3">
    <Switch @bind-Value="IsChecked"/>
    <Label>Is Checked</Label>
</div>

@code
{
    private bool IsChecked;
}
```

One-way controlled state using the `ValueChanged` callback, with a default initial value and the smaller size:

```razor
@using ShadcnBlazor.Switches
@using ShadcnBlazor.Labels

<div class="flex items-center gap-3">
    <Switch Value="@Value" ValueChanged="@(v => Value = v)" Size="@SwitchSize.Sm"/>
    <Label>Small switch</Label>
</div>

@code
{
    private bool Value;
}
```

A disabled switch, alongside a custom-styled switch (blue track when checked) using the inherited `ClassName`:

```razor
@using ShadcnBlazor.Switches
@using ShadcnBlazor.Labels

<div class="flex items-center gap-3">
    <Switch Value="true" Disabled="true"/>
    <Label>Enable notifications</Label>
</div>

<div class="flex items-center gap-3">
    <Switch @bind-Value="IsChecked2"
            ClassName="data-checked:bg-blue-600 data-unchecked:bg-neutral-300 dark:data-unchecked:bg-neutral-700"/>
    <Label>Custom style</Label>
</div>

@code
{
    private bool IsChecked2;
}
```

Multiple switches whose state is tracked together, where a `ValueChanged` handler logs each toggle:

```razor
@using ShadcnBlazor.Switches

@for (int i = 0; i < Settings.Length; i++)
{
    <div class="flex items-center gap-3">
        <Switch Value="@Settings[i].Enabled" ValueChanged="v => ToggleAsync(i, v)"/>
        <span>@Settings[i].Name</span>
    </div>
}

@code
{
    private Setting[] Settings =
    {
        new("Notifications", true),
        new("Auto-save", false)
    };

    private Task ToggleAsync(int index, bool value)
    {
        Settings[index] = Settings[index] with { Enabled = value };
        return Task.CompletedTask;
    }

    private record Setting(string Name, bool Enabled);
}
```
