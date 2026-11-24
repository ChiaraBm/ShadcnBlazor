---
title: Progress
description: Renders a horizontal progress bar whose fill width is driven by Value and Max, exposed via data attributes.
tags: [progress, progress-bar, indicator, status, feedback]
---

# Progress

A horizontal progress bar used to show the completion of a task or the advancement of an operation, such as upload progress, download completion, or a long-running process. The fill width is computed from `Value` (treated as a percentage, 0-100) while `Value` and `Max` are also exposed as `data-value` / `data-max` attributes on both the outer element and the inner indicator, so external CSS or JavaScript can react to the state. Notable features include a built-in rounded full-width track (`bg-primary/20 h-2 w-full overflow-hidden rounded-full`), a transition-animated primary-colored indicator, and the standard `ShadcnBase` parameters for extra classes and attributes.

## Usage

```razor
<Progress Value="60" Max="100" />
```

## Parameters

| Name | Type | Default | Description |
|---|---|---|---|
| `Value` | `int` | `0` | Current progress value. It is rendered as `data-value` and drives the fill position of the indicator via `transform: translateX(-@(100 - Value)%)`, so it is treated as a percentage (0-100). |
| `Max` | `int` | `0` | Maximum value of the progress. It is rendered as the `data-max` attribute on the outer element and the indicator. |

The component inherits from `ShadcnBase`, so the base parameters (such as `ClassName` and `Props`) are also available. `ClassName` is merged with the default track classes (`bg-primary/20 relative h-2 w-full overflow-hidden rounded-full`), and `Props` (a `Dictionary<string, object>?`) is merged into the rendered attributes of the outer element.

## Examples

Progress bar showing an in-flight value:

```razor
<div class="space-y-2">
    <Progress Value="60" Max="100" />
    <p class="text-sm text-muted-foreground">60%</p>
</div>
```

Multiple progress bars with different values:

```razor
<div class="space-y-4">
    <Progress Value="25" Max="100" />
    <Progress Value="50" Max="100" />
    <Progress Value="75" Max="100" />
    <Progress Value="100" Max="100" />
</div>
```

Thicker track via the inherited `ClassName` (merged with the default `h-2` height):

```razor
<Progress Value="80" Max="100" ClassName="h-4" />
```

Custom element attributes via the inherited `Props` dictionary:

```razor
<Progress Value="45" Max="100"
          Props="new Dictionary<string, object?> { { "title", "Download progress" } }" />
```
