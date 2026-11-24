---
title: Slider
description: Renders a styled slider control (track, fill, thumb) over a hidden range input with two-way binding and a disabled state.
tags: [slider, range, input, value-picker, form]
---

# Slider

A slider control for choosing a single value from a continuous range — use it when the user should drag a thumb rather than type, e.g. for volume, brightness, or approximate numeric selection where precise input is not required. The component draws a muted track with a primary fill whose width is normalized from `Value`, `Min` and `Max`, plus a centered thumb, while the actual interaction is handled by a visually hidden (`opacity-0`) native `<input type="range">`, so keyboard and screen-reader support are preserved. Notable features include two-way value binding through `Value`/`ValueChanged`, an optional `DefaultValue` applied at initialization, a custom `Min`/`Max` range, and a `Disabled` state that dims the control and blocks pointer events.

## Usage

```razor
<Slider Value="50" Min="0" Max="100" />
```

## Parameters

| Name | Type | Default | Description |
|---|---|---|---|
| `Min` | `int` | `0` | Minimum allowed value. Used together with `Max` to normalize `Value` into the 0-100% fill width and thumb position. |
| `Max` | `int` | `100` | Maximum allowed value. Used together with `Min` to normalize `Value` into the 0-100% fill width and thumb position. |
| `Value` | `int` | `0` | Current value of the slider. Two-way bindable via `@bind-Value`; the hidden range input binds to it with `@bind` / `@bind:event="oninput"`. |
| `ValueChanged` | `EventCallback<int>` | `default` | Invoked with the new `Value` after the user changes the slider, via the range input's `@bind:after` callback. |
| `DefaultValue` | `int?` | `null` | Optional initial value. When set, `Value` is initialized to it in `OnInitialized`. |
| `Disabled` | `bool` | `false` | Disables the slider: sets `data-disabled` on the wrapper and `disabled` on the input, dimming the control (opacity 50%) and disabling pointer events. |

The component inherits from `ShadcnBase`, so the base parameters are also available: `ClassName` (a `string`) is merged with the default wrapper classes `relative w-full h-6 flex items-center data-[disabled]:opacity-50 data-[disabled]:pointer-events-none` in `OnParametersSet`, and `Props` (a `Dictionary<string, object>?`) is applied as extra attributes on the inner range input.

## Examples

Two-way binding of the slider value to a component field:

```razor
<div class="w-full max-w-sm space-y-2">
    <div class="flex justify-between text-sm">
        <span>Volume</span>
        <span class="text-muted-foreground">@volume</span>
    </div>
    <Slider @bind-Value="volume" Min="0" Max="100" />
</div>

@code {
    private int volume = 50;
}
```

Custom range with a two-way bound after-changed callback:

```razor
<div class="w-full max-w-sm">
    <Slider @bind-Value="brightness" Min="0" Max="100" @bind-Value:after="OnBrightnessChanged" />
</div>

@code {
    private int brightness = 80;

    private Task OnBrightnessChanged(int value)
        => Task.CompletedTask;
}
```

Initial value from `DefaultValue`, plus a disabled slider:

```razor
<div class="space-y-6">
    <Slider DefaultValue="25" />
    <Slider Value="75" Min="0" Max="100" Disabled="true" />
</div>
```

Narrowed range (a 10-90 scale) rendered with a custom wrapper class via the inherited `ClassName`:

```razor
<Slider Value="42" Min="10" Max="90" ClassName="max-w-64" />
```
