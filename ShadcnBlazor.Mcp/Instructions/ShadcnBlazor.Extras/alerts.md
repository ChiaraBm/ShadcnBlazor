---
title: Alert
description: Programmatic alerts via AlertService rendered by AlertLauncher: info, success, warning and error designs.
tags: [alerts, launcher, alertservice, transient, notifications, extras]
---

# Alert

A service-driven transient alert system from `ShadcnBlazor.Extras`: you render an `<AlertLauncher>` somewhere in the UI, then inject `AlertService` and call `InfoAsync`, `SuccessAsync`, `WarningAsync` or `ErrorAsync` to stack alerts above the launcher, each with a dismiss button. The four built-in designs (`InfoAlert`, `SuccessAlert`, `WarningAlert`, `ErrorAlert`) are color-coded via Tailwind classes with Lucide icons and a title/description layout. Alerts can auto-close after a configurable delay, and fully custom designs are supported by inheriting `AlertBase` and launching them with `LaunchCustomAsync<T>`.

## Usage

```razor
@using ShadcnBlazor.Buttons
@using ShadcnBlazor.Extras.Alerts

@inject AlertService AlertService

<AlertLauncher />

<Button @onclick="ShowInfoAlert">Show alert</Button>

@code
{
    private Task ShowInfoAlert()
        => AlertService.InfoAsync("Heads up!", "This is an info alert.");
}
```

`AlertService` is registered as scoped by `AddShadcnBlazorExtras()` on the service collection. Exactly one `<AlertLauncher />` should be placed per page (or per app layout); alerts stack vertically above it in a `flex flex-col` container.

## Parameters

| Name | Type | Default | Description |
|---|---|---|---|
| `Model` | `AlertModel` | `null` | `AlertBase` (inherited by every design). The alert model carrying `ComponentType` and `Attributes`; the built-in dismiss button closes the alert through `AlertService.CloseAsync(Model)`. |
| `Title` | `string` | `null` | The alert title text; rendered in the `<AlertTitle>` of every built-in design (`InfoAlert`, `SuccessAlert`, `WarningAlert`, `ErrorAlert`). |
| `Description` | `string` | `null` | The alert body text; rendered in the `<AlertDescription>` of every built-in design. |
| `ClassName` | `string` | `null` | `AlertLauncher` (inherited from `ShadcnBase`). Tailwind classes merged via `Cn(...)` with the launcher's default `flex flex-col w-full gap-y-2.5 mb-5` stack classes. |
| `Props` | `Dictionary<string, object>?` | `null` | `AlertLauncher` (inherited from `ShadcnBase`, `CaptureUnmatchedValues = true`). Unmatched attributes applied to the launcher wrapper `div` via `@attributes="Props"`. |

## Examples

Show all four built-in designs; alerts remain until dismissed by the user:

```razor
@using ShadcnBlazor.Buttons
@using ShadcnBlazor.Extras.Alerts

@inject AlertService AlertService

<AlertLauncher />

<div class="flex flex-wrap gap-2">
    <Button Variant="ButtonVariant.Outline" @onclick="() => AlertService.InfoAsync("Info", "An informational message.")">Info</Button>
    <Button Variant="ButtonVariant.Outline" @onclick="() => AlertService.SuccessAsync("Saved", "Your changes were stored.")">Success</Button>
    <Button Variant="ButtonVariant.Outline" @onclick="() => AlertService.WarningAsync("Warning", "Review before continuing.")">Warning</Button>
    <Button Variant="ButtonVariant.Outline" @onclick="() => AlertService.ErrorAsync("Failed", "The request could not be completed.")">Error</Button>
</div>
```

Auto-hiding alerts: pass `hideDelay` in milliseconds and the alert closes itself after that delay:

```razor
@using ShadcnBlazor.Buttons
@using ShadcnBlazor.Extras.Alerts

@inject AlertService AlertService

<AlertLauncher />

<Button @onclick="ShowToastLikeAlert">Delete</Button>

@code
{
    private Task ShowToastLikeAlert()
        => AlertService.SuccessAsync("Deleted", "The item was removed.", hideDelay: 4000);
}
```

Programmatically closing an alert: every launch method returns the created `AlertModel`, which can be closed later via `AlertService.CloseAsync(model)`:

```razor
@using ShadcnBlazor.Buttons
@using ShadcnBlazor.Extras.Alerts

@inject AlertService AlertService

<AlertLauncher />

<Button @onclick="ShowLongTaskAlert">Start task</Button>

@code
{
    private AlertModel? ActiveAlert;

    private async Task ShowLongTaskAlert()
    {
        ActiveAlert = await AlertService.InfoAsync("Working...", "The background task has started.");

        await Task.Delay(5000); // simulate work

        if (ActiveAlert != null)
        {
            await AlertService.CloseAsync(ActiveAlert);
            ActiveAlert = null;
        }
    }
}
```

Custom alert designs: create your own component that inherits `AlertBase`, then launch it with `LaunchCustomAsync<T>` and set parameters through the attribute callback. The custom design below (`CustomAlert.razor`, in the same folder as the page) follows the built-in layouts and keeps the dismiss button working via the inherited `CloseAsync`:

```razor
@using LucideBlazor
@using ShadcnBlazor.Alerts
@using ShadcnBlazor.Buttons

@inherits AlertBase

<Alert ClassName="w-full flex flex-row items-center gap-3 border-violet-500/80 bg-violet-500/5 text-violet-500">
    <div class="flex shrink-0 items-center">
        <InfoIcon ClassName="size-6 text-violet-500/60" />
    </div>
    <div class="flex flex-1 items-center justify-between gap-4">
        <div class="flex flex-col gap-0.5">
            <AlertTitle>@Title</AlertTitle>
            <AlertDescription ClassName="text-violet-500/80">
                @Description
            </AlertDescription>
        </div>
        <div class="flex items-center shrink-0 gap-2">
            <Button @onclick="CloseAsync" Size="ButtonSize.Sm" Variant="ButtonVariant.Outline">
                Dismiss
            </Button>
        </div>
    </div>
</Alert>

@code
{
    [Parameter] public string Title { get; set; }
    [Parameter] public string Description { get; set; }
}
```

The page that launches it:

```razor
@using ShadcnBlazor.Buttons
@using ShadcnBlazor.Extras.Alerts

@inject AlertService AlertService

<AlertLauncher />

<Button @onclick="ShowCustom">Launch custom alert</Button>

@code
{
    private Task ShowCustom()
        => AlertService.LaunchCustomAsync<CustomAlert>(attributes =>
        {
            attributes[nameof(CustomAlert.Title)] = "Heads up!";
            attributes[nameof(CustomAlert.Description)] = "A design built on AlertBase.";
        }, hideDelay: 5000);
}
```
