---
title: Accordion
description: Collapsible accordion lists with single- or multi-select behavior, animated open/close states, and per-item theming.
tags: [accordion, collapsible, disclosure, expandable, sections]
---
# Accordion

The Accordion renders a list of collapsible sections, each made of an `AccordionItem`, an `AccordionTrigger` (a button with a rotating chevron) and an `AccordionContent` that animates open and closed. It supports two behaviors: `AccordionType.Single`, where opening an item closes all others, and `AccordionType.Multiple`, where any number of items can stay open at once. Use it for FAQ pages, settings groups, and disclosure panels that reveal longer text on demand. The initial open state is set with `DefaultValue` (one item) or `DefaultValues` (several items).

## Usage

```razor
@using ShadcnBlazor.Accordions

<Accordion>
    <AccordionItem Value="1">
        <AccordionTrigger>Is it accessible?</AccordionTrigger>
        <AccordionContent>
            <p class="text-muted-foreground">Yes. It follows the same WAI-ARIA APG design pattern as the original Radix UI primitive.</p>
        </AccordionContent>
    </AccordionItem>
    <AccordionItem Value="2">
        <AccordionTrigger>Is it styled?</AccordionTrigger>
        <AccordionContent>
            <p class="text-muted-foreground">Yes. It comes with default styles that match the other ShadcnBlazor components.</p>
        </AccordionContent>
    </AccordionItem>
</Accordion>
```

## Parameters

| Name | Type | Default | Description |
| --- | --- | --- | --- |
| ChildContent | RenderFragment | — | The accordion items to render. Declared by `Accordion`, `AccordionItem`, `AccordionContent` and `AccordionTrigger`. |
| DefaultValue | string? | null | The value of the single item that is open initially (`Accordion`). |
| DefaultValues | string[]? | null | The values of the items that are open initially (`Accordion`). Takes precedence over `DefaultValue`. |
| Type | AccordionType | AccordionType.Single | Selects the accordion behavior. `AccordionType.Single` (0) opens at most one item at a time; `AccordionType.Multiple` (1) lets any number of items stay open (`Accordion`). |
| Value | string | — | The value that identifies this item and matches it against the accordion's open values (`AccordionItem`). |
| ClassName | string | — | Extra Tailwind classes merged into the root element. Inherited by all four components from `ShadcnBase`. |
| Props | Dictionary<string, object>? | — | Catch-all for unmatched attributes, applied to the root element. Inherited by all four components from `ShadcnBase`. |

## Examples

### Standard single accordion with an initially open item

```razor
@using ShadcnBlazor.Accordions

<Accordion Type="AccordionType.Single"
           DefaultValue="shipping"
           ClassName="w-full max-w-md -space-y-px">
    <AccordionItem Value="shipping"
                   ClassName="overflow-hidden border bg-background px-4 first:rounded-t-lg last:rounded-b-lg last:border-b">
        <AccordionTrigger>Shipping and delivery</AccordionTrigger>
        <AccordionContent>
            <p class="text-muted-foreground">Orders placed before 2 pm are dispatched the same business day. Standard delivery takes 2 to 4 working days, and express options arrive within 24 hours of dispatch.</p>
        </AccordionContent>
    </AccordionItem>

    <AccordionItem Value="returns"
                   ClassName="overflow-hidden border bg-background px-4 first:rounded-t-lg last:rounded-b-lg last:border-b">
        <AccordionTrigger>Returns and exchanges</AccordionTrigger>
        <AccordionContent>
            <p class="text-muted-foreground">You have 30 days from delivery to start a return. Items must be unused and in their original packaging, and refunds are issued to the original payment method within 5 working days.</p>
        </AccordionContent>
    </AccordionItem>

    <AccordionItem Value="payments"
                   ClassName="overflow-hidden border bg-background px-4 first:rounded-t-lg last:rounded-b-lg last:border-b">
        <AccordionTrigger>Payment methods</AccordionTrigger>
        <AccordionContent>
            <p class="text-muted-foreground">We accept all major credit and debit cards, PayPal, and Apple Pay. Every transaction is encrypted end to end, and saved payment methods speed up future checkouts.</p>
        </AccordionContent>
    </AccordionItem>
</Accordion>
```

### Multiple-select accordion

```razor
@using ShadcnBlazor.Accordions

<Accordion Type="AccordionType.Multiple"
           DefaultValues='("filters", "sorting")'
           ClassName="w-full max-w-md -space-y-px">
    <AccordionItem Value="filters"
                   ClassName="overflow-hidden border bg-background px-4 first:rounded-t-lg last:rounded-b-lg last:border-b">
        <AccordionTrigger>Filters</AccordionTrigger>
        <AccordionContent>
            <p class="text-muted-foreground">Filters are applied as soon as you toggle them, and each filter group keeps its own state.</p>
        </AccordionContent>
    </AccordionItem>

    <AccordionItem Value="sorting"
                   ClassName="overflow-hidden border bg-background px-4 first:rounded-t-lg last:rounded-b-lg last:border-b">
        <AccordionTrigger>Sorting</AccordionTrigger>
        <AccordionContent>
            <p class="text-muted-foreground">Choose one sort order per column; columns can be combined with a secondary sort key.</p>
        </AccordionContent>
    </AccordionItem>

    <AccordionItem Value="views"
                   ClassName="overflow-hidden border bg-background px-4 first:rounded-t-lg last:rounded-b-lg last:border-b">
        <AccordionTrigger>Views</AccordionTrigger>
        <AccordionContent>
            <p class="text-muted-foreground">Saved views store the current filter and sort combinations and can be shared with your team.</p>
        </AccordionContent>
    </AccordionItem>
</Accordion>
```

### Multi-level accordion with custom trigger icons

```razor
@using LucideBlazor
@using ShadcnBlazor.Accordions

<Accordion Type="AccordionType.Single"
           DefaultValue="item-1"
           ClassName="w-full max-w-md -space-y-1">
    <AccordionItem Value="item-1"
                   ClassName="overflow-hidden border bg-background first:rounded-t-lg last:rounded-b-lg last:border-b">
        <AccordionTrigger ClassName="group px-4 py-3 hover:no-underline [&>svg]:hidden">
            <div class="flex w-full items-center justify-between">
                <span class="text-left">Getting Started</span>
                <div class="relative size-4 shrink-0">
                    <PlusIcon
                        ClassName="absolute inset-0 size-4 text-muted-foreground transition-opacity duration-200 group-data-[state=open]:opacity-0"/>
                    <MinusIcon
                        ClassName="absolute inset-0 size-4 text-muted-foreground opacity-0 transition-opacity duration-200 group-data-[state=open]:opacity-100"/>
                </div>
            </div>
        </AccordionTrigger>
        <AccordionContent ClassName="ps-7">
            <p class="text-muted-foreground">Sign up with your work email to activate your workspace. Every teammate you invite receives an email with a link to join, and roles are assigned on their first login.</p>
        </AccordionContent>
    </AccordionItem>

    <AccordionItem Value="item-2"
                   ClassName="overflow-hidden border bg-background first:rounded-t-lg last:rounded-b-lg last:border-b">
        <AccordionTrigger ClassName="group px-4 py-3 hover:no-underline [&>svg]:hidden">
            <div class="flex w-full items-center justify-between">
                <span class="text-left">Billing &amp; Plans</span>
                <div class="relative size-4 shrink-0">
                    <PlusIcon
                        ClassName="absolute inset-0 size-4 text-muted-foreground transition-opacity duration-200 group-data-[state=open]:opacity-0"/>
                    <MinusIcon
                        ClassName="absolute inset-0 size-4 text-muted-foreground opacity-0 transition-opacity duration-200 group-data-[state=open]:opacity-100"/>
                </div>
            </div>
        </AccordionTrigger>
        <AccordionContent ClassName="ps-7">
            <p class="text-muted-foreground">Plans are billed monthly or annually in advance. You can switch tiers at any time, and the difference is prorated on your next invoice.</p>
        </AccordionContent>
    </AccordionItem>
</Accordion>
```
