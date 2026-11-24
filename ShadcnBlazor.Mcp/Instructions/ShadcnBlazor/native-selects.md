---
title: Native Select
description: Styled native <select> with option and optgroup children, a chevron indicator, and HTML attribute passthrough.
tags: [select, native-select, dropdown, options, optgroup, form]
---

# Native Select

The NativeSelects folder wraps the browser's native `<select>` in a styled component that keeps all of its native keyboard and accessibility behavior while matching the library's design system. `NativeSelect` renders a bordered, focusable select with a `ChevronDownIcon` indicator on the right, and is populated with `NativeSelectOption` items and optionally grouped `NativeSelectOptGroup` blocks. Because the underlying element is a real `<select>`, any standard HTML attribute (`multiple`, `required`, `disabled`, `value`, ...) is forwarded to it, so it works with forms and `EditContext` without extra state.

Use `NativeSelect` when you need a lightweight, dependency-free dropdown rather than a full `Select` portal component.

## Usage

```razor
<NativeSelect>
    <NativeSelectOption>Vanilla</NativeSelectOption>
    <NativeSelectOption>Chocolate</NativeSelectOption>
    <NativeSelectOption>Strawberry</NativeSelectOption>
</NativeSelect>
```

## Parameters

### NativeSelect

| Name | Type | Default | Description |
| --- | --- | --- | --- |
| `ChildContent` | `RenderFragment` | — | The `NativeSelectOption` and/or `NativeSelectOptGroup` items rendered inside the `<select data-slot="native-select">`. |
| `ClassName` | `string` | `null` | *(inherited from `ShadcnBase`)* Extra classes merged into the base select styling via tailwind-merge. |
| `Props` | `Dictionary<string, object>?` | `null` | *(inherited from `ShadcnBase`)* Any unmatched HTML attributes applied to the `<select>` element (e.g. `multiple`, `required`, `value`). |

### NativeSelectOptGroup

| Name | Type | Default | Description |
| --- | --- | --- | --- |
| `ChildContent` | `RenderFragment` | — | The `NativeSelectOption` elements contained in the `<optgroup data-slot="native-select-optgroup">`. |
| `ClassName` | `string` | `null` | *(inherited from `ShadcnBase`)* Applied as the class of the `<optgroup>`; the only `ShadcnBase` member the markup uses. |
| `Props` | `Dictionary<string, object>?` | `null` | *(inherited from `ShadcnBase`)* Declared by `ShadcnBase` but not spread onto the `<optgroup>` in the markup — only `ClassName` is applied. |

### NativeSelectOption

| Name | Type | Default | Description |
| --- | --- | --- | --- |
| `ChildContent` | `RenderFragment` | — | The display text of the `<option data-slot="native-select-option">`. |
| `ClassName` | `string` | `null` | *(inherited from `ShadcnBase`)* Applied directly as the class of the `<option>`. |
| `Props` | `Dictionary<string, object>?` | `null` | *(inherited from `ShadcnBase`)* Any unmatched HTML attributes applied to the `<option>` element (e.g. `value`, `disabled`, `selected`). |

## Examples

### Basic options

```razor
<NativeSelect>
    <NativeSelectOption>Apple</NativeSelectOption>
    <NativeSelectOption>Orange</NativeSelectOption>
    <NativeSelectOption>Cherry</NativeSelectOption>
</NativeSelect>
```

### Grouped options with NativeSelectOptGroup

```razor
<NativeSelect>
    <NativeSelectOptGroup>
        <NativeSelectOption>Apple</NativeSelectOption>
        <NativeSelectOption>Orange</NativeSelectOption>
    </NativeSelectOptGroup>
    <NativeSelectOptGroup>
        <NativeSelectOption>Salmon</NativeSelectOption>
        <NativeSelectOption>Tuna</NativeSelectOption>
    </NativeSelectOptGroup>
</NativeSelect>
```

### Multi-select with HTML attribute passthrough

Standard `<select>` attributes flow through `Props` onto the native element.

```razor
<NativeSelect multiple required disabled="IsLoading">
    <NativeSelectOption>Alpha</NativeSelectOption>
    <NativeSelectOption>Beta</NativeSelectOption>
    <NativeSelectOption>Gamma</NativeSelectOption>
</NativeSelect>

@code
{
    private bool IsLoading;
}
```

### Reading the selected value via @ref

Because the component renders a plain native `<select>`, take a `@ref` to it and read the value through `HTMLSelectElement`.

```razor
<NativeSelect @ref="Select">
    <NativeSelectOption>One</NativeSelectOption>
    <NativeSelectOption>Two</NativeSelectOption>
</NativeSelect>
<Button OnClick="ReadValue">Read</Button>

@code
{
    private ElementReference Select;
    private string Selected = "";

    private async Task ReadValue()
    {
        var select = await Select.GetElementAsync<HTMLSelectElement>();
        Selected = select.Value;
    }
}
```
