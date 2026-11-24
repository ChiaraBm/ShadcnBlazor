---
title: Sonner
description: Toast notification item components: Sonner, SonnerHeader, SonnerTitle, and SonnerDescription for composing toasts.
tags: [sonner, toast, notification, alert, feedback]
---

# Sonner

A set of building blocks for composing toast notification items (`<li>` elements intended for a toast list). `<Sonner>` renders a self-contained toast with `tabindex="0"` for keyboard focus; `<SonnerHeader>`, `<SonnerTitle>`, and `<SonnerDescription>` lay out the inner content — a vertical header, a semibold title, and free-form description text. Use these when building custom toast/notification popups. Each component inherits from `ShadcnBase`, so `ClassName` merges extra Tailwind classes into the computed class list and `Props` collects unmatched attributes that are spread onto the rendered element (`@attributes="Props"`).

## Usage

```razor
@using ShadcnBlazor.Sonners

<Sonner>
    <SonnerHeader>
        <SonnerTitle>Payment successful</SonnerTitle>
        <SonnerDescription>Thanks for your business.</SonnerDescription>
    </SonnerHeader>
</Sonner>
```

## Parameters

### Sonner

| Name | Type | Default | Description |
|---|---|---|---|
| `ChildContent` | `RenderFragment` | `null` | The toast content, typically a `<SonnerHeader>` containing a `<SonnerTitle>` and optional `<SonnerDescription>`; rendered inside an `<li tabindex="0">` with `bg-popover rounded-lg py-3.5 px-5 border flex flex-row items-center justify-between w-fit text-sm gap-x-3.5`. |

### SonnerHeader

| Name | Type | Default | Description |
|---|---|---|---|
| `ChildContent` | `RenderFragment` | `null` | The header content, usually a `<SonnerTitle>` and `<SonnerDescription>`; rendered inside a `<div>` with `flex flex-col`. |

### SonnerTitle

| Name | Type | Default | Description |
|---|---|---|---|
| `ChildContent` | `RenderFragment` | `null` | The toast title text; rendered inside a `<div>` with `font-semibold`. |

### SonnerDescription

| Name | Type | Default | Description |
|---|---|---|---|
| `ChildContent` | `RenderFragment` | `null` | The toast body text; rendered inside a `<div>` with whatever classes `ClassName` provides. |

All components inherit from `ShadcnBase`, so the base parameters are also available. `ClassName` (a `string`) is merged into the computed classes, and `Props` (a `Dictionary<string, object>?` with `CaptureUnmatchedValues = true`) collects unmatched attributes that are applied to the rendered element via `@attributes="Props"`.

## Examples

A simple success toast:

```razor
@using ShadcnBlazor.Sonners

<Sonner>
    <SonnerHeader>
        <SonnerTitle>Success</SonnerTitle>
        <SonnerDescription>Your changes have been saved.</SonnerDescription>
    </SonnerHeader>
</Sonner>
```

A title-only toast:

```razor
@using ShadcnBlazor.Sonners

<Sonner>
    <SonnerHeader>
        <SonnerTitle>Uploading…</SonnerTitle>
    </SonnerHeader>
</Sonner>
```

A destructive toast with a custom width and an inline action button:

```razor
@using ShadcnBlazor.Sonners

<Sonner ClassName="w-full max-w-lg text-destructive">
    <SonnerHeader>
        <SonnerTitle>Something went wrong</SonnerTitle>
        <SonnerDescription>Unable to connect to the server.</SonnerDescription>
    </SonnerHeader>
    <button type="button" class="shrink-0 rounded-md px-2 py-1 text-sm hover:bg-black/5">
        Retry
    </button>
</Sonner>
```

A list of multiple toasts rendered together:

```razor
@using ShadcnBlazor.Sonners

<ul class="flex flex-col gap-2">
    <Sonner>
        <SonnerHeader>
            <SonnerTitle>Payment successful</SonnerTitle>
            <SonnerDescription>Your invoice was processed.</SonnerDescription>
        </SonnerHeader>
    </Sonner>
    <Sonner>
        <SonnerHeader>
            <SonnerTitle>New message</SonnerTitle>
            <SonnerDescription>Chiara sent you a message.</SonnerDescription>
        </SonnerHeader>
    </Sonner>
</ul>
```
