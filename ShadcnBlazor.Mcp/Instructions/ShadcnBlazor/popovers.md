---
title: Popover
description: A floating panel anchored to a trigger or anchor element, rendered through a portal and closable by clicking the backdrop.
tags: [popover, overlay, portal, positioning, trigger, dialog]
---

# Popover

A composable set of components for rendering a floating panel next to an element when it is clicked. Use it for interactive content that needs to be anchored to a specific place on the page — feedback forms, share panels, quick settings, confirmation or input dialogs — rather than for hover-triggered previews (use a hover card) or command menus (use a dropdown). `<PopoverTrigger>` registers a click handler on its default `<Button>` element (or on any element through `Slot`) and opens the popover; `<PopoverContent>` is rendered through a portal and positioned by `PositionService` relative to the trigger via `Side`, `Align` and `SideOffset`, optionally around a `<PopoverAnchor>` element instead of the trigger. The content has a transparent full-screen backdrop that closes the popover when clicked.

## Usage

```razor
@using ShadcnBlazor.Popovers

<Popover>
    <PopoverTrigger>
        Share feedback
    </PopoverTrigger>
    <PopoverContent ClassName="w-80">
        <div class="space-y-4">
            <div>
                <h4 class="mb-1 font-semibold">Tell us what you think</h4>
                <p class="text-xs text-muted-foreground">
                    Your input shapes what we build next.
                </p>
            </div>
            <TextareaInputField @bind-Value="Feedback" placeholder="What could we improve?" rows="4" />
            <Button ClassName="w-full">Send</Button>
        </div>
    </PopoverContent>
</Popover>

@code
{
    private string Feedback = "";
}
```

`PopoverContent` inherits from `ShadcnBase`, so the base parameters are also available on it. `ClassName` (a `string`) is merged into the element's Tailwind classes, and `Props` (a `Dictionary<string, object>?` with `CaptureUnmatchedValues = true`) collects any unmatched attributes and spreads them onto the rendered element.

## Parameters

| Name | Type | Default | Description |
|---|---|---|---|
| `ChildContent` (Popover) | `RenderFragment` | `null` | The root content of the popover, normally one `<PopoverTrigger>` plus a `<PopoverContent>` and optionally a `<PopoverAnchor>`. Cascades the popover state so the trigger can open it and the content can be rendered. |
| `DefaultOpen` (Popover) | `bool` | `false` | Whether the popover is open on the first render. |
| `OnOpenChange` (Popover) | `EventCallback<bool>` | `null` | Fired with the new open state whenever the popover is opened or closed, e.g. by a trigger click or a backdrop click. |
| `ChildContent` (PopoverTrigger) | `RenderFragment` | `null` | The inner content of the default `<Button>` trigger element that opens the popover on click. |
| `Slot` (PopoverTrigger) | `RenderFragment<Dictionary<string, object?>>?` | `null` | When set, the click handler and `data-slot` attributes are attached to a `<span>` wrapping this template instead of the default `<Button>`. Render your own element inside the template and apply the template attributes with `@attributes="context"`. |
| `ChildContent` (PopoverAnchor) | `RenderFragment` | `null` | The element that `<PopoverContent>` is positioned around instead of the trigger. |
| `ChildContent` (PopoverContent) | `RenderFragment` | `null` | The popover's inner content. Rendered in the `Popover` portal next to the trigger or anchor, at the coordinates computed from `Side`, `Align` and `SideOffset`. |
| `Side` (PopoverContent) | `PositionSide` | `PositionSide.Bottom` | Which side of the trigger or anchor the content appears on: `PositionSide.Right`, `PositionSide.Left`, `PositionSide.Bottom` or `PositionSide.Top`. |
| `Align` (PopoverContent) | `PositionAlignment` | `PositionAlignment.Center` | Alignment along the trigger's axis: `PositionAlignment.Start`, `PositionAlignment.Center` or `PositionAlignment.End`. |
| `SideOffset` (PopoverContent) | `double` | `10` | Distance in pixels between the trigger and the content. |

## Examples

Standard feedback-style popover adapted from the repo's `FeedbackPopover` block, with a `Slot`-based trigger and a textarea inside the content:

```razor
@using LucideBlazor
@using ShadcnBlazor.Buttons
@using ShadcnBlazor.Inputs
@using ShadcnBlazor.Popovers

<Popover>
    <PopoverTrigger>
        <Slot>
            <Button @attributes="context" Variant="ButtonVariant.Outline">
                <MessageSquareIcon ClassName="h-4 w-4" />
                Share feedback
            </Button>
        </Slot>
    </PopoverTrigger>
    <PopoverContent ClassName="w-80">
        <div class="space-y-4">
            <div>
                <h4 class="mb-1 font-semibold">Tell us what you think</h4>
                <p class="text-xs text-muted-foreground">
                    Your input shapes what we build next.
                </p>
            </div>
            <TextareaInputField @bind-Value="Feedback" placeholder="What could we improve?" rows="4" />
            <Button ClassName="w-full">Send</Button>
        </div>
    </PopoverContent>
</Popover>

@code
{
    private string Feedback = "";
}
```

Positioning variants using the `Side`, `Align` and `SideOffset` parameters, so the content can appear on any side of the trigger with a custom distance:

```razor
@using ShadcnBlazor.Popovers

<Popover>
    <PopoverTrigger>
        On the right
    </PopoverTrigger>
    <PopoverContent Side="PositionSide.Right" Align="PositionAlignment.Center" SideOffset="16" ClassName="w-72">
        <p class="text-sm">Shown to the right of the trigger, 16px away.</p>
    </PopoverContent>
</Popover>

<Popover>
    <PopoverTrigger>
        Above
    </PopoverTrigger>
    <PopoverContent Side="PositionSide.Top" Align="PositionAlignment.Start" SideOffset="12" ClassName="w-72">
        <p class="text-sm">Shown above the trigger, aligned to its start.</p>
    </PopoverContent>
</Popover>
```

Combined usage: a share panel adapted from the repo's `SharePopover` block, with actions and a copy-link input inside the content:

```razor
@using LucideBlazor
@using ShadcnBlazor.Buttons
@using ShadcnBlazor.Inputs
@using ShadcnBlazor.Popovers
@using ShadcnBlazor.Separators

<Popover>
    <PopoverTrigger>
        <Slot>
            <Button @attributes="context" Variant="ButtonVariant.Outline">
                <Share2Icon ClassName="h-4 w-4" />
                Share
            </Button>
        </Slot>
    </PopoverTrigger>
    <PopoverContent ClassName="w-80">
        <div class="space-y-4">
            <h4 class="font-semibold">Share this article</h4>
            <div class="flex gap-2">
                <Button ClassName="flex-1" Variant="ButtonVariant.Outline">
                    <GlobeIcon ClassName="h-4 w-4" />
                    Website
                </Button>
                <Button ClassName="flex-1" Variant="ButtonVariant.Outline">
                    <MailIcon ClassName="h-4 w-4" />
                    Email
                </Button>
            </div>
            <Separator />
            <div class="space-y-2">
                <p class="text-xs text-muted-foreground">Or copy the link</p>
                <div class="flex gap-2">
                    <TextInputField @bind-Value="ShareUrl" readOnly />
                    <Button Size="ButtonSize.Icon" Variant="ButtonVariant.Outline">
                        <CopyIcon ClassName="h-4 w-4" />
                    </Button>
                </div>
            </div>
        </div>
    </PopoverContent>
</Popover>

@code
{
    private string ShareUrl = "https://acme.dev/blog/launch";
}
```

Anchor the content to an arbitrary element instead of the trigger, while still tracking the open state through `OnOpenChange`:

```razor
@using ShadcnBlazor.Popovers

<Popover OnOpenChange="@(open => { IsOpen = open; })">
    <PopoverTrigger>
        Show profile info
    </PopoverTrigger>
    <PopoverAnchor>
        <span class="inline-flex items-center rounded-md border bg-primary px-2 py-1 text-xs font-semibold text-primary-foreground">
            ShadcnBlazor
        </span>
    </PopoverAnchor>
    <PopoverContent Side="PositionSide.Bottom" Align="PositionAlignment.End" ClassName="w-80">
        <div class="space-y-1">
            <p class="text-sm">
                Super cool and fast framework for dotnet.
            </p>
        </div>
    </PopoverContent>
</Popover>

@code
{
    private bool IsOpen;
}
```
