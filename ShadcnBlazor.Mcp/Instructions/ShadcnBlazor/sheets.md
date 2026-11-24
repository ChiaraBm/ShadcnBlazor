---
title: Sheet
description: A sheet that slides in from the edge of the screen, opened by a trigger or from C#, with overlay, header, footer and close button.
tags: [sheet, drawer, slide-over, overlay, trigger, backdrop]
---

# Sheet

A sheet slides in from one edge of the screen (`SheetSide.Top`, `Right`, `Bottom` or `Left`) to display supplementary content without losing the context of the page. It is the right choice for sidebars, settings panels, and forms that should stay anchored to an edge, as opposed to a centered `Dialog`. It can be opened by clicking a `SheetTrigger` or programmatically via `OpenAsync()` / `CloseAsync()` from C# code, and the open state is reported through the `OnOpenChange` callback. The content is rendered in a portal (`SectionName="@PortalNames.Sheet"`) on top of a dimmed `SheetOverlay`, with `SheetHeader` / `SheetFooter` / `SheetTitle` / `SheetDescription` structuring the content, and a close button (`X`) in the top-right corner.

## Usage

```razor
@using ShadcnBlazor.Sheets
@using ShadcnBlazor.Buttons

<Sheet>
    <SheetTrigger>
        <Slot>
            <Button Variant="ButtonVariant.Outline" @attributes="context">Open</Button>
        </Slot>
    </SheetTrigger>
    <SheetContent>
        <SheetHeader>
            <SheetTitle>Edit profile</SheetTitle>
            <SheetDescription>Make changes to your profile here.</SheetDescription>
        </SheetHeader>
        <SheetFooter>
            <Button type="submit">Save changes</Button>
            <SheetClose>Close</SheetClose>
        </SheetFooter>
    </SheetContent>
</Sheet>
```

## Parameters

### Sheet

| Name | Type | Default | Description |
| --- | --- | --- | --- |
| `ChildContent` | `RenderFragment` | — | The sheet's trigger and content, composed inside the `Sheet` so they receive the cascaded sheet. |
| `OnOpenChange` | `EventCallback<bool>` | — | Fired with the new open/closed state whenever the sheet opens or closes. |
| `DefaultOpen` | `bool` | `false` | When `true`, the sheet is opened automatically on first render. |

The component also exposes `IsOpen` (read-only `bool`) and the methods `OpenAsync()` / `CloseAsync()` so the sheet can be controlled from C# code.

### SheetTrigger

| Name | Type | Default | Description |
| --- | --- | --- | --- |
| `ChildContent` | `RenderFragment` | — | The trigger content, rendered inside a `<span>` whose `onclick` calls `OpenAsync()`. |
| `Slot` | `RenderFragment<Dictionary<string, object?>>?` | `null` | Renders a custom element instead of the default `<span>`; the template receives the computed attributes (including `onclick` that opens the sheet). |

### SheetContent

| Name | Type | Default | Description |
| --- | --- | --- | --- |
| `ChildContent` | `RenderFragment` | — | The sheet body, rendered inside a portal (`SectionName="@PortalNames.Sheet"`) together with the overlay. |
| `Side` | `SheetSide` | `SheetSide.Right` | Which edge of the screen the sheet slides in from: `SheetSide.Top`, `SheetSide.Right`, `SheetSide.Bottom` or `SheetSide.Left`. |

### SheetOverlay

No `[Parameter]` properties; it reads the cascaded `Sheet` and renders a fixed, full-screen dimmed `div` (`data-slot="sheet-overlay"`, `data-state` of `open` / `closed`).

### SheetHeader

| Name | Type | Default | Description |
| --- | --- | --- | --- |
| `ChildContent` | `RenderFragment` | — | Header content, typically `SheetTitle` and `SheetDescription`. |

### SheetTitle

| Name | Type | Default | Description |
| --- | --- | --- | --- |
| `ChildContent` | `RenderFragment` | — | The title text, rendered in a `div` with `data-slot="sheet-title"` and `font-semibold` styling. |

### SheetDescription

| Name | Type | Default | Description |
| --- | --- | --- | --- |
| `ChildContent` | `RenderFragment` | — | The description text, rendered in a `div` with `data-slot="sheet-description"` and `text-muted-foreground text-sm` styling. |

### SheetFooter

| Name | Type | Default | Description |
| --- | --- | --- | --- |
| `ChildContent` | `RenderFragment` | — | Footer content, typically `SheetClose` and action buttons. |

### SheetClose

| Name | Type | Default | Description |
| --- | --- | --- | --- |
| `ChildContent` | `RenderFragment` | — | The close button content, rendered inside a `<span>` whose `onclick` calls the parent's `CloseAsync()`. |
| `Slot` | `RenderFragment<Dictionary<string, object?>>?` | `null` | Renders a custom element instead of the default `<span>`; the template receives the computed attributes (including `onclick` that closes the sheet). |

All components inherit from `ShadcnBase`, so the base parameters are also available: `ClassName` (`string`) is merged into the element's classes, and `Props` (`Dictionary<string, object>?`, captured from unmatched attributes) is merged into the rendered element.

## Examples

### Profile form sheet from the `SheetSc1` showcase

Adapted from the `SheetSc1` showcase: the trigger and the close button use the `Slot` template to render a real `Button` with all computed trigger attributes, and the content holds a small profile form.

```razor
@using ShadcnBlazor.Sheets
@using ShadcnBlazor.Buttons
@using ShadcnBlazor.Inputs
@using ShadcnBlazor.Labels

<Sheet>
    <SheetTrigger>
        <Slot>
            <Button Variant="ButtonVariant.Outline" @attributes="context">Open</Button>
        </Slot>
    </SheetTrigger>
    <SheetContent>
        <SheetHeader>
            <SheetTitle>Edit profile</SheetTitle>
            <SheetDescription>
                Make changes to your profile here. Click save when you&apos;re done.
            </SheetDescription>
        </SheetHeader>
        <div class="grid flex-1 auto-rows-min gap-6 px-4">
            <div class="grid gap-3">
                <Label for="sheet-demo-name">Name</Label>
                <TextInputField id="sheet-demo-name" @bind-Value="Name" />
            </div>
            <div class="grid gap-3">
                <Label for="sheet-demo-username">Username</Label>
                <TextInputField id="sheet-demo-username" @bind-Value="Username" />
            </div>
        </div>
        <SheetFooter>
            <Button type="submit">Save changes</Button>
            <SheetClose>
                <Slot>
                    <Button Variant="ButtonVariant.Outline" @attributes="context">Close</Button>
                </Slot>
            </SheetClose>
        </SheetFooter>
    </SheetContent>
</Sheet>

@code
{
    private string Name = "Pedro Duarte";
    private string Username = "@peduarte";
}
```

### Open the sheet from a different side

`Side` changes which edge the sheet slides in from. `SheetSide.Left` anchors it to the left edge with a right border; `Top` and `Bottom` render horizontal sheets with a top or bottom border.

```razor
@using ShadcnBlazor.Sheets
@using ShadcnBlazor.Buttons

<Sheet>
    <SheetTrigger>Open left sheet</SheetTrigger>
    <SheetContent Side="SheetSide.Left">
        <SheetHeader>
            <SheetTitle>Navigation</SheetTitle>
            <SheetDescription>This sheet slides in from the left edge.</SheetDescription>
        </SheetHeader>
        <SheetFooter>
            <SheetClose>Close</SheetClose>
        </SheetFooter>
    </SheetContent>
</Sheet>

<Sheet>
    <SheetTrigger>Open bottom sheet</SheetTrigger>
    <SheetContent Side="SheetSide.Bottom">
        <SheetHeader>
            <SheetTitle>Bottom panel</SheetTitle>
            <SheetDescription>This sheet slides up from the bottom edge.</SheetDescription>
        </SheetHeader>
        <SheetFooter>
            <SheetClose>Close</SheetClose>
        </SheetFooter>
    </SheetContent>
</Sheet>
```

### Open the sheet by default, programmatically and track state changes

`DefaultOpen` opens the sheet on first render. The exposed `OpenAsync()` / `CloseAsync()` methods allow full C# control of the sheet, and `OnOpenChange` reports state changes.

```razor
@using ShadcnBlazor.Sheets
@using ShadcnBlazor.Buttons

<Sheet DefaultOpen="true">
    <SheetContent>
        <SheetHeader>
            <SheetTitle>Started automatically</SheetTitle>
            <SheetDescription>This sheet was opened with DefaultOpen.</SheetDescription>
        </SheetHeader>
        <SheetFooter>
            <SheetClose>Close</SheetClose>
        </SheetFooter>
    </SheetContent>
</Sheet>

<Button @onclick="OpenSheet">Open programmatically</Button>
<Button @onclick="CloseSheet">Close it</Button>

<Sheet Ref="SheetRef" OnOpenChange="OnSheetOpenChange">
    <SheetTrigger>Open from trigger</SheetTrigger>
    <SheetContent Side="SheetSide.Right">
        <SheetHeader>
            <SheetTitle>Programmatic</SheetTitle>
            <SheetDescription>Opened or closed from code via OpenAsync() / CloseAsync().</SheetDescription>
        </SheetHeader>
        <SheetFooter>
            <SheetClose>Close</SheetClose>
        </SheetFooter>
    </SheetContent>
</Sheet>

@code
{
    private Sheet? SheetRef;

    private Task OpenSheet() => SheetRef.OpenAsync();

    private Task CloseSheet() => SheetRef.CloseAsync();

    private void OnSheetOpenChange(bool isOpen)
    {
        Console.WriteLine($"sheet is now {(isOpen ? "open" : "closed")}");
    }
}
```
