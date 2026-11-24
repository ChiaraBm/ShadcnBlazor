---
title: Empty
description: A centered empty-state surface with composable header, icon, title, description, and content regions shown when no data is available.
tags: [empty, empty-state, placeholder, no-results, content]
---

# Empty

`Empty` is a centered, dashed-border container that fills whatever space is available and presents a friendly message when there is no data to show — an empty list, a search with no results, or a first-run experience. It is built from a set of small, composable primitives: `EmptyHeader`, `EmptyMedia`, `EmptyTitle`, `EmptyDescription`, and `EmptyContent`, which you stack inside the parent `Empty` to assemble the full empty state. `EmptyMedia` accepts a `Variant` of `Default` or `Icon`; the `Icon` variant wraps its child in a muted, rounded square that automatically sizes an inner `<svg>` icon, which is the pattern used to place a `SearchIcon`-style glyph above the title.

## Usage

```razor
<Empty>
    <EmptyHeader>
        <EmptyTitle>No results</EmptyTitle>
        <EmptyDescription>Your search returned no results.</EmptyDescription>
    </EmptyHeader>
    <EmptyContent>
        Try a different search term or filter.
    </EmptyContent>
</Empty>
```

## Parameters

`Empty`, `EmptyHeader`, `EmptyMedia`, `EmptyContent`, `EmptyDescription`, and `EmptyTitle` all inherit `ClassName` and `Props` from `ShadcnBase`. Every component also declares its own `ChildContent`. `EmptyMedia` additionally exposes a `Variant` of type `EmptyMediaVariant` (values: `Default`, `Icon`). There are no event callbacks or cascading parameters in the Empty set. The rows below list one `[Parameter]` per component. Note that `Props` is applied to the root `<div>` only on `Empty`, `EmptyHeader`, and `EmptyMedia` (the other three declare it but do not spread `@attributes`).

| Name | Type | Default | Description |
| --- | --- | --- | --- |
| ChildContent (Empty) | RenderFragment | null | The root's content. Wrap `EmptyHeader`, `EmptyContent`, and any other primitives inside. |
| ClassName (Empty) | string | null | Extra CSS classes merged with the base `flex min-w-0 flex-1 flex-col items-center justify-center gap-6 rounded-lg border-dashed p-6 text-center text-balance md:p-12`. |
| Props (Empty) | Dictionary<string, object>? | null | Captured unmatched attributes applied to the `<div data-slot="empty">`. |
| ChildContent (EmptyHeader) | RenderFragment | null | The header's content. Wrap `EmptyMedia`, `EmptyTitle`, and `EmptyDescription` inside. |
| ClassName (EmptyHeader) | string | null | Extra CSS classes merged with the base `flex max-w-sm flex-col items-center gap-2 text-center`. |
| Props (EmptyHeader) | Dictionary<string, object>? | null | Captured unmatched attributes applied to the `<div data-slot="empty-header">`. |
| ChildContent (EmptyMedia) | RenderFragment | null | The media content — typically an icon when `Variant` is `Icon`. |
| Variant (EmptyMedia) | EmptyMediaVariant | EmptyMediaVariant.Default | `Default` renders a transparent, size-agnostic container; `Icon` renders a muted rounded `size-10` square that sizes a child `<svg>` to `size-6`. |
| ClassName (EmptyMedia) | string | null | Extra CSS classes merged with the base `flex shrink-0 items-center justify-center mb-2 [&_svg]:pointer-events-none [&_svg]:shrink-0` plus the variant classes. |
| Props (EmptyMedia) | Dictionary<string, object>? | null | Captured unmatched attributes applied to the `<div data-slot="empty-icon">`. |
| ChildContent (EmptyTitle) | RenderFragment | null | The heading text for the empty state. |
| ClassName (EmptyTitle) | string | null | Extra CSS classes merged with the base `text-lg font-medium tracking-tight`. |
| Props (EmptyTitle) | Dictionary<string, object>? | null | Inherited from `ShadcnBase`; captured but not spread onto the `<div data-slot="empty-title">`. |
| ChildContent (EmptyDescription) | RenderFragment | null | The supplementary text under the title; child `<a>` elements are styled with an underline that turns primary on hover. |
| ClassName (EmptyDescription) | string | null | Extra CSS classes merged with the base `text-muted-foreground [&>a:hover]:text-primary text-sm/relaxed [&>a]:underline [&>a]:underline-offset-4`. |
| Props (EmptyDescription) | Dictionary<string, object>? | null | Inherited from `ShadcnBase`; captured but not spread onto the `<div data-slot="empty-description">`. |
| ChildContent (EmptyContent) | RenderFragment | null | The main body content of the empty state (often an action or call to action). |
| ClassName (EmptyContent) | string | null | Extra CSS classes merged with the base `flex w-full max-w-sm min-w-0 flex-col items-center gap-4 text-sm text-balance`. |
| Props (EmptyContent) | Dictionary<string, object>? | null | Inherited from `ShadcnBase`; captured but not spread onto the `<div data-slot="empty-content">`. |

## Examples

### Empty state with icon, title, and description

The `Icon` variant of `EmptyMedia` wraps an icon in a muted rounded square and sizes any inner `<svg>` to `size-6`. This mirrors the pattern used by the built-in data grid.

```razor
<Empty>
    <EmptyHeader>
        <EmptyMedia Variant="EmptyMediaVariant.Icon">
            <SearchIcon/>
        </EmptyMedia>
        <EmptyTitle>No items</EmptyTitle>
        <EmptyDescription>
            There are no items in this list yet.
        </EmptyDescription>
    </EmptyHeader>
</Empty>
```

### Full empty state with a content region

`EmptyContent` adds a centered body below the header, which is the right place for a call to action or an explanatory note.

```razor
<Empty>
    <EmptyHeader>
        <EmptyMedia Variant="EmptyMediaVariant.Icon">
            <SearchIcon/>
        </EmptyMedia>
        <EmptyTitle>No results</EmptyTitle>
        <EmptyDescription>Your search returned no results.</EmptyDescription>
    </EmptyHeader>
    <EmptyContent>
        Try a different search term or clear the filters to see all items.
    </EmptyContent>
</Empty>
```

### Empty state with an actionable description link

`EmptyDescription` styles any child `<a>` with an underline that turns primary on hover, so a "Browse all" or "Reset" link reads as part of the description.

```razor
<Empty>
    <EmptyHeader>
        <EmptyTitle>Nothing here yet</EmptyTitle>
        <EmptyDescription>
            Start by <a href="#">adding your first entry</a>.
        </EmptyDescription>
    </EmptyHeader>
    <EmptyContent>
        Once you add an entry it will appear in this view.
    </EmptyContent>
</Empty>
```

`Empty` uses `flex-1` and `justify-center`, so when it is placed inside a fixed-height or grid cell (as the data grid does by wrapping it in a full-width, centered container) it vertically and horizontally centers the entire empty state within the available space.
