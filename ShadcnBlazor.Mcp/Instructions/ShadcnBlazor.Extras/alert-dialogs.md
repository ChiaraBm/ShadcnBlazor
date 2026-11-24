---
title: Alert Dialog
description: Service-driven alert dialogs launched from C# code with confirm, info, success, warning and error variants.
tags: [alert-dialog, dialog, service, launcher, confirm]
---

# Alert Dialog

A programmatic, modal alert dialog for the `ShadcnBlazor.Extras` package. Instead of composing the dialog in markup, you inject `AlertDialogService` and call a method such as `ConfirmAsync`, `InfoAsync`, `SuccessAsync`, `WarningAsync` or `ErrorAsync` from any C# handler — the service renders the matching built-in design on top of the page. Built-in designs are confirm (with or without a destructive confirm button) and the four single-button notification variants, and you can register your own design by inheriting `AlertDialogBase` and launching it with `LaunchCustomAsync<T>`. The returned `AlertDialogModel` can be used to close the dialog later, and every design supports closing on backdrop click via `model.CloseOnBackdropClick`.

## Usage

Register the service once with `AddShadcnBlazorExtras()` and place `<AlertDialogLauncher />` in a layout (or directly in a page). Then inject `AlertDialogService` and call a variant from an event handler.

```razor
@using ShadcnBlazor.Buttons
@using ShadcnBlazor.Extras.AlertDialogs
@inject AlertDialogService AlertDialogService

<AlertDialogLauncher />

<Button Variant="ButtonVariant.Outline" @onclick="ShowInfoAsync">Show info</Button>

@code
{
    private Task ShowInfoAsync()
        => AlertDialogService.InfoAsync("Saved", "Your changes have been saved.");
}
```

## Parameters

### AlertDialogBase

Base class for every design. All designs inherit from it and receive the `Model` and the cascaded `AlertDialog` automatically.

| Name | Type | Default | Description |
| --- | --- | --- | --- |
| `Model` | `AlertDialogModel` | — | The model for the currently open dialog, injected into each design so it can be closed programmatically or read for configuration. |

The class also exposes a cascading `AlertDialog` (`[CascadingParameter] public AlertDialog AlertDialog`) used internally by the designs to call `CloseAsync()`.

### ConfirmAlertDialog

A confirm dialog with a neutral `WButton` confirm and an `AlertDialogCancel` button.

| Name | Type | Default | Description |
| --- | --- | --- | --- |
| `Title` | `string` | — | The dialog title text. |
| `Description` | `string` | — | The dialog description text. |
| `OnConfirm` | `Func<Task>` | — | Invoked when the confirm button is clicked; the dialog closes after it completes. |

### ConfirmDangerAlertDialog

Same layout as `ConfirmAlertDialog`, but the confirm button uses `ButtonVariant.Destructive` and a warning icon.

| Name | Type | Default | Description |
| --- | --- | --- | --- |
| `Title` | `string` | — | The dialog title text. |
| `Description` | `string` | — | The dialog description text. |
| `OnConfirm` | `Func<Task>` | — | Invoked when the destructive confirm button is clicked; the dialog closes after it completes. |

### ErrorAlertDialog

Single-close-button error dialog with a red `CircleXIcon`.

| Name | Type | Default | Description |
| --- | --- | --- | --- |
| `Title` | `string` | — | The dialog title text. |
| `Description` | `string` | — | The dialog description text. |

### InfoAlertDialog

Single-close-button info dialog with a blue `InfoIcon`.

| Name | Type | Default | Description |
| --- | --- | --- | --- |
| `Title` | `string` | — | The dialog title text. |
| `Description` | `string` | — | The dialog description text. |

### SuccessAlertDialog

Single-close-button success dialog with an emerald `CircleCheckBigIcon`.

| Name | Type | Default | Description |
| --- | --- | --- | --- |
| `Title` | `string` | — | The dialog title text. |
| `Description` | `string` | — | The dialog description text. |

### WarningAlertDialog

Single-close-button warning dialog with a yellow `TriangleAlertIcon`.

| Name | Type | Default | Description |
| --- | --- | --- | --- |
| `Title` | `string` | — | The dialog title text. |
| `Description` | `string` | — | The dialog description text. |

### AlertDialogModel

The object returned by the launch methods.

| Name | Type | Default | Description |
| --- | --- | --- | --- |
| `Attributes` | `Dictionary<string, object?>` | — | The parameter attributes passed to the design component. |
| `ComponentType` | `Type` | — | The type of the rendered design component. |
| `Dialog` | `AlertDialog` | — | The underlying `AlertDialog` instance for this dialog. |
| `CloseOnBackdropClick` | `bool` | `false` | When `true`, clicking the backdrop closes this dialog. |

### AlertDialogService methods

The public API used from C# (registered scoped via `AddShadcnBlazorExtras()`). Each launch method returns the `AlertDialogModel` for the opened dialog.

| Method | Description |
| --- | --- |
| `ConfirmAsync(string title, string description, Func<Task> onConfirm)` | Opens a `ConfirmAlertDialog`. |
| `ConfirmDangerAsync(string title, string description, Func<Task> onConfirm)` | Opens a `ConfirmDangerAlertDialog`. |
| `InfoAsync(string title, string description)` | Opens an `InfoAlertDialog`. |
| `SuccessAsync(string title, string description)` | Opens a `SuccessAlertDialog`. |
| `WarningAsync(string title, string description)` | Opens a `WarningAlertDialog`. |
| `ErrorAsync(string title, string description)` | Opens an `ErrorAlertDialog`. |
| `CloseAsync(AlertDialogModel dialogModel)` | Closes the dialog represented by the model. |
| `LaunchCustomAsync<T>(Action<Dictionary<string, object?>>? callback = null) where T : AlertDialogBase` | Opens a custom `AlertDialogBase` design and lets you set its parameters through the `callback`. |

## Examples

### Chained confirmations (adapted from the real showcase)

`ConfirmAsync` and `ConfirmDangerAsync` take an `onConfirm` callback, so dialogs can be chained: confirming the first opens the danger variant, which opens a success notification.

```razor
@using ShadcnBlazor.Buttons
@using ShadcnBlazor.Extras.AlertDialogs

<AlertDialogLauncher />

<Button Variant="ButtonVariant.Outline" @onclick="LaunchConfirmAsync">Confirm</Button>

@code
{
    private async Task LaunchConfirmAsync()
    {
        await AlertDialogService.ConfirmAsync(
            "Are you absolutely sure?",
            "This action cannot be undone. This will permanently delete your data.",
            async () =>
            {
                await Task.Delay(1500);

                await AlertDialogService.ConfirmDangerAsync(
                    "Are you really sure?",
                    "This is the danger variant. Press confirm again to finish.",
                    async () =>
                    {
                        await AlertDialogService.SuccessAsync(
                            "Operation has been performed",
                            "Everything is done.");
                    }
                );
            }
        );
    }
}
```

### Close a dialog programmatically

Every launch method returns an `AlertDialogModel`. Keep a reference to it and pass it to `CloseAsync` to dismiss the dialog from code (for example after a timer or a background operation).

```razor
@using ShadcnBlazor.Buttons
@using ShadcnBlazor.Extras.AlertDialogs
@inject AlertDialogService AlertDialogService

<AlertDialogLauncher />

<Button @onclick="ShowAndCloseAsync">Show for 2 seconds</Button>

@code
{
    private async Task ShowAndCloseAsync()
    {
        var model = await AlertDialogService.InfoAsync("Working", "Please wait…");
        await Task.Delay(2000);
        await AlertDialogService.CloseAsync(model);
    }
}
```

### Custom dialog via LaunchCustomAsync

Create a design by inheriting `AlertDialogBase`, then open it with `LaunchCustomAsync<T>` and set its parameters in the callback.

`MyDialog.razor`

```razor
@using ShadcnBlazor.Buttons
@using ShadcnBlazor.Extras.AlertDialogs

@inherits AlertDialogBase

<AlertDialogHeader>
    <AlertDialogTitle>@Title</AlertDialogTitle>
    <AlertDialogDescription>@Description</AlertDialogDescription>
</AlertDialogHeader>
<AlertDialogFooter>
    <AlertDialogCancel>Close</AlertDialogCancel>
</AlertDialogFooter>

@code
{
    [Parameter] public string Title { get; set; }
    [Parameter] public string Description { get; set; }
}
```

Page that launches it

```razor
@using ShadcnBlazor.Buttons
@using ShadcnBlazor.Extras.AlertDialogs
@using ShadcnBlazor.Extras.AlertDialogs.Designs

<AlertDialogLauncher />

<Button @onclick="LaunchCustomAsync">Open custom</Button>

@code
{
    private async Task LaunchCustomAsync()
    {
        await AlertDialogService.LaunchCustomAsync<MyDialog>(attributes =>
        {
            attributes[nameof(MyDialog.Title)] = "Custom title";
            attributes[nameof(MyDialog.Description)] = "A fully custom design.";
            attributes[nameof(AlertDialogModel.CloseOnBackdropClick)] = true;
        });
    }
}
```
