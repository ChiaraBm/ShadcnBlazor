---
title: Common Components
description: LazyLoader defers child rendering until an async Load callback finishes; WButton shows a spinner while its OnClick runs.
tags: [lazy-loader, worker-button, wbutton, spinner, async-loading, button]
---

# Common Components

The Common folder bundles two small helper components: `LazyLoader` and `WButton`. `LazyLoader` defers rendering of its child content until an async `Load` callback completes, showing a spinner in the meantime. `WButton` wraps the standard shadcn `Button` so that, while its `OnClick` handler is executing, it disables itself and swaps its content for a spinner.

Both inherit from `ShadcnBase`, so they accept a `ClassName` override and arbitrary attributes (spread through `Props`) to customize the underlying spinner or button element.

## LazyLoader

`LazyLoader` renders a centered `Spinner` until the async `Load` delegate has completed, then renders `ChildContent` in its place. `Load` is invoked automatically on first render; the delegate receives the `LazyLoader` instance so the callback can call `SetTextAsync` to surface a progress message under the spinner and `ReloadAsync` to re-run the load (which is a no-op while a load is already in progress). While loading, `IsLoading` is `true`; once `Load` returns, the spinner is replaced by the child content.

### Usage

```razor
@using ShadcnBlazor.Extras.Common

<LazyLoader Load="LoadItemsAsync">
    <ul>
        @foreach (var item in _items)
        {
            <li>@item.Name</li>
        }
    </ul>
</LazyLoader>
```

### Parameters

| Name | Type | Default | Description |
| --- | --- | --- | --- |
| `ChildContent` | `RenderFragment` | — | The content rendered once loading has completed. |
| `Load` | `Func<LazyLoader, Task>` | — | The async delegate invoked on first render (and by `ReloadAsync`) to fetch the data displayed by `ChildContent`. It receives the `LazyLoader` instance so the callback can call `SetTextAsync` while loading. |
| `ClassName` | `string` | `null` | *(inherited from `ShadcnBase`)* Merged via `Cn` with the default `"size-10"` and applied to the `Spinner`. |
| `Props` | `Dictionary<string, object>?` | `null` | *(inherited from `ShadcnBase`)* Extra attributes spread onto the `Spinner`. |

### Examples

#### Basic list loading with a progress message

`SetTextAsync` is called from inside `Load`; it shows the given text under the spinner until loading finishes.

```razor
@using ShadcnBlazor.Extras.Common

<LazyLoader Load="LoadItemsAsync">
    <ul>
        @foreach (var item in _items)
        {
            <li>@item.Name</li>
        }
    </ul>
</LazyLoader>

@code
{
    private List<Item> _items = [];

    private async Task LoadItemsAsync(LazyLoader loader)
    {
        await loader.SetTextAsync("Loading items…");
        _items = await _service.GetItemsAsync();
    }
}
```

#### Restyling the spinner and reloading

`ClassName` and arbitrary attributes land on the `Spinner`; `ReloadAsync` re-runs `Load` (guarded so it cannot start while a load is in flight).

```razor
@using ShadcnBlazor.Extras.Common

<LazyLoader Load="LoadItemsAsync" ClassName="size-16" data-testid="items-loader">
    @(_items.Count) items
</LazyLoader>
<WButton OnClick="@(() => _loader.ReloadAsync())">Reload</WButton>

@code
{
    [CascadingParameter] private LazyLoader? _loader { get; set; }

    private List<Item> _items = [];

    private async Task LoadItemsAsync(LazyLoader loader)
    {
        _loader = loader;
        _items = await _service.GetItemsAsync();
    }
}
```

## WButton

`WButton` renders a standard shadcn `Button` and, on click, calls `HandleAsync`, which sets `IsWorking`, disables the button, and invokes the `OnClick` callback. While `IsWorking` is `true`, the button shows a `Spinner`: if `Text` is empty it renders as an icon-sized button (`ButtonSize.Icon`) containing only the spinner, and once `SetTextAsync` has set a value it renders at `Size` with the spinner plus the text. When the callback completes the button returns to its normal `ChildContent` state and becomes clickable again.

### Usage

```razor
@using ShadcnBlazor.Extras.Common
@using ShadcnBlazor.Buttons

<WButton Size="ButtonSize.Lg" OnClick="SubmitAsync">
    Submit
</WButton>
```

### Parameters

| Name | Type | Default | Description |
| --- | --- | --- | --- |
| `ChildContent` | `RenderFragment` | — | The content rendered inside the `Button` while the button is not working. |
| `Size` | `ButtonSize` | `ButtonSize.Default` | Size of the button when not working (and when working with text set). Valid values: `ButtonSize.Sm`, `ButtonSize.Default`, `ButtonSize.Lg`, `ButtonSize.IconSm`, `ButtonSize.Icon`, `ButtonSize.IconLg`. |
| `Variant` | `ButtonVariant` | `ButtonVariant.Default` | Variant of the button. Valid values: `ButtonVariant.Default`, `ButtonVariant.Outline`, `ButtonVariant.Destructive`, `ButtonVariant.Secondary`, `ButtonVariant.Ghost`, `ButtonVariant.Link`. |
| `OnClick` | `EventCallback<WButton>` | — | Invoked when the button is clicked. While it runs, the button is disabled and shows a `Spinner` (icon-sized when no text, otherwise spinner plus `Text`). |
| `ClassName` | `string` | `null` | *(inherited from `ShadcnBase`)* Merged via `Cn` and applied to the `Button`. |
| `Props` | `Dictionary<string, object>?` | `null` | *(inherited from `ShadcnBase`)* Extra attributes spread onto the `Button`. |

### Examples

#### Save button that shows "Saving…" while running

`SetTextAsync` is called from inside `OnClick`; the button then renders the spinner plus the text until the handler returns.

```razor
@using ShadcnBlazor.Extras.Common

<WButton OnClick="SaveAsync">Save</WButton>

@code
{
    private async Task SaveAsync(WButton button)
    {
        await button.SetTextAsync("Saving…");
        await _service.SaveAsync();
    }
}
```

#### Styled button that shows only a spinner

With no text set, the working state collapses to an icon-sized spinner button.

```razor
@using ShadcnBlazor.Extras.Common
@using ShadcnBlazor.Buttons

<WButton Size="ButtonSize.Lg" Variant="ButtonVariant.Outline" OnClick="PublishAsync">
    Publish
</WButton>

@code
{
    private async Task PublishAsync(WButton button)
    {
        // No SetTextAsync call, so the working state is an icon-sized spinner.
        await _service.PublishAsync();
    }
}
```
