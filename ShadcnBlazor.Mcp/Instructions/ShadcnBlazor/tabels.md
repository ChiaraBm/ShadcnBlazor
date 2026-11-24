---
title: Table
description: A shadcn-style table built from composable primitives: header, body, rows, cells, caption, and footer.
tags: [table, data-table, table-row, table-cell, caption]
---

# Table

`Table` is a composable, shadcn-styled data table made from eight small primitives: `Table` (a scrollable container around the `<table>` element), `TableCaption`, `TableHeader`, `TableHead`, `TableBody`, `TableRow`, `TableCell`, and `TableFooter`. Nest the structural elements (`TableHeader`, `TableBody`, optionally `TableFooter` and `TableCaption`) inside `Table`, and build each row from a `TableRow` containing `TableHead` (header rows) or `TableCell` (body rows). Use it for any tabular data — inventories, reports, settings matrices — and you get a responsive, overflow-scrolling table with row hover and selected-row styling out of the box. Every primitive inherits `ClassName` and `Props` from `ShadcnBase`, so you can merge extra classes and spread arbitrary HTML attributes onto the underlying element.

## Usage

```razor
<Table>
    <TableHeader>
        <TableRow>
            <TableHead>Name</TableHead>
            <TableHead>Status</TableHead>
        </TableRow>
    </TableHeader>
    <TableBody>
        <TableRow>
            <TableCell>Ada</TableCell>
            <TableCell>Active</TableCell>
        </TableRow>
    </TableBody>
</Table>
```

## Parameters

Each of the eight components declares a `ChildContent` parameter and inherits `ClassName` and `Props` from `ShadcnBase`. The rows below list one `[Parameter]` per component.

| Name | Type | Default | Description |
| --- | --- | --- | --- |
| ChildContent (Table) | RenderFragment | null | The table's content. Wrap the `TableCaption`, `TableHeader`, `TableBody`, and `TableFooter` inside. |
| ClassName (Table) | string | null | Extra CSS classes merged with the base `w-full caption-bottom text-sm`. Applied to the `<table data-slot="table">`. |
| Props (Table) | Dictionary<string, object>? | null | Captured unmatched attributes; note that `Table` does not spread them onto its rendered markup. |
| ChildContent (TableCaption) | RenderFragment | null | The caption text shown above the table. |
| ClassName (TableCaption) | string | null | Extra CSS classes merged with the base `text-muted-foreground mt-4 text-sm`. |
| Props (TableCaption) | Dictionary<string, object>? | null | Captured unmatched attributes applied to the `<caption data-slot="table-caption">`. |
| ChildContent (TableHeader) | RenderFragment | null | The header content. Wrap the header `TableRow` elements inside. |
| ClassName (TableHeader) | string | null | Extra CSS classes merged with the base `[&_tr]:border-b`. |
| Props (TableHeader) | Dictionary<string, object>? | null | Captured unmatched attributes applied to the `<thead data-slot="table-header">`. |
| ChildContent (TableHead) | RenderFragment | null | The label text (and optional icons) shown in a single header cell. |
| ClassName (TableHead) | string | null | Extra CSS classes merged with the base `text-foreground h-10 px-2 text-left align-middle font-medium whitespace-nowrap [&:has([role=checkbox])]:pr-0 [&>[role=checkbox]]:translate-y-[2px]`. |
| Props (TableHead) | Dictionary<string, object>? | null | Captured unmatched attributes applied to the `<th data-slot="table-head">`. |
| ChildContent (TableBody) | RenderFragment | null | The body content. Wrap the data `TableRow` elements inside. |
| ClassName (TableBody) | string | null | Extra CSS classes merged with the base `[&_tr:last-child]:border-0`. |
| Props (TableBody) | Dictionary<string, object>? | null | Captured unmatched attributes applied to the `<tbody data-slot="table-body">`. |
| ChildContent (TableRow) | RenderFragment | null | A single table row's content. Wrap the `TableHead`/`TableCell` entries inside. |
| ClassName (TableRow) | string | null | Extra CSS classes merged with the base `hover:bg-muted/50 data-[state=selected]:bg-muted border-b transition-colors`. |
| Props (TableRow) | Dictionary<string, object>? | null | Captured unmatched attributes applied to the `<tr data-slot="table-row">`. |
| ChildContent (TableCell) | RenderFragment | null | The content shown in a single body cell. |
| ClassName (TableCell) | string | null | Extra CSS classes merged with the base `p-2 align-middle whitespace-nowrap [&:has([role=checkbox])]:pr-0 [&>[role=checkbox]]:translate-y-[2px]`. |
| Props (TableCell) | Dictionary<string, object>? | null | Captured unmatched attributes applied to the `<td data-slot="table-cell">`. |
| ChildContent (TableFooter) | RenderFragment | null | The footer content. Wrap the footer `TableRow` elements inside. |
| ClassName (TableFooter) | string | null | Extra CSS classes merged with the base `bg-muted/50 border-t font-medium [&>tr]:last:border-b-0`. |
| Props (TableFooter) | Dictionary<string, object>? | null | Captured unmatched attributes applied to the `<tfoot data-slot="table-footer">`. |

## Examples

### Full data table with caption and footer

```razor
<Table>
    <TableCaption>A list of your recent invoices.</TableCaption>
    <TableHeader>
        <TableRow>
            <TableHead>Invoice</TableHead>
            <TableHead>Customer</TableHead>
            <TableHead>Status</TableHead>
            <TableHead>Amount</TableHead>
        </TableRow>
    </TableHeader>
    <TableBody>
        <TableRow>
            <TableCell>INV-001</TableCell>
            <TableCell>Acme Corp</TableCell>
            <TableCell>Paid</TableCell>
            <TableCell>$250.00</TableCell>
        </TableRow>
        <TableRow>
            <TableCell>INV-002</TableCell>
            <TableCell>Northwind</TableCell>
            <TableCell>Pending</TableCell>
            <TableCell>$150.00</TableCell>
        </TableRow>
    </TableBody>
    <TableFooter>
        <TableRow>
            <TableCell colspan="3">Total</TableCell>
            <TableCell>$400.00</TableCell>
        </TableRow>
    </TableFooter>
</Table>
```

`TableCaption` renders a `<caption>`, so it must sit inside `Table` alongside the structural elements. `TableFooter` renders a `<tfoot>` with a muted background (`bg-muted/50 border-t font-medium`), and its last row loses the bottom border (`[&>tr]:last:border-b-0`).

### Marking a row as selected

```razor
<Table>
    <TableHeader>
        <TableRow>
            <TableHead>Name</TableHead>
            <TableHead>Role</TableHead>
        </TableRow>
    </TableHeader>
    <TableBody>
        <TableRow data-state="selected">
            <TableCell>Ada</TableCell>
            <TableCell>Admin</TableCell>
        </TableRow>
        <TableRow>
            <TableCell>Ben</TableCell>
            <TableCell>Member</TableCell>
        </TableRow>
    </TableBody>
</Table>
```

The unmatched `data-state` attribute lands in `Props` and is spread onto the `<tr data-slot="table-row">`. The row's base classes include `data-[state=selected]:bg-muted`, so a row with `data-state="selected"` is highlighted.

### Overriding styles with ClassName

```razor
<Table ClassName="border rounded-lg">
    <TableHeader>
        <TableRow>
            <TableHead ClassName="text-center">Invoice</TableHead>
            <TableHead>Customer</TableHead>
        </TableRow>
    </TableHeader>
    <TableBody>
        <TableRow>
            <TableCell ClassName="text-center">INV-001</TableCell>
            <TableCell>Acme Corp</TableCell>
        </TableRow>
    </TableBody>
</Table>
```

`ClassName` is merged on top of each component's base classes (via Tailwind merge), so overrides such as centered cells or an additional border work as expected. Each primitive also accepts its own `ClassName`.
