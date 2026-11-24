---
title: Kbd
description: Renders a keyboard key or a group of keys to display shortcuts and key combinations.
tags: [keyboard, key, shortcut, kbd, key-combo]
---

# Kbd

The Kbd component renders a single keyboard key (`<kbd>`) to display key shortcuts, while KbdGroup lays out a row of keys as a combination. Use it to represent keyboard shortcuts and commands in your UI, such as search hints or editor keybindings. Both components inherit from `ShadcnBase`, so they accept `ClassName` for style overrides and pass through any unmatched attributes via `Props`.

## Usage

```razor
<Kbd>⌘</Kbd>
```

## Parameters

The following parameters are shared by both `Kbd` and `KbdGroup`.

| Name | Type | Default | Description |
|------|------|---------|-------------|
| ChildContent | RenderFragment | null | The content rendered inside the key (e.g. the key label or an icon). |
| ClassName | string | null | Inherited from `ShadcnBase`. Extra CSS classes merged with the base classes via `Cn`. |
| Props | Dictionary<string, object>? | null | Inherited from `ShadcnBase`. Captures unmatched attributes and applies them to the root element. |

## Examples

A single key and a shortcut combination:

```razor
<Kbd>⌘</Kbd>
<KbdGroup>
    <Kbd>⌘</Kbd>
    <Kbd>K</Kbd>
</KbdGroup>
```

Overriding the look of a key with `ClassName`:

```razor
<Kbd ClassName="bg-emerald-100 text-emerald-900">↵</Kbd>
```

Passing arbitrary attributes through `Props`:

```razor
<Kbd title="Toggle command palette">⌘K</Kbd>
```
