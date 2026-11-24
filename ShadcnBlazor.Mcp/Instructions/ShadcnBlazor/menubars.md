---
title: Menubar
description: Desktop-style menu bar whose top-level menus open dropdowns of items, shortcuts, checkbox/radio items and submenus.
tags: [menubar, menu, menu-bar, submenu, checkbox, radio]
---

# Menubar

The Menubar is a composable set of components that renders a horizontal, desktop-style application menu bar. Instead of a single anchored menu, it hosts several top-level `<MenubarMenu>` elements (each with a `<MenubarTrigger>` label such as *File*, *Edit* or *View*) that open a `<MenubarContent>` dropdown below the trigger. The bar itself is a static horizontal strip; once any menu is open a full-viewport backdrop renders behind the bar and closes everything when clicked. Inside each dropdown you compose `<MenubarItem>`, `<MenubarLabel>`, `<MenubarSeparator>`, `<MenubarShortcut>`, `<MenubarCheckboxItem>`, a `<MenubarRadioGroup>` of `<MenubarRadioItem>`s, and nested `<MenubarSub>` submenus. Because menu and submenu content is rendered through a portal, the hosting layout must contain a `<PortalOutlet />`.

## Usage

```razor
@using ShadcnBlazor.Menubars

<Menubar>
    <MenubarMenu>
        <MenubarTrigger>File</MenubarTrigger>
        <MenubarContent>
            <MenubarItem OnClick="() => Console.WriteLine('New Tab')">
                New Tab
                <MenubarShortcut>⌘T</MenubarShortcut>
            </MenubarItem>
            <MenubarSeparator />
            <MenubarItem Variant="MenubarItemVariant.Destructive" OnClick="() => Console.WriteLine('Close Tab')">
                Close Tab
                <MenubarShortcut>⌘W</MenubarShortcut>
            </MenubarItem>
        </MenubarContent>
    </MenubarMenu>
</Menubar>
```

## Parameters

| Name | Type | Default | Description |
|---|---|---|---|
| `ChildContent` (Menubar) | `RenderFragment` | `null` | The root content of the bar, normally one or more `<MenubarMenu>` elements. Cascades the bar so each menu can open it and the outside-click backdrop can close it. |
| `ChildContent` (MenubarMenu) | `RenderFragment` | `null` | Normally one `<MenubarTrigger>` and one `<MenubarContent>`. Represents a single top-level menu and manages its open/focus state. |
| `ChildContent` (MenubarTrigger) | `RenderFragment` | `null` | The top-level label (e.g. *File*, *Edit*). Clicking, hovering or focusing it opens the menu. |
| `ChildContent` (MenubarContent) | `RenderFragment` | `null` | The menu items (items, labels, separators, checkbox/radio items, submenus). Rendered in the `Menubar` portal below the trigger, with a full-viewport backdrop that closes the bar on click. |
| `Side` (MenubarContent) | `PositionSide` | `PositionSide.Bottom` | The side of the trigger the dropdown opens to: `PositionSide.Right`, `PositionSide.Left`, `PositionSide.Bottom` or `PositionSide.Top`. |
| `Align` (MenubarContent) | `PositionAlignment` | `PositionAlignment.Start` | How the dropdown is aligned to the trigger: `PositionAlignment.Start`, `PositionAlignment.Center` or `PositionAlignment.End`. |
| `SideOffset` (MenubarContent) | `double` | `8` | Distance in pixels between the dropdown and the trigger edge on `Side`. |
| `ChildContent` (MenubarItem) | `RenderFragment` | `null` | The item's inner content, typically a label and optionally a `<MenubarShortcut>`. |
| `Inset` (MenubarItem) | `bool` | `false` | Indents the item with extra left padding. |
| `Disabled` (MenubarItem) | `bool` | `false` | Makes the item unclickable and dims it. |
| `Variant` (MenubarItem) | `MenubarItemVariant` | `MenubarItemVariant.Default` | `MenubarItemVariant.Default` for a normal item, `MenubarItemVariant.Destructive` for red-styled destructive actions. |
| `OnClick` (MenubarItem) | `EventCallback` | `default` | Invoked when the item is clicked. The menu is closed automatically after the callback. |
| `ChildContent` (MenubarLabel) | `RenderFragment` | `null` | The label text, rendered as a non-interactive section heading. |
| `Inset` (MenubarLabel) | `bool` | `false` | Indents the label with extra left padding. |
| `ChildContent` (MenubarSeparator) | `RenderFragment` | `null` | Optional content inside the horizontal divider line; normally left empty. |
| `Inset` (MenubarSeparator) | `bool` | `false` | Indents the divider with extra left padding. |
| `ChildContent` (MenubarShortcut) | `RenderFragment` | `null` | The keyboard-shortcut text, rendered right-aligned next to the item text (e.g. `Ctrl+C`). |
| `Inset` (MenubarShortcut) | `bool` | `false` | Indents the shortcut with extra left padding. |
| `ChildContent` (MenubarCheckboxItem) | `RenderFragment` | `null` | The item's text. A check icon is rendered automatically while `Value` is `true`. |
| `Value` (MenubarCheckboxItem) | `bool` | `false` | The checked state of the item. |
| `ValueChanged` (MenubarCheckboxItem) | `EventCallback<bool>` | `default` | Invoked with the new value every time the item is toggled. |
| `Disabled` (MenubarCheckboxItem) | `bool` | `false` | Makes the item unclickable and dims it. |
| `CloseOnClick` (MenubarCheckboxItem) | `bool` | `true` | Whether the menu closes after the item is toggled. |
| `DefaultValue` (MenubarCheckboxItem) | `bool?` | `null` | Initial `Value` applied on initialization when `Value` is not set. |
| `ChildContent` (MenubarRadioGroup) | `RenderFragment` | `null` | The `<MenubarRadioItem>` entries of the group. |
| `Value` (MenubarRadioGroup) | `string` | `null` | The value of the currently selected radio item. |
| `ValueChanged` (MenubarRadioGroup) | `EventCallback<string>` | `default` | Invoked with the newly selected value. |
| `CloseOnClick` (MenubarRadioGroup) | `bool` | `true` | Whether the menu closes after a radio item is selected. |
| `DefaultValue` (MenubarRadioGroup) | `string?` | `null` | Initial `Value` applied on initialization when `Value` is not set. |
| `ChildContent` (MenubarRadioItem) | `RenderFragment` | `null` | The item's text. |
| `Value` (MenubarRadioItem) | `string` | `null` | The value this radio item represents. The item is highlighted with a dot icon while it matches the group's `Value`. |
| `Disabled` (MenubarRadioItem) | `bool` | `false` | Makes the item unclickable and dims it. |
| `ChildContent` (MenubarSub) | `RenderFragment` | `null` | Normally one `<MenubarSubTrigger>` and one `<MenubarSubContent>`. The submenu opens on hover/focus of the trigger. |
| `ChildContent` (MenubarSubTrigger) | `RenderFragment` | `null` | The submenu trigger's label; a chevron icon is appended automatically. |
| `Inset` (MenubarSubTrigger) | `bool` | `false` | Indents the trigger with extra left padding. |
| `ChildContent` (MenubarSubContent) | `RenderFragment` | `null` | The submenu's items, rendered in the `MenubarSub` portal to the right of the trigger. |

Every component in this folder inherits from `ShadcnBase`, so the base parameters are also available on each one. `ClassName` (a `string`) is merged into the element's Tailwind classes, and `Props` (a `Dictionary<string, object>?` with `CaptureUnmatchedValues = true`) collects any unmatched attributes and spreads them onto the rendered element.

## Examples

A full desktop-style bar with **File**, **Edit**, **View** and **Profiles** top-level menus, combining items, shortcuts, a disabled item, a nested `<MenubarSub>` submenu, checkbox items and a radio group (adapted from the repo's `MenubarSc1` showcase):

```razor
@using ShadcnBlazor.Menubars

<Menubar>
    <MenubarMenu>
        <MenubarTrigger>File</MenubarTrigger>
        <MenubarContent>
            <MenubarItem>
                New Tab
                <MenubarShortcut>⌘T</MenubarShortcut>
            </MenubarItem>
            <MenubarItem>
                New Window
                <MenubarShortcut>⌘N</MenubarShortcut>
            </MenubarItem>
            <MenubarItem Disabled="true">New Incognito Window</MenubarItem>
            <MenubarSeparator />
            <MenubarSub>
                <MenubarSubTrigger>Share</MenubarSubTrigger>
                <MenubarSubContent>
                    <MenubarItem>Email link</MenubarItem>
                    <MenubarItem>Messages</MenubarItem>
                    <MenubarItem>Notes</MenubarItem>
                </MenubarSubContent>
            </MenubarSub>
            <MenubarSeparator />
            <MenubarItem>
                Print...
                <MenubarShortcut>⌘P</MenubarShortcut>
            </MenubarItem>
        </MenubarContent>
    </MenubarMenu>
    <MenubarMenu>
        <MenubarTrigger>View</MenubarTrigger>
        <MenubarContent>
            <MenubarCheckboxItem>Always Show Bookmarks Bar</MenubarCheckboxItem>
            <MenubarCheckboxItem DefaultValue="true">Always Show Full URLs</MenubarCheckboxItem>
            <MenubarSeparator />
            <MenubarItem Inset="true">
                Reload
                <MenubarShortcut>⌘R</MenubarShortcut>
            </MenubarItem>
            <MenubarItem Disabled="true" Inset="true">
                Force Reload
                <MenubarShortcut>⇧⌘R</MenubarShortcut>
            </MenubarItem>
            <MenubarSeparator />
            <MenubarItem Inset="true">Toggle Fullscreen</MenubarItem>
        </MenubarContent>
    </MenubarMenu>
    <MenubarMenu>
        <MenubarTrigger>Profiles</MenubarTrigger>
        <MenubarContent>
            <MenubarRadioGroup @bind-Value="Profile">
                <MenubarRadioItem Value="andy">Andy</MenubarRadioItem>
                <MenubarRadioItem Value="benoit">Benoit</MenubarRadioItem>
                <MenubarRadioItem Value="Luis">Luis</MenubarRadioItem>
            </MenubarRadioGroup>
            <MenubarSeparator />
            <MenubarItem Inset="true">Edit...</MenubarItem>
        </MenubarContent>
    </MenubarMenu>
</Menubar>

@code
{
    private string Profile = "benoit";
}
```

Checkbox and radio menus that stay open after selection (`CloseOnClick="false"`), with two-way `Value` binding and a `<MenubarLabel>` section:

```razor
@using ShadcnBlazor.Menubars

<Menubar>
    <MenubarMenu>
        <MenubarTrigger>Layout</MenubarTrigger>
        <MenubarContent>
            <MenubarLabel>Options</MenubarLabel>
            <MenubarCheckboxItem Value="@ShowToolbar" ValueChanged="@(value => ShowToolbar = value)" CloseOnClick="false">
                Show Toolbar
            </MenubarCheckboxItem>
            <MenubarCheckboxItem Value="@ShowGrid" ValueChanged="@(value => ShowGrid = value)" CloseOnClick="false">
                Show Grid
            </MenubarCheckboxItem>
            <MenubarSeparator />
            <MenubarLabel>Orientation</MenubarLabel>
            <MenubarRadioGroup Value="@Orientation" ValueChanged="@(value => Orientation = value)" CloseOnClick="false">
                <MenubarRadioItem Value="landscape">Landscape</MenubarRadioItem>
                <MenubarRadioItem Value="portrait">Portrait</MenubarRadioItem>
            </MenubarRadioGroup>
        </MenubarContent>
    </MenubarMenu>
</Menubar>

@code
{
    private bool ShowToolbar = true;
    private bool ShowGrid = false;
    private string Orientation = "landscape";
}
```

Action menu using the destructive variant, `Inset` items and a nested submenu, where every action is driven by the item's `OnClick` callback and the menu closes automatically after the callback:

```razor
@using ShadcnBlazor.Menubars

<Menubar>
    <MenubarMenu>
        <MenubarTrigger>Workspace</MenubarTrigger>
        <MenubarContent>
            <MenubarItem OnClick="() => Console.WriteLine('new')">
                New Workspace
                <MenubarShortcut>⌘N</MenubarShortcut>
            </MenubarItem>
            <MenubarItem Inset OnClick="() => Console.WriteLine('duplicate')">Duplicate</MenubarItem>
            <MenubarSeparator />
            <MenubarSub>
                <MenubarSubTrigger>Move to</MenubarSubTrigger>
                <MenubarSubContent>
                    <MenubarItem>Inbox</MenubarItem>
                    <MenubarItem>Archive</MenubarItem>
                </MenubarSubContent>
            </MenubarSub>
            <MenubarSeparator />
            <MenubarItem Variant="MenubarItemVariant.Destructive" OnClick="() => Console.WriteLine('delete')">
                Delete Workspace
                <MenubarShortcut>⌘⌫</MenubarShortcut>
            </MenubarItem>
        </MenubarContent>
    </MenubarMenu>
</Menubar>
```
