---
title: Button
description: Styled trigger button with size and variant options, plus ButtonGroup for arranging related buttons and separators.
tags: [button, button-group, action, trigger, separator, variant]
---

# Button

A clickable control for triggering actions, with configurable `ButtonVariant` (Default, Outline, Destructive, Secondary, Ghost, Link) and `ButtonSize` (Sm, Default, Lg, IconSm, Icon, IconLg). Use it for submitting forms, launching commands, or toggling state. The `ButtonGroup` component arranges multiple buttons into a joined row or column, and `ButtonGroupSeparator` / `ButtonGroupText` provide separators and labeled sections within a group.

## Usage

```razor
<Button>Click me</Button>
```

## Parameters

### Button

| Name | Type | Default | Description |
| --- | --- | --- | --- |
| `ChildContent` | `RenderFragment` | — | The button label or icon content. |
| `Slot` | `RenderFragment<Dictionary<string, object?>>?` | `null` | Renders a custom element with the button's computed attributes (including `data-slot="button"` and `type`). |
| `Size` | `ButtonSize` | `ButtonSize.Default` | Button size: `Sm`, `Default`, `Lg`, `IconSm`, `Icon`, `IconLg`. |
| `Variant` | `ButtonVariant` | `ButtonVariant.Default` | Visual style: `Default`, `Outline`, `Destructive`, `Secondary`, `Ghost`, `Link`. |
| `Type` | `string` | `"button"` | The `type` attribute of the underlying `<button>` element. |

### ButtonGroup

| Name | Type | Default | Description |
| --- | --- | --- | --- |
| `ChildContent` | `RenderFragment` | — | The buttons or other controls arranged as a group. |
| `Orientation` | `ButtonGroupOrientation` | `ButtonGroupOrientation.Horizontal` | Layout direction: `Horizontal` or `Vertical`. |

### ButtonGroupSeparator

| Name | Type | Default | Description |
| --- | --- | --- | --- |
| `Orientation` | `SeparatorOrientation` | `SeparatorOrientation.Vertical` | Separator orientation; the element is rendered as a `Separator` with `data-slot="button-group-separator"`. |

### ButtonGroupText

| Name | Type | Default | Description |
| --- | --- | --- | --- |
| `ChildContent` | `RenderFragment` | — | The section label content rendered in a muted box. |
| `Slot` | `RenderFragment<Dictionary<string, object?>>?` | `null` | Renders a custom element with the computed wrapper attributes. |

## Examples

### Variants

```razor
<Button Variant="ButtonVariant.Default">Default</Button>
<Button Variant="ButtonVariant.Secondary">Secondary</Button>
<Button Variant="ButtonVariant.Outline">Outline</Button>
<Button Variant="ButtonVariant.Destructive">Destructive</Button>
<Button Variant="ButtonVariant.Ghost">Ghost</Button>
<Button Variant="ButtonVariant.Link">Link</Button>
```

### Sizes

```razor
<Button Size="ButtonSize.Default">Default</Button>
<Button Size="ButtonSize.Lg">Large</Button>
<Button Size="ButtonSize.Sm">Small</Button>
<Button Size="ButtonSize.Icon">Icon</Button>
<Button Size="ButtonSize.IconLg">Icon Large</Button>
<Button Size="ButtonSize.IconSm">Icon Small</Button>
```

### Button group with text and separator

```razor
<ButtonGroup>
    <ButtonGroupText>Podcast</ButtonGroupText>
    <Button Size="ButtonSize.Icon">Subscribe</Button>
    <ButtonGroupSeparator />
    <Button Size="ButtonSize.Icon">Share</Button>
</ButtonGroup>
```

### Submit button in a form

```razor
<EditForm OnValidSubmit="@OnSubmit">
    <InputText @bind-Value="Name" />
    <Button Type="submit">Submit</Button>
</EditForm>

@code
{
    private string? Name;
    private void OnSubmit() => Console.WriteLine(Name);
}
```
