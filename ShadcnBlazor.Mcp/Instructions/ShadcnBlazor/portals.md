---
title: Portal Outlet
description: Host-side outlet that renders floating overlays (dialogs, menus, tooltips, etc.) by receiving the named Blazor sections they are portaled into.
tags: [portal, outlet, overlay, sections, render]
---

# Portal Outlet

`PortalOutlet` is the single host-side component that materializes all floating overlays in ShadcnBlazor. Components such as `Dialog`, `DropdownMenu`, `Tooltip`, `Sheet` and others render their content into named Blazor `SectionOutlet` sections; `PortalOutlet` is the one element that actually draws those sections on the page. Without it in your layout, overlays would never appear. It exposes no parameters and no state — just place `<PortalOutlet />` once in your layout (typically at the bottom of `MainLayout`) and every overlay in the app is rendered through it.

## Usage

```razor
<!-- MainLayout.razor -->
<PortalOutlet />
```

Register the services in `Program.cs`:

```csharp
builder.Services.AddShadcnBlazor();
```

## Parameters

`PortalOutlet` declares no `[Parameter]` properties. It renders a fixed set of `SectionOutlet` elements for the overlay sections `PortalNames` defines (`AlertDialog`, `ContextMenu`, `ContextMenuSub`, `Dialog`, `Drawer`, `DropdownMenu`, `DropdownMenuSub`, `HoverCard`, `Menubar`, `MenubarSub`, `NavigationMenu`, `Popover`, `Select`, `Sheet`, `Tooltip`). The only attribute it accepts is the standard unmatched-attribute capture of its base class; no named parameter is required or available.

## Examples

### Minimal overlay host

A layout containing `<PortalOutlet />` is sufficient for every dialog, menu, popover and tooltip in the app to render.

```razor
@* MainLayout.razor *@
<main>
    @Body
</main>
<PortalOutlet />
```

### Overlay content rendered through the outlet

Once the outlet is present, overlay components portaled into it appear on top of the page regardless of where the trigger lives.

```razor
@using ShadcnBlazor.Dialogs

<Dialog>
    <DialogTrigger>Open</DialogTrigger>
    <DialogContent>
        <DialogHeader>
            <DialogTitle>Rendered via PortalOutlet</DialogTitle>
        </DialogHeader>
        <DialogDescription>This dialog is drawn by the SectionOutlet at the root.</DialogDescription>
        <DialogFooter>
            <DialogClose>Close</DialogClose>
        </DialogFooter>
    </DialogContent>
</Dialog>
```

### Multiple overlays, one outlet

A single `<PortalOutlet />` services all overlay families at once; there is no need to add an outlet per component.

```razor
@using ShadcnBlazor.Dialogs
@using ShadcnBlazor.Dropdowns

<Dialog>
    <DialogTrigger>Dialog</DialogTrigger>
    <DialogContent>
        <DialogHeader><DialogTitle>Dialog overlay</DialogTitle></DialogHeader>
        <DialogFooter><DialogClose>Close</DialogClose></DialogFooter>
    </DialogContent>
</Dialog>

<DropdownMenu>
    <DropdownMenuTrigger>Menu</DropdownMenuTrigger>
    <DropdownMenuContent>
        <DropdownMenuItem>Item</DropdownMenuItem>
    </DropdownMenuContent>
</DropdownMenu>

<PortalOutlet />
```
