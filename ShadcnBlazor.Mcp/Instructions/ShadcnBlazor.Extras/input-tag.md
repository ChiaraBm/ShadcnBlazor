---
title: Input Tag
description: Free-form tag input: type and press Enter or Space to add a tag as a pill, click a tag to remove it; two-way bound List<string>.
tags: [input, tags, chips, multi-value, form, free-text]
---

# Input Tag

The InputTags folder provides `InputTags`, a free-form tag input. It renders a wrapping container of pill-shaped tags plus a single-line `<input>` for new entries: typing text and pressing `Enter` or `Space` adds the trimmed text to the bound `List<string>`, and clicking an existing tag removes it. The list is kept in place (mutated directly) and reported through `ValueChanged`, so callers always receive the same `List<string>` instance they bound.

Use `InputTags` when a form field accepts an open-ended set of values — search facets, filters, labels, tags — and a fixed `Select`/`Combobox` is not sufficient. The container picks up the standard shadcn input styling, including the focus ring and the `aria-invalid` destructive ring, and `ClassName` can override or extend the appearance.

## Usage

```razor
@using ShadcnBlazor.Extras.InputTag

<InputTags Value="Tags" ValueChanged="OnTagsChanged" />

@code
{
    private List<string> Tags = new();

    private void OnTagsChanged(List<string> value)
    {
        // Tags was mutated in place; value is the same list instance.
    }
}
```

## Parameters

| Name | Type | Default | Description |
| --- | --- | --- | --- |
| `Value` | `List<string>` | `new()` | The tags to render. Each entry is rendered as a pill; clicking a pill removes it. New tags are appended to this same list. |
| `ValueChanged` | `EventCallback<List<string>>` | — | Invoked after a tag was added (via `Enter`/`Space`) or removed (via clicking a pill), with the mutated `Value` list. |
| `ClassName` | `string` | `null` | *(inherited from `ShadcnBase`)* Merged via `Cn` with the container's base input styling. |
| `Props` | `Dictionary<string, object>?` | `null` | *(inherited from `ShadcnBase`)* Extra attributes spread onto the inner `<input>` element, e.g. `placeholder`, `disabled` or `aria-invalid`. |

## Examples

### Initial tags with a Label

```razor
@using ShadcnBlazor.Extras.InputTag
@using ShadcnBlazor.Labels

<div class="grid gap-2">
    <Label for="tags">Patents</Label>
    <InputTags id="tags" Value="Tags" />
</div>

@code
{
    private List<string> Tags = ["1", "2", "3", "6"];
}
```

### Two-way binding with ValueChanged

Pressing `Enter` or `Space` with non-empty text appends the trimmed tag; clicking a pill removes it. Both paths invoke `ValueChanged` with the same list instance.

```razor
@using ShadcnBlazor.Extras.InputTag

<InputTags Value="Tags" ValueChanged="OnTagsChanged" />

<p>Current tags: @(string.Join(", ", Tags))</p>

@code
{
    private List<string> Tags = ["alpha"];

    private void OnTagsChanged(List<string> value)
    {
        // value is the same instance as Tags, already updated.
        // e.g. persist or validate here
    }
}
```

### Customizing the inner input via Props and ClassName

`Props` lands on the inner `<input>`, so placeholders and ARIA state work out of the box; the `aria-invalid` state also activates the built-in destructive ring. `ClassName` restyles the wrapping container.

```razor
@using ShadcnBlazor.Extras.InputTag

<InputTags Value="Tags"
           ValueChanged="OnTagsChanged"
           ClassName="max-w-sm"
           placeholder="Add a tag and press Enter"
           disabled />

@code
{
    private List<string> Tags = ["one", "two"];

    private void OnTagsChanged(List<string> value) { }
}
```
