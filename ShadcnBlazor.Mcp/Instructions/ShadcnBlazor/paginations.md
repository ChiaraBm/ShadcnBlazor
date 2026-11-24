---
title: Pagination
description: Components for page navigation: nav, item list, page links with active state, ellipsis, and previous/next buttons.
tags: [pagination, navigation, links, pages, shadcnblazor]
---

# Pagination

The Paginations folder provides the building blocks for a paginated navigation control: a `nav` wrapper (`Pagination`), a horizontal item list (`PaginationContent`), individual list items (`PaginationItem`), clickable page links (`PaginationLink`) with active-page styling, an ellipsis marker (`PaginationEllipsis`), and ready-made previous/next buttons (`PaginationPrevious`, `PaginationNext`). Use these together to build paginated tables, feeds, or galleries. The previous/next button labels are taken from the library's translations, so they follow the active language automatically.

## Usage

```razor
<Pagination>
    <PaginationContent>
        <PaginationItem>
            <PaginationPrevious Size="ButtonSize.Icon" />
        </PaginationItem>
        <PaginationItem>
            <PaginationLink IsActive="true">1</PaginationLink>
        </PaginationItem>
        <PaginationItem>
            <PaginationLink>2</PaginationLink>
        </PaginationItem>
        <PaginationItem>
            <PaginationNext Size="ButtonSize.Icon" />
        </PaginationItem>
    </PaginationContent>
</Pagination>
```

## Parameters

| Name | Type | Default | Description |
| --- | --- | --- | --- |
| `ChildContent` (on `Pagination`) | `RenderFragment` | — | Content rendered inside the `nav` wrapper (normally a `PaginationContent`). |
| `ChildContent` (on `PaginationContent`) | `RenderFragment` | — | Content rendered inside the `ul` that holds the `PaginationItem`s. |
| `ChildContent` (on `PaginationItem`) | `RenderFragment` | — | Content rendered inside the `li`, normally a single `PaginationLink`. |
| `ChildContent` (on `PaginationLink`) | `RenderFragment` | — | Content rendered inside the link (page number, label, or icons). |
| `IsActive` (on `PaginationLink`) | `bool` | `false` | Marks the link as the current page; renders it with the `Outline` button variant and sets `aria-current="page"`. |
| `Size` (on `PaginationLink`) | `ButtonSize` | `ButtonSize.Icon` | Size of the underlying `Button`. Values: `Sm`, `Default`, `Lg`, `IconSm`, `Icon`, `IconLg`. |
| `Size` (on `PaginationPrevious`) | `ButtonSize` | `ButtonSize.Default` | Size of the "Previous" button. Values: `Sm`, `Default`, `Lg`, `IconSm`, `Icon`, `IconLg`. |
| `Size` (on `PaginationNext`) | `ButtonSize` | `ButtonSize.Default` | Size of the "Next" button. Values: `Sm`, `Default`, `Lg`, `IconSm`, `Icon`, `IconLg`. |

`PaginationEllipsis` declares no parameters.

## Examples

### Standard pagination with an active page

```razor
<Pagination>
    <PaginationContent>
        <PaginationItem>
            <PaginationPrevious Size="ButtonSize.Icon" />
        </PaginationItem>
        <PaginationItem>
            <PaginationLink>1</PaginationLink>
        </PaginationItem>
        <PaginationItem>
            <PaginationLink>2</PaginationLink>
        </PaginationItem>
        <PaginationItem>
            <PaginationLink IsActive="true">3</PaginationLink>
        </PaginationItem>
        <PaginationItem>
            <PaginationLink>4</PaginationLink>
        </PaginationItem>
        <PaginationItem>
            <PaginationLink>5</PaginationLink>
        </PaginationItem>
        <PaginationItem>
            <PaginationNext Size="ButtonSize.Icon" />
        </PaginationItem>
    </PaginationContent>
</Pagination>
```

### Pagination with an ellipsis for a long page range

```razor
<Pagination>
    <PaginationContent>
        <PaginationItem>
            <PaginationPrevious Size="ButtonSize.Icon" />
        </PaginationItem>
        <PaginationItem>
            <PaginationLink IsActive="true">1</PaginationLink>
        </PaginationItem>
        <PaginationItem>
            <PaginationLink>2</PaginationLink>
        </PaginationItem>
        <PaginationItem>
            <PaginationLink>3</PaginationLink>
        </PaginationItem>
        <PaginationItem>
            <PaginationEllipsis />
        </PaginationItem>
        <PaginationItem>
            <PaginationLink>5</PaginationLink>
        </PaginationItem>
        <PaginationItem>
            <PaginationLink>6</PaginationLink>
        </PaginationItem>
        <PaginationItem>
            <PaginationNext Size="ButtonSize.Icon" />
        </PaginationItem>
    </PaginationContent>
</Pagination>
```

### Large-size variant with translated previous/next labels

```razor
<Pagination>
    <PaginationContent>
        <PaginationItem>
            <PaginationPrevious Size="ButtonSize.Lg" />
        </PaginationItem>
        <PaginationItem>
            <PaginationLink Size="ButtonSize.Lg" IsActive="true">1</PaginationLink>
        </PaginationItem>
        <PaginationItem>
            <PaginationLink Size="ButtonSize.Lg">2</PaginationLink>
        </PaginationItem>
        <PaginationItem>
            <PaginationLink Size="ButtonSize.Lg">3</PaginationLink>
        </PaginationItem>
        <PaginationItem>
            <PaginationNext Size="ButtonSize.Lg" />
        </PaginationItem>
    </PaginationContent>
</Pagination>
```
