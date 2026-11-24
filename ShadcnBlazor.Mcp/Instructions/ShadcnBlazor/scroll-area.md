---
title: Scroll Area
description: Custom-styled scroll container with a custom, JS-positioned scroll thumb that can be shown on hover, on scroll, always or only when content overflows.
tags: [scroll-area, scroll, scrollbar, overflow, interaction]
---

# Scroll Area

A custom scroll container that replaces the native scrollbar with its own `<ScrollBar>` thumb driven by JavaScript interop (`shadcnBlazor.scrollBar.*`), giving you the thin, rounded, theme-aware look of shadcn/ui inside a Blazor component. Use it whenever a fixed-size box contains more content than it can display — long lists, code blocks, chat histories, horizontally overflowing cards — and you want the scroll indicators to match the rest of the design system rather than the browser chrome. The `Type` parameter controls when the scrollbar shows (`ScrollAreaType.Hover` by default), `Direction` (`ScrollAreaDirection.Ltr` / `ScrollAreaDirection.Rtl`) flips the layout for right-to-left content, and `ScrollHideDelay` tunes how long after the last interaction the thumb stays visible. An inner `<ScrollBar Orientation="..."/>` is rendered automatically; you can also add explicit `<ScrollBar>` elements for a specific orientation inside the `<CascadingValue>` it provides.

## Usage

```razor
@using ShadcnBlazor.ScrollArea

<ScrollArea ClassName="h-72 w-48 rounded-md border">
    <div class="p-4">
        <h4 class="mb-4 text-sm leading-none font-medium">Items</h4>
        <div class="text-sm">First item</div>
        <div class="text-sm">Second item</div>
        <div class="text-sm">Third item</div>
    </div>
</ScrollArea>
```

## Parameters

| Name | Type | Default | Description |
|---|---|---|---|
| `ChildContent` (ScrollArea) | `RenderFragment?` | `null` | The scrollable inner content rendered inside the `overflow: scroll` viewport. |
| `Type` (ScrollArea) | `ScrollAreaType?` | `ScrollAreaType.Hover` | When the scrollbar is shown: `ScrollAreaType.Auto` (only when the content overflows), `ScrollAreaType.Always` (always visible), `ScrollAreaType.Hover` (visible while the pointer is over the area, default) or `ScrollAreaType.Scroll` (visible only while scrolling, then hidden after `ScrollHideDelay` milliseconds). |
| `Direction` (ScrollArea) | `ScrollAreaDirection?` | `ScrollAreaDirection.Ltr` | Layout direction for the scrollbar placement: `ScrollAreaDirection.Ltr` or `ScrollAreaDirection.Rtl`. |
| `ScrollHideDelay` (ScrollArea) | `int` | `600` | Delay in milliseconds before the scrollbar fades out, used by both the hover and scroll visibility modes. |
| `Orientation` (ScrollBar) | `ScrollBarOrientation` | `ScrollBarOrientation.Vertical` | Which axis the `<ScrollBar>` element drives: `ScrollBarOrientation.Vertical` or `ScrollBarOrientation.Horizontal`. |
| `ClassName` (base) | `string` | - | Merged into the Tailwind classes of the root `<div>` (via the `Cn` helper). |
| `Props` (base) | `Dictionary<string, object>?` | - | Unmatched attributes (`CaptureUnmatchedValues = true`) spread onto the root element. |

`ScrollArea` and `ScrollBar` both inherit from `ShadcnBase`, so the base parameters above are available on both.

## Examples

Vertical scroll area with an always-visible scrollbar and RTL direction, adapted from the repo's `ScrollAreaSc1` vertical block:

```razor
@using ShadcnBlazor.ScrollArea
@using ShadcnBlazor.Separators

<div class="group relative mt-4 mb-12 flex flex-col">
<ScrollArea Direction="ScrollAreaDirection.Rtl" Type="ScrollAreaType.Always" ClassName="h-72 w-48 rounded-md border">
    <div class="p-4">
        <h4 class="mb-4 text-sm leading-none font-medium">Items</h4>
        @foreach (var item in Items)
        {
            <div class="text-sm">
                @item
            </div>
            <Separator ClassName="my-2"/>
        }
    </div>
</ScrollArea>
</div>

@code
{
    private List<string> Items { get; set; } = ["One", "Two", "Three"];
}
```

Horizontal scroll area that only appears when the content overflows (`ScrollAreaType.Auto`), using an explicit horizontal `<ScrollBar>`, adapted from the repo's `ScrollAreaSc1` horizontal block:

```razor
@using ShadcnBlazor.ScrollArea

<div class="group relative mt-4 mb-12 flex flex-col">
    <ScrollArea Direction="ScrollAreaDirection.Rtl"  Type="ScrollAreaType.Auto" ClassName="rounded-md border whitespace-nowrap w-80">
        <div class="flex w-max space-x-4 p-4">
            @foreach (var item in Items)
            {
                <div class="shrink-0">
                    <div class="overflow-hidden rounded-md">
                        @item
                    </div>
                </div>
            }
        </div>
        <ScrollBar
            Orientation="@ScrollBarOrientation.Horizontal"
        />
    </ScrollArea>
</div>

@code
{
    private List<string> Items { get; set; } = ["Jesper <3", "Blo.by", "Chiara", "ShadcnBlazor", "LucideBlazor"];
}
```

Scrollbar that only appears while scrolling and then fades out after a custom delay, showing how to combine `Type="ScrollAreaType.Scroll"` with a tuned `ScrollHideDelay`:

```razor
@using ShadcnBlazor.ScrollArea

<ScrollArea Type="ScrollAreaType.Scroll" ScrollHideDelay="1000" ClassName="h-64 w-56 rounded-md border">
    <div class="p-4">
        @for (var i = 0; i < 40; i++)
        {
            <div class="mb-2 text-sm">Row @i</div>
        }
    </div>
</ScrollArea>
```

Explicit horizontal `<ScrollBar>` rendered alongside the default auto one, so the area shows both a vertical and a horizontal custom scrollbar:

```razor
@using ShadcnBlazor.ScrollArea

<ScrollArea ClassName="h-48 w-72 overflow-hidden rounded-md border">
    <div class="flex w-max gap-4 p-4">
        @for (var i = 0; i < 20; i++)
        {
            <div class="h-24 w-24 shrink-0 rounded-md bg-muted">Card @i</div>
        }
    </div>
    <ScrollBar Orientation="@ScrollBarOrientation.Horizontal" />
</ScrollArea>
```
