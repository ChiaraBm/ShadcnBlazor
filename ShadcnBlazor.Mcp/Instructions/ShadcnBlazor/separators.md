---
title: Separator
description: Thin visual divider line, horizontal or vertical, that separates content regions in layouts.
tags: [separator, divider, layout, orientation]
---
# Separator

The Separator renders a thin, non-interactive line that divides content regions — a 1-pixel-wide horizontal rule or a 1-pixel-tall vertical bar. It is the building block for separating sections of a form, splitting a card into independent panels, or dividing items inside a flex row. The orientation is chosen with the `Orientation` parameter (`SeparatorOrientation.Horizontal` or `SeparatorOrientation.Vertical`), and the `Decorative` flag (default `true`) tells screen readers to skip the line, which is the correct semantics in almost every layout. The element carries `data-slot="separator"` and a `data-orientation` attribute, so its styling can be extended with the Tailwind classes of your theme.

## Usage

```razor
@using ShadcnBlazor.Separators

<Separator />
```

## Parameters

| Name | Type | Default | Description |
| --- | --- | --- | --- |
| `ChildContent` | `RenderFragment` | — | Content rendered inside the separator element. |
| `Decorative` | `bool` | `true` | Rendered as the `decorative` attribute; when `true`, assistive technologies skip the line. |
| `Orientation` | `SeparatorOrientation` | `SeparatorOrientation.Horizontal` | Orientation of the line: `SeparatorOrientation.Horizontal` (0) renders a full-width 1px-tall rule; `SeparatorOrientation.Vertical` (1) renders a full-height 1px-wide bar. |
| `ClassName` | `string` | — | Extra Tailwind classes merged into the root element via Tailwind merge. Inherited from `ShadcnBase`. |
| `Props` | `Dictionary<string, object>?` | — | Any unmatched attributes (`CaptureUnmatchedValues`) applied to the root element. Inherited from `ShadcnBase`. |

## Examples

### Vertical separator inside a flex row

```razor
@using ShadcnBlazor.Separators

<div class="flex items-center gap-4">
    <span>Left panel</span>
    <Separator Orientation="SeparatorOrientation.Vertical" />
    <span>Right panel</span>
</div>
```

### Non-decorative separator with custom styling

```razor
@using ShadcnBlazor.Separators

<Separator
    Decorative="false"
    ClassName="border-t" />
```

### Passing extra attributes through `Props`

```razor
@using ShadcnBlazor.Separators

<Separator data-slot="section-divider" aria-label="Section boundary" />
```
