---
title: Spinner
description: A small, continuously rotating loading indicator rendered with the Lucide LoaderCircle icon and Tailwind animate-spin.
tags: [spinner, loading, indicator, icon, lucide, animation]
---

# Spinner

Spinner is a lightweight loading indicator. Use it whenever content is being fetched or a long-running operation is in progress, to signal that the application is working. It renders the Lucide `LoaderCircleIcon` and applies the `size-4` and `animate-spin` Tailwind classes by default. The default classes are merged with any `ClassName` you supply, so the indicator can be resized or restyled without losing its animation.

## Usage

```razor
<Spinner />
```

## Parameters

The component inherits its parameters from `ShadcnBase`.

| Name | Type | Default | Description |
|------|------|---------|-------------|
| `ClassName` | `string` | `null` | Additional Tailwind CSS classes merged with the built-in `size-4 animate-spin` classes via `TailwindMerge`. Use it to resize or restyle the spinner. |
| `Props` | `Dictionary<string, object>?` | `null` | Captures any unmatched attributes passed to the component (from `ShadcnBase`). |

## Examples

Basic usage, which renders a 4-unit spinner that rotates continuously.

```razor
<Spinner />
```

Resize the spinner by supplying a different Tailwind size class. The class is merged with the default classes, so the animation is preserved.

```razor
<Spinner ClassName="size-8" />
```

Combined usage inside a loading state, alongside descriptive text.

```razor
<div class="flex items-center gap-2">
    <Spinner />
    <span>Loading…</span>
</div>
```
