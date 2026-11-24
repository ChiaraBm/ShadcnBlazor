---
title: Card
description: A composable card surface with header, title, description, content, footer, and action regions built from stacked primitives.
tags: [card, layout, header, footer, content, container]
---

# Card

`Card` is a bordered, rounded, shadowed container that organizes related content into a single visual unit. It is built from a set of small, composable primitives — `CardHeader`, `CardTitle`, `CardDescription`, `CardContent`, `CardFooter`, and `CardAction` — that you stack inside the parent `Card`. Use it for any self-contained block of information or forms, such as a profile summary, a dashboard tile, or a settings panel. The header uses a CSS grid so a `CardAction` can be pinned to the top-right while the title and description stack in the left column.

## Usage

```razor
<Card>
    <CardHeader>
        <CardTitle>Card Title</CardTitle>
        <CardDescription>Card Description</CardDescription>
    </CardHeader>
    <CardContent>
        Card Content
    </CardContent>
    <CardFooter>
        Card Footer
    </CardFooter>
</Card>
```

## Parameters

`Card`, `CardAction`, `CardHeader`, `CardTitle`, `CardDescription`, `CardContent`, and `CardFooter` all inherit `ClassName` and `Props` from `ShadcnBase`. Every component also declares its own `ChildContent`. There are no event callbacks, enums, or cascading parameters in the Card set. The rows below list one `[Parameter]` per component.

| Name | Type | Default | Description |
| --- | --- | --- | --- |
| ChildContent (Card) | RenderFragment | null | The card's content. Wrap the `CardHeader`, `CardContent`, `CardFooter`, and any `CardAction` inside. |
| ClassName (Card) | string | null | Extra CSS classes merged with the base `bg-card text-card-foreground flex flex-col gap-6 rounded-xl border py-6 shadow-sm`. |
| Props (Card) | Dictionary<string, object>? | null | Captured unmatched attributes applied to the `<div data-slot="card">`. |
| ChildContent (CardHeader) | RenderFragment | null | The header's content. Wrap `CardTitle`, `CardDescription`, and any `CardAction` inside. |
| ClassName (CardHeader) | string | null | Extra CSS classes merged with the base `@container/card-header grid auto-rows-min grid-rows-[auto_auto] items-start gap-2 px-6 has-data-[slot=card-action]:grid-cols-[1fr_auto] [.border-b]:pb-6`. |
| Props (CardHeader) | Dictionary<string, object>? | null | Captured unmatched attributes applied to the `<div data-slot="card-header">`. |
| ChildContent (CardTitle) | RenderFragment | null | The heading text for the card. |
| ClassName (CardTitle) | string | null | Extra CSS classes merged with the base `leading-none font-semibold`. |
| Props (CardTitle) | Dictionary<string, object>? | null | Captured unmatched attributes applied to the `<div data-slot="card-title">`. |
| ChildContent (CardDescription) | RenderFragment | null | The supplementary text shown under the title. |
| ClassName (CardDescription) | string | null | Extra CSS classes merged with the base `text-muted-foreground text-sm`. |
| Props (CardDescription) | Dictionary<string, object>? | null | Captured unmatched attributes applied to the `<div data-slot="card-description">`. |
| ChildContent (CardAction) | RenderFragment | null | The content of the top-right action (typically a button or link). |
| ClassName (CardAction) | string | null | Extra CSS classes merged with the base `col-start-2 row-span-2 row-start-1 self-start justify-self-end`. |
| Props (CardAction) | Dictionary<string, object>? | null | Captured unmatched attributes applied to the `<div data-slot="card-action">`. |
| ChildContent (CardContent) | RenderFragment | null | The main body content of the card. |
| ClassName (CardContent) | string | null | Extra CSS classes merged with the base `px-6`. |
| Props (CardContent) | Dictionary<string, object>? | null | Captured unmatched attributes applied to the `<div data-slot="card-content">`. |
| ChildContent (CardFooter) | RenderFragment | null | The footer content, typically a row of buttons. |
| ClassName (CardFooter) | string | null | Extra CSS classes merged with the base `flex items-center px-6 [.border-t]:pt-6`. |
| Props (CardFooter) | Dictionary<string, object>? | null | Captured unmatched attributes applied to the `<div data-slot="card-footer">`. |

## Examples

### Full card with header, content, and footer

```razor
<Card>
    <CardHeader>
        <CardTitle>Payment Methods</CardTitle>
        <CardDescription>
            Manage your payment methods and defaults.
        </CardDescription>
    </CardHeader>
    <CardContent>
        <p>Card content goes here — for example a list or a form.</p>
    </CardContent>
    <CardFooter>
        <p class="text-muted-foreground text-xs">
            Click save to confirm your changes.
        </p>
    </CardFooter>
</Card>
```

### Card with a top-right action

```razor
<Card>
    <CardHeader>
        <CardTitle>Account</CardTitle>
        <CardDescription>Manage your account settings.</CardDescription>
        <CardAction>
            <span class="text-muted-foreground text-sm">Settings</span>
        </CardAction>
    </CardHeader>
    <CardContent>
        <p>The card action sits in the top-right corner while the title and description stack in the left column.</p>
    </CardContent>
</Card>
```

Because `CardHeader` uses the `has-data-[slot=card-action]:grid-cols-[1fr_auto]` class, placing a `CardAction` inside the header automatically switches the header to a two-column grid. `CardAction` carries `col-start-2 row-span-2 row-start-1` so it is pinned to the top-right.

### Compact card with custom styling via ClassName

```razor
<Card ClassName="bg-muted shadow-none border-dashed">
    <CardHeader>
        <CardTitle>Invite a teammate</CardTitle>
    </CardHeader>
    <CardContent>
        <p>Drop a card here or click to add one.</p>
    </CardContent>
</Card>
```

`ClassName` on the root `Card` is merged with the base classes, so overrides such as `shadow-none` or a different background work as expected. Each child primitive likewise accepts its own `ClassName`.
