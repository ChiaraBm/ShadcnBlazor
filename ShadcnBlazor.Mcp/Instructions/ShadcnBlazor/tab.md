---
title: Tabs
description: A tabbed interface that switches between content panels via trigger buttons, driven by cascading state.
tags: [tabs, navigation, trigger, list, content, state]
---

# Tabs

`Tabs` is a tabbed interface that switches between multiple content panels using a row of trigger buttons. The parent `Tabs` holds the active value and cascades it to its children, so `TabsTrigger` buttons set the active panel and `TabsContent` panels show or hide themselves accordingly. Use it when you need to present related information in distinct, switchable sections, such as a profile page, a settings dialog, or a documentation sidebar. The set is composed of three primitives — `TabsList` (the button strip), `TabsTrigger` (an individual button), and `TabsContent` (a single panel) — all wrapped by the `Tabs` container.

## Usage

```razor
<Tabs DefaultValue="account">
    <TabsList>
        <TabsTrigger Value="account">Account</TabsTrigger>
        <TabsTrigger Value="password">Password</TabsTrigger>
    </TabsList>
    <TabsContent Value="account">
        Account content
    </TabsContent>
    <TabsContent Value="password">
        Password content
    </TabsContent>
</Tabs>
```

## Parameters

`Tabs`, `TabsList`, `TabsTrigger`, and `TabsContent` all inherit `ClassName` and `Props` from `ShadcnBase`. `TabsTrigger` and `TabsContent` each take a `Tabs` cascading parameter (their parent). The rows below list one `[Parameter]` per component.

| Name | Type | Default | Description |
| --- | --- | --- | --- |
| ChildContent (Tabs) | RenderFragment | null | The tab set's content. Wrap the `TabsList` and the `TabsContent` panels inside. |
| DefaultValue (Tabs) | string | "" | The panel that is active when the `Tabs` first renders. Must match a `TabsContent`/`TabsTrigger` `Value`. |
| OnValueChanged (Tabs) | EventCallback<string> | default | Fired with the newly active `Value` whenever a `TabsTrigger` is clicked. |
| ClassName (Tabs) | string | null | Extra CSS classes merged with the base `flex flex-col gap-2`. |
| Props (Tabs) | Dictionary<string, object>? | null | Captured unmatched attributes applied to the `<div data-slot="tabs">`. |
| ChildContent (TabsList) | RenderFragment | null | The button strip's content. Wrap the `TabsTrigger` buttons inside. |
| ClassName (TabsList) | string | null | Extra CSS classes merged with the base `bg-muted text-muted-foreground inline-flex h-9 w-fit items-center justify-center rounded-lg p-[3px]`. |
| Props (TabsList) | Dictionary<string, object>? | null | Captured unmatched attributes applied to the `<div data-slot="tabs-list">`. |
| ChildContent (TabsTrigger) | RenderFragment | null | The label text (and optional icons) shown on a single tab button. |
| Value (TabsTrigger) | string | "" | The identifier of the panel this trigger activates. Match it to a `TabsContent Value`. |
| Disabled (TabsTrigger) | bool | false | Disables the trigger button so it cannot be clicked. |
| ClassName (TabsTrigger) | string | null | Extra CSS classes merged with the trigger's base classes. |
| Props (TabsTrigger) | Dictionary<string, object>? | null | Captured unmatched attributes applied to the `<button data-slot="tabs-trigger">`. |
| ChildContent (TabsContent) | RenderFragment | null | The body content shown when the panel is active. |
| Value (TabsContent) | string | "" | The identifier that links this panel to a `TabsTrigger Value`. |
| ClassName (TabsContent) | string | null | Extra CSS classes merged with the base `flex-1 outline-none`. |
| Props (TabsContent) | Dictionary<string, object>? | null | Captured unmatched attributes applied to the `<div data-slot="tabs-content">`. |

## Examples

### Full tab set with default panel

```razor
<Tabs DefaultValue="account">
    <TabsList>
        <TabsTrigger Value="account">Account</TabsTrigger>
        <TabsTrigger Value="password">Password</TabsTrigger>
    </TabsList>
    <TabsContent Value="account">
        <p>Change your name and email address.</p>
    </TabsContent>
    <TabsContent Value="password">
        <p>Update your password.</p>
    </TabsContent>
</Tabs>
```

`TabsContent` panels are hidden (`hidden` attribute) when their `Value` does not match the active `Tabs` value, and `data-state` is set to `active` or `inactive` to drive styling.

### Reacting to tab switches with OnValueChanged

```razor
<Tabs DefaultValue="overview" OnValueChanged="@(v => Console.WriteLine($"Switched to {v}"))">
    <TabsList>
        <TabsTrigger Value="overview">Overview</TabsTrigger>
        <TabsTrigger Value="activity">Activity</TabsTrigger>
    </TabsList>
    <TabsContent Value="overview">
        <p>Project overview.</p>
    </TabsContent>
    <TabsContent Value="activity">
        <p>Recent activity log.</p>
    </TabsContent>
</Tabs>
```

`OnValueChanged` receives the newly active `Value`, so it can be used to persist the selection or drive other state.

### Disabling a trigger

```razor
<Tabs DefaultValue="profile">
    <TabsList>
        <TabsTrigger Value="profile">Profile</TabsTrigger>
        <TabsTrigger Value="billing" Disabled>Billing</TabsTrigger>
    </TabsList>
    <TabsContent Value="profile">
        <p>Profile details.</p>
    </TabsContent>
    <TabsContent Value="billing">
        <p>Billing is not available yet.</p>
    </TabsContent>
</Tabs>
```

A `TabsTrigger` with `Disabled="true"` sets `disabled` and `data-disabled` on its button, so it is unclickable and styled as disabled.

### Customizing the look with ClassName

```razor
<Tabs DefaultValue="general" ClassName="gap-4">
    <TabsList ClassName="bg-background border">
        <TabsTrigger Value="general">General</TabsTrigger>
        <TabsTrigger Value="advanced">Advanced</TabsTrigger>
    </TabsList>
    <TabsContent Value="general">
        <p>General settings.</p>
    </TabsContent>
    <TabsContent Value="advanced">
        <p>Advanced settings.</p>
    </TabsContent>
</Tabs>
```

`ClassName` is merged on top of each component's base classes, so overrides such as a different background, spacing, or border work as expected. Each child primitive also accepts its own `ClassName`.
