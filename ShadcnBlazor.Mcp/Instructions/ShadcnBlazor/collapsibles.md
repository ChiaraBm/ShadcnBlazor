---
title: Collapsible
description: Single open/closed panel with a trigger that toggles its content, with default-open and disabled states and an OnOpenChange callback.
tags: [collapsible, disclosure, expandable, toggle, panel]
---
# Collapsible

The Collapsible renders a single open/closed panel made of a `CollapsibleTrigger` (a clickable span, or any element via the `Slot` template) and a `CollapsibleContent` whose markup is rendered or hidden according to the panel state. Use it for on-demand disclosure of details — settings sections, star lists, footnotes — wherever one piece of content should hide behind a trigger. Unlike `Accordion`, it manages a single independent state: `DefaultOpen` controls the initial state, `Disabled` locks the trigger in place, and `OnOpenChange` reports every toggle with the new `bool` state. The trigger and content expose `data-state` (`open`/`closed`) and `data-disabled` attributes so Tailwind arbitrary variants like `group-data-[state=open]:...` can style both states.

## Usage

```razor
@using ShadcnBlazor.Collapsibles

<Collapsible>
    <CollapsibleTrigger>Open the panel</CollapsibleTrigger>
    <CollapsibleContent>
        <p class="text-muted-foreground">This content is rendered only while the panel is open.</p>
    </CollapsibleContent>
</Collapsible>
```

## Parameters

| Name | Type | Default | Description |
| --- | --- | --- | --- |
| ChildContent | RenderFragment | null | The child components to render. Declared by `Collapsible`, `CollapsibleTrigger` and `CollapsibleContent`. |
| DefaultOpen | bool | false | Whether the panel starts open (`Collapsible`). Read once in `OnInitialized` into the internal `IsOpen` state. |
| Disabled | bool | false | Locks the trigger: while set, clicks are ignored and the `data-disabled` attribute is exposed (`Collapsible`). |
| OnOpenChange | EventCallback<bool> | EventCallback.Default | Invoked after each toggle with the new open state (`Collapsible`). |
| Slot | RenderFragment<Dictionary<string, object>>? | null | Template that renders the trigger as a custom element instead of a `<span>`. Receives an attributes dictionary containing `data-state`, `data-disabled` and `onclick` (`CollapsibleTrigger`). |
| Props | Dictionary<string, object>? | — | Catch-all for unmatched attributes (`CollapsibleTrigger`). |
| ClassName | string | — | Extra Tailwind classes merged into the root element. Inherited by all three components from `ShadcnBase`. |
| Props | Dictionary<string, object>? | — | Catch-all for unmatched attributes, applied to the root element. Inherited by `Collapsible` and `CollapsibleContent` from `ShadcnBase`. |

`CollapsibleTrigger` and `CollapsibleContent` receive their owning `Collapsible` through a `[CascadingParameter]`; they are not authored directly.

## Examples

### Standard collapsible with a button trigger (from the Sc1 showcase)

```razor
@using LucideBlazor
@using ShadcnBlazor.Collapsibles
@using ShadcnBlazor.Buttons

<Collapsible ClassName="flex w-[350px] flex-col gap-2">
    <div class="flex items-center justify-between gap-4 px-4">
        <h4 class="text-sm font-semibold">
            @@peduarte starred 3 repositories
        </h4>
        <CollapsibleTrigger>
            <Slot>
                <Button Variant="ButtonVariant.Ghost" Size="ButtonSize.Icon" ClassName="size-8" @attributes="context">
                    <ChevronsUpDownIcon/>
                </Button>
            </Slot>
        </CollapsibleTrigger>
    </div>
    <div class="rounded-md border px-4 py-2 font-mono text-sm">
        @@radix-ui/primitives
    </div>
    <CollapsibleContent ClassName="flex flex-col gap-2">
        <div class="rounded-md border px-4 py-2 font-mono text-sm">
            @@radix-ui/colors
        </div>
        <div class="rounded-md border px-4 py-2 font-mono text-sm">
            @@stitches/react
        </div>
    </CollapsibleContent>
</Collapsible>
```

### Open by default with state reporting

```razor
@using ShadcnBlazor.Collapsibles

<Collapsible DefaultOpen="true"
             OnOpenChange="state => Console.WriteLine($"panel is now {(state ? "open" : "closed")})">
    <CollapsibleTrigger>Toggle the panel</CollapsibleTrigger>
    <CollapsibleContent>
        <p class="text-muted-foreground">The panel starts open, and every toggle reports the new state through OnOpenChange.</p>
    </CollapsibleContent>
</Collapsible>
```

### Disabled panel with themed states

```razor
@using ShadcnBlazor.Collapsibles

<Collapsible Disabled="true"
             ClassName="flex max-w-md flex-col gap-2">
    <CollapsibleTrigger ClassName="text-sm font-medium hover:underline group-data-[state=closed]:opacity-50">
        Read the full changelog
    </CollapsibleTrigger>
    <CollapsibleContent ClassName="border bg-muted px-4 py-2 text-sm">
        <p class="text-muted-foreground">This trigger never responds to clicks while Disabled is set.</p>
    </CollapsibleContent>
</Collapsible>
```
