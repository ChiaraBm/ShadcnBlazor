---
title: Navigation Menu
description: Navigation menu with hover-triggered dropdown items, portaled content panels and animated indicators.
tags: [navigation-menu, menu, hover, dropdown, portal, navigation]
---

# Navigation Menu

A composable set of components for building a primary navigation bar where items open a dropdown panel on hover. `<NavigationMenu>` is the root: it tracks the items, debounces open/close with `OpenDelay`, `CloseDelay` and `SkipDelay`, and switches between items with a slide motion. `<NavigationMenuItem>` holds a `<NavigationMenuTrigger>` (a button that auto-appends a rotating chevron) and an optional `<NavigationMenuContent>`; the content is rendered through a portal, positioned by `PositionService` below the trigger, and is kept open while hovered. `<NavigationMenuLink>` renders an `<a>` that closes the open item when clicked, and `<NavigationMenuIndicator>` is the small animated arrow under an open trigger. Because the content is portaled, the hosting layout must contain a `<PortalOutlet />`.

## Usage

```razor
@using ShadcnBlazor.NavigationMenus

<NavigationMenu>
    <NavigationMenuList>
        <NavigationMenuItem>
            <NavigationMenuTrigger>Getting started</NavigationMenuTrigger>
            <NavigationMenuContent ClassName="w-56">
                <NavigationMenuLink href="getting-started">Announcements</NavigationMenuLink>
                <NavigationMenuLink href="docs">Documentation</NavigationMenuLink>
                <NavigationMenuLink href="help">Help</NavigationMenuLink>
            </NavigationMenuContent>
        </NavigationMenuItem>
        <NavigationMenuLink href="blog">Blog</NavigationMenuLink>
    </NavigationMenuList>
</NavigationMenu>
```

## Parameters

| Name | Type | Default | Description |
|---|---|---|---|
| `ChildContent` (NavigationMenu) | `RenderFragment` | `null` | The root content, normally a `<NavigationMenuList>`. Cascades the menu so items can register and the triggers/content can open and close them. |
| `OpenDelay` (NavigationMenu) | `int` | `150` | Debounce in milliseconds after the pointer enters a trigger before the item's content opens. |
| `CloseDelay` (NavigationMenu) | `int` | `150` | Debounce in milliseconds after the pointer leaves both the trigger and the content before the item closes. |
| `SkipDelay` (NavigationMenu) | `int` | `300` | Time window in milliseconds after an open in which opening another item skips the `OpenDelay` debounce entirely (instant switch). |
| `ChildContent` (NavigationMenuList) | `RenderFragment` | `null` | The `<ul>` content, normally `<NavigationMenuItem>` elements and bare links. |
| `ChildContent` (NavigationMenuItem) | `RenderFragment` | `null` | The item content: a `<NavigationMenuTrigger>` plus an optional `<NavigationMenuContent>`. Cascades the item so the trigger and content can talk to it; while open, a `<NavigationMenuIndicator>` is rendered with `DataState="visible"`. |
| `ChildContent` (NavigationMenuTrigger) | `RenderFragment` | `null` | The trigger button's label. A `ChevronDownIcon` is appended automatically and rotates while the item is open; hover and click events open/close the item. |
| `ChildContent` (NavigationMenuContent) | `RenderFragment` | `null` | The panel content, rendered in the `NavigationMenu` portal and positioned below the trigger. Set the panel width via `ClassName` (e.g. `w-56` or `grid grid-cols-2 w-96`). |
| `ChildContent` (NavigationMenuLink) | `RenderFragment` | `null` | The link label, rendered inside the default `<a>`. Clicking the link closes the open item. |
| `Slot` (NavigationMenuLink) | `RenderFragment<Dictionary<string, object?>>?` | `null` | When set, the link's element attributes (classes, `data-slot="navigation-menu-link"`, close-on-click `onclick`, `data-active` when the item is open) are passed to this template. Render your own element (e.g. a Blazor `<NavLink>`) and apply them with `@attributes="context"`. |
| `DataState` (NavigationMenuIndicator) | `string` | `null` | `"visible"` or `"hidden"`; controls the indicator's `data-state`, which drives its `animate-in`/`animate-out` fade. |
| `ChildContent` (NavigationMenuIndicator) | `RenderFragment?` | `null` | Optional content rendered above the indicator's arrow; the small rotated square arrow is always rendered. |

`NavigationMenuItem`, `NavigationMenuTrigger` and `NavigationMenuContent` receive their menu context through `[CascadingParameter]` (`Parent`) — they must be nested as shown above and are not passed directly. All components inherit from `ShadcnBase`, so the base parameters are available on them too: `ClassName` (a `string`) is merged into the element's Tailwind classes, and `Props` (a `Dictionary<string, object>?` with `CaptureUnmatchedValues = true`) collects any unmatched attributes and spreads them onto the rendered element.

## Examples

A full menu with a hover-open dropdown and a plain top-level link, following the real component structure:

```razor
@using ShadcnBlazor.NavigationMenus

<NavigationMenu>
    <NavigationMenuList>
        <NavigationMenuItem>
            <NavigationMenuTrigger>Getting started</NavigationMenuTrigger>
            <NavigationMenuContent ClassName="w-56">
                <NavigationMenuLink href="announcements">
                    Announcements
                </NavigationMenuLink>
                <NavigationMenuLink href="docs">
                    Documentation
                </NavigationMenuLink>
                <NavigationMenuLink href="help">
                    Help
                </NavigationMenuLink>
            </NavigationMenuContent>
        </NavigationMenuItem>
        <NavigationMenuItem>
            <NavigationMenuTrigger>Components</NavigationMenuTrigger>
            <NavigationMenuContent ClassName="grid w-96 grid-cols-2">
                <NavigationMenuLink href="buttons">Buttons</NavigationMenuLink>
                <NavigationMenuLink href="inputs">Inputs</NavigationMenuLink>
                <NavigationMenuLink href="dialog">Dialog</NavigationMenuLink>
                <NavigationMenuLink href="dropdown">Dropdown</NavigationMenuLink>
            </NavigationMenuContent>
        </NavigationMenuItem>
        <NavigationMenuLink href="blog">Blog</NavigationMenuLink>
    </NavigationMenuList>
</NavigationMenu>
```

Routing links via `Slot` so a Blazor `<NavLink>` carries the link's classes, active state and close-on-click handler:

```razor
@using ShadcnBlazor.NavigationMenus

<NavigationMenu>
    <NavigationMenuList>
        <NavigationMenuItem>
            <NavigationMenuTrigger>Docs</NavigationMenuTrigger>
            <NavigationMenuContent ClassName="w-56">
                <NavigationMenuLink>
                    <Slot>
                        <NavLink href="getting-started" class="flex items-center gap-2 rounded-lg p-2 text-sm" @attributes="context">
                            Getting started
                        </NavLink>
                    </Slot>
                </NavigationMenuLink>
                <NavigationMenuLink>
                    <Slot>
                        <NavLink href="api" class="flex items-center gap-2 rounded-lg p-2 text-sm" @attributes="context">
                            API reference
                        </NavLink>
                    </Slot>
                </NavigationMenuLink>
            </NavigationMenuContent>
        </NavigationMenuItem>
    </NavigationMenuList>
</NavigationMenu>
```

Tuned hover debouncing with the root's `OpenDelay`, `CloseDelay` and `SkipDelay` parameters, so the menu opens slowly, lingers after the pointer leaves, and switches between items instantly:

```razor
@using ShadcnBlazor.NavigationMenus

<NavigationMenu OpenDelay="200" CloseDelay="500" SkipDelay="600">
    <NavigationMenuList>
        <NavigationMenuItem>
            <NavigationMenuTrigger>Products</NavigationMenuTrigger>
            <NavigationMenuContent ClassName="w-64">
                <NavigationMenuLink href="product-a">Product A</NavigationMenuLink>
                <NavigationMenuLink href="product-b">Product B</NavigationMenuLink>
            </NavigationMenuContent>
        </NavigationMenuItem>
        <NavigationMenuItem>
            <NavigationMenuTrigger>About</NavigationMenuTrigger>
            <NavigationMenuContent ClassName="w-64">
                <NavigationMenuLink href="team">Team</NavigationMenuLink>
                <NavigationMenuLink href="contact">Contact</NavigationMenuLink>
            </NavigationMenuContent>
        </NavigationMenuItem>
    </NavigationMenuList>
</NavigationMenu>
```

Combined usage: `ClassName` styling on the trigger and panel, a wide panel with a mix of links, and a bare top-level link that does not need a dropdown. When the first item is opened, its `<NavigationMenuIndicator>` fades in automatically below the trigger:

```razor
@using ShadcnBlazor.NavigationMenus

<NavigationMenu>
    <NavigationMenuList>
        <NavigationMenuItem>
            <NavigationMenuTrigger ClassName="font-semibold">Resources</NavigationMenuTrigger>
            <NavigationMenuContent ClassName="w-72">
                <NavigationMenuLink href="docs">
                    Documentation
                </NavigationMenuLink>
                <NavigationMenuLink href="api">
                    API reference
                </NavigationMenuLink>
                <NavigationMenuLink href="changelog">
                    Changelog
                </NavigationMenuLink>
            </NavigationMenuContent>
        </NavigationMenuItem>
        <NavigationMenuLink href="github" href-target="_blank">GitHub</NavigationMenuLink>
    </NavigationMenuList>
</NavigationMenu>
```
