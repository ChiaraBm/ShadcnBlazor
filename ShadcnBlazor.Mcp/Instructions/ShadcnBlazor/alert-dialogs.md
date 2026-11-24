---
title: Alert Dialog
description: Modal dialog that interrupts the user to confirm an action, with trigger, header, footer, and confirm/cancel buttons.
tags: [alert-dialog, dialog, confirm, overlay, modal]
---

# Alert Dialog

A blocking, modal dialog used to ask the user for confirmation before proceeding with an action such as deleting or publishing something. It cannot be dismissed with the `Esc` key — the user must explicitly choose the `AlertDialogAction` (confirm) or `AlertDialogCancel` button. The set is composed of small building blocks: `AlertDialogTrigger` opens the dialog, `AlertDialogContent` renders the dialog box on top of `AlertDialogOverlay`, and `AlertDialogHeader` / `AlertDialogFooter` / `AlertDialogTitle` / `AlertDialogDescription` structure the content. The dialog state can be controlled from C# via `OpenAsync()` / `CloseAsync()` and the `DefaultOpen` / `OnOpenChange` parameters.

## Usage

```razor
<AlertDialog>
    <AlertDialogTrigger>Delete</AlertDialogTrigger>
    <AlertDialogContent>
        <AlertDialogHeader>
            <AlertDialogTitle>Are you absolutely sure?</AlertDialogTitle>
            <AlertDialogDescription>This action cannot be undone.</AlertDialogDescription>
        </AlertDialogHeader>
        <AlertDialogFooter>
            <AlertDialogCancel>Cancel</AlertDialogCancel>
            <AlertDialogAction>Continue</AlertDialogAction>
        </AlertDialogFooter>
    </AlertDialogContent>
</AlertDialog>
```

## Parameters

### AlertDialog

| Name | Type | Default | Description |
| --- | --- | --- | --- |
| `ChildContent` | `RenderFragment` | — | The trigger, overlay and content composed inside the dialog. |
| `OnOpenChange` | `EventCallback<bool>` | — | Fired with the new open/closed state whenever the dialog opens or closes. |
| `DefaultOpen` | `bool` | `false` | When `true`, the dialog is opened automatically on first render. |

The component also exposes `IsOpen` (read-only `bool`) and the methods `OpenAsync()` / `CloseAsync()` so the dialog can be controlled from C# code.

### AlertDialogTrigger

| Name | Type | Default | Description |
| --- | --- | --- | --- |
| `ChildContent` | `RenderFragment` | — | The trigger button content. Clicking the rendered button calls `OpenAsync()`. |
| `Slot` | `RenderFragment<Dictionary<string, object?>>?` | `null` | Renders a custom element instead of the default `<button>`; the template receives the computed attributes (`aria-haspopup="dialog"`, `aria-expanded`, `data-state`, `onclick`). |

### AlertDialogContent

| Name | Type | Default | Description |
| --- | --- | --- | --- |
| `ChildContent` | `RenderFragment` | — | The dialog body, rendered inside a portal (`SectionName="@PortalNames.AlertDialog"`) together with the overlay. |
| `CloseOnBackdropClick` | `bool` | `false` | When `true`, clicking the overlay backdrop calls `CloseAsync()`. |

### AlertDialogOverlay

| Name | Type | Default | Description |
| --- | --- | --- | --- |
| `ChildContent` | `RenderFragment` | — | Content rendered inside the full-screen overlay `div`. |
| `Slot` | `RenderFragment<Dictionary<string, object?>>?` | `null` | Renders a custom element instead of the default overlay `div`; the template receives the computed attributes (`data-slot`, `data-state`, `class`). |

### AlertDialogHeader

| Name | Type | Default | Description |
| --- | --- | --- | --- |
| `ChildContent` | `RenderFragment` | — | Header content, typically `AlertDialogTitle` and `AlertDialogDescription`. |

### AlertDialogFooter

| Name | Type | Default | Description |
| --- | --- | --- | --- |
| `ChildContent` | `RenderFragment` | — | Footer content, typically `AlertDialogCancel` and `AlertDialogAction`. |

### AlertDialogTitle

| Name | Type | Default | Description |
| --- | --- | --- | --- |
| `ChildContent` | `RenderFragment` | — | The title text, rendered in a `<h2>` with `data-slot="alert-dialog-title"`. |
| `Slot` | `RenderFragment<Dictionary<string, object?>>?` | `null` | Renders a custom element instead of the default `<h2>`; the template receives the computed attributes. |

### AlertDialogDescription

| Name | Type | Default | Description |
| --- | --- | --- | --- |
| `ChildContent` | `RenderFragment` | — | The description text, rendered in a `<p>` with `data-slot="alert-dialog-description"`. |
| `Slot` | `RenderFragment<Dictionary<string, object?>>?` | `null` | Renders a custom element instead of the default `<p>`; the template receives the computed attributes. |

### AlertDialogAction

| Name | Type | Default | Description |
| --- | --- | --- | --- |
| `ChildContent` | `RenderFragment` | — | The confirm button content. |
| `OnClick` | `EventCallback` | — | Invoked on click, before the dialog closes automatically. |

### AlertDialogCancel

| Name | Type | Default | Description |
| --- | --- | --- | --- |
| `ChildContent` | `RenderFragment` | — | The cancel button content, rendered as an outlined `Button`. Clicking it closes the dialog. |

All components inherit from `ShadcnBase`, so the base parameters are also available: `ClassName` (`string`) is merged into the element's classes, and `Props` (`Dictionary<string, object>?`, captured from unmatched attributes) is merged into the rendered element.

## Examples

### Confirmation dialog with an event callback

`AlertDialogAction.OnClick` fires before the dialog closes, so side effects can be performed there; `AlertDialog.OnOpenChange` reports state changes.

```razor
<AlertDialog OnOpenChange="@OnDialogOpenChange">
    <AlertDialogTrigger>Delete user</AlertDialogTrigger>
    <AlertDialogContent>
        <AlertDialogHeader>
            <AlertDialogTitle>Are you absolutely sure?</AlertDialogTitle>
            <AlertDialogDescription>This action cannot be undone. This will permanently delete your account.</AlertDialogDescription>
        </AlertDialogHeader>
        <AlertDialogFooter>
            <AlertDialogCancel>Cancel</AlertDialogCancel>
            <AlertDialogAction OnClick="OnConfirm">Delete</AlertDialogAction>
        </AlertDialogFooter>
    </AlertDialogContent>
</AlertDialog>

@code
{
    private void OnConfirm()
    {
        // the dialog closes automatically after this
        Console.WriteLine("user deleted");
    }

    private void OnDialogOpenChange(bool isOpen)
    {
        Console.WriteLine($"dialog is now {(isOpen ? "open" : "closed")}");
    }
}
```

### Open the dialog by default or programmatically

`DefaultOpen` opens the dialog on first render. The exposed `OpenAsync()` / `CloseAsync()` methods allow full C# control of the dialog.

```razor
<AlertDialog DefaultOpen="true">
    <AlertDialogTrigger>Open</AlertDialogTrigger>
    <AlertDialogContent>
        <AlertDialogHeader>
            <AlertDialogTitle>Started automatically</AlertDialogTitle>
            <AlertDialogDescription>This dialog was opened with DefaultOpen.</AlertDialogDescription>
        </AlertDialogHeader>
        <AlertDialogFooter>
            <AlertDialogCancel>Close</AlertDialogCancel>
        </AlertDialogFooter>
    </AlertDialogContent>
</AlertDialog>

<Button @onclick="OpenDialog">Open programmatically</Button>

<AlertDialog Ref="DialogRef">
    <AlertDialogTrigger>Trigger</AlertDialogTrigger>
    <AlertDialogContent>
        <AlertDialogHeader>
            <AlertDialogTitle>Programmatic</AlertDialogTitle>
            <AlertDialogDescription>Opened from code via OpenAsync().</AlertDialogDescription>
        </AlertDialogHeader>
        <AlertDialogFooter>
            <AlertDialogCancel>Close</AlertDialogCancel>
        </AlertDialogFooter>
    </AlertDialogContent>
</AlertDialog>

@code
{
    private AlertDialog? DialogRef;
    private Task OpenDialog() => DialogRef.OpenAsync();
}
```

### Allow closing on backdrop click

By default the dialog is modal and cannot be closed by clicking the backdrop. Set `CloseOnBackdropClick` on `AlertDialogContent` to make the backdrop dismiss the dialog.

```razor
<AlertDialog>
    <AlertDialogTrigger>Discard changes?</AlertDialogTrigger>
    <AlertDialogContent CloseOnBackdropClick="true">
        <AlertDialogHeader>
            <AlertDialogTitle>Discard unsaved changes?</AlertDialogTitle>
            <AlertDialogDescription>You can also click the backdrop to dismiss this dialog.</AlertDialogDescription>
        </AlertDialogHeader>
        <AlertDialogFooter>
            <AlertDialogCancel>Keep editing</AlertDialogCancel>
            <AlertDialogAction>Discard</AlertDialogAction>
        </AlertDialogFooter>
    </AlertDialogContent>
</AlertDialog>
```

### Custom trigger element with the Slot template

`Slot` renders a custom element (for example an anchor or a card) with all the trigger's computed attributes.

```razor
<AlertDialog>
    <AlertDialogTrigger>
        <Slot Context="attrs">
            <a href="javascript:void(0)" @attributes="attrs">
                Click the card to confirm
            </a>
        </Slot>
    </AlertDialogTrigger>
    <AlertDialogContent>
        <AlertDialogHeader>
            <AlertDialogTitle>Custom trigger</AlertDialogTitle>
            <AlertDialogDescription>The anchor received the dialog trigger attributes.</AlertDialogDescription>
        </AlertDialogHeader>
        <AlertDialogFooter>
            <AlertDialogCancel>Cancel</AlertDialogCancel>
            <AlertDialogAction>Continue</AlertDialogAction>
        </AlertDialogFooter>
    </AlertDialogContent>
</AlertDialog>
```
