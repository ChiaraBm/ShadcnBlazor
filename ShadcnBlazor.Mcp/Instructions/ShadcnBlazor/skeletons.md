---
title: Skeleton
description: A pulsing placeholder box used to show loading states while content is being fetched.
tags: [skeleton, loading, placeholder, pulse, shimmer]
---

# Skeleton

The Skeleton component renders a pulsing placeholder box (`bg-accent animate-pulse rounded-md`) that stands in for content while it is still loading, mirroring the shadcn/ui skeleton. Use it to build loading states for avatars, text lines, cards or any layout that has not finished fetching, so the UI does not flash an empty layout. Because it inherits from `ShadcnBase`, every instance accepts `ClassName` for size and shape overrides and passes through unmatched attributes via `Props`.

## Usage

```razor
<Skeleton ClassName="h-4 w-40" />
```

## Parameters

| Name | Type | Default | Description |
|------|------|---------|-------------|
| ChildContent | RenderFragment | null | The content rendered inside the placeholder box. |
| ClassName | string | null | Inherited from `ShadcnBase`. Extra CSS classes merged with the base classes (`bg-accent animate-pulse rounded-md`) via `Cn`. |
| Props | Dictionary<string, object>? | null | Inherited from `ShadcnBase`. Captures unmatched attributes and applies them to the root element. |

## Examples

A loading row with an avatar and text lines (adapted from the test showcase):

```razor
<div class="flex items-center space-x-4">
    <Skeleton ClassName="h-12 w-12 rounded-full" />
    <div class="space-y-2">
        <Skeleton ClassName="h-4 w-[250px]" />
        <Skeleton ClassName="h-4 w-[200px]" />
    </div>
</div>
```

A card-shaped placeholder using `ChildContent` and a custom size via `ClassName`:

```razor
<Skeleton ClassName="h-32 w-full">
    <div class="h-full w-full" />
</Skeleton>
```

Overriding the pulse look and passing an arbitrary attribute through `Props`:

```razor
<Skeleton ClassName="h-4 w-32 bg-muted" data-testid="skeleton-line" />
```
