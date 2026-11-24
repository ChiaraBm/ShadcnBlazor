---
title: Item
description: Composable list-item primitive with media, title, description, actions, and footer regions plus variant and size styling
tags: [item, list, media, layout, separator, variant]
---

# Item

`Item` renders a horizontal, flex-wrapped row (`<div data-slot="item">`) intended as a single entry inside a list, with its `variant` and `size` styling exposed through `data-variant` and `data-size` attributes. Compose it from `ItemGroup` (the `role="list"` container), `ItemMedia`, `ItemContent` (which holds `ItemTitle` and `ItemDescription`), `ItemHeader`, `ItemFooter`, `ItemSeparator`, and `ItemActions`. Use it for lists where each row pairs media with a title and description — user lists, file pickers, settings rows, navigation entries — and the base classes give anchor items a hover highlight automatically. `ItemSeparator` is built on the shared `Separator` component with a horizontal orientation and `my-0` class.

## Usage

```razor
<ItemGroup>
    <Item>
        <ItemContent>
            <ItemTitle>Item title</ItemTitle>
            <ItemDescription>Item description</ItemDescription>
        </ItemContent>
    </Item>
</ItemGroup>
```

## Parameters

Every component inherits `ClassName` and `Props` from `ShadcnBase`. `ItemSeparator` is built on `ShadcnBlazor.Separators.Separator`. There are no event callbacks in the Item set; the `Slot` parameter on `Item` is a render slot, not an event. The rows below list every `[Parameter]` across the folder.

| Name | Type | Default | Description |
| --- | --- | --- | --- |
| ChildContent (Item) | RenderFragment | null | The item's content. Wrap `ItemMedia`, `ItemContent`, `ItemHeader`, `ItemFooter`, `ItemSeparator`, and `ItemActions` inside. |
| Slot (Item) | RenderFragment&lt;Dictionary&lt;string, object?&gt;&gt;? | null | Optional render slot that receives the computed attribute bag (`data-slot`, `data-variant`, `data-size`, `class`) and renders instead of the default `<div data-slot="item">`. |
| Size (Item) | ItemSize | ItemSize.Default | Item size: `Default` uses `p-4 gap-4`, `Sm` uses `py-3 px-4 gap-2.5`. Sets the `data-size` attribute. |
| Variant (Item) | ItemVariant | ItemVariant.Default | Item visual style: `Default` (bg-transparent), `Outline` (border-border), `Muted` (bg-muted/50). Sets the `data-variant` attribute. |
| ClassName (Item) | string | null | Extra CSS classes merged with the base `group/item flex items-center border border-transparent text-sm rounded-md transition-colors [a]:hover:bg-accent/50 [a]:transition-colors duration-100 flex-wrap outline-none focus-visible:border-ring focus-visible:ring-ring/50 focus-visible:ring-[3px]` plus the size and variant classes. |
| Props (Item) | Dictionary&lt;string, object?&gt; | null | Captured unmatched attributes applied to the root `<div data-slot="item">`. |
| ChildContent (ItemGroup) | RenderFragment | null | The group's content. Wrap the `Item` elements inside. |
| ClassName (ItemGroup) | string | null | Used verbatim as the `class` attribute of the `<div role="list">` (the base classes `group/item-group flex flex-col` are computed in code but the element renders `ClassName` directly). |
| Props (ItemGroup) | Dictionary&lt;string, object?&gt; | null | Captured unmatched attributes applied to the `<div role="list" data-slot="item-group">`. |
| ChildContent (ItemMedia) | RenderFragment | null | The media content (icon, image, or any leading element). |
| Variant (ItemMedia) | ItemMediaVariant | ItemMediaVariant.Default | Media style: `Default` (bg-transparent), `Icon` (`size-8 border rounded-sm bg-muted [&_svg:not([class*='size-'])]:size-4`), `Image` (`size-10 rounded-sm overflow-hidden [&_img]:size-full [&_img]:object-cover`). Sets the `data-variant` attribute. |
| ClassName (ItemMedia) | string | null | Extra CSS classes merged with the base media classes and the variant classes. |
| Props (ItemMedia) | Dictionary&lt;string, object?&gt; | null | Captured unmatched attributes applied to the `<div data-slot="item-media">`. |
| ChildContent (ItemContent) | RenderFragment | null | The main body content. Wrap `ItemTitle` and `ItemDescription` inside. |
| ClassName (ItemContent) | string | null | Extra CSS classes merged with the base `flex flex-1 flex-col gap-1 [&+[data-slot=item-content]]:flex-none`. |
| Props (ItemContent) | Dictionary&lt;string, object?&gt; | null | Captured unmatched attributes applied to the `<div data-slot="item-content">`. |
| ChildContent (ItemTitle) | RenderFragment | null | The title text for the item. |
| ClassName (ItemTitle) | string | null | Extra CSS classes merged with the base `flex w-fit items-center gap-2 text-sm leading-snug font-medium`. |
| Props (ItemTitle) | Dictionary&lt;string, object?&gt; | null | Captured unmatched attributes applied to the `<div data-slot="item-title">`. |
| ChildContent (ItemDescription) | RenderFragment | null | The supplementary text shown under the title. |
| ClassName (ItemDescription) | string | null | Extra CSS classes merged with the base `text-muted-foreground line-clamp-2 text-sm leading-normal font-normal text-balance [&>a:hover]:text-primary [&>a]:underline [&>a]:underline-offset-4`. |
| Props (ItemDescription) | Dictionary&lt;string, object?&gt; | null | Captured unmatched attributes applied to the `<p data-slot="item-description">`. |
| ChildContent (ItemHeader) | RenderFragment | null | The header content (spans the full row). |
| ClassName (ItemHeader) | string | null | Extra CSS classes merged with the base `flex basis-full items-center justify-between gap-2`. |
| Props (ItemHeader) | Dictionary&lt;string, object?&gt; | null | Captured unmatched attributes applied to the `<div data-slot="item-header">`. |
| ChildContent (ItemFooter) | RenderFragment | null | The footer content (spans the full row). |
| ClassName (ItemFooter) | string | null | Extra CSS classes merged with the base `flex basis-full items-center justify-between gap-2`. |
| Props (ItemFooter) | Dictionary&lt;string, object?&gt; | null | Captured unmatched attributes applied to the `<div data-slot="item-footer">`. |
| ChildContent (ItemActions) | RenderFragment | null | The actions content, typically a row of buttons or links. |
| ClassName (ItemActions) | string | null | Extra CSS classes merged with the base `flex items-center gap-2`. |
| Props (ItemActions) | Dictionary&lt;string, object?&gt; | null | Captured unmatched attributes applied to the `<div data-slot="item-actions">`. |
| ChildContent (ItemSeparator) | RenderFragment | null | The separator's content (passed through to `Separator`). |
| ClassName (ItemSeparator) | string | null | Extra CSS classes merged with the base `my-0` on the horizontal `Separator`. |
| Props (ItemSeparator) | Dictionary&lt;string, object?&gt; | null | Captured unmatched attributes applied to the `<Separator data-slot="item-separator">`. |

## Examples

### Item with icon media, title, description, and actions

```razor
<ItemGroup>
    <Item Variant="ItemVariant.Outline">
        <ItemMedia Variant="ItemMediaVariant.Icon">
            <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" class="size-5">
                <circle cx="12" cy="12" r="10" />
                <path d="M12 6v6l4 2" />
            </svg>
        </ItemMedia>
        <ItemContent>
            <ItemTitle>Meeting notes</ItemTitle>
            <ItemDescription>Updated 10 minutes ago</ItemDescription>
        </ItemContent>
        <ItemActions>
            <span class="text-muted-foreground text-sm">Open</span>
        </ItemActions>
    </Item>
</ItemGroup>
```

### Group of items with variants, sizes, and an anchor item

```razor
<ItemGroup>
    <Item Variant="ItemVariant.Muted" Size="ItemSize.Sm">
        <ItemContent>
            <ItemTitle>Archived project</ItemTitle>
            <ItemDescription>This item uses the Muted variant and the Sm size.</ItemDescription>
        </ItemContent>
    </Item>
    <Item>
        <ItemMedia Variant="ItemMediaVariant.Icon">
            <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" class="size-5">
                <path d="M20 21v-2a4 4 0 0 0-4-4H8a4 4 0 0 0-4 4v2" />
                <circle cx="12" cy="7" r="4" />
            </svg>
        </ItemMedia>
        <ItemContent>
            <ItemTitle>Chiara Baumgartner</ItemTitle>
            <ItemDescription>chiara@example.com</ItemDescription>
        </ItemContent>
    </Item>
    <a href="/settings">
        <Item>
            <ItemContent>
                <ItemTitle>Settings</ItemTitle>
                <ItemDescription>Anchored items get a hover background automatically.</ItemDescription>
            </ItemContent>
        </Item>
    </a>
</ItemGroup>
```

Because the `Item` base classes include `[a]:hover:bg-accent/50 [a]:transition-colors duration-100`, wrapping an `Item` in an `<a>` (or adding the `a` class) gives it a hover highlight with no extra markup.

### Item with image media, header with actions, separator, and footer

```razor
<ItemGroup>
    <Item Variant="ItemVariant.Outline">
        <ItemHeader>
            <ItemTitle>Design system</ItemTitle>
            <ItemActions>
                <span class="text-muted-foreground text-sm">2 weeks ago</span>
            </ItemActions>
        </ItemHeader>
        <ItemMedia Variant="ItemMediaVariant.Image">
            <img src="/images/design-system.png" alt="Design system preview" />
        </ItemMedia>
        <ItemContent>
            <ItemTitle>Shared components and tokens</ItemTitle>
            <ItemDescription>A collection of reusable UI primitives with a default theme.</ItemDescription>
        </ItemContent>
        <ItemSeparator />
        <ItemFooter>
            <span class="text-muted-foreground text-sm">42 components</span>
            <span class="text-muted-foreground text-sm">v2.1.0</span>
        </ItemFooter>
    </Item>
</ItemGroup>
```

`ItemHeader` and `ItemFooter` both use `basis-full`, so they wrap onto their own full-width row inside the flex-wrapped `Item`; `ItemSeparator` renders a horizontal `Separator` with `my-0` between sections.

### Custom root element with the Slot parameter

```razor
<Item Slot='@(attrs =>
    <a href="https://example.com" @attributes="@attrs">
        Linked item rendered by the slot
    </a>)' />
```

When `Slot` is provided it is invoked with the fully computed attribute dictionary (`data-slot="item"`, `data-variant`, `data-size`, and the merged `class`), so the slot renders instead of the default `<div>` while keeping all styling.
