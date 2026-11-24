---
title: Toast
description: Toast notifications via ToastService and ToastLauncher: info, success, warning, error and progress designs built on Sonner.
tags: [toasts, sonner, toastservice, notification, launcher, progress]
---

# Toast

A service-driven toast notification system from `ShadcnBlazor.Extras`: you render a `<ToastLauncher />` somewhere in the UI (typically the app layout), then inject `ToastService` and call `InfoAsync`, `SuccessAsync`, `WarningAsync`, `ErrorAsync` or `ProgressAsync` to stack toasts in a fixed, centered column at the top of the page. Each toast is rendered inside a `<Sonner>` element with slide-in/out animations, and auto-closes after a configurable `hideDelay` (5000 ms by default). Five built-in designs (`InfoToast`, `SuccessToast`, `WarningToast`, `ErrorToast`, `ProgressToast`) are provided; `ProgressToast` shows a spinner and invokes a callback with the toast instance so you can update its title/description before it closes, and fully custom designs are supported by inheriting `ToastBase` and launching them with `LaunchCustomAsync<T>`.

## Usage

```razor
@using ShadcnBlazor.Buttons
@using ShadcnBlazor.Extras.Toasts

@inject ToastService ToastService

<ToastLauncher />

<Button @onclick="ShowToast">Show toast</Button>

@code
{
    private Task ShowToast()
        => ToastService.InfoAsync("Heads up!", "This is a toast.");
}
```

`ToastService` is registered as scoped by `AddShadcnBlazorExtras()` on the service collection. Place exactly one `<ToastLauncher />` per layout; it anchors the fixed, centered toast stack at the top of the page (`fixed top-0 left-1/2 -translate-x-1/2`).

## Parameters

| Name | Type | Default | Description |
|---|---|---|---|
| `Model` | `ToastModel` | `null` | `ToastBase` (inherited by every design). The toast model carrying `ComponentType`, `Attributes` and `IsOpen`; the launcher sets it on each toast and `CloseAsync` calls `ToastService.CloseAsync(Model)`. |
| `Title` | `string` | `null` | The toast title text; rendered in the `<SonnerTitle>` of every built-in design (`InfoToast`, `SuccessToast`, `WarningToast`, `ErrorToast`, `ProgressToast`). |
| `Description` | `string` | `null` | The toast body text; rendered in the `<SonnerDescription>` of every built-in design. |
| `Callback` | `Func<ProgressToast, Task>` | `null` | `ProgressToast` only. A callback invoked with the toast instance on first render, allowing you to call `UpdateAsync` to change the title/description; the toast closes itself once the callback completes. |

## Examples

Show all four built-in designs; each toast auto-closes after 5 seconds:

```razor
@using ShadcnBlazor.Buttons
@using ShadcnBlazor.Extras.Toasts

@inject ToastService ToastService

<ToastLauncher />

<div class="flex flex-wrap gap-2">
    <Button Variant="ButtonVariant.Outline" @onclick="() => ToastService.InfoAsync("Info", "An informational message.")">Info</Button>
    <Button Variant="ButtonVariant.Outline" @onclick="() => ToastService.SuccessAsync("Saved", "Your changes were stored.")">Success</Button>
    <Button Variant="ButtonVariant.Outline" @onclick="() => ToastService.WarningAsync("Warning", "Review before continuing.")">Warning</Button>
    <Button Variant="ButtonVariant.Outline" @onclick="() => ToastService.ErrorAsync("Failed", "The request could not be completed.")">Error</Button>
</div>
```

A progress toast with a callback that updates its content before it auto-closes — the spinner is shown for the duration of the callback:

```razor
@using ShadcnBlazor.Buttons
@using ShadcnBlazor.Extras.Toasts
@using ShadcnBlazor.Extras.Toasts.Designs

@inject ToastService ToastService

<ToastLauncher />

<Button @onclick="Upload">Upload</Button>

@code
{
    private async Task Upload()
        => await ToastService.ProgressAsync("Uploading…", "Starting the transfer.", async toast =>
        {
            await Task.Delay(2000);
            await toast.UpdateAsync("Uploading…", "60% complete.");

            await Task.Delay(2000);
            await toast.UpdateAsync("Done", "The transfer finished.");
        });
}
```

Programmatically closing a toast: every launch method returns the created `ToastModel`, which can be closed later via `ToastService.CloseAsync(model)` — pass `hideDelay: -1` to disable auto-close:

```razor
@using ShadcnBlazor.Buttons
@using ShadcnBlazor.Extras.Toasts

@inject ToastService ToastService

<ToastLauncher />

<Button @onclick="StartTask">Start task</Button>

@code
{
    private ToastModel? ActiveToast;

    private async Task StartTask()
    {
        ActiveToast = await ToastService.InfoAsync("Working…", "The background task has started.", hideDelay: -1);

        await Task.Delay(5000); // simulate work

        if (ActiveToast != null)
        {
            await ToastService.CloseAsync(ActiveToast);
            ActiveToast = null;
        }
    }
}
```

A custom toast design: create your own component that inherits `ToastBase`, then launch it with `LaunchCustomAsync<T>` and set parameters through the attribute callback. The custom design below (`CustomToast.razor`, in the same folder as the page) follows the built-in layouts:

```razor
@using LucideBlazor
@using ShadcnBlazor.Sonners

@inherits ToastBase

<div>
    <div class="flex size-8 items-center justify-center rounded-full bg-violet-100 dark:bg-violet-900">
        <BellIcon ClassName="size-4 text-violet-600 dark:text-violet-400" />
    </div>
</div>

<SonnerHeader>
    <SonnerTitle>@Title</SonnerTitle>
    <SonnerDescription>@Description</SonnerDescription>
</SonnerHeader>

@code
{
    [Parameter] public string Title { get; set; }
    [Parameter] public string Description { get; set; }
}
```

The page that launches it:

```razor
@using ShadcnBlazor.Buttons
@using ShadcnBlazor.Extras.Toasts

@inject ToastService ToastService

<ToastLauncher />

<Button @onclick="ShowCustom">Launch custom toast</Button>

@code
{
    private Task ShowCustom()
        => ToastService.LaunchCustomAsync<CustomToast>(attributes =>
        {
            attributes[nameof(CustomToast.Title)] = "Heads up!";
            attributes[nameof(CustomToast.Description)] = "A design built on ToastBase.";
        }, hideDelay: 5000);
}
```
