---
title: Badge
description: Small pill-shaped badge for labels, statuses, counts and tags, with primary, secondary, destructive and outline variants.
tags: [badge, label, status, pill, variant]
---

# Badge

A small, pill-shaped `span` used to display labels, statuses, counts, or tags next to other content (for example "New", "Updated", "Draft"). It renders an inline-flex element with `data-slot="badge"` and is sized to fit its content (`w-fit whitespace-nowrap shrink-0`). Four visual variants are available via `BadgeVariant` (Primary, Secondary, Destructive, Outline), each with built-in hover styles when the badge wraps a link. Inherited from `ShadcnBase`, the component also accepts `ClassName` for extra classes and a `Props` dictionary to merge additional attributes into the rendered element.

## Usage

```razor
<ShadcnBadge>New</ShadcnBadge>
```

## Parameters

| Name | Type | Default | Description |
|---|---|---|---|
| `ChildContent` | `RenderFragment` | `null` | The inner content of the badge. |
| `Slot` | `RenderFragment<Dictionary<string, object?>>?` | `null` | When set, this template is invoked with the computed attributes instead of rendering the default `<span>`; use it to wrap the badge in a different element (e.g. an `<a>`). |
| `Variant` | `BadgeVariant` | `BadgeVariant.Primary` | Visual style of the badge. Values: `Primary = 0`, `Secondary = 1`, `Destructive = 2`, `Outline = 3`. |

The component inherits from `ShadcnBase`, so the base parameters (such as `ClassName` and `Props`) are also available. `ClassName` is appended to the variant classes, and `Props` (a `Dictionary<string, object?>`) is merged into the rendered attributes.

## Examples

Different variants:

```razor
<div class="flex items-center gap-2">
    <ShadcnBadge>Default</ShadcnBadge>
    <ShadcnBadge Variant="BadgeVariant.Secondary">Secondary</ShadcnBadge>
    <ShadcnBadge Variant="BadgeVariant.Destructive">Destructive</ShadcnBadge>
    <ShadcnBadge Variant="BadgeVariant.Outline">Outline</ShadcnBadge>
</div>
```

Badge wrapping a link, using the `Slot` template so the computed classes/attributes are applied to the anchor:

```razor
<ShadcnBadge Variant="BadgeVariant.Secondary">
    <Slot Context="attrs">
        <a href="https://example.com" @attributes="attrs">
            <SvgIcon Icon="Icons.Lucide.Link" Class="ml-1" />
            Learn more
        </a>
    </Slot>
</ShadcnBadge>
```

Badge with custom classes via the inherited `ClassName`:

```razor
<ShadcnBadge Variant="BadgeVariant.Outline" ClassName="px-3">
    Featured
</ShadcnBadge>
```

Custom element attributes via the inherited `Props` dictionary:

```razor
<ShadcnBadge Variant="BadgeVariant.Primary"
             Props="new Dictionary<string, object?> { { "title", "Status badge" } }">
    Active
</ShadcnBadge>
```
