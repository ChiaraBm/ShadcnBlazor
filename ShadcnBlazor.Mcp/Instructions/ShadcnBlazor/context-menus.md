---
title: Context Menu
description: Right-click context menu with items, shortcuts, checkbox/radio items, labels, separators and submenus.
tags: [context-menu, right-click, menu, submenu, checkbox, radio]
---

# Context Menu

A composable set of components for rendering a right-click context menu that opens at the exact cursor position. Use it for actions that are specific to an element under the pointer — file managers, canvas editors, tables, media libraries — and that do not fit a standard dropdown. `<ContextMenuTrigger>` captures the `contextmenu` event (on its default `<div>`, or on any existing element through its `Slot` template) and hands the click coordinates to the menu root; `<ContextMenuContent>` then renders the items in a portal next to the pointer, with `<ContextMenuItem>`, `<ContextMenuLabel>`, `<ContextMenuSeparator>`, `<ContextMenuShortcut>`, `<ContextMenuCheckboxItem>`, `<ContextMenuRadioGroup>` and nested `<ContextMenuSub>` submenus available as building blocks. Because the menu content is rendered through a portal, the hosting layout must contain a `<PortalOutlet />`.

## Usage

```razor
@using ShadcnBlazor.ContextMenus

<ContextMenu>
    <ContextMenuTrigger ClassName="flex h-32 w-full items-center justify-center rounded-md border border-dashed text-sm">
        Right click to open the menu
    </ContextMenuTrigger>
    <ContextMenuContent>
        <ContextMenuItem OnClick="() => Console.WriteLine('Copied')">Copy</ContextMenuItem>
        <ContextMenuSeparator />
        <ContextMenuItem Variant="ContextMenuItemVariant.Destructive" OnClick="() => Console.WriteLine('Deleted')">Delete</ContextMenuItem>
    </ContextMenuContent>
</ContextMenu>
```

## Parameters

| Name | Type | Default | Description |
|---|---|---|---|
| `ChildContent` (ContextMenu) | `RenderFragment` | `null` | The root content of the menu, normally one `<ContextMenuTrigger>` and one `<ContextMenuContent>`. Cascades the menu state so the trigger can open it and the items can close it. |
| `ChildContent` (ContextMenuTrigger) | `RenderFragment` | `null` | The inner content of the default `<div>` trigger element that captures right-clicks. |
| `Slot` (ContextMenuTrigger) | `RenderFragment?` | `null` | When set, the `contextmenu` event is attached to a `<span>` wrapping this template instead of the default `<div>`. Render your own element inside the template and apply the template attributes with `@attributes="context"`. |
| `ClassName` (ContextMenuTrigger) | `string?` | `null` | Classes applied to the trigger's default `<div>` element. |
| `ChildContent` (ContextMenuContent) | `RenderFragment` | `null` | The menu items (items, labels, separators, checkbox/radio items, submenus). Rendered in the `ContextMenu` portal at the click coordinates, with a full-viewport backdrop that closes the menu on click. |
| `ChildContent` (ContextMenuItem) | `RenderFragment` | `null` | The item's inner content, typically an icon, a label and optionally a `<ContextMenuShortcut>`. |
| `Variant` (ContextMenuItem) | `ContextMenuItemVariant` | `ContextMenuItemVariant.Default` | `ContextMenuItemVariant.Default` for a normal item, `ContextMenuItemVariant.Destructive` for red-styled destructive actions. |
| `Inset` (ContextMenuItem) | `bool` | `false` | Indents the item with extra left padding. |
| `Disabled` (ContextMenuItem) | `bool` | `false` | Makes the item unclickable and dims it. |
| `OnClick` (ContextMenuItem) | `EventCallback` | `default` | Invoked when the item is clicked. The menu is closed automatically after the callback. |
| `ChildContent` (ContextMenuLabel) | `RenderFragment` | `null` | The label text, rendered as a non-interactive section heading. |
| `Inset` (ContextMenuLabel) | `bool` | `false` | Indents the label with extra left padding. |
| `ChildContent` (ContextMenuSeparator) | `RenderFragment` | `null` | Optional content inside the horizontal divider line; normally left empty. |
| `ChildContent` (ContextMenuShortcut) | `RenderFragment` | `null` | The keyboard-shortcut text, rendered right-aligned next to the item text (e.g. `Ctrl+C`). |
| `ChildContent` (ContextMenuCheckboxItem) | `RenderFragment` | `null` | The item's text. A check icon is rendered automatically while `Value` is `true`. |
| `Value` (ContextMenuCheckboxItem) | `bool` | `false` | The checked state of the item. |
| `ValueChanged` (ContextMenuCheckboxItem) | `EventCallback<bool>` | `default` | Invoked with the new value every time the item is toggled. |
| `Disabled` (ContextMenuCheckboxItem) | `bool` | `false` | Makes the item unclickable and dims it. |
| `CloseOnClick` (ContextMenuCheckboxItem) | `bool` | `true` | Whether the menu closes after the item is toggled. |
| `ChildContent` (ContextMenuRadioGroup) | `RenderFragment` | `null` | The `<ContextMenuRadioItem>` entries of the group. |
| `Value` (ContextMenuRadioGroup) | `string` | `null` | The value of the currently selected radio item. |
| `ValueChanged` (ContextMenuRadioGroup) | `EventCallback<string>` | `default` | Invoked with the newly selected value. |
| `CloseOnClick` (ContextMenuRadioGroup) | `bool` | `true` | Whether the menu closes after a radio item is selected. |
| `Value` (ContextMenuRadioItem) | `string` | `null` | The value this radio item represents. The item is highlighted with a dot icon while it matches the group's `Value`. |
| `Disabled` (ContextMenuRadioItem) | `bool` | `false` | Makes the item unclickable and dims it. |
| `ChildContent` (ContextMenuRadioItem) | `RenderFragment` | `null` | The item's text. |
| `ChildContent` (ContextMenuSub) | `RenderFragment` | `null` | Normally one `<ContextMenuSubTrigger>` and one `<ContextMenuSubContent>`. The submenu opens on hover/focus of the trigger. |
| `ChildContent` (ContextMenuSubTrigger) | `RenderFragment` | `null` | The submenu trigger's label; a chevron icon is appended automatically. |
| `Inset` (ContextMenuSubTrigger) | `bool` | `false` | Indents the trigger with extra left padding. |
| `ChildContent` (ContextMenuSubContent) | `RenderFragment` | `null` | The submenu's items, rendered in the `ContextMenuSub` portal to the right of the trigger. |

Every component in this folder inherits from `ShadcnBase`, so the base parameters are also available on each one. `ClassName` (a `string`) is merged into the element's Tailwind classes, and `Props` (a `Dictionary<string, object>?` with `CaptureUnmatchedValues = true`) collects any unmatched attributes and spreads them onto the rendered element.

## Examples

Standard menu with Lucide icons, a `<ContextMenuShortcut>` badge and a destructive item (adapted from the repo's `ContextMenuStandard2` block):

```razor
@using LucideBlazor
@using ShadcnBlazor.ContextMenus

<ContextMenu>
    <ContextMenuTrigger ClassName="flex h-[200px] w-full max-w-lg items-center justify-center rounded-md border border-dashed text-sm">
        Right click to open the menu
    </ContextMenuTrigger>
    <ContextMenuContent ClassName="w-56">
        <ContextMenuItem>
            <EyeIcon />
            <span>Preview</span>
            <ContextMenuShortcut>⌘P</ContextMenuShortcut>
        </ContextMenuItem>
        <ContextMenuItem>
            <CopyIcon />
            <span>Copy URL</span>
            <ContextMenuShortcut>⌘C</ContextMenuShortcut>
        </ContextMenuItem>
        <ContextMenuItem>
            <Share2Icon />
            <span>Share</span>
            <ContextMenuShortcut>⌘S</ContextMenuShortcut>
        </ContextMenuItem>
        <ContextMenuItem>
            <DownloadIcon />
            <span>Download</span>
            <ContextMenuShortcut>⌘D</ContextMenuShortcut>
        </ContextMenuItem>
        <ContextMenuItem Variant="ContextMenuItemVariant.Destructive">
            <TrashIcon />
            <span>Delete</span>
            <ContextMenuShortcut>⌘⌫</ContextMenuShortcut>
        </ContextMenuItem>
    </ContextMenuContent>
</ContextMenu>
```

File-manager style menu adapted from the repo's `ContextMenuFile1` block, where every action is driven by the item's `OnClick` callback and the menu closes automatically after the callback:

```razor
@using LucideBlazor
@using ShadcnBlazor.ContextMenus

<ContextMenu>
    <ContextMenuTrigger ClassName="flex h-[200px] w-full max-w-lg items-center justify-center rounded-md border border-dashed text-sm">
        Right click on the document
    </ContextMenuTrigger>
    <ContextMenuContent ClassName="w-56">
        <ContextMenuItem OnClick="OpenFile">
            <FolderOpenIcon />
            <span>Open</span>
            <ContextMenuShortcut>↵</ContextMenuShortcut>
        </ContextMenuItem>
        <ContextMenuItem OnClick="RenameFile">
            <EditIcon />
            <span>Rename</span>
            <ContextMenuShortcut>F2</ContextMenuShortcut>
        </ContextMenuItem>
        <ContextMenuSeparator />
        <ContextMenuItem OnClick="DuplicateFile">
            <CopyIcon />
            <span>Duplicate</span>
            <ContextMenuShortcut>⌘D</ContextMenuShortcut>
        </ContextMenuItem>
        <ContextMenuSeparator />
        <ContextMenuItem Variant="ContextMenuItemVariant.Destructive" OnClick="DeleteFile">
            <Trash2Icon />
            <span>Delete</span>
            <ContextMenuShortcut>⌘⌫</ContextMenuShortcut>
        </ContextMenuItem>
    </ContextMenuContent>
</ContextMenu>

@code
{
    private void OpenFile() => Console.WriteLine("open");
    private void RenameFile() => Console.WriteLine("rename");
    private void DuplicateFile() => Console.WriteLine("duplicate");
    private void DeleteFile() => Console.WriteLine("delete");
}
```

Menu organized with `<ContextMenuLabel>` sections and `<ContextMenuSeparator>` dividers (adapted from the repo's `ContextMenuStandard4` block):

```razor
@using LucideBlazor
@using ShadcnBlazor.ContextMenus

<ContextMenu>
    <ContextMenuTrigger ClassName="flex h-[200px] w-full max-w-lg items-center justify-center rounded-md border border-dashed text-sm">
        Right click anywhere in this area
    </ContextMenuTrigger>
    <ContextMenuContent ClassName="w-56">
        <ContextMenuLabel>Workspace</ContextMenuLabel>
        <ContextMenuSeparator />
        <ContextMenuItem OnClick="() => Console.WriteLine('profile')">
            <UserIcon />
            <span>My Profile</span>
        </ContextMenuItem>
        <ContextMenuItem OnClick="() => Console.WriteLine('preferences')">
            <SettingsIcon />
            <span>Preferences</span>
        </ContextMenuItem>
        <ContextMenuSeparator />
        <ContextMenuLabel>Support</ContextMenuLabel>
        <ContextMenuSeparator />
        <ContextMenuItem OnClick="() => Console.WriteLine('docs')">
            <HelpCircleIcon />
            <span>Documentation</span>
        </ContextMenuItem>
        <ContextMenuSeparator />
        <ContextMenuItem Variant="ContextMenuItemVariant.Destructive" OnClick="() => Console.WriteLine('signout')">
            <LogOutIcon />
            <span>Sign Out</span>
        </ContextMenuItem>
    </ContextMenuContent>
</ContextMenu>
```

Combined usage: checkbox items with two-way binding, a radio group, a nested `<ContextMenuSub>` submenu, plus a `Slot`-based trigger so an existing element (here a file row) carries the right-click handler:

```razor
@using LucideBlazor
@using ShadcnBlazor.ContextMenus

<ContextMenu>
    <ContextMenuTrigger>
        <Slot>
            <div class="flex cursor-default items-center gap-2 rounded-md border p-2 text-sm" @attributes="context">
                <FileTextIcon ClassName="size-4 text-muted-foreground" />
                quarterly-report.xlsx
            </div>
        </Slot>
    </ContextMenuTrigger>
    <ContextMenuContent ClassName="w-56">
        <ContextMenuItem OnClick="() => Console.WriteLine('rename')">
            <EditIcon />
            <span>Rename</span>
        </ContextMenuItem>
        <ContextMenuSeparator />
        <ContextMenuLabel>Layout</ContextMenuLabel>
        <ContextMenuSeparator />
        <ContextMenuCheckboxItem Value="@ShowToolbar" ValueChanged="@(value => ShowToolbar = value)">
            Show Toolbar
        </ContextMenuCheckboxItem>
        <ContextMenuCheckboxItem Value="@ShowGrid" ValueChanged="@(value => ShowGrid = value)">
            Show Grid
        </ContextMenuCheckboxItem>
        <ContextMenuSeparator />
        <ContextMenuLabel>Orientation</ContextMenuLabel>
        <ContextMenuSeparator />
        <ContextMenuRadioGroup Value="@Orientation" ValueChanged="@(value => Orientation = value)" CloseOnClick="false">
            <ContextMenuRadioItem Value="landscape">Landscape</ContextMenuRadioItem>
            <ContextMenuRadioItem Value="portrait">Portrait</ContextMenuRadioItem>
        </ContextMenuRadioGroup>
        <ContextMenuSeparator />
        <ContextMenuSub>
            <ContextMenuSubTrigger>Inset</ContextMenuSubTrigger>
            <ContextMenuSubContent>
                <ContextMenuItem Inset OnClick="() => Console.WriteLine('customize')">Customize Toolbar</ContextMenuItem>
                <ContextMenuItem Inset OnClick="() => Console.WriteLine('reset')">Reset Layout</ContextMenuItem>
            </ContextMenuSubContent>
        </ContextMenuSub>
        <ContextMenuSeparator />
        <ContextMenuItem Variant="ContextMenuItemVariant.Destructive" OnClick="() => Console.WriteLine('delete')">
            <Trash2Icon />
            <span>Delete</span>
            <ContextMenuShortcut>⌘⌫</ContextMenuShortcut>
        </ContextMenuItem>
    </ContextMenuContent>
</ContextMenu>

@code
{
    private bool ShowToolbar = true;
    private bool ShowGrid = false;
    private string Orientation = "landscape";
}
```
