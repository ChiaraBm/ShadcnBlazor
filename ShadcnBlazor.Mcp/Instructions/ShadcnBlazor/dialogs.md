---
title: Dialog
description: A modal dialog opened by a trigger or from C#, with overlay, header, footer, close button, and backdrop-click dismissal.
tags: [dialog, modal, overlay, trigger, backdrop]
---

# Dialog

A non-blocking modal dialog for focused tasks such as editing a form, showing details, or confirming an action. It can be opened by clicking a `DialogTrigger` or programmatically via `OpenAsync()` / `CloseAsync()` from C# code, and the open state can be reported through the `OnOpenChange` callback. The dialog content is rendered in a portal (`SectionName="@PortalNames.Dialog"`) on top of a dimmed `DialogOverlay`, with `DialogHeader` / `DialogFooter` / `DialogTitle` / `DialogDescription` structuring the content. By default a close button (`X`) is shown in the top-right corner; backdrop-click dismissal is opt-in through `CloseOnBackdropClick`.

## Usage

```razor
@using ShadcnBlazor.Dialogs
@using ShadcnBlazor.Buttons

<Dialog>
    <DialogTrigger>Open Dialog</DialogTrigger>
    <DialogContent>
        <DialogHeader>
            <DialogTitle>Dialog title</DialogTitle>
            <DialogDescription>Something to tell the user.</DialogDescription>
        </DialogHeader>
        <DialogFooter>
            <DialogClose>Close</DialogClose>
        </DialogFooter>
    </DialogContent>
</Dialog>
```

## Parameters

### Dialog

| Name | Type | Default | Description |
| --- | --- | --- | --- |
| `ChildContent` | `RenderFragment` | — | The dialog's trigger and content, composed inside the dialog. |
| `OnOpenChange` | `EventCallback<bool>` | — | Fired with the new open/closed state whenever the dialog opens or closes. |
| `DefaultOpen` | `bool` | `false` | When `true`, the dialog is opened automatically on first render. |

The component also exposes `IsOpen` (read-only `bool`) and the methods `OpenAsync()` / `CloseAsync()` so the dialog can be controlled from C# code.

### DialogTrigger

| Name | Type | Default | Description |
| --- | --- | --- | --- |
| `ChildContent` | `RenderFragment` | — | The trigger button content. Clicking the rendered button calls `OpenAsync()`. |
| `Slot` | `RenderFragment<Dictionary<string, object?>>?` | `null` | Renders a custom element instead of the default `<button>`; the template receives the computed attributes (`aria-haspopup="dialog"`, `aria-expanded`, `data-state`, `onclick`). |

### DialogContent

| Name | Type | Default | Description |
| --- | --- | --- | --- |
| `ChildContent` | `RenderFragment` | — | The dialog body, rendered inside a portal (`SectionName="@PortalNames.Dialog"`) together with the overlay. |
| `ShowCloseButton` | `bool` | `true` | When `true` (default), renders the `X` close button in the top-right corner that calls `CloseAsync()`. |
| `CloseOnBackdropClick` | `bool` | `false` | When `true`, clicking the overlay backdrop calls `CloseAsync()`. |

### DialogOverlay

| Name | Type | Default | Description |
| --- | --- | --- | --- |
| `ChildContent` | `RenderFragment` | — | Content rendered inside the full-screen overlay `div` (`data-slot="dialog-overlay"`). |
| `Slot` | `RenderFragment<Dictionary<string, object?>>?` | `null` | Renders a custom element instead of the default overlay `div`; the template receives the computed attributes (`data-slot`, `data-state`, `class`). |

### DialogHeader

| Name | Type | Default | Description |
| --- | --- | --- | --- |
| `ChildContent` | `RenderFragment` | — | Header content, typically `DialogTitle` and `DialogDescription`. |

### DialogFooter

| Name | Type | Default | Description |
| --- | --- | --- | --- |
| `ChildContent` | `RenderFragment` | — | Footer content, typically `DialogClose` and action buttons. |

### DialogTitle

| Name | Type | Default | Description |
| --- | --- | --- | --- |
| `ChildContent` | `RenderFragment` | — | The title text, rendered in a `<h2>` with `data-slot="dialog-title"`. |
| `Slot` | `RenderFragment<Dictionary<string, object?>>?` | `null` | Renders a custom element instead of the default `<h2>`; the template receives the computed attributes. |

### DialogDescription

| Name | Type | Default | Description |
| --- | --- | --- | --- |
| `ChildContent` | `RenderFragment` | — | The description text, rendered in a `<p>` with `data-slot="dialog-description"`. |
| `Slot` | `RenderFragment<Dictionary<string, object?>>?` | `null` | Renders a custom element instead of the default `<p>`; the template receives the computed attributes. |

### DialogClose

| Name | Type | Default | Description |
| --- | --- | --- | --- |
| `ChildContent` | `RenderFragment` | — | The close button content, rendered as a `<button type="button">`. Clicking it calls the parent's `CloseAsync()`. |
| `Slot` | `RenderFragment<Dictionary<string, object?>>?` | `null` | Renders a custom element instead of the default `<button>`; the template receives the computed attributes (including `onclick` that closes the dialog). |

All components inherit from `ShadcnBase`, so the base parameters are also available: `ClassName` (`string`) is merged into the element's classes, and `Props` (`Dictionary<string, object>?`, captured from unmatched attributes) is merged into the rendered element.

## Examples

### Form dialog opened by default

Adapted from the `DialogSc1` showcase: the dialog opens on first render (`DefaultOpen="true"`), the trigger and the cancel button use the `Slot` template to render a real `Button` with all computed trigger attributes, and `ClassName` sizes the content.

```razor
@using ShadcnBlazor.Dialogs
@using ShadcnBlazor.Buttons
@using ShadcnBlazor.Inputs
@using ShadcnBlazor.Labels

<Dialog DefaultOpen="true">
    <form>
        <DialogTrigger>
            <Slot>
                <Button Variant="ButtonVariant.Outline" @attributes="context">Open Dialog</Button>
            </Slot>
        </DialogTrigger>
        <DialogContent ClassName="sm:max-w-[425px]">
            <DialogHeader>
                <DialogTitle>Edit profile</DialogTitle>
                <DialogDescription>
                    Make changes to your profile here. Click save when you&apos;re
                    done.
                </DialogDescription>
            </DialogHeader>
            <div class="grid gap-4">
                <div class="grid gap-3">
                    <Label>Name</Label>
                    <TextInputField name="name" @bind-Value="Name" />
                </div>
                <div class="grid gap-3">
                    <Label>Username</Label>
                    <TextInputField name="username" @bind-Value="Username" />
                </div>
            </div>
            <DialogFooter>
                <DialogClose>
                    <Slot>
                        <Button Variant="ButtonVariant.Outline" @attributes="context">Cancel</Button>
                    </Slot>
                </DialogClose>
                <Button type="submit">Save changes</Button>
            </DialogFooter>
        </DialogContent>
    </form>
</Dialog>

@code
{
    private string Name = "Pedro Duarte";
    private string Username = "@peduarte";
}
```

### Open the dialog by default or programmatically

`DefaultOpen` opens the dialog on first render. The exposed `OpenAsync()` / `CloseAsync()` methods allow full C# control of the dialog, and `OnOpenChange` reports state changes.

```razor
@using ShadcnBlazor.Dialogs
@using ShadcnBlazor.Buttons

<Dialog DefaultOpen="true">
    <DialogContent>
        <DialogHeader>
            <DialogTitle>Started automatically</DialogTitle>
            <DialogDescription>This dialog was opened with DefaultOpen.</DialogDescription>
        </DialogHeader>
        <DialogFooter>
            <DialogClose>Close</DialogClose>
        </DialogFooter>
    </DialogContent>
</Dialog>

<Button @onclick="OpenDialog">Open programmatically</Button>
<Button @onclick="CloseDialog">Close it</Button>

<Dialog Ref="DialogRef" OnOpenChange="OnDialogOpenChange">
    <DialogTrigger>Open from trigger</DialogTrigger>
    <DialogContent>
        <DialogHeader>
            <DialogTitle>Programmatic</DialogTitle>
            <DialogDescription>Opened or closed from code via OpenAsync() / CloseAsync().</DialogDescription>
        </DialogHeader>
        <DialogFooter>
            <DialogClose>Close</DialogClose>
        </DialogFooter>
    </DialogContent>
</Dialog>

@code
{
    private Dialog? DialogRef;

    private Task OpenDialog() => DialogRef.OpenAsync();

    private Task CloseDialog() => DialogRef.CloseAsync();

    private void OnDialogOpenChange(bool isOpen)
    {
        Console.WriteLine($"dialog is now {(isOpen ? "open" : "closed")}");
    }
}
```

### Allow closing on backdrop click and hide the close button

By default the dialog is modal and can only be closed through the `X` button or a `DialogClose`. Set `CloseOnBackdropClick` on `DialogContent` to make the backdrop dismiss the dialog, and `ShowCloseButton="false"` to remove the top-right close button.

```razor
@using ShadcnBlazor.Dialogs
@using ShadcnBlazor.Buttons

<Dialog>
    <DialogTrigger>Discard changes?</DialogTrigger>
    <DialogContent CloseOnBackdropClick="true" ShowCloseButton="false">
        <DialogHeader>
            <DialogTitle>Discard unsaved changes?</DialogTitle>
            <DialogDescription>You can also click the backdrop to dismiss this dialog.</DialogDescription>
        </DialogHeader>
        <DialogFooter>
            <DialogClose>Keep editing</DialogClose>
            <Button>Discard</Button>
        </DialogFooter>
    </DialogContent>
</Dialog>
```

### Custom trigger element with the Slot template

`Slot` renders a custom element (for example an anchor or a card) with all the trigger's computed attributes (`aria-haspopup="dialog"`, `aria-expanded`, `data-state`, `onclick`).

```razor
@using ShadcnBlazor.Dialogs

<Dialog>
    <DialogTrigger>
        <Slot Context="attrs">
            <a href="javascript:void(0)" @attributes="attrs">
                Click the card to open
            </a>
        </Slot>
    </DialogTrigger>
    <DialogContent>
        <DialogHeader>
            <DialogTitle>Custom trigger</DialogTitle>
            <DialogDescription>The anchor received the dialog trigger attributes.</DialogDescription>
        </DialogHeader>
        <DialogFooter>
            <DialogClose>Close</DialogClose>
        </DialogFooter>
    </DialogContent>
</Dialog>
```
