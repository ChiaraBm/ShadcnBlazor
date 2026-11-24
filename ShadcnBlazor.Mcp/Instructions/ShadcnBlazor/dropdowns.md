---
title: Dropdown Menu
description: Click-triggered dropdown menu with items, shortcuts, checkbox/radio items, labels, separators and nested submenus.
tags: [dropdown, dropdown-menu, menu, submenu, checkbox, radio]
---

# Dropdown Menu

A composable set of components for rendering a click-triggered dropdown menu anchored to an arbitrary trigger element. Use it for action lists, account/profile menus, view or layout switches, and any "more options" affordance that should open from a button or any other element. `<DropdownMenuTrigger>` registers itself with the menu root and opens it on click (on its default `<span>`, or on any existing element through its `Slot` template); `<DropdownMenuContent>` then renders the items in a portal near the trigger with a full-viewport backdrop that closes the menu on outside click. Available building blocks are `<DropdownMenuItem>`, `<DropdownMenuLabel>`, `<DropdownMenuSeparator>`, `<DropdownMenuShortcut>`, `<DropdownMenuCheckboxItem>`, `<DropdownMenuRadioGroup>`/`<DropdownMenuRadioItem>`, and nested `<DropdownMenuSub>` submenus. Because the menu content is rendered through a portal, the hosting layout must contain a `<PortalOutlet />`.

## Usage

```razor
@using ShadcnBlazor.Dropdowns

<DropdownMenu>
    <DropdownMenuTrigger ClassName="rounded-md border px-3 py-2 text-sm">
        Open menu
    </DropdownMenuTrigger>
    <DropdownMenuContent>
        <DropdownMenuItem OnClick="() => Console.WriteLine('Profile')">Profile</DropdownMenuItem>
        <DropdownMenuSeparator />
        <DropdownMenuItem Variant="DropdownMenuItemVariant.Destructive" OnClick="() => Console.WriteLine('Sign out')">Sign out</DropdownMenuItem>
    </DropdownMenuContent>
</DropdownMenu>
```

## Parameters

| Name | Type | Default | Description |
|---|---|---|---|
| `ChildContent` (DropdownMenu) | `RenderFragment` | `null` | The root content of the menu, normally one `<DropdownMenuTrigger>` and one `<DropdownMenuContent>`. Cascades the menu state so the trigger can open it and the items can close it. |
| `ChildContent` (DropdownMenuTrigger) | `RenderFragment` | `null` | The inner content of the default `<span>` trigger element that opens the menu on click. |
| `Slot` (DropdownMenuTrigger) | `RenderFragment<Dictionary<string, object?>>?` | `null` | When set, the click handler is attached to a `<span>` wrapping this template instead of the default `<span>` content. Render your own element inside the template and apply the template attributes with `@attributes="context"`. |
| `ChildContent` (DropdownMenuContent) | `RenderFragment` | `null` | The menu items (items, labels, separators, checkbox/radio items, submenus). Rendered in the `DropdownMenu` portal near the trigger, with a full-viewport backdrop that closes the menu on click. |
| `Side` (DropdownMenuContent) | `PositionSide` | `PositionSide.Bottom` | The side of the trigger the menu opens to: `PositionSide.Right`, `PositionSide.Left`, `PositionSide.Bottom` or `PositionSide.Top`. |
| `Align` (DropdownMenuContent) | `PositionAlignment` | `PositionAlignment.Start` | How the menu is aligned to the trigger: `PositionAlignment.Start`, `PositionAlignment.Center` or `PositionAlignment.End`. |
| `SideOffset` (DropdownMenuContent) | `double` | `6` | Distance in pixels between the menu and the trigger edge on `Side`. |
| `ChildContent` (DropdownMenuItem) | `RenderFragment` | `null` | The item's inner content, typically an icon, a label and optionally a `<DropdownMenuShortcut>`. |
| `Slot` (DropdownMenuItem) | `RenderFragment<Dictionary<string, object?>>?` | `null` | When set, the item renders your own element inside the template with all item attributes (`class`, `onclick`, `onfocusin`, …) applied via `@attributes="context"` instead of the default `<div>`. |
| `Disabled` (DropdownMenuItem) | `bool` | `false` | Makes the item unclickable and dims it. |
| `Inset` (DropdownMenuItem) | `bool` | `false` | Indents the item with extra left padding. |
| `Variant` (DropdownMenuItem) | `DropdownMenuItemVariant` | `DropdownMenuItemVariant.Default` | `DropdownMenuItemVariant.Default` for a normal item, `DropdownMenuItemVariant.Destructive` for red-styled destructive actions. |
| `OnClick` (DropdownMenuItem) | `EventCallback` | `default` | Invoked when the item is clicked. The menu is closed automatically after the callback. |
| `ChildContent` (DropdownMenuLabel) | `RenderFragment` | `null` | The label text, rendered as a non-interactive section heading. |
| `Inset` (DropdownMenuLabel) | `bool` | `false` | Indents the label with extra left padding. |
| `ChildContent` (DropdownMenuSeparator) | `RenderFragment` | `null` | Optional content inside the horizontal divider line; normally left empty. |
| `ChildContent` (DropdownMenuShortcut) | `RenderFragment` | `null` | The keyboard-shortcut text, rendered right-aligned next to the item text (e.g. `Ctrl+C`). |
| `ChildContent` (DropdownMenuGroup) | `RenderFragment` | `null` | Content of the group container that wraps related items. |
| `ChildContent` (DropdownMenuCheckboxItem) | `RenderFragment` | `null` | The item's text. A check icon is rendered automatically while `Value` is `true`. |
| `Value` (DropdownMenuCheckboxItem) | `bool` | `false` | The checked state of the item. |
| `ValueChanged` (DropdownMenuCheckboxItem) | `EventCallback<bool>` | `default` | Invoked with the new value every time the item is toggled. |
| `Disabled` (DropdownMenuCheckboxItem) | `bool` | `false` | Makes the item unclickable and dims it. |
| `CloseOnClick` (DropdownMenuCheckboxItem) | `bool` | `true` | Whether the menu closes after the item is toggled. |
| `DefaultValue` (DropdownMenuCheckboxItem) | `bool` | `false` | Initial `Value` applied on initialization. |
| `ChildContent` (DropdownMenuRadioGroup) | `RenderFragment` | `null` | The `<DropdownMenuRadioItem>` entries of the group. |
| `Value` (DropdownMenuRadioGroup) | `string` | `null` | The value of the currently selected radio item. |
| `ValueChanged` (DropdownMenuRadioGroup) | `EventCallback<string>` | `default` | Invoked with the newly selected value. |
| `CloseOnClick` (DropdownMenuRadioGroup) | `bool` | `true` | Whether the menu closes after a radio item is selected. |
| `DefaultValue` (DropdownMenuRadioGroup) | `string` | `null` | Initial `Value` applied on initialization. |
| `Value` (DropdownMenuRadioItem) | `string` | `null` | The value this radio item represents. The item is highlighted with a dot icon while it matches the group's `Value`. |
| `Disabled` (DropdownMenuRadioItem) | `bool` | `false` | Makes the item unclickable and dims it. |
| `ChildContent` (DropdownMenuRadioItem) | `RenderFragment` | `null` | The item's text. |
| `ChildContent` (DropdownMenuSub) | `RenderFragment` | `null` | Normally one `<DropdownMenuSubTrigger>` and one `<DropdownMenuSubContent>`. The submenu opens on hover/focus of the trigger. |
| `Inset` (DropdownMenuSubTrigger) | `bool` | `false` | Indents the trigger with extra left padding. |
| `ChildContent` (DropdownMenuSubTrigger) | `RenderFragment` | `null` | The submenu trigger's label; a chevron icon is appended automatically. |
| `ChildContent` (DropdownMenuSubContent) | `RenderFragment` | `null` | The submenu's items, rendered in the `DropdownMenuSub` portal to the right of the trigger. |

Every component in this folder inherits from `ShadcnBase`, so the base parameters are also available on each one. `ClassName` (a `string`) is merged into the element's Tailwind classes, and `Props` (a `Dictionary<string, object>?` with `CaptureUnmatchedValues = true`) collects any unmatched attributes and spreads them onto the rendered element.

## Examples

Standard menu with Lucide icons, a `<DropdownMenuShortcut>` badge, a destructive item and a `Slot`-based trigger so an existing element (here a button) opens the menu (adapted from the repo's `DropdownMenuStandard2` block):

```razor
@using LucideBlazor
@using ShadcnBlazor.Buttons
@using ShadcnBlazor.Dropdowns

<DropdownMenu>
    <DropdownMenuTrigger>
        <Slot>
            <Button class="flex w-fit items-center gap-2" @attributes="context">
                <BellIcon ClassName="size-4" /> Notifications
            </Button>
        </Slot>
    </DropdownMenuTrigger>
    <DropdownMenuContent ClassName="w-56" Side="PositionSide.Bottom" Align="PositionAlignment.End">
        <DropdownMenuItem>
            <UserIcon />
            <span>Profile</span>
            <DropdownMenuShortcut>⇧⌘P</DropdownMenuShortcut>
        </DropdownMenuItem>
        <DropdownMenuItem>
            <CreditCardIcon />
            <span>Billing</span>
            <DropdownMenuShortcut>⌘B</DropdownMenuShortcut>
        </DropdownMenuItem>
        <DropdownMenuSeparator />
        <DropdownMenuItem Variant="DropdownMenuItemVariant.Destructive">
            <LogOutIcon />
            <span>Log out</span>
            <DropdownMenuShortcut>⇧⌘Q</DropdownMenuShortcut>
        </DropdownMenuItem>
    </DropdownMenuContent>
</DropdownMenu>
```

View-switching menu adapted from the repo's `DropdownMenu01` block, where every action is driven by the item's `OnClick` callback and the menu closes automatically after the callback:

```razor
@using LucideBlazor
@using ShadcnBlazor.Dropdowns

<DropdownMenu>
    <DropdownMenuTrigger ClassName="flex h-10 w-fit items-center justify-center gap-1 rounded-md border px-3 text-sm font-medium transition-colors">
        <SettingsIcon ClassName="size-4" />
        View
        <ChevronDownIcon ClassName="size-4" />
    </DropdownMenuTrigger>
    <DropdownMenuContent ClassName="w-56" Side="PositionSide.Bottom" Align="PositionAlignment.Start">
        <DropdownMenuLabel>Main Menu</DropdownMenuLabel>
        <DropdownMenuSeparator />
        <DropdownMenuItem OnClick="() => SwitchView('profile')">
            <UserIcon />
            <span>Profile</span>
        </DropdownMenuItem>
        <DropdownMenuItem OnClick="() => SwitchView('billing')">
            <CreditCardIcon />
            <span>Billing</span>
        </DropdownMenuItem>
        <DropdownMenuItem OnClick="() => SwitchView('settings')">
            <SettingsIcon />
            <span>Settings</span>
        </DropdownMenuItem>
        <DropdownMenuItem OnClick="() => SwitchView('team')">
            <UsersIcon />
            <span>Team</span>
        </DropdownMenuItem>
    </DropdownMenuContent>
</DropdownMenu>

@code
{
    private void SwitchView(string view) => Console.WriteLine($"switching to {view}");
}
```

Menu organized with `<DropdownMenuLabel>` sections, `<DropdownMenuSeparator>` dividers and a `<DropdownMenuGroup>` (adapted from the repo's `DropdownMenuStandard4` block):

```razor
@using LucideBlazor
@using ShadcnBlazor.Dropdowns

<DropdownMenu>
    <DropdownMenuTrigger ClassName="flex h-10 w-fit items-center justify-center gap-1 rounded-md border px-3 text-sm font-medium transition-colors">
        <MenuIcon ClassName="size-4" />
        Menu
    </DropdownMenuTrigger>
    <DropdownMenuContent ClassName="w-56">
        <DropdownMenuLabel>Workspace</DropdownMenuLabel>
        <DropdownMenuGroup>
            <DropdownMenuItem OnClick="() => Console.WriteLine('profile')">
                <UserIcon />
                <span>My Profile</span>
            </DropdownMenuItem>
            <DropdownMenuItem OnClick="() => Console.WriteLine('preferences')">
                <SettingsIcon />
                <span>Preferences</span>
            </DropdownMenuItem>
        </DropdownMenuGroup>
        <DropdownMenuSeparator />
        <DropdownMenuLabel>Support</DropdownMenuLabel>
        <DropdownMenuGroup>
            <DropdownMenuItem OnClick="() => Console.WriteLine('docs')">
                <HelpCircleIcon />
                <span>Documentation</span>
            </DropdownMenuItem>
            <DropdownMenuItem OnClick="() => Console.WriteLine('support')">
                <MessageSquareIcon />
                <span>Support</span>
            </DropdownMenuItem>
        </DropdownMenuGroup>
        <DropdownMenuSeparator />
        <DropdownMenuItem Variant="DropdownMenuItemVariant.Destructive" OnClick="() => Console.WriteLine('signout')">
            <LogOutIcon />
            <span>Sign Out</span>
        </DropdownMenuItem>
    </DropdownMenuContent>
</DropdownMenu>
```

Combined usage: checkbox items with two-way binding, a radio group with `CloseOnClick="false"`, a nested `<DropdownMenuSub>` submenu, and a destructive item:

```razor
@using LucideBlazor
@using ShadcnBlazor.Dropdowns

<DropdownMenu>
    <DropdownMenuTrigger ClassName="flex h-10 w-fit items-center justify-center gap-1 rounded-md border px-3 text-sm font-medium transition-colors">
        <SlidersHorizontalIcon ClassName="size-4" />
        Layout
    </DropdownMenuTrigger>
    <DropdownMenuContent ClassName="w-56">
        <DropdownMenuLabel>Layout</DropdownMenuLabel>
        <DropdownMenuCheckboxItem Value="@ShowToolbar" ValueChanged="@(value => ShowToolbar = value)" CloseOnClick="false">
            Show Toolbar
        </DropdownMenuCheckboxItem>
        <DropdownMenuCheckboxItem Value="@ShowGrid" ValueChanged="@(value => ShowGrid = value)" CloseOnClick="false">
            Show Grid
        </DropdownMenuCheckboxItem>
        <DropdownMenuSeparator />
        <DropdownMenuLabel>Orientation</DropdownMenuLabel>
        <DropdownMenuRadioGroup Value="@Orientation" ValueChanged="@(value => Orientation = value)" CloseOnClick="false">
            <DropdownMenuRadioItem Value="landscape">Landscape</DropdownMenuRadioItem>
            <DropdownMenuRadioItem Value="portrait">Portrait</DropdownMenuRadioItem>
        </DropdownMenuRadioGroup>
        <DropdownMenuSeparator />
        <DropdownMenuSub>
            <DropdownMenuSubTrigger>Tools</DropdownMenuSubTrigger>
            <DropdownMenuSubContent>
                <DropdownMenuItem Inset OnClick="() => Console.WriteLine('customize')">Customize Toolbar</DropdownMenuItem>
                <DropdownMenuItem Inset OnClick="() => Console.WriteLine('reset')">Reset Layout</DropdownMenuItem>
            </DropdownMenuSubContent>
        </DropdownMenuSub>
        <DropdownMenuSeparator />
        <DropdownMenuItem Variant="DropdownMenuItemVariant.Destructive" OnClick="() => Console.WriteLine('clear-all')">
            <Trash2Icon />
            <span>Clear All Data</span>
            <DropdownMenuShortcut>⌘⌫</DropdownMenuShortcut>
        </DropdownMenuItem>
    </DropdownMenuContent>
</DropdownMenu>

@code
{
    private bool ShowToolbar = true;
    private bool ShowGrid = false;
    private string Orientation = "landscape";
}
```
