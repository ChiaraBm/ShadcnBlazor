---
title: Tooltip
description: Hover tooltip that positions its content beside any trigger element, with configurable side, alignment, arrow and default-open state.
tags: [tooltip, hover, positioning, portal, arrow]
---

# Tooltip

A composable tooltip that shows a small piece of help text when the pointer hovers over an element. Use it to label icons, buttons and form controls that need a short contextual explanation without a permanent caption. `<Tooltip>` owns the open/closed state and cascades it, `<TooltipTrigger>` attaches `onmouseenter`/`onmouseleave` to a default `<button>` (or to any existing element through its `Slot` template), and `<TooltipContent>` is positioned next to the trigger by `PositionService` inside a portal, with an automatically aligned arrow. Because the content is rendered through a portal, the hosting layout must contain a `<PortalOutlet />`.

## Usage

```razor
@using ShadcnBlazor.Tooltips
@using ShadcnBlazor.Interop

<Tooltip>
    <TooltipTrigger>Hover</TooltipTrigger>
    <TooltipContent>
        <p>Add to library</p>
    </TooltipContent>
</Tooltip>
```

## Parameters

| Name | Type | Default | Description |
|---|---|---|---|
| `ChildContent` (Tooltip) | `RenderFragment` | `null` | The root content of the tooltip, normally one `<TooltipTrigger>` and one `<TooltipContent>`. Cascades the `Tooltip` instance so the trigger can open and close it. |
| `DefaultOpen` (Tooltip) | `bool` | `false` | Whether the tooltip starts open. `Open` and `ShouldRenderContent` are initialized from it in `OnInitialized`. |
| `ChildContent` (TooltipTrigger) | `RenderFragment` | `null` | The inner text of the default `<button>` trigger that captures `onmouseenter`/`onmouseleave`. |
| `Slot` (TooltipTrigger) | `RenderFragment<Dictionary<string, object?>>?` | `null` | When set, the hover events are attached to a `<span>` wrapping this template instead of the default `<button>`. Render your own element inside the template and apply the template attributes with `@attributes="context"`. |
| `ClassName` (TooltipTrigger) | `string?` | `null` | Classes applied to the trigger's default `<Button>` wrapper. |
| `ChildContent` (TooltipContent) | `RenderFragment` | `null` | The tooltip's inner content (e.g. a `<p>`). Rendered in the `Tooltip` portal next to the trigger element, together with an arrow positioned per `Side`. |
| `Side` (TooltipContent) | `PositionSide` | `PositionSide.Top` | Which side of the trigger the content appears on: `PositionSide.Top`, `PositionSide.Bottom`, `PositionSide.Left` or `PositionSide.Right`. Also sets the content's `data-side` attribute and the arrow position. |
| `Align` (TooltipContent) | `PositionAlignment` | `PositionAlignment.Center` | Alignment of the content relative to the trigger: `PositionAlignment.Start`, `PositionAlignment.Center` or `PositionAlignment.End`. |
| `SideOffset` (TooltipContent) | `double` | `12` | Distance in pixels between the trigger and the content along the `Side` axis. |

Every component in this folder inherits from `ShadcnBase`, so the base parameters are also available on each one. `ClassName` (a `string`) is merged into the element's Tailwind classes, and `Props` (a `Dictionary<string, object>?` with `CaptureUnmatchedValues = true`) collects any unmatched attributes and spreads them onto the rendered element.

## Examples

Standard hover tooltip with a `Slot`-based `<Button>` trigger, as in the repo's `TooltipSc1` showcase:

```razor
@using ShadcnBlazor.Tooltips
@using ShadcnBlazor.Buttons
@using ShadcnBlazor.Interop

<Tooltip>
    <TooltipTrigger>
        <Slot>
            <Button Variant="ButtonVariant.Outline" @attributes="context">Hover</Button>
        </Slot>
    </TooltipTrigger>
    <TooltipContent Side="PositionSide.Top">
        <p>Add to library</p>
    </TooltipContent>
</Tooltip>
```

Content placed on the other sides, with explicit `Align` and a larger `SideOffset`:

```razor
@using ShadcnBlazor.Tooltips
@using ShadcnBlazor.Interop

<Tooltip>
    <TooltipTrigger>Bottom</TooltipTrigger>
    <TooltipContent Side="PositionSide.Bottom" Align="PositionAlignment.End" SideOffset="20">
        <p>Appears below the button</p>
    </TooltipContent>
</Tooltip>

<Tooltip>
    <TooltipTrigger>Left</TooltipTrigger>
    <TooltipContent Side="PositionSide.Left">
        <p>Appears to the left</p>
    </TooltipContent>
</Tooltip>

<Tooltip>
    <TooltipTrigger>Right</TooltipTrigger>
    <TooltipContent Side="PositionSide.Right" Align="PositionAlignment.Start">
        <p>Appears to the right</p>
    </TooltipContent>
</Tooltip>
```

A tooltip that starts open (`DefaultOpen`) with custom content styling through `ClassName`:

```razor
@using ShadcnBlazor.Tooltips
@using ShadcnBlazor.Interop

<Tooltip DefaultOpen="true">
    <TooltipTrigger>Always visible</TooltipTrigger>
    <TooltipContent Side="PositionSide.Top" ClassName="text-sm">
        <p>This tooltip is rendered open from the start.</p>
    </TooltipContent>
</Tooltip>
```

Combined usage: multiple triggers inside one page, mixing the default `<button>` trigger and a `Slot`-based trigger around a custom element, each with its own side and alignment:

```razor
@using ShadcnBlazor.Tooltips
@using ShadcnBlazor.Buttons
@using ShadcnBlazor.Interop

<div class="flex items-center gap-4">
    <Tooltip>
        <TooltipTrigger>Default button</TooltipTrigger>
        <TooltipContent Side="PositionSide.Top">
            <p>Uses the built-in trigger button</p>
        </TooltipContent>
    </Tooltip>

    <Tooltip>
        <TooltipTrigger>
            <Slot>
                <span class="cursor-help rounded-md border border-dashed px-3 py-1.5 text-sm" @attributes="context">Slot span</span>
            </Slot>
        </TooltipTrigger>
        <TooltipContent Side="PositionSide.Right" Align="PositionAlignment.Start">
            <p>Attached to your own element via the Slot template</p>
        </TooltipContent>
    </Tooltip>
</div>
```
