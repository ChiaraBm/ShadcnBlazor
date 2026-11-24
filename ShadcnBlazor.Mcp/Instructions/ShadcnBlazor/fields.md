---
title: Field
description: Composable form primitives: fields, fieldgroups, fieldsets, labels, titles, descriptions, legends and separators.
tags: [field, forms, label, fieldset, legend, separator]
---

# Field

The Field family provides the small composable primitives used to build consistent, accessible form layouts: `Field` wraps a single form control with a `FieldLabel`, an input and an optional `FieldDescription`; `FieldGroup` and `FieldSet` group fields together (the set renders a real `<fieldset>` and pairs with a `FieldLegend`); `FieldContent`/`FieldTitle`/`FieldDescription` build a rich field body; and `FieldSeparator` draws a divider between fieldsets. `Field` supports three orientations — `Vertical`, `Horizontal` and `Responsive` — and, because the wrapper reacts to a `data-invalid` attribute with destructive-colored text, the components slot cleanly into validation flows. Use them whenever you assemble a form: combine a `Field` with any input component (`TextInputField`, `Select`, `Checkbox`, `RadioGroup`, …) and group related fields into `FieldSet`s.

## Usage

```razor
@using ShadcnBlazor.Fields
@using ShadcnBlazor.Inputs

<Field>
    <FieldLabel for="name">Name</FieldLabel>
    <TextInputField @bind-Value="Name" id="name" placeholder="Your name" />
    <FieldDescription>Your display name</FieldDescription>
</Field>

@code
{
    private string Name;
}
```

## Parameters

| Name | Type | Default | Description |
|---|---|---|---|
| `ChildContent` | `RenderFragment` | `null` | **(Field)** The content of the field: typically a `FieldLabel`, an input component and optionally a `FieldDescription`. |
| `Orientation` | `FieldOrientation` | `FieldOrientation.Vertical` | **(Field)** Layout orientation of the field and its children. Values: `Vertical = 0`, `Horizontal = 1`, `Responsive = 2`. Sets the `data-orientation` attribute on the rendered `div`. |
| `ChildContent` | `RenderFragment` | `null` | **(FieldContent)** Content rendered inside the `field-content` wrapper; combine with `FieldTitle` and `FieldDescription` to build rich field bodies. |
| `ChildContent` | `RenderFragment` | `null` | **(FieldDescription)** Helper text, rendered as a `<p>` with `data-slot="field-description"`. |
| `ChildContent` | `RenderFragment` | `null` | **(FieldGroup)** Fields and fieldsets to group together, rendered inside a `div` with `data-slot="field-group"`. |
| `ChildContent` | `RenderFragment` | `null` | **(FieldLabel)** The label text; rendered as a `Label` component with `data-slot="field-label"`. Unmatched attributes (such as `for`) are forwarded to the label. |
| `ChildContent` | `RenderFragment` | `null` | **(FieldLegend)** Legend text for a `FieldSet`, rendered as a `<legend>` with `data-slot="field-legend"`. |
| `Variant` | `FieldLegendVariant` | `FieldLegendVariant.Legend` | **(FieldLegend)** Legend variant. Values: `Legend = 0`, `Label = 1`. Sets the `data-variant` attribute on the rendered `<legend>`. |
| `ChildContent` | `RenderFragment?` | `null` | **(FieldSeparator)** Optional separator text (e.g. "or"); when omitted the separator renders a bare `Separator` line. |
| `ChildContent` | `RenderFragment` | `null` | **(FieldSet)** Grouped fields rendered inside a `<fieldset>` with `data-slot="field-set"`; typically contains a `FieldLegend`, a `FieldDescription` and a `FieldGroup`. |
| `ChildContent` | `RenderFragment` | `null` | **(FieldTitle)** Title text rendered inside a field's content area. |

All components inherit from `ShadcnBase`, so the base parameters (`ClassName` and `Props`) are also available on every one of them. `ClassName` is merged with the component's default Tailwind classes, and `Props` (a `Dictionary<string, object?>`) captures any unmatched attributes and applies them to the rendered element.

## Examples

A simple form with validation feedback. A `Field` whose `data-invalid` attribute is `true` colors its text in the destructive color (adapted from the login form):

```razor
@using ShadcnBlazor.Buttons
@using ShadcnBlazor.Fields
@using ShadcnBlazor.Inputs

<form class="w-full max-w-sm space-y-4" @onsubmit="OnSubmit">
    <FieldGroup>
        <Field data-invalid="@(EmailInvalid ? "true" : "false")">
            <FieldLabel for="email">Email</FieldLabel>
            <TextInputField
                @bind-Value="Email"
                id="email"
                aria-invalid="@(EmailInvalid ? "true" : "false")"
                ClassName="bg-background"
                placeholder="name@company.com"
                type="email" />
            @if (EmailInvalid)
            {
                <p class="text-sm text-destructive">
                    <span class="font-medium leading-none">Enter a valid email address.</span>
                </p>
            }
        </Field>
    </FieldGroup>
    <Button ClassName="w-full" Type="submit">Sign in</Button>
</form>

@code
{
    private string Email = string.Empty;
    private bool Submitted;

    private bool EmailInvalid => Submitted && !Email.Contains('@');

    private void OnSubmit() => Submitted = true;
}
```

A checkout form with `FieldSet`s, `FieldLegend`, `FieldDescription`, a `FieldSeparator` between sets, a `Horizontal` field hosting a `Checkbox`, and a horizontal row of action buttons (adapted from the checkout showcase):

```razor
@using ShadcnBlazor.Buttons
@using ShadcnBlazor.Checkboxes
@using ShadcnBlazor.Fields
@using ShadcnBlazor.Inputs

<div class="w-full max-w-md">
    <form>
        <FieldGroup>
            <FieldSet>
                <FieldLegend>Payment Method</FieldLegend>
                <FieldDescription>All transactions are secure and encrypted</FieldDescription>
                <FieldGroup>
                    <Field>
                        <FieldLabel for="card-name">Name on Card</FieldLabel>
                        <TextInputField @bind-Value="Name" id="card-name" placeholder="Evil Rabbit" required />
                    </Field>
                    <Field>
                        <FieldLabel for="card-number">Card Number</FieldLabel>
                        <TextInputField @bind-Value="CardNumber" id="card-number" placeholder="1234 5678 9012 3456" required />
                        <FieldDescription>Enter your 16-digit card number</FieldDescription>
                    </Field>
                </FieldGroup>
            </FieldSet>
            <FieldSeparator />
            <FieldSet>
                <FieldLegend>Billing Address</FieldLegend>
                <FieldDescription>The billing address associated with your payment method</FieldDescription>
                <FieldGroup>
                    <Field Orientation="FieldOrientation.Horizontal">
                        <Checkbox id="same-as-shipping" DefaultValue="true" />
                        <FieldLabel for="same-as-shipping" ClassName="font-normal">
                            Same as shipping address
                        </FieldLabel>
                    </Field>
                </FieldGroup>
            </FieldSet>
            <Field Orientation="FieldOrientation.Horizontal">
                <Button Type="submit">Submit</Button>
                <Button Variant="ButtonVariant.Outline">Cancel</Button>
            </Field>
        </FieldGroup>
    </form>
</div>

@code
{
    private string Name;
    private string CardNumber;
}
```

Choice cards built from a `RadioGroup` where each option is a `FieldLabel` wrapping a `Horizontal` `Field` whose `FieldContent` holds a `FieldTitle` and a `FieldDescription` (adapted from the choice-cards block):

```razor
@using ShadcnBlazor.Fields
@using ShadcnBlazor.RadioGroups

<div class="w-full max-w-md">
    <FieldGroup>
        <FieldSet>
            <FieldLabel>Deployment Target</FieldLabel>
            <FieldDescription>Pick where your application will be deployed.</FieldDescription>
            <RadioGroup @bind-Value="SelectedTarget">
                <FieldLabel ClassName="bg-background" for="production">
                    <Field Orientation="FieldOrientation.Horizontal">
                        <FieldContent>
                            <FieldTitle>Production</FieldTitle>
                            <FieldDescription>Ship to the live environment with full traffic.</FieldDescription>
                        </FieldContent>
                        <RadioGroupItem id="production" Value="production" />
                    </Field>
                </FieldLabel>
                <FieldLabel ClassName="bg-background" for="staging">
                    <Field Orientation="FieldOrientation.Horizontal">
                        <FieldContent>
                            <FieldTitle>Staging</FieldTitle>
                            <FieldDescription>Preview changes on an isolated environment first.</FieldDescription>
                        </FieldContent>
                        <RadioGroupItem id="staging" Value="staging" />
                    </Field>
                </FieldLabel>
            </RadioGroup>
        </FieldSet>
    </FieldGroup>
</div>

@code
{
    private string SelectedTarget = "production";
}
```

All three orientations in one view: the default `Vertical` layout stacks the label, input and description; `Horizontal` places the control next to the label; `Responsive` is vertical on small screens and horizontal on `md` and up:

```razor
@using ShadcnBlazor.Checkboxes
@using ShadcnBlazor.Fields
@using ShadcnBlazor.Inputs

<div class="flex w-full max-w-md flex-col gap-6">
    <Field>
        <FieldLabel for="name">Name</FieldLabel>
        <TextInputField @bind-Value="Name" id="name" placeholder="Your name" />
        <FieldDescription>Your display name</FieldDescription>
    </Field>
    <Field Orientation="FieldOrientation.Horizontal">
        <Checkbox id="terms" DefaultValue="false" />
        <FieldLabel for="terms" ClassName="font-normal">Accept the terms</FieldLabel>
    </Field>
    <Field Orientation="FieldOrientation.Responsive">
        <FieldLabel for="company">Company</FieldLabel>
        <TextInputField @bind-Value="Company" id="company" placeholder="Company" />
    </Field>
</div>

@code
{
    private string Name;
    private string Company;
}
```
