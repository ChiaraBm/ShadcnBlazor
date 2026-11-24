---
title: Marker
description: Displays a labeled status or progress marker row with an optional icon and separator or border variants.
tags: [marker, status, progress, icon, variant, separator]
---

# Marker

A Marker is a compact, single-line row that surfaces status, progress, or event messages (like a task runner or git console output). It pairs a small `MarkerIcon` with a `MarkerContent` text, and supports a `Separator` or `Border` visual variant to group consecutive markers. Inherited from `ShadcnBase`, the component also accepts `ClassName` for extra classes and a `Props` dictionary to merge additional attributes (such as `role`) into the rendered `<div>` with `data-slot="marker"`.

## Usage

```razor
@using ShadcnBlazor.Markers
@using LucideBlazor

<Marker>
    <MarkerIcon>
        <GitBranchIcon />
    </MarkerIcon>
    <MarkerContent>Checked out feature/chat</MarkerContent>
</Marker>
```

## Parameters

| Name | Type | Default | Description |
|---|---|---|---|
| `ChildContent` | `RenderFragment` | `null` | The inner content of the marker. Declared identically in `Marker`, `MarkerContent`, and `MarkerIcon`. |
| `Slot` | `RenderFragment<Dictionary<string, object?>>?` | `null` | On `Marker` only. When set, this template is invoked with the computed attributes instead of rendering the default `<div>`; use it to wrap the marker in a different element (e.g. an `<a>`). |
| `Variant` | `MarkerVariant` | `MarkerVariant.Default` | On `Marker` only. Visual style of the marker. Values: `Default = 0`, `Separator = 1`, `Border = 2`. |

The components inherit from `ShadcnBase`, so the base parameters are also available. `ClassName` is a `string` appended to the computed classes, and `Props` is a `Dictionary<string, object>?` (captured via unmatched attributes) that is merged into the rendered element's attributes.

## Examples

Marker with an icon:

```razor
@using ShadcnBlazor.Markers
@using LucideBlazor

<Marker>
    <MarkerIcon>
        <GitBranchIcon />
    </MarkerIcon>
    <MarkerContent>Checked out feature/chat</MarkerContent>
</Marker>
```

Loading marker with a spinner, the `shimmer` class on the content, and the `role` attribute:

```razor
@using ShadcnBlazor.Markers
@using ShadcnBlazor.Spinners

<Marker role="status">
    <MarkerIcon>
        <Spinner />
    </MarkerIcon>
    <MarkerContent ClassName="shimmer">Compiling the solution...</MarkerContent>
</Marker>
```

Separator variant to group markers with a horizontal rule:

```razor
@using ShadcnBlazor.Markers

<Marker Variant="MarkerVariant.Separator">
    <MarkerContent>Build finished</MarkerContent>
</Marker>
```

Full showcase combining all variants:

```razor
@using LucideBlazor
@using ShadcnBlazor.Markers
@using ShadcnBlazor.Spinners

<div class="flex w-full max-w-sm flex-col gap-8 py-12">
    <Marker>
        <MarkerIcon>
            <GitBranchIcon />
        </MarkerIcon>
        <MarkerContent>Checked out feature/chat</MarkerContent>
    </Marker>
    <Marker role="status">
        <MarkerIcon>
            <Spinner />
        </MarkerIcon>
        <MarkerContent ClassName="shimmer">Compiling the solution...</MarkerContent>
    </Marker>
    <Marker Variant="MarkerVariant.Separator">
        <MarkerContent>Build finished</MarkerContent>
    </Marker>
    <Marker>
        <MarkerIcon>
            <FileSearchIcon />
        </MarkerIcon>
        <MarkerContent>Scanned 12 files</MarkerContent>
    </Marker>
</div>
```
