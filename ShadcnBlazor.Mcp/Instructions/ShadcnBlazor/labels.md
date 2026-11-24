---
title: Label
description: Accessible form field label rendered as a <label> element with Tailwind styling, for pairing with form inputs.
tags: [form, label, accessibility, input, text]
---

# Label

The Label component renders an accessible HTML `<label>` element with `data-slot="label"` and the standard Tailwind styling used across form controls (medium font, tight line height, non-selectable text). It is used to provide a visible, accessible name for form inputs such as text fields, checkboxes, radios and switches. Notable features: extra styling can be added through `ClassName` (merged with twMerge), any unmatched attributes (for example `for` or `id`) are forwarded to the underlying `<label>` via `Props`, and the label dims and disables pointer events when it sits inside a disabled group or is paired with a disabled peer input.

## Usage

```razor
<Label>Email</Label>
```

## Parameters

| Name | Type | Default | Description |
|---|---|---|---|
| `ChildContent` | `RenderFragment` | — | The text or markup displayed inside the `<label>` element. |
| `ClassName` | `string` | — | Additional Tailwind CSS classes, merged with the component's default classes via twMerge (inherited from `ShadcnBase`). |
| `Props` | `Dictionary<string, object>?` | — | Captures unmatched attributes (`CaptureUnmatchedValues = true`) and forwards them to the `<label>` element via `@attributes` (inherited from `ShadcnBase`). |

## Examples

### Basic label with an input

```razor
<Label for="username">Username</Label>
<input id="username" type="text" class="mt-1" />
```

### Custom styling with ClassName

```razor
<Label ClassName="text-red-500 text-base">Required field</Label>
```

### Label with extra attributes forwarded via Props

```razor
<Label id="email-label" title="Your work email">Work email</Label>
```

### Label inside a disabled group

The label inherits `pointer-events-none` and `opacity-50` when a parent has the `group-data-[disabled=true]` pattern, e.g. a disabled switch group:

```razor
<div class="group" data-disabled="true">
    <Label>Status</Label>
    <input type="checkbox" class="peer" />
</div>
```
