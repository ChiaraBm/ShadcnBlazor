---
title: Avatar
description: Displays a user avatar image with a fallback that is shown only until the image finishes loading.
tags: [avatar, image, fallback, media, profile]
---

# Avatar

`Avatar` is a container that displays a user or account picture, with an optional `AvatarImage` that is revealed once it has finished loading. Until the image loads, an `AvatarFallback` is shown instead (typically initials or a placeholder). The component tracks loading state internally and swaps the fallback for the image automatically, so you never write that logic yourself. Use it whenever you need a person's or entity's profile picture with a graceful, dependency-free fallback.

## Usage

```razor
<Avatar>
    <AvatarImage src="https://github.com/shadcnblazor.png" />
    <AvatarFallback>CN</AvatarFallback>
</Avatar>
```

## Parameters

`Avatar`, `AvatarFallback`, and `AvatarImage` all inherit `ClassName` and `Props` from `ShadcnBase`. The component-specific parameters are listed below, one row per `[Parameter]` across the files in the folder.

| Name | Type | Default | Description |
| --- | --- | --- | --- |
| ChildContent (Avatar) | RenderFragment | null | The avatar's content. Wrap `AvatarImage` and `AvatarFallback` inside. Rendered within a `CascadingValue` that exposes the `Avatar` instance. |
| ClassName (Avatar) | string | null | Extra CSS classes merged with the base `relative flex size-8 shrink-0 overflow-hidden rounded-full`. |
| Props (Avatar) | Dictionary<string, object>? | null | Captured unmatched attributes applied to the wrapping `<div data-slot="avatar">`. |
| ChildContent (AvatarFallback) | RenderFragment | null | The fallback content (e.g. initials), shown until `Avatar.IsLoaded` is true. |
| Avatar (AvatarFallback) | Avatar | null | `[CascadingParameter]` reference to the parent `Avatar`, used to hide the fallback once the image loads. Resolved internally — do not set it. |
| ClassName (AvatarFallback) | string | null | Extra CSS classes merged with the base `bg-muted flex size-full items-center justify-center rounded-full`. |
| Props (AvatarFallback) | Dictionary<string, object>? | null | Captured unmatched attributes applied to the fallback `<div data-slot="avatar-fallback">`. |
| Avatar (AvatarImage) | Avatar | null | `[CascadingParameter]` reference to the parent `Avatar`, used to report that the image has loaded. Resolved internally — do not set it. |
| ClassName (AvatarImage) | string | null | Extra CSS classes merged with the base `aspect-square size-full`. |
| Props (AvatarImage) | Dictionary<string, object>? | null | Captured unmatched attributes (e.g. `src`, `alt`) applied to the `<img data-slot="avatar-image">`. |

## Examples

### Image with an initials fallback

```razor
<Avatar>
    <AvatarImage src="https://github.com/shadcnblazor.png" alt="User" />
    <AvatarFallback>CN</AvatarFallback>
</Avatar>
```

The `AvatarFallback` (with its `bg-muted` background) is visible until the image's `onload` fires, after which the image is revealed in its place.

### Larger avatar via ClassName

```razor
<Avatar ClassName="size-16">
    <AvatarImage src="https://github.com/shadcnblazor.png" alt="User" />
    <AvatarFallback>
        <span class="text-lg">CN</span>
    </AvatarFallback>
</Avatar>
```

`ClassName` is merged with the default `size-8`, so the avatar (and its `size-full` children) grow together.

### Fallback only (no image)

```razor
<Avatar ClassName="size-12 bg-accent">
    <AvatarFallback>
        <span class="font-medium">JD</span>
    </AvatarFallback>
</Avatar>
```

When no `AvatarImage` is provided, `IsLoaded` never flips, so the fallback is always displayed — use this for users with no profile photo.
