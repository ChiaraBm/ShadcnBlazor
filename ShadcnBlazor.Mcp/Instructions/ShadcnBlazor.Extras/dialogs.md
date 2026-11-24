---
title: Dialog
description: Launch ShadcnBlazor Dialog components programmatically from C# with DialogService, DialogLauncher, and DialogBase.
tags: [dialog, modal, dialogbase, dialogservice, dialoglauncher]
---

# Dialog

The Extras package turns the core `Dialog` into a programmatically launched dialog: instead of declaring the dialog markup in a page, you write a standalone component that inherits `DialogBase`, register `DialogService` with `AddShadcnBlazorExtras()`, drop a `<DialogLauncher/>` into a layout, and call `DialogService.LaunchAsync<T>()` from any C# event handler. This fits flows where the dialog is triggered by program logic rather than a UI trigger, such as opening a form after a validation error. Each launched dialog is rendered through a `DynamicComponent` wrapped in a core `<Dialog DefaultOpen="true">`, and the `DialogModel` returned by `LaunchAsync` lets the caller control the content size, the close button, and backdrop-click dismissal.

## Usage

Register the extras services in the host (`builder.Services.AddShadcnBlazorExtras();`), then launch a dialog from code. The `<DialogLauncher/>` must live in a component that stays alive for the whole session (the test app places it in `MainLayout` / `EmptyLayout`).

The dialog component inherits `DialogBase` and receives the core `Dialog` as a cascading parameter:

```razor
@using ShadcnBlazor.Dialogs

@inherits ShadcnBlazor.Extras.Dialogs.DialogBase

<DialogHeader>
    <DialogTitle>Example dialog</DialogTitle>
    <DialogDescription>Launched programmatically with DialogService.LaunchAsync.</DialogDescription>
</DialogHeader>
<DialogFooter>
    <DialogClose>Close</DialogClose>
</DialogFooter>
```

The page that opens it:

```razor
@using ShadcnBlazor.Buttons
@using ShadcnBlazor.Extras.Dialogs

@inject DialogService DialogService

<Button @onclick="LaunchAsync" Variant="ButtonVariant.Outline">Launch dialog</Button>
<DialogLauncher/>

@code
{
    private async Task LaunchAsync() => await DialogService.LaunchAsync<ExampleDialog>();
}
```

## Parameters

### DialogBase

| Name | Type | Default | Description |
| --- | --- | --- | --- |
| `Model` | `DialogModel` | — | The dialog model the `DialogLauncher` sets up for the component. It holds the launched `ComponentType`, the rendered core `Dialog`, and the dialog's settings (`ClassName`, `ShowCloseButton`, `CloseOnBackdropClick`). |

`DialogBase` also receives the core dialog as `[CascadingParameter] public Dialog Dialog` (`ShadcnBlazor.Dialogs.Dialog`) and exposes `protected Task CloseAsync()`, which closes the dialog from inside the component. `DialogModel` is a plain class with `Type ComponentType`, `Dictionary<string, object?> Attributes`, `Dialog Dialog`, `string ClassName`, `bool CloseOnBackdropClick`, and `bool ShowCloseButton` (default `true`).

### DialogLauncher

`DialogLauncher` declares no `[Parameter]` properties. It renders a core `<Dialog DefaultOpen="true" OnOpenChange="...">` per open `DialogModel` (the inner content is a `DialogContent` that hosts a `DynamicComponent` of the dialog's type), removes the model when the dialog closes, and registers itself with the injected `DialogService`. It exposes:

- `Task<DialogModel> LaunchAsync<T>(Action<Dictionary<string, object?>>? parameters = null, Action<DialogModel>? onConfigure = null) where T : DialogBase` — creates a `DialogModel` for `T`, invokes `onConfigure` to adjust it, passes the `Model` to the component, invokes `parameters` to fill the remaining component parameters, and returns the model.
- `Task CloseAsync(DialogModel model)` — closes a previously launched dialog.

`DialogService` is a scoped service that proxies `LaunchAsync<T>` to the registered launcher.

## Examples

### Launch a dialog with parameters and custom content size

Adapted from `ShadcnBlazor.Test/Views/Blocks.razor` with `CopyBlockDialog.razor`: the `parameters` callback fills the component's `[Parameter]` properties (by `nameof`), and `onConfigure` sizes the dialog content with `ClassName`.

```razor
@using ShadcnBlazor.Buttons
@using ShadcnBlazor.Extras.Dialogs

@inject DialogService DialogService

<Button @onclick="LaunchAsync" Variant="ButtonVariant.Outline">Launch dialog</Button>
<DialogLauncher/>

@code
{
    private async Task LaunchAsync()
    {
        await DialogService.LaunchAsync<GreetingDialog>(parameters =>
        {
            parameters[nameof(GreetingDialog.Name)] = "Chiara";
        }, model =>
        {
            model.ClassName = "sm:max-w-lg";
        });
    }
}
```

```razor
@using ShadcnBlazor.Dialogs

@inherits ShadcnBlazor.Extras.Dialogs.DialogBase

<DialogHeader>
    <DialogTitle>Hello @Name</DialogTitle>
    <DialogDescription>Name was passed through the parameters callback of LaunchAsync.</DialogDescription>
</DialogHeader>
<DialogFooter>
    <DialogClose>Close</DialogClose>
</DialogFooter>

@code
{
    [Parameter] public string Name { get; set; }
}
```

### A form dialog that closes itself

Adapted from `ShadcnBlazor.Test/ExtraShowcases/Components/ExampleDialog.razor`: the dialog content uses the cascaded `Dialog.CloseAsync()` to close the dialog after a successful submit, and the cancel button reuses the trigger `Slot` template to render a real `Button`.

```razor
@using ShadcnBlazor.Dialogs
@using ShadcnBlazor.Inputs
@using ShadcnBlazor.Labels
@using ShadcnBlazor.Buttons

@inherits ShadcnBlazor.Extras.Dialogs.DialogBase

<DialogHeader>
    <DialogTitle>Feedback</DialogTitle>
    <DialogDescription>Send us your feedback.</DialogDescription>
</DialogHeader>
<div class="space-y-2">
    <Label for="feedback">Feedback</Label>
    <TextareaField
          ClassName="min-h-[120px]"
          id="feedback"
          placeholder="Your feedback"/>
</div>
<DialogFooter>
    <DialogClose>
        <Slot>
            <Button Type="button" Variant="ButtonVariant.Outline" @attributes="context">
                Cancel
            </Button>
        </Slot>
    </DialogClose>
    <Button @onclick="OnSubmitAsync" Type="button">
        Submit
    </Button>
</DialogFooter>

@code
{
    private async Task OnSubmitAsync() => await Dialog.CloseAsync();
}
```

### Close on backdrop click and hide the close button

Adapted from `ShadcnBlazor.Test/ExtraShowcases/DialogSc1.razor`: `onConfigure` adjusts the `DialogModel` before the dialog is rendered, so the backdrop dismisses the dialog and the top-right `X` button is hidden (`ShowCloseButton` defaults to `true`).

```razor
@using ShadcnBlazor.Buttons
@using ShadcnBlazor.Extras.Dialogs

@inject DialogService DialogService

<Button @onclick="LaunchAsync" Variant="ButtonVariant.Outline">Dismissable dialog</Button>
<DialogLauncher/>

@code
{
    private async Task LaunchAsync() => await DialogService.LaunchAsync<ExampleDialog>(onConfigure: model =>
    {
        model.CloseOnBackdropClick = true;
        model.ShowCloseButton = false;
    });
}
```

### Launch and close a dialog from outside

`LaunchAsync` returns the `DialogModel`; keep it and hand it to the launcher's `CloseAsync(DialogModel)` to close the dialog from code (the model's `Dialog` property is also the underlying core dialog).

```razor
@using ShadcnBlazor.Buttons
@using ShadcnBlazor.Extras.Dialogs

@inject DialogService DialogService

<Button @onclick="LaunchAsync" Variant="ButtonVariant.Outline">Launch dialog</Button>
<Button @onclick="CloseAsync" @disabled="Model is null">Close it</Button>
<DialogLauncher @ref="Launcher"/>

@code
{
    private DialogLauncher Launcher;
    private DialogModel Model;

    private async Task LaunchAsync() => Model = await DialogService.LaunchAsync<ExampleDialog>();

    private Task CloseAsync() => Launcher.CloseAsync(Model);
}
```
