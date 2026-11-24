---
title: Input
description: Input fields for text, numbers, dates and datetimes, with InputGroup addons and the InputOTP one-time-password widget.
tags: [input, text-input, input-group, otp, textarea, form]
---

# Input

The Inputs folder provides the styled form-field building blocks of the library: `TextInputField`, `DateInputField`, `DateTimeInputField`, `NumberInputField` and `TextareaInputField` are styled replacements for the plain `InputText` that work with `EditForm` validation, because they all inherit from `InputBase<TValue>`. Composite controls such as URL or currency fields are built by wrapping `InputGroupInput` or `InputGroupTextarea` in an `InputGroup` together with `InputGroupAddon`, `InputGroupButton` and `InputGroupText`. For masked one-time-password entry, `InputOTP` renders a row of `InputOTPGroup` / `InputOTPSlot` boxes behind a hidden numeric input and fires `OnInputValid` once the complete code has been typed.

## Usage

```razor
<TextInputField @bind-Value="Name" />

@code
{
    private string? Name = "";
}
```

## Parameters

### TextInputField

| Name | Type | Default | Description |
| --- | --- | --- | --- |
| `ClassName` | `string?` | `null` | Extra CSS classes merged into the base input styling via tailwind-merge. |
| `BindEvent` | `string` | `"onchange"` | DOM event that triggers the `Value` change; use `"oninput"` to update on every keystroke. |

### DateInputField

| Name | Type | Default | Description |
| --- | --- | --- | --- |
| `ClassName` | `string?` | `null` | Extra CSS classes merged into the base input styling via tailwind-merge. |
| `BindEvent` | `string` | `"onchange"` | DOM event that triggers the `Value` change; use `"oninput"` to update on every keystroke. |

### DateTimeInputField

| Name | Type | Default | Description |
| --- | --- | --- | --- |
| `ClassName` | `string?` | `null` | Extra CSS classes merged into the base input styling via tailwind-merge. |
| `BindEvent` | `string` | `"onchange"` | DOM event that triggers the `Value` change; use `"oninput"` to update on every keystroke. |

### NumberInputField

`NumberInputField` is generic (`@typeparam TValue`) and formats/parses numeric values with `CultureInfo.InvariantCulture`.

| Name | Type | Default | Description |
| --- | --- | --- | --- |
| `ClassName` | `string?` | `null` | Extra CSS classes merged into the base input styling via tailwind-merge. |
| `BindEvent` | `string` | `"onchange"` | DOM event that triggers the `Value` change; use `"oninput"` to update on every keystroke. |
| `ParsingErrorMessage` | `string` | `"The {0} field must be a number."` | Validation message when the entered text cannot be parsed into `TValue`; `{0}` is replaced by `DisplayName` or the field name. |

### TextareaInputField

| Name | Type | Default | Description |
| --- | --- | --- | --- |
| `ClassName` | `string?` | `null` | Extra CSS classes merged into the base textarea styling via tailwind-merge. |
| `BindEvent` | `string` | `"onchange"` | DOM event that triggers the `Value` change; use `"oninput"` to update on every keystroke. |

### InputGroup

| Name | Type | Default | Description |
| --- | --- | --- | --- |
| `ChildContent` | `RenderFragment` | — | The group content: an `InputGroupInput` or `InputGroupTextarea` plus optional addons. |
| `ClassName` | `string` | `null` | *(inherited from `ShadcnBase`)* Extra classes merged into the group wrapper. |
| `Props` | `Dictionary<string, object>?` | `null` | *(inherited from `ShadcnBase`)* Any unmatched HTML attributes applied to the `<div data-slot="input-group">`. |

### InputGroupAddon

| Name | Type | Default | Description |
| --- | --- | --- | --- |
| `ChildContent` | `RenderFragment` | — | The addon content, e.g. an `InputGroupText` or `InputGroupButton`. |
| `Align` | `InputGroupAddonAlign` | `InputGroupAddonAlign.InlineStart` | Position of the addon: `InlineStart`, `InlineEnd`, `BlockStart` or `BlockEnd`. |
| `ClassName` | `string` | `null` | *(inherited from `ShadcnBase`)* Extra classes merged into the addon. |
| `Props` | `Dictionary<string, object>?` | `null` | *(inherited from `ShadcnBase`)* Any unmatched HTML attributes applied to the addon div. |

### InputGroupButton

| Name | Type | Default | Description |
| --- | --- | --- | --- |
| `ChildContent` | `RenderFragment` | — | The button label or icon content. |
| `Size` | `InputGroupButtonSize` | `InputGroupButtonSize.Xs` | Size variant: `Xs`, `Sm`, `IconXs` or `IconSm`. |
| `Variant` | `ButtonVariant` | `ButtonVariant.Ghost` | Visual style: `Default`, `Outline`, `Destructive`, `Secondary`, `Ghost`, `Link`. |
| `Type` | `string` | `"button"` | The `type` attribute of the underlying `Button`. |
| `ClassName` | `string` | `null` | *(inherited from `ShadcnBase`)* Extra classes merged into the button. |
| `Props` | `Dictionary<string, object>?` | `null` | *(inherited from `ShadcnBase`)* Any unmatched HTML attributes applied to the button. |

### InputGroupText

| Name | Type | Default | Description |
| --- | --- | --- | --- |
| `ChildContent` | `RenderFragment` | — | The static text shown inside the group, e.g. a prefix or suffix label. |
| `ClassName` | `string` | `null` | *(inherited from `ShadcnBase`)* Extra classes merged into the text span. |
| `Props` | `Dictionary<string, object>?` | `null` | *(inherited from `ShadcnBase`)* Any unmatched HTML attributes applied to the span. |

### InputGroupInput

| Name | Type | Default | Description |
| --- | --- | --- | --- |
| `ClassName` | `string?` | `null` | Extra CSS classes merged into the group input styling via tailwind-merge. |
| `BindEvent` | `string` | `"onchange"` | DOM event that triggers the `Value` change; use `"oninput"` to update on every keystroke. |

### InputGroupTextarea

| Name | Type | Default | Description |
| --- | --- | --- | --- |
| `ClassName` | `string?` | `null` | Extra CSS classes merged into the group textarea styling via tailwind-merge. |
| `BindEvent` | `string` | `"onchange"` | DOM event that triggers the `Value` change; use `"oninput"` to update on every keystroke. |

### InputOTP

`InputOTP` also exposes the public (non-parameter) properties `Value` and `IsFocused` to its cascaded children, and the hidden numeric input enforces `MaxLenght` as its `maxlength`.

| Name | Type | Default | Description |
| --- | --- | --- | --- |
| `ChildContent` | `RenderFragment` | — | The `InputOTPGroup` / `InputOTPSlot` / `InputOPTSeparator` content, cascaded via `CascadingValue`. |
| `MaxLenght` | `int` | `0` | Maximum number of characters accepted; `OnInputValid` fires when `Value.Length` reaches this value. |
| `OnInputValid` | `EventCallback<string>` | `EventCallback.Empty` | Invoked with the complete `Value` once the full code has been entered. |
| `Disabled` | `bool` | `false` | Disables the hidden input and dims the whole widget. |
| `ClassName` | `string` | `null` | *(inherited from `ShadcnBase`)* Extra classes merged into the wrapper. |
| `Props` | `Dictionary<string, object>?` | `null` | *(inherited from `ShadcnBase`)* Any unmatched HTML attributes applied to the hidden `<input>`. |

### InputOTPGroup

| Name | Type | Default | Description |
| --- | --- | --- | --- |
| `ChildContent` | `RenderFragment` | — | The `InputOTPSlot` boxes (and separators) contained in the group. |
| `Parent` | `InputOTP?` | cascaded automatically | *(CascadingParameter)* The enclosing `InputOTP`; a `NullReferenceException` is thrown when used outside one. |
| `ClassName` | `string` | `null` | *(inherited from `ShadcnBase`)* Extra classes merged into the group div. |
| `Props` | `Dictionary<string, object>?` | `null` | *(inherited from `ShadcnBase`)* Any unmatched HTML attributes applied to the group div. |

### InputOTPSlot

| Name | Type | Default | Description |
| --- | --- | --- | --- |
| `Index` | `int` | `0` | Zero-based position of the slot; shows `Parent.Value[Index]` and the caret while that position is active. |
| `Parent` | `InputOTP` | cascaded automatically | *(CascadingParameter)* The enclosing `InputOTP`; a `NullReferenceException` is thrown when used outside one. |
| `ClassName` | `string` | `null` | *(inherited from `ShadcnBase`)* Extra classes merged into the slot. |
| `Props` | `Dictionary<string, object>?` | `null` | *(inherited from `ShadcnBase`)* Any unmatched HTML attributes applied to the slot div. |

### InputOPTSeparator

The component name is spelled `InputOPTSeparator` in the source (the tag is `<InputOPTSeparator />`), not `InputOTPSeparator`.

| Name | Type | Default | Description |
| --- | --- | --- | --- |
| `Parent` | `InputOTP?` | cascaded automatically | *(CascadingParameter)* The enclosing `InputOTP`; a `NullReferenceException` is thrown when used outside one. |
| `ClassName` | `string` | `null` | *(inherited from `ShadcnBase`)* Applied as the class of the separator div. |
| `Props` | `Dictionary<string, object>?` | `null` | *(inherited from `ShadcnBase`)* Any unmatched HTML attributes applied to the separator div. |

All `InputBase`-derived components above (`TextInputField`, `DateInputField`, `DateTimeInputField`, `NumberInputField`, `TextareaInputField`, `InputGroupInput`, `InputGroupTextarea`) additionally accept the standard Blazor `InputBase<TValue>` parameters, such as `Value`, `ValueChanged`, `AdditionalAttributes`, `EditContext`, `Name`, `Id`, `DisplayName`, `BindConverter`, `CssClass` and `AriaInvalid`.

## Examples

### Field variants (text, date, datetime, textarea)

```razor
<TextInputField @bind-Value="Name" placeholder="Name" />
<DateInputField @bind-Value="StartDate" />
<DateTimeInputField @bind-Value="MeetingAt" />
<TextareaInputField @bind-Value="Notes" BindEvent="oninput" />

@code
{
    private string? Name = "";
    private DateOnly StartDate;
    private DateTimeOffset MeetingAt;
    private string? Notes = "";
}
```

### Number input with a custom parsing error

```razor
<NumberInputField TValue="double"
                 @bind-Value="Amount"
                 BindEvent="oninput"
                 ParsingErrorMessage="The amount must be a number." />

@code
{
    private double Amount;
}
```

### Input group with addons and a button

```razor
<InputGroup>
    <InputGroupAddon Align="InputGroupAddonAlign.InlineStart">
        <InputGroupText>https://</InputGroupText>
    </InputGroupAddon>
    <InputGroupInput @bind-Value="Url" />
    <InputGroupAddon Align="InputGroupAddonAlign.InlineEnd">
        <InputGroupButton Size="InputGroupButtonSize.Xs" Variant="ButtonVariant.Ghost">Save</InputGroupButton>
    </InputGroupAddon>
</InputGroup>

@code
{
    private string? Url = "";
}
```

### One-time-password input with completion callback

```razor
<InputOTP MaxLenght="6" OnInputValid="OnOtpValid" Disabled="IsLocked">
    <InputOTPGroup>
        <InputOTPSlot Index="0" />
        <InputOTPSlot Index="1" />
        <InputOPTSeparator />
        <InputOTPSlot Index="2" />
        <InputOTPSlot Index="3" />
    </InputOTPGroup>
</InputOTP>

@code
{
    private bool IsLocked;
    private void OnOtpValid(string code) => Console.WriteLine($"OTP: {code}");
}
```
