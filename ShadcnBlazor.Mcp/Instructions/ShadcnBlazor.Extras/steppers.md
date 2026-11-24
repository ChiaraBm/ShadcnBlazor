---
title: Stepper
description: A multi-step Stepper for guided flows, with horizontal or vertical steps, active/completed state, and prev/next controls.
tags: [stepper, steps, wizard, onboarding, prev-next]
---

# Stepper

A multi-step control for guiding users through a sequence of steps — onboarding, forms, checkouts, or status trackers. `Stepper` renders a list of `StepperItem`s, each showing a `StepperTrigger` (indicator plus title/description) and a `StepperSeparator`, with an optional `StepperContent` panel per step. Steps report an active/completed state through `StepperState`, and the list can be laid out `Horizontal` or `Vertical`. Navigation is available by clicking a trigger or via the `StepperPrev`/`StepperNext` buttons, and the active step is fully controllable through `Value`/`OnValueChanged`.

## Usage

No service registration is needed; just add the using and compose the components.

```razor
@using ShadcnBlazor.Extras.Steppers

<Stepper DefaultValue="first">
    <StepperList>
        <StepperItem Value="first">
            <StepperTrigger>
                <StepperIndicator/>
                <StepperTitle>First</StepperTitle>
            </StepperTrigger>
            <StepperSeparator/>
        </StepperItem>
        <StepperItem Value="second">
            <StepperTrigger>
                <StepperIndicator/>
                <StepperTitle>Second</StepperTitle>
            </StepperTrigger>
            <StepperSeparator/>
        </StepperItem>
    </StepperList>
    <StepperContent Value="first">First step content</StepperContent>
    <StepperContent Value="second">Second step content</StepperContent>
</Stepper>
```

## Parameters

Every component inherits `ClassName` (extra Tailwind classes, merged with the built-in ones via `Cn`) and `Props` (`[Parameter(CaptureUnmatchedValues = true)]` attributes applied to the root element) from `ShadcnBase`. The tables below list each component's own `[Parameter]` properties.

### Stepper

| Name | Type | Default | Description |
| --- | --- | --- | --- |
| `ChildContent` | `RenderFragment` | — | The stepper's children: the `StepperList`, the per-step `StepperContent` panels, and optionally `StepperPrev`/`StepperNext`. |
| `Value` | `string?` | `""` | Value of the currently active step. When not bound it falls back to `DefaultValue`, then to the first registered step. |
| `DefaultValue` | `string?` | `""` | Value of the active step on the initial render, used when `Value` is not set. |
| `OnValueChanged` | `EventCallback<string>` | — | Invoked whenever the active step changes. |
| `Orientation` | `StepperOrientation` | `StepperOrientation.Horizontal` | Layout direction: `Horizontal` or `Vertical`. |
| `Disabled` | `bool` | `false` | Disables the whole stepper; every `StepperTrigger` becomes unclickable. |
| `Interactive` | `bool` | `true` | When `false`, clicking a `StepperTrigger` does not change the active step. |

`Stepper` also exposes a public API used by the other components and by calling code:

| Member | Type | Description |
| --- | --- | --- |
| `Position` | `int` | Zero-based index of the active step in the registered step list. |
| `CanGoPrevious` | `bool` | `true` when there is a step before the active one. |
| `CanGoNext` | `bool` | `true` when there is a step after the active one. |
| `SwitchAsync(string value)` | `Task` | Switches to the step with the given value and raises `OnValueChanged`. |
| `NextAsync()` | `Task` | Switches to the next step when `CanGoNext` is `true`. |
| `PreviousAsync()` | `Task` | Switches to the previous step when `CanGoPrevious` is `true`. |

### StepperItem

| Name | Type | Default | Description |
| --- | --- | --- | --- |
| `ChildContent` | `RenderFragment` | — | The step's content: a `StepperTrigger` and a `StepperSeparator`. |
| `Value` | `string` | `""` | The unique identifier of this step, used by the parent `Stepper` for state and by `StepperContent`/`StepperTrigger` for matching. |
| `Completed` | `bool` | `false` | Marks this step as completed, rendering its indicator in the `Completed` state. |
| `Disabled` | `bool` | `false` | Disables this step's trigger. |

### StepperList

| Name | Type | Default | Description |
| --- | --- | --- | --- |
| `ChildContent` | `RenderFragment` | — | The `StepperItem` elements. The list is rendered as a `role="tablist"` whose `aria-orientation` follows the stepper's orientation. |

### StepperTrigger

| Name | Type | Default | Description |
| --- | --- | --- | --- |
| `ChildContent` | `RenderFragment` | — | The trigger content, typically a `StepperIndicator` plus a `StepperTitle` and/or `StepperDescription`. Clicking it switches the stepper to this step's `Value`, unless disabled or the stepper is non-interactive. |

### StepperIndicator

| Name | Type | Default | Description |
| --- | --- | --- | --- |
| `ChildContent` | `RenderFragment<StepperState>?` | `null` | Custom indicator content that receives the current `StepperState`. When omitted, a `CheckIcon` is rendered for a completed step and the 1-based step position otherwise. |

### StepperTitle

| Name | Type | Default | Description |
| --- | --- | --- | --- |
| `ChildContent` | `RenderFragment` | — | The step's title text. |

### StepperDescription

| Name | Type | Default | Description |
| --- | --- | --- | --- |
| `ChildContent` | `RenderFragment` | — | Muted description text, typically shown under the step's title. |

### StepperContent

| Name | Type | Default | Description |
| --- | --- | --- | --- |
| `ChildContent` | `RenderFragment` | — | The panel content shown when this step is active. |
| `Value` | `string` | `""` | The step value this panel belongs to. It renders only while that step is active (otherwise it is `hidden`). |

### StepperSeparator

`StepperSeparator` declares no `[Parameter]` properties. It renders a separator line between steps and is omitted entirely for the last step. Its color reflects the step's state.

### StepperPrev

| Name | Type | Default | Description |
| --- | --- | --- | --- |
| `ChildContent` | `RenderFragment` | — | Label for the previous-step button when no `Slot` is given. |
| `Slot` | `RenderFragment<Dictionary<string, object?>>?` | `null` | Renders a custom button element carrying the computed attributes (`data-slot="stepper-prev"`, `disabled`, `onclick`). |
| `Disabled` | `bool` | `false` | Disables the previous button. The button is additionally disabled when there is no previous step (`!Stepper.CanGoPrevious`). |

### StepperNext

| Name | Type | Default | Description |
| --- | --- | --- | --- |
| `ChildContent` | `RenderFragment` | — | Label for the next-step button when no `Slot` is given. |
| `Slot` | `RenderFragment<Dictionary<string, object?>>?` | `null` | Renders a custom button element carrying the computed attributes (`data-slot="stepper-next"`, `disabled`, `onclick`). |
| `Disabled` | `bool` | `false` | Disables the next button. The button is additionally disabled when there is no next step (`!Stepper.CanGoNext`). |

### StepperState and StepperOrientation

`StepperState` has three values: `Inactive` (0), `Active` (1) and `Completed` (2). `StepperOrientation` has two values: `Horizontal` (0) and `Vertical` (1).

## Examples

### Prev/Next navigation (adapted from the real showcase)

Use a `@ref` to read `Position`, and wrap `Button`s in `StepperPrev`/`StepperNext` through their `Slot` so the controls stay in sync with the current step.

```razor
@using ShadcnBlazor.Buttons
@using ShadcnBlazor.Extras.Steppers

<Stepper @ref="_stepper" DefaultValue="objective" ClassName="w-full max-w-md">
    <StepperList>
        @foreach (var step in Steps)
        {
            <StepperItem Value="@step.Value" @key="step.Value">
                <StepperTrigger>
                    <StepperIndicator/>
                </StepperTrigger>
                <StepperSeparator/>
            </StepperItem>
        }
    </StepperList>
    @foreach (var step in Steps)
    {
        <StepperContent Value="@step.Value" @key="step.Value" ClassName="rounded-md border bg-card p-5">
            <h3 class="text-lg font-semibold">@step.Title</h3>
            <p class="mt-2 text-sm text-muted-foreground">@step.Body</p>
        </StepperContent>
    }
    <div class="flex items-center justify-between">
        <StepperPrev>
            <Slot>
                <Button Variant="ButtonVariant.Outline" @attributes="context">Previous</Button>
            </Slot>
        </StepperPrev>
        <p class="text-sm text-muted-foreground">Step @(_stepper.Position + 1) of @Steps.Length</p>
        <StepperNext>
            <Slot>
                <Button @attributes="context">Next</Button>
            </Slot>
        </StepperNext>
    </div>
</Stepper>

@code
{
    private Stepper _stepper;

    private static readonly Step[] Steps =
    [
        new("objective", "Set the objective", "What does a win look like?"),
        new("segment", "Pick a segment", "Who should see this first?"),
        new("copy", "Write the copy", "Keep the main CTA short and clear.")
    ];

    record Step(string Value, string Title, string Body);
}
```

### Vertical stepper with titles and descriptions

Set `Orientation="StepperOrientation.Vertical"` and add a `StepperTitle` and `StepperDescription` per step.

```razor
@using ShadcnBlazor.Extras.Steppers

<Stepper DefaultValue="transit" Orientation="StepperOrientation.Vertical" ClassName="w-full max-w-lg">
    <StepperList>
        @foreach (var step in Steps)
        {
            <StepperItem Value="@step.Value" @key="step.Value">
                <StepperTrigger ClassName="not-last:pb-6">
                    <StepperIndicator/>
                    <div class="flex flex-col gap-1">
                        <StepperTitle>@step.Title</StepperTitle>
                        <StepperDescription>@step.Description</StepperDescription>
                    </div>
                </StepperTrigger>
                <StepperSeparator ClassName="absolute inset-y-0 top-5 left-3.5 -z-10 -order-1 h-full -translate-x-1/2"/>
            </StepperItem>
        }
    </StepperList>
    @foreach (var step in Steps)
    {
        <StepperContent Value="@step.Value" @key="step.Value"
                        ClassName="flex flex-col gap-2 rounded-lg border bg-card p-4 text-card-foreground">
            <h4 class="font-semibold">@step.Title</h4>
            <p class="text-sm text-muted-foreground">@step.Description</p>
        </StepperContent>
    }
</Stepper>

@code
{
    private static readonly Step[] Steps =
    [
        new("ordered", "Ordered", "We've received your order"),
        new("packed", "Packed", "Your items are boxed"),
        new("transit", "In transit", "On its way to you"),
        new("signed", "Signed", "Delivered and confirmed")
    ];

    record Step(string Value, string Title, string Description);
}
```

### Controlled stepper with OnValueChanged and external buttons

Bind `Value` and `OnValueChanged` so the active step is driven by your own state, and move navigation into plain `Button`s.

```razor
@using ShadcnBlazor.Buttons
@using ShadcnBlazor.Extras.Steppers

<div class="flex w-full max-w-md flex-col gap-4">
    <Stepper Value="@_step" OnValueChanged="@(v => _step = v)">
        <StepperList>
            @foreach (var step in Steps)
            {
                <StepperItem Value="@step.Value" @key="step.Value">
                    <StepperTrigger>
                        <StepperIndicator/>
                        <StepperTitle>@step.Title</StepperTitle>
                    </StepperTrigger>
                    <StepperSeparator ClassName="mx-3"/>
                </StepperItem>
            }
        </StepperList>
    </Stepper>
    <div class="flex items-center justify-between">
        <Button Variant="ButtonVariant.Outline" Disabled="@(_index <= 0)" @onclick="GoBack">Back</Button>
        <p class="text-sm text-muted-foreground">Step @(_index + 1) of @Steps.Length</p>
        <Button Disabled="@(_index >= Steps.Length - 1)" @onclick="GoForward">Continue</Button>
    </div>
</div>

@code
{
    private string _step = "details";
    private int _index => Array.FindIndex(Steps, s => s.Value == _step);

    private void GoBack()
    {
        if (_index > 0)
            _step = Steps[_index - 1].Value;
    }

    private void GoForward()
    {
        if (_index < Steps.Length - 1)
            _step = Steps[_index + 1].Value;
    }

    private static readonly Step[] Steps =
    [
        new("details", "Details"),
        new("cover", "Cover"),
        new("release", "Release")
    ];

    record Step(string Value, string Title);
}
```

### Custom indicator with StepperState

Provide `ChildContent` on `StepperIndicator` as a `RenderFragment<StepperState>` to render per-state markup, such as icons with a check on completion.

```razor
@using System.Diagnostics.CodeAnalysis
@using LucideBlazor
@using ShadcnBlazor.Extras.Steppers

<Stepper DefaultValue="protect" ClassName="w-full max-w-2xl">
    <StepperList>
        @foreach (var step in Steps)
        {
            <StepperItem Value="@step.Value" @key="step.Value">
                <StepperTrigger>
                    <StepperIndicator ChildContent="Indicator(step.Icon)"/>
                    <StepperTitle>@step.Title</StepperTitle>
                </StepperTrigger>
                <StepperSeparator ClassName="mx-3"/>
            </StepperItem>
        }
    </StepperList>
</Stepper>

@code
{
    private static readonly Dictionary<string, object> IconSize = new()
    {
        ["ClassName"] = "size-4"
    };

    private RenderFragment<StepperState> Indicator(Type icon) => state => state == StepperState.Completed
        ? @<CheckIcon ClassName="size-4"/>
        : @<DynamicComponent Type="@icon" Parameters="IconSize"/>;

    private static readonly Step[] Steps =
    [
        new("draft", "Draft", typeof(PencilLineIcon)),
        new("protect", "Protect", typeof(ShieldCheckIcon)),
        new("deploy", "Deploy", typeof(RocketIcon)),
        new("shipped", "Shipped", typeof(RocketIcon))
    ];

    record Step(string Value, string Title, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)] Type Icon);
}
```
