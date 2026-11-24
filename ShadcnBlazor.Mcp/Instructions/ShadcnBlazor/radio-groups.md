---
title: Radio Group
description: A single-selection radio group whose items render as buttons, with two-way binding, default values and group- or item-level disabling.
tags: [radio, group, form, input, selection]
---

# Radio Group

A single-selection control that lets the user pick exactly one option from a set of mutually exclusive choices (such as choosing a layout density, a plan tier, or a contact preference). Each option is a `RadioGroupItem` rendered as a native `<button type="button">` that toggles its `data-state` between `checked` and `unchecked`; the checked item shows a Lucide `CircleIcon` indicator filled with the primary color. The group holds the selected value in `Value` (two-way bindable via `ValueChanged`), can be seeded with a `DefaultValue`, and either the whole group or any individual item can be disabled.

## Usage

```razor
@using ShadcnBlazor.RadioGroups
@using ShadcnBlazor.Labels

<RadioGroup DefaultValue="default">
    <div class="flex items-center gap-3">
        <RadioGroupItem Value="default" id="r1" />
        <Label for="r1">Default</Label>
    </div>
    <div class="flex items-center gap-3">
        <RadioGroupItem Value="compact" id="r2" />
        <Label for="r2">Compact</Label>
    </div>
</RadioGroup>
```

## Parameters

| Name | Type | Default | Description |
|---|---|---|---|
| `ChildContent` | `RenderFragment` | `null` | Content rendered inside a `CascadingValue` of type `RadioGroup`. Typically rows that pair a `RadioGroupItem` with a `Label`. |
| `Disabled` | `bool` | `false` | Disables the whole group; every item is disabled and sets `data-disabled` on the group. |
| `DefaultValue` | `string` | `null` | Optional one-time seed value. If a value is provided, `OnInitialized` copies it into `Value`; subsequent changes are driven by interaction. |
| `Value` | `string` | `null` | The currently selected value of the group. An item is checked when its `Value` equals this. |
| `ValueChanged` | `EventCallback<string>` | `null` | Invoked with the new value every time an item is clicked, through the group's `SetValueAsync`. Pair it with `@bind-Value`. |

`RadioGroupItem`:

| Name | Type | Default | Description |
|---|---|---|---|
| `Value` | `string` | `null` | The value this item represents. The item is checked when it equals the group's `Value`; clicking it calls `RadioGroup.SetValueAsync(Value)`. |
| `Disabled` | `bool` | `false` | Disables this item (a disabled group disables its items as well). A disabled item ignores clicks. |

The item receives the group through a `[CascadingParameter]` named `RadioGroup` and inherits from `ShadcnBase`, so the base parameters are also available: `ClassName` (a `string`) is merged with the component's built-in button classes, and `Props` (a `Dictionary<string, object>?`) captures any unmatched attributes such as `id` and merges them into the rendered `<button>`.

## Examples

Basic labeled radio group from the showcases, with one option pre-selected:

```razor
@using ShadcnBlazor.RadioGroups
@using ShadcnBlazor.Labels

<RadioGroup DefaultValue="comfortable">
    <div class="flex items-center gap-3">
        <RadioGroupItem Value="default" id="r1" />
        <Label for="r1">Default</Label>
    </div>
    <div class="flex items-center gap-3">
        <RadioGroupItem Value="comfortable" id="r2" />
        <Label for="r2">Comfortable</Label>
    </div>
    <div class="flex items-center gap-3">
        <RadioGroupItem Value="compact" id="r3" />
        <Label for="r3">Compact</Label>
    </div>
</RadioGroup>
```

Controlled state with two-way binding, reading the selection back into the page:

```razor
@using ShadcnBlazor.RadioGroups
@using ShadcnBlazor.Labels

<RadioGroup @bind-Value="SelectedDensity">
    <div class="flex items-center gap-3">
        <RadioGroupItem Value="default" id="d1" />
        <Label for="d1">Default</Label>
    </div>
    <div class="flex items-center gap-3">
        <RadioGroupItem Value="comfortable" id="d2" />
        <Label for="d2">Comfortable</Label>
    </div>
    <div class="flex items-center gap-3">
        <RadioGroupItem Value="compact" id="d3" />
        <Label for="d3">Compact</Label>
    </div>
</RadioGroup>
<p class="mt-3 text-sm text-muted-foreground">Selected: @(SelectedDensity ?? "none")</p>

@code
{
    private string? SelectedDensity = "default";
}
```

Disabling the whole group, and disabling a single item while the rest stay selectable:

```razor
@using ShadcnBlazor.RadioGroups
@using ShadcnBlazor.Labels

<RadioGroup DefaultValue="team">
    <div class="flex items-center gap-3">
        <RadioGroupItem Value="team" id="t1" />
        <Label for="t1">Team</Label>
    </div>
    <div class="flex items-center gap-3">
        <RadioGroupItem Value="business" id="t2" Disabled />
        <Label for="t2">Business (unavailable)</Label>
    </div>
</RadioGroup>

<RadioGroup DefaultValue="s1" Disabled>
    <div class="flex items-center gap-3">
        <RadioGroupItem Value="s1" id="s1" />
        <Label for="s1">Option 1</Label>
    </div>
</RadioGroup>
```

Selection cards: each item drives a bordered card that combines a `Label` and a `Badge`, with an offset `ClassName` so the indicator lines up with the title:

```razor
@using ShadcnBlazor.Badges
@using ShadcnBlazor.Labels
@using ShadcnBlazor.RadioGroups

<RadioGroup ClassName="gap-3" DefaultValue="team">
    <div class="relative flex cursor-pointer items-start space-x-3 rounded-lg border bg-background p-4 shadow-sm transition-colors hover:bg-accent">
        <RadioGroupItem ClassName="mt-1" id="starter" Value="starter" />
        <div class="grid flex-1 gap-2 leading-none">
            <div class="flex items-center justify-between">
                <Label ClassName="cursor-pointer font-medium" for="starter">
                    Starter
                </Label>
                <Badge Variant="BadgeVariant.Secondary">$0/mo</Badge>
            </div>
            <p class="text-sm text-muted-foreground">
                For solo makers getting off the ground.
            </p>
        </div>
    </div>
    <div class="relative flex cursor-pointer items-start space-x-3 rounded-lg border bg-background p-4 shadow-sm transition-colors hover:bg-accent">
        <RadioGroupItem ClassName="mt-1" id="team" Value="team" />
        <div class="grid flex-1 gap-2 leading-none">
            <div class="flex items-center justify-between">
                <Label ClassName="cursor-pointer font-medium" for="team">
                    Team
                </Label>
                <Badge>$18/mo</Badge>
            </div>
            <p class="text-sm text-muted-foreground">
                For growing teams that need more room.
            </p>
        </div>
    </div>
</RadioGroup>
```
