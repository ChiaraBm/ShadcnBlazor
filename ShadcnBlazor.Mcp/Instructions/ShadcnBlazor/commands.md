---
title: Command
description: Searchable command palette that filters grouped command items on input, with an empty state and a dialog wrapper.
tags: [command, palette, search, menu, cmdk, dialog]
---

# Command

A set of components that implement a searchable command palette (built on the `cmdk` attributes). The root `Command` cascades itself to its children and keeps a registry of the `CommandItem`s and `CommandGroup`s it contains, so typing into `CommandInput` filters items by their `Name` and automatically hides groups that have no visible items left. The palette can be embedded inline anywhere, or wrapped in a `CommandDialog` to open as a centered overlay. The children render the `cmdk-root`, `cmdk-input`, `cmdk-list`, `cmdk-group`, `cmdk-item`, and `cmdk-separator` attributes, and `CommandEmpty` shows a fallback message whenever no item matches.

## Usage

```razor
@using ShadcnBlazor.Commands

<Command>
    <CommandInput placeholder="Type a command or search..." />
    <CommandList>
        <CommandEmpty>No results found.</CommandEmpty>
        <CommandGroup Heading="Suggestions">
            <CommandItem Name="Calendar">
                <span>Calendar</span>
            </CommandItem>
        </CommandGroup>
    </CommandList>
</Command>
```

## Parameters

The components all inherit from `ShadcnBase`, so the base `ClassName` (appended to the built-in classes) and `Props` (a `Dictionary<string, object?>` merged into the rendered attributes) are also available on every one. `CommandInput` has no `[Parameter]` of its own; extra markup attributes such as `placeholder` are passed straight through `Props`.

| Name | Type | Default | Description |
|---|---|---|---|
| `Command.ChildContent` | `RenderFragment` | `null` | The inner content of the palette (`CommandInput`, `CommandList`, groups, items). |
| `Command.OnItemClicked` | `EventCallback<CommandItem>` | `null` | Invoked when a `CommandItem` is clicked; the clicked item is passed to the callback. `CommandDialog` wires this internally to close the dialog. |
| `CommandDialog.ChildContent` | `RenderFragment` | `null` | The command palette content rendered inside the dialog. |
| `CommandDialog.Title` | `string` | `"Command Palette"` | Screen-reader-only dialog title. |
| `CommandDialog.Description` | `string` | `"Search for a command to run..."` | Screen-reader-only dialog description. |
| `CommandDialog.ShowCloseButton` | `bool` | `true` | Shows a close button in the dialog content when `true`. |
| `CommandDialog.OnOpenChange` | `EventCallback<bool>` | `null` | Invoked when the dialog open state changes (`true` = opened, `false` = closed). |
| `CommandDialog.DefaultOpen` | `bool` | `false` | Opens the dialog on the first render when `true`. |
| `Command.Command` (`CommandEmpty`) | `Command` `[CascadingParameter]` | — | Cascaded from the parent `Command`; exposes `IsEmpty` so the empty state is hidden while items match. |
| `CommandEmpty.ChildContent` | `RenderFragment` | `null` | Content shown when no items match the search. |
| `Command.Command` (`CommandGroup`) | `Command` `[CascadingParameter]` | — | Cascaded from the parent `Command`. |
| `CommandGroup.ChildContent` | `RenderFragment` | `null` | The `CommandItem`s that belong to this group. |
| `CommandGroup.Heading` | `string?` | `null` | Optional heading text rendered above the group's items; omitted from the output when `null`. |
| `Command.Command` (`CommandInput`) | `Command` `[CascadingParameter]` | — | Cascaded from the parent `Command`; typed input is routed to its filtering. |
| `CommandItem.Command` | `Command` `[CascadingParameter]` | — | Cascaded from the parent `Command`. |
| `CommandItem.CommandGroup` | `CommandGroup` `[CascadingParameter]` | — | Cascaded from the enclosing `CommandGroup`. |
| `CommandItem.ChildContent` | `RenderFragment` | `null` | The content of the item (icon, label, shortcut). |
| `CommandItem.Name` | `string` (required) | — | The searchable name of the item; typing in the input filters on this value. |
| `CommandItem.Disabled` | `bool` | `false` | Makes the item non-interactive when `true`. |
| `CommandItem.OnClicked` | `EventCallback` | `null` | Invoked when the item is clicked. |
| `CommandList.ChildContent` | `RenderFragment` | `null` | The scrollable list content (empty state and groups). |
| `Command.Command` (`CommandSeparator`) | `Command` `[CascadingParameter]` | — | Cascaded from the parent `Command`; the separator is hidden while a search is active. |
| `CommandShortcut.ChildContent` | `RenderFragment` | `null` | The shortcut text (e.g. `⌘P`) rendered right-aligned inside a `CommandItem`. |

## Examples

Full inline palette with groups, a disabled item, and keyboard-shortcut hints:

```razor
@using LucideBlazor
@using ShadcnBlazor.Commands

<Command ClassName="rounded-lg border shadow-md md:min-w-[450px]">
    <CommandInput placeholder="Type a command or search..." />
    <CommandList>
        <CommandEmpty>No results found.</CommandEmpty>
        <CommandGroup Heading="Suggestions">
            <CommandItem Name="Calendar" OnClicked="OnCalendar">
                <CalendarIcon />
                <span>Calendar</span>
            </CommandItem>
            <CommandItem Name="Calculator" Disabled="true">
                <CalculatorIcon />
                <span>Calculator</span>
            </CommandItem>
        </CommandGroup>
        <CommandSeparator />
        <CommandGroup Heading="Settings">
            <CommandItem Name="Profile" OnClicked="OnProfile">
                <UserIcon />
                <span>Profile</span>
                <CommandShortcut>⌘P</CommandShortcut>
            </CommandItem>
            <CommandItem Name="Settings" OnClicked="OnSettings">
                <SettingsIcon />
                <span>Settings</span>
                <CommandShortcut>⌘S</CommandShortcut>
            </CommandItem>
        </CommandGroup>
    </CommandList>
</Command>

@code {
    private void OnCalendar() { /* open the calendar */ }
    private void OnProfile() { /* open the profile */ }
    private void OnSettings() { /* open the settings */ }
}
```

The palette as a dialog via `CommandDialog`; selecting an item closes it automatically (the inner `Command` is wired to the dialog's close action):

```razor
@using ShadcnBlazor.Commands

<CommandDialog
    Title="Command Palette"
    Description="Search for a command to run..."
    DefaultOpen="true"
    OnOpenChange="OnOpenChanged">
    <CommandInput placeholder="Type a command or search..." />
    <CommandList>
        <CommandEmpty>No results found.</CommandEmpty>
        <CommandGroup Heading="Actions">
            <CommandItem Name="New File" OnClicked="OnNewFile">
                <span>New File</span>
                <CommandShortcut>⌘N</CommandShortcut>
            </CommandItem>
            <CommandItem Name="Export" OnClicked="OnExport">
                <span>Export</span>
                <CommandShortcut>⌘E</CommandShortcut>
            </CommandItem>
        </CommandGroup>
    </CommandList>
</CommandDialog>

@code {
    private void OnOpenChanged(bool isOpen) { /* react to the dialog opening/closing */ }
    private void OnNewFile() { /* create a new file */ }
    private void OnExport() { /* export */ }
}
```

Binding items from a list and handling the selection through both `OnClicked` (per item) and `OnItemClicked` (on the root):

```razor
@using ShadcnBlazor.Commands

<Command OnItemClicked="OnItemClicked">
    <CommandInput placeholder="Search..." />
    <CommandList>
        <CommandEmpty>Nothing found.</CommandEmpty>
        <CommandGroup>
            @foreach (var item in Items)
            {
                <CommandItem @key="item" Name="@item" OnClicked="@(() => Select(item))">
                    <span>@item</span>
                </CommandItem>
            }
        </CommandGroup>
    </CommandList>
</Command>

@code {
    private readonly string[] Items = ["Apple", "Banana", "Cherry", "Grape"];
    private string Selected;

    private void Select(string item) => Selected = item;
    private void OnItemClicked(CommandItem item) { /* a command was picked */ }
}
```
