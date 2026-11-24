---
title: Form Handler
description: EnhancedEditForm with async submit and IsProcessing state, plus SubmitButton and FormValidationSummary for validation.
tags: [forms, edit-form, validation, submit-button, processing-state]
---

# Form Handler

The Forms folder provides a small form-handling toolkit for Blazor. `EnhancedEditForm` wraps the built-in `EditForm` and turns submission into an asynchronous flow: it exposes `IsProcessing` and runs three optional callbacks (`OnSubmit`, `OnValidSubmit`, `OnInvalidSubmit`) that each receive the `EditContext` and `ValidationMessageStore` and return `Task<bool>`. It also offers `SubmitAsync()` to trigger submission programmatically through the `ShadcnBlazorExtras.submitForm` JavaScript helper. Pair it with `SubmitButton`, which swaps its label for a spinner plus `ProgressText` while the form is processing, and with `FormValidationSummary`, a drop-in replacement for the standard `<ValidationSummary>` that renders all validation errors inside a destructive `Alert`. Use this when you need async submit handling, a processing indicator on the submit control, and aggregated validation feedback.

All three components inherit from `ShadcnBase`, so each also accepts the base `ClassName` (`string`) and `Props` (capture-all unmatched attributes) parameters.

## Usage

```razor
@using Microsoft.AspNetCore.Components.Forms
@using ShadcnBlazor.Extras.Forms

<EnhancedEditForm Model="@Model" OnSubmit="SaveAsync">
    @(_ => {
        <InputText @bind-Value="Model.Name" placeholder="Name" />
        <SubmitButton>Save</SubmitButton>
    })
</EnhancedEditForm>

@code
{
    public class Profile { public string Name { get; set; } = ""; }
    private Profile Model { get; set; } = new();

    private async Task<bool> SaveAsync(EditContext context, ValidationMessageStore store)
    {
        await Task.Delay(800);   // simulate a round-trip
        return true;             // true => keep the submitted values
    }
}
```

## Parameters

### EnhancedEditForm

| Name | Type | Default | Description |
| --- | --- | --- | --- |
| `ChildContent` | `RenderFragment<EditContext>` | `null` | Form body. Invoked once per render with the component's `EditContext` as its argument, inside a `CascadingValue` whose value is the `EnhancedEditForm` instance. |
| `Model` | `object` | `null` | The model object. An `EditContext` and `ValidationMessageStore` are created from it during `OnInitialized`. |
| `OnSubmit` | `Func<EditContext, ValidationMessageStore, Task<bool>>?` | `null` | Called on every submit, whether the model is valid or not. Returning `false` re-notifies validation-state changes. |
| `OnValidSubmit` | `Func<EditContext, ValidationMessageStore, Task<bool>>?` | `null` | Called only when the model passes validation. Returning `false` re-notifies validation-state changes. |
| `OnInvalidSubmit` | `Func<EditContext, ValidationMessageStore, Task<bool>>?` | `null` | Called only when the model fails validation. Returning `false` re-notifies validation-state changes. |

### SubmitButton

| Name | Type | Default | Description |
| --- | --- | --- | --- |
| `ChildContent` | `RenderFragment` | `null` | Label shown inside the submit `Button` while the form is not processing. |
| `Size` | `ButtonSize` | `ButtonSize.Default` | Size of the rendered `Button`. |
| `Variant` | `ButtonVariant` | `ButtonVariant.Default` | Variant of the rendered `Button`. |
| `ProgressText` | `string` | `Translations.Forms_SubmitButton_ProgressText` (`Submitting`) | Label shown next to the `Spinner` while the cascaded form is processing. |
| `Form` | `EnhancedEditForm` | `—` (cascading) | `[CascadingParameter]` supplied by the enclosing `EnhancedEditForm`. When its `IsProcessing` is `true` the button renders disabled with a spinner and `ProgressText`; otherwise it renders `ChildContent`. |

### FormValidationSummary

| Name | Type | Default | Description |
| --- | --- | --- | --- |
| `CurrentEditContext` | `EditContext?` | `—` (cascading) | `[CascadingParameter]` supplied by the enclosing `EditForm`. When present and non-empty, renders a destructive `Alert` titled "Following issues were found:" listing each message from `GetValidationMessages()`. Throws `InvalidOperationException` if not provided. |

## Examples

### Submit button that switches to a spinner while processing

```razor
@using Microsoft.AspNetCore.Components.Forms
@using ShadcnBlazor.Buttons
@using ShadcnBlazor.Extras.Forms

<EnhancedEditForm Model="@Model" OnSubmit="SaveAsync">
    @(_ => {
        <InputText @bind-Value="Model.Name" placeholder="Name" />
        <SubmitButton Variant="ButtonVariant.Secondary" ProgressText="Saving…">Save</SubmitButton>
    })
</EnhancedEditForm>

@code
{
    public class Profile { public string Name { get; set; } = ""; }
    private Profile Model { get; set; } = new();

    private async Task<bool> SaveAsync(EditContext context, ValidationMessageStore store)
    {
        await Task.Delay(1200);   // SubmitButton shows "Saving…" + spinner during this
        return true;
    }
}
```

### Validation summary inside a standard EditForm

`FormValidationSummary` cascades an `EditContext` from a plain `EditForm` (it is a drop-in for `<ValidationSummary>`), so use it there rather than inside `EnhancedEditForm`.

```razor
@using ShadcnBlazor.Extras.Forms

<EditForm Model="@User" OnValidSubmit="() => ValidSubmitted()">
    <InputText @bind-Value="User.Name" />
    <ValidationMessage For="() => User.Name" />
    <FormValidationSummary />
    <button type="submit">Submit</button>
</EditForm>

@code
{
    public class User { public string Name { get; set; } = ""; }
    private User User { get; set; } = new();

    private void ValidSubmitted() { /* persist User */ }
}
```

### All three submit callbacks plus programmatic submission

`OnSubmit` always runs; `OnValidSubmit` only when valid; `OnInvalidSubmit` only when invalid. `SubmitAsync()` fires the form from outside any `<button type="submit">`.

```razor
@using Microsoft.AspNetCore.Components.Forms
@using ShadcnBlazor.Buttons
@using ShadcnBlazor.Extras.Forms

<EnhancedEditForm @ref="FormRef"
                  Model="@Model"
                  OnSubmit="AnySubmitAsync"
                  OnValidSubmit="ValidSubmitAsync"
                  OnInvalidSubmit="InvalidSubmitAsync">
    @(_ => {
        <InputText @bind-Value="Model.Email" />
        <ValidationMessage For="() => Model.Email" />
        <SubmitButton>Save</SubmitButton>
    })
</EnhancedEditForm>

<Button OnClick="SubmitManually">Submit programmatically</Button>

@code
{
    private EnhancedEditForm? FormRef;

    public class Account { public string Email { get; set; } = ""; }
    private Account Model { get; set; } = new();

    private async Task<bool> AnySubmitAsync(EditContext ctx, ValidationMessageStore store)
    {
        await Task.Delay(100);
        return true;
    }

    private async Task<bool> ValidSubmitAsync(EditContext ctx, ValidationMessageStore store)
    {
        await Task.Delay(100);   // runs only when the model is valid
        return true;
    }

    private async Task<bool> InvalidSubmitAsync(EditContext ctx, ValidationMessageStore store)
    {
        await Task.Delay(100);   // runs only when the model is invalid
        return true;
    }

    private async Task SubmitManually() => await FormRef!.SubmitAsync();
}
```
