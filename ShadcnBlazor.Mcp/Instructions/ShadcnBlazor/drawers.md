---
title: Drawer
description: A sheet that slides in from a screen edge, with trigger, overlay, header, footer, title, description, and close button.
tags: [drawer, overlay, sheet, direction, trigger, close]
---

# Drawer

A drawer is a panel that slides in from one edge of the viewport — by default from the bottom (the mobile "sheet" pattern), but also from the top, left, or right via the `Direction` parameter. It is useful for secondary actions, filters, or a compact navigation panel that shouldn't permanently occupy screen space. The set is composed of building blocks: `DrawerTrigger` opens the drawer, `DrawerContent` renders the panel on top of a dimming `DrawerOverlay`, `DrawerHeader` / `DrawerFooter` / `DrawerTitle` / `DrawerDescription` structure the content, and `DrawerClose` dismisses it. State can be controlled from C# via `OpenAsync()` / `CloseAsync()` and the `DefaultOpen` / `OnOpenChanged` parameters, and the content is portalled so the panel overlays the rest of the page.

## Usage

```razor
<Drawer>
    <DrawerTrigger>Open drawer</DrawerTrigger>
    <DrawerContent>
        <DrawerHeader>
            <DrawerTitle>Drawer title</DrawerTitle>
            <DrawerDescription>This is a description of the drawer.</DrawerDescription>
        </DrawerHeader>
        <p>Drawer body content.</p>
        <DrawerFooter>
            <DrawerClose>Close</DrawerClose>
        </DrawerFooter>
    </DrawerContent>
</Drawer>
```

The drawer body is rendered through a portal, so the host must include the portal outlet (`<PortalOutlet PortalName="@PortalNames.Drawer" />`), which the ShadcnBlazor host setup already wires up.

## Parameters

### Drawer

| Name | Type | Default | Description |
| --- | --- | --- | --- |
| `ChildContent` | `RenderFragment` | — | The `DrawerTrigger`, `DrawerContent`, etc. composed inside the drawer. The `Drawer` instance is cascaded to all children. |
| `DefaultOpen` | `bool` | `false` | When `true`, the drawer is opened automatically on first render. |
| `OnOpenChanged` | `EventCallback` | — | Invoked with the new `IsOpen` value whenever the drawer opens or closes. |
| `Direction` | `DrawerDirection` | `DrawerDirection.Bottom` | Which screen edge the drawer slides in from. Values: `DrawerDirection.Top`, `DrawerDirection.Bottom`, `DrawerDirection.Left`, `DrawerDirection.Right`. |

The component also exposes a read-only `IsOpen` (`bool`) and the methods `OpenAsync()` / `CloseAsync()` so the drawer can be controlled from C# code.

### DrawerTrigger

| Name | Type | Default | Description |
| --- | --- | --- | --- |
| `ChildContent` | `RenderFragment` | — | The trigger content. Clicking the rendered element calls `OpenAsync()`. |
| `Slot` | `RenderFragment<Dictionary<string, object?>>?` | `null` | Renders a custom element instead of the default clickable `<span>`; the template receives the computed attributes (`onclick`). |

### DrawerContent

| Name | Type | Default | Description |
| --- | --- | --- | --- |
| `ChildContent` | `RenderFragment` | — | The drawer body, rendered inside a portal (`SectionName="@PortalNames.Drawer"`) together with the overlay. |

### DrawerOverlay

| Name | Type | Default | Description |
| --- | --- | --- | --- |
| (no own `[Parameter]` properties) | — | — | Full-screen dimming overlay (`data-slot="drawer-overlay"`). Clicking it calls `CloseAsync()`. |

### DrawerHeader

| Name | Type | Default | Description |
| --- | --- | --- | --- |
| `ChildContent` | `RenderFragment` | — | Header content, typically `DrawerTitle` and `DrawerDescription`. |

### DrawerFooter

| Name | Type | Default | Description |
| --- | --- | --- | --- |
| `ChildContent` | `RenderFragment` | — | Footer content, typically a `DrawerClose` button. |

### DrawerTitle

| Name | Type | Default | Description |
| --- | --- | --- | --- |
| `ChildContent` | `RenderFragment` | — | The title text, rendered in a `div` with `data-slot="drawer-title"`. |

### DrawerDescription

| Name | Type | Default | Description |
| --- | --- | --- | --- |
| `ChildContent` | `RenderFragment` | — | The description text, rendered in a `div` with `data-slot="drawer-description"`. |

### DrawerClose

| Name | Type | Default | Description |
| --- | --- | --- | --- |
| `ChildContent` | `RenderFragment` | — | The close button content. Clicking the rendered element calls `CloseAsync()`. |
| `Slot` | `RenderFragment<Dictionary<string, object?>>?` | `null` | Renders a custom element instead of the default clickable `<span>`; the template receives the computed attributes (`onclick`). |

`DrawerContent`, `DrawerOverlay`, `DrawerHeader`, `DrawerFooter`, `DrawerTitle`, and `DrawerDescription` inherit from `ShadcnBase`, so the base parameters are also available: `ClassName` (`string`) is merged into the element's classes, and `Props` (`Dictionary<string, object>?`, captured from unmatched attributes) is merged into the rendered element.

## Examples

### Side drawer sliding in from the right

Set `Direction` to change which edge the drawer enters from. `Left`, `Right`, and `Top` are available alongside the default `Bottom`.

```razor
<Drawer Direction="DrawerDirection.Right">
    <DrawerTrigger>Filters</DrawerTrigger>
    <DrawerContent>
        <DrawerHeader>
            <DrawerTitle>Filters</DrawerTitle>
            <DrawerDescription>Narrow the list with the filters below.</DrawerDescription>
        </DrawerHeader>
        <input class="border rounded p-2" placeholder="Search..." />
        <DrawerFooter>
            <DrawerClose>Done</DrawerClose>
        </DrawerFooter>
    </DrawerContent>
</Drawer>
```

### React to open/close changes with OnOpenChanged

`Drawer.OnOpenChanged` reports the new state, so side effects such as analytics or focus management can be performed there.

```razor
<Drawer OnOpenChanged="@OnDrawerChanged">
    <DrawerTrigger>Open drawer</DrawerTrigger>
    <DrawerContent>
        <DrawerHeader>
            <DrawerTitle>Event example</DrawerTitle>
            <DrawerDescription>OnOpenChanged fires with the new state.</DrawerDescription>
        </DrawerHeader>
        <DrawerFooter>
            <DrawerClose>Close</DrawerClose>
        </DrawerFooter>
    </DrawerContent>
</Drawer>

@code
{
    private void OnDrawerChanged(bool isOpen)
    {
        Console.WriteLine($"drawer is now {(isOpen ? "open" : "closed")}");
    }
}
```

### Open by default or programmatically

`DefaultOpen` opens the drawer on first render. The exposed `OpenAsync()` / `CloseAsync()` methods allow full C# control of the drawer.

```razor
<Drawer DefaultOpen="true">
    <DrawerTrigger>Open</DrawerTrigger>
    <DrawerContent>
        <DrawerHeader>
            <DrawerTitle>Started automatically</DrawerTitle>
            <DrawerDescription>This drawer was opened with DefaultOpen.</DrawerDescription>
        </DrawerHeader>
        <DrawerFooter>
            <DrawerClose>Close</DrawerClose>
        </DrawerFooter>
    </DrawerContent>
</Drawer>

<Button @onclick="OpenDrawer">Open programmatically</Button>

<Drawer Ref="DrawerRef">
    <DrawerTrigger>Trigger</DrawerTrigger>
    <DrawerContent>
        <DrawerHeader>
            <DrawerTitle>Programmatic</DrawerTitle>
            <DrawerDescription>Opened from code via OpenAsync().</DrawerDescription>
        </DrawerHeader>
        <DrawerFooter>
            <DrawerClose>Close</DrawerClose>
        </DrawerFooter>
    </DrawerContent>
</Drawer>

@code
{
    private Drawer? DrawerRef;
    private Task OpenDrawer() => DrawerRef.OpenAsync();
}
```

### Custom trigger element with the Slot template

`Slot` renders a custom element (for example an anchor or a card) with all the trigger's computed attributes.

```razor
<Drawer>
    <DrawerTrigger>
        <Slot Context="attrs">
            <a href="javascript:void(0)" @attributes="attrs">
                Click the card to open the drawer
            </a>
        </Slot>
    </DrawerTrigger>
    <DrawerContent>
        <DrawerHeader>
            <DrawerTitle>Custom trigger</DrawerTitle>
            <DrawerDescription>The anchor received the drawer trigger attributes.</DrawerDescription>
        </DrawerHeader>
        <DrawerFooter>
            <DrawerClose>Close</DrawerClose>
        </DrawerFooter>
    </DrawerContent>
</Drawer>
```
