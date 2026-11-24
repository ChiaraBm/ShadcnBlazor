---
title: Alert
description: Inline callout for status messages, with title and description subcomponents and Default or Destructive variants.
tags: [alert, notification, message, status, callout]
---

# Alert

An inline callout (`div` with `role="alert"`) used to surface important status messages — errors, warnings, payment confirmations — inside a page. It is composed of `<AlertTitle>` and optional `<AlertDescription>` subcomponents, and its grid layout automatically shifts content when a direct child SVG icon is present. Two visual variants are available via `AlertVariant` (`Default` and `Destructive`); colored look-and-feel (e.g. blue "info" or amber "warning") is added through the inherited `ClassName` parameter with Tailwind classes. Inherited from `ShadcnBase`, the component also accepts a `Props` dictionary to merge additional attributes into the rendered element.

## Usage

```razor
@using ShadcnBlazor.Alerts

<Alert>
    <AlertTitle>Heads up!</AlertTitle>
    <AlertDescription>Remember to check the documentation.</AlertDescription>
</Alert>
```

## Parameters

### Alert

| Name | Type | Default | Description |
|---|---|---|---|
| `Variant` | `AlertVariant` | `AlertVariant.Default` | Visual style of the alert. Values: `Default = 0` (`bg-card text-card-foreground`), `Destructive = 1` (destructive text color). |
| `ChildContent` | `RenderFragment` | `null` | The inner content of the alert, typically an `<AlertTitle>` and an optional `<AlertDescription>`. |

### AlertTitle

| Name | Type | Default | Description |
|---|---|---|---|
| `ChildContent` | `RenderFragment` | `null` | The title text of the alert; rendered with `font-medium tracking-tight line-clamp-1`. |

### AlertDescription

| Name | Type | Default | Description |
|---|---|---|---|
| `ChildContent` | `RenderFragment` | `null` | The body text of the alert; rendered with muted foreground and relaxed line height. |

The components inherit from `ShadcnBase`, so the base parameters are also available. `ClassName` (a `string`) is merged into the computed classes, and `Props` (a `Dictionary<string, object>?` with `CaptureUnmatchedValues = true`) collects unmatched attributes that are applied to the rendered element — only `Alert` applies `Props` to its markup (`@attributes="Props"`).

## Examples

Info-style alert using custom Tailwind classes via `ClassName`:

```razor
@using ShadcnBlazor.Alerts

<Alert ClassName="w-full max-w-lg border-blue-600/80 bg-blue-600/5 text-blue-600">
    <AlertTitle>Heads up</AlertTitle>
</Alert>
```

Error alert with a destructive border and background:

```razor
@using ShadcnBlazor.Alerts

<Alert ClassName="w-full max-w-lg border-destructive/80 bg-destructive/5 text-destructive">
    <AlertTitle>Something went wrong</AlertTitle>
</Alert>
```

Warning alert with a description:

```razor
@using ShadcnBlazor.Alerts

<Alert ClassName="w-full max-w-lg border-amber-600/80 bg-amber-600/5 text-amber-600">
    <AlertTitle>Heads up</AlertTitle>
    <AlertDescription ClassName="text-amber-600/80">
        Deleting this workspace will permanently remove all of its contents. This cannot be undone.
    </AlertDescription>
</Alert>
```

Alert with an action row, using the grid layout to place buttons opposite the title:

```razor
@using ShadcnBlazor.Alerts
@using ShadcnBlazor.Buttons

<Alert ClassName="w-full max-w-lg">
    <div class="col-span-2 flex items-center justify-between gap-4">
        <div class="flex flex-col gap-0.5">
            <AlertTitle>Payment complete</AlertTitle>
            <AlertDescription>
                Your invoice has been processed and a receipt was sent to your email.
            </AlertDescription>
        </div>
        <div class="flex shrink-0 gap-2">
            <Button Size="ButtonSize.Sm" Variant="ButtonVariant.Default">
                View receipt
            </Button>
            <Button Size="ButtonSize.Sm" Variant="ButtonVariant.Outline">
                Close
            </Button>
        </div>
    </div>
</Alert>
```
