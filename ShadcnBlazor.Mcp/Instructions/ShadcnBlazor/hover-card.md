---
title: Hover Card
description: Card that appears on hover to show supplementary content like profiles or previews next to the hovered element.
tags: [hover-card, hover, overlay, portal, positioning]
---

# Hover Card

A composable set of components for rendering a floating card next to an element while it is hovered. Use it for quick look-ups that should appear without a click — user profiles, image previews, short descriptions of an icon or label — and where the content is read-only rather than a click-targeted action menu. `<HoverCardTrigger>` wraps its content in a `<span>` that captures the `mouseenter`/`mouseleave` events, debounced by the root's `OpenDelay` and `CloseDelay` before opening and closing. `<HoverCardContent>` is rendered through a portal and positioned by `PositionService` relative to the trigger via `Side`, `Align` and `SideOffset`. Because the content is rendered through a portal, the hosting layout must contain a `<PortalOutlet />`.

## Usage

```razor
@using ShadcnBlazor.Avatars
@using ShadcnBlazor.Buttons
@using ShadcnBlazor.HoverCard

<HoverCard>
    <HoverCardTrigger>
        <Button Variant="ButtonVariant.Link">@@Blazor</Button>
    </HoverCardTrigger>
    <HoverCardContent ClassName="w-80">
        <div class="flex justify-between gap-4">
            <div class="flex items-center">
                <Avatar ClassName="ml-4">
                    <AvatarImage src="/shadcnblazor.svg"/>
                    <AvatarFallback>VC</AvatarFallback>
                </Avatar>
                <h4 class="text-sm font-semibold mr-4 ml-4">@@Blazor</h4>
            </div>
            <div class="space-y-1">
                <p class="text-sm">
                    Super cool and fast framework for dotnet.
                </p>
            </div>
        </div>
    </HoverCardContent>
</HoverCard>
```

## Parameters

| Name | Type | Default | Description |
|---|---|---|---|
| `ChildContent` (HoverCard) | `RenderFragment` | `null` | The root content of the card, normally one `<HoverCardTrigger>` and one `<HoverCardContent>`. Cascades the card state so the trigger can open it and the content can keep it open while hovered. |
| `DefaultOpen` (HoverCard) | `bool` | `false` | Whether the card is open on the first render. |
| `OpenDelay` (HoverCard) | `int` | `300` | Delay in milliseconds after the pointer enters before the card opens. |
| `CloseDelay` (HoverCard) | `int` | `700` | Delay in milliseconds after the pointer has left both the trigger and the content before the card closes. |
| `ChildContent` (HoverCardTrigger) | `RenderFragment` | `null` | The inner content of the default `<span>` trigger element that captures the hover events. |
| `Slot` (HoverCardTrigger) | `RenderFragment<Dictionary<string, object?>>?` | `null` | When set, the `mouseenter`/`mouseleave` handlers are attached to a `<span>` wrapping this template instead of the default `<span>`. Render your own element inside the template and apply the template attributes with `@attributes="context"`. |
| `ChildContent` (HoverCardContent) | `RenderFragment` | `null` | The card's inner content. Rendered in the `HoverCard` portal next to the trigger, at the coordinates computed from `Side`, `Align` and `SideOffset`. |
| `Side` (HoverCardContent) | `PositionSide` | `PositionSide.Bottom` | Which side of the trigger the card appears on: `PositionSide.Right`, `PositionSide.Left`, `PositionSide.Bottom` or `PositionSide.Top`. |
| `Align` (HoverCardContent) | `PositionAlignment` | `PositionAlignment.Center` | Alignment along the trigger's axis: `PositionAlignment.Start`, `PositionAlignment.Center` or `PositionAlignment.End`. |
| `SideOffset` (HoverCardContent) | `int` | `8` | Distance in pixels between the trigger and the card. |

`HoverCardContent` inherits from `ShadcnBase`, so the base parameters are also available on it. `ClassName` (a `string`) is merged into the element's Tailwind classes, and `Props` (a `Dictionary<string, object>?` with `CaptureUnmatchedValues = true`) collects any unmatched attributes and spreads them onto the rendered element.

## Examples

Standard profile-style card adapted from the repo's `HoverCardSc1` block, with an `<Avatar>` and a description text inside the card:

```razor
@using ShadcnBlazor.Avatars
@using ShadcnBlazor.Buttons
@using ShadcnBlazor.HoverCard

<HoverCard>
    <HoverCardTrigger>
        <Button Variant="ButtonVariant.Link">@@Blazor</Button>
    </HoverCardTrigger>
    <HoverCardContent ClassName="w-80">
        <div class="flex justify-between gap-4">
            <div class="flex items-center">
                <Avatar ClassName="ml-4">
                    <AvatarImage src="/shadcnblazor.svg"/>
                    <AvatarFallback>VC</AvatarFallback>
                </Avatar>
                <h4 class="text-sm font-semibold mr-4 ml-4">@@Blazor</h4>
            </div>
            <div class="space-y-1">
                <p class="text-sm">
                    Super cool and fast framework for dotnet.
                </p>
            </div>
        </div>
    </HoverCardContent>
</HoverCard>
```

Positioning variants using the `Side`, `Align` and `SideOffset` parameters, so the card can appear on any side of the trigger with a custom distance:

```razor
@using ShadcnBlazor.Buttons
@using ShadcnBlazor.HoverCard

<HoverCard>
    <HoverCardTrigger>
        <Button Variant="ButtonVariant.Link">On the right</Button>
    </HoverCardTrigger>
    <HoverCardContent Side="PositionSide.Right" Align="PositionAlignment.Center" SideOffset="16" ClassName="w-72">
        <p class="text-sm">Shown to the right of the trigger, 16px away.</p>
    </HoverCardContent>
</HoverCard>

<HoverCard>
    <HoverCardTrigger>
        <Button Variant="ButtonVariant.Link">Above</Button>
    </HoverCardTrigger>
    <HoverCardContent Side="PositionSide.Top" Align="PositionAlignment.Start" SideOffset="12" ClassName="w-72">
        <p class="text-sm">Shown above the trigger, aligned to its start.</p>
    </HoverCardContent>
</HoverCard>
```

`Slot`-based trigger so an existing element (here a badge) carries the hover handler instead of the trigger's default `<span>`:

```razor
@using ShadcnBlazor.HoverCard

<HoverCard>
    <HoverCardTrigger>
        <Slot>
            <span class="inline-flex items-center rounded-md border bg-primary px-2 py-1 text-xs font-semibold text-primary-foreground" @attributes="context">
                ShadcnBlazor
            </span>
        </Slot>
    </HoverCardTrigger>
    <HoverCardContent ClassName="w-80">
        <div class="space-y-1">
            <p class="text-sm">
                Super cool and fast framework for dotnet.
            </p>
        </div>
    </HoverCardContent>
</HoverCard>
```

Combined usage: a card that is already open on the first render (`DefaultOpen`) with tuned open/close debouncing, and richer content:

```razor
@using ShadcnBlazor.Avatars
@using ShadcnBlazor.Buttons
@using ShadcnBlazor.HoverCard

<HoverCard DefaultOpen OpenDelay="100" CloseDelay="1000">
    <HoverCardTrigger>
        <Button Variant="ButtonVariant.Link">@@Blazor</Button>
    </HoverCardTrigger>
    <HoverCardContent Side="PositionSide.Bottom" Align="PositionAlignment.End" ClassName="w-80">
        <div class="flex justify-between gap-4">
            <div class="flex items-center">
                <Avatar ClassName="ml-4">
                    <AvatarImage src="/shadcnblazor.svg"/>
                    <AvatarFallback>VC</AvatarFallback>
                </Avatar>
                <h4 class="text-sm font-semibold mr-4 ml-4">@@Blazor</h4>
            </div>
            <div class="space-y-1">
                <p class="text-sm">
                    Super cool and fast framework for dotnet.
                </p>
            </div>
        </div>
    </HoverCardContent>
</HoverCard>
```
