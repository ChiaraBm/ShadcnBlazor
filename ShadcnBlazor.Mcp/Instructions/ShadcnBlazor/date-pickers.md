---
title: Date Picker
description: Popover date picker built on Button and Calendar that lets the user choose a single date with a "Select date" trigger and two-way binding.
tags: [date-picker, calendar, popover, date-only, picker]
---

# Date Picker

A single-date picker that wraps the `Calendar` in a `Popover`. It renders an outline `Button` (with a `ChevronDownIcon`) that opens the calendar; the button label shows "Select date" when nothing is chosen or the short date string of the current `Value`. Selecting a day in the calendar invokes `ValueChanged`, closes the popover automatically, and updates the trigger. Use it when you need a compact, inline-friendly way to pick one `DateOnly` value in a form or table cell, with the same `ViewStart`/`ViewEnd` bounds and `Entries` highlighting the `Calendar` supports.

## Usage

```razor
@using ShadcnBlazor.DatePickers

<DatePickers />
```

Because `Value` is a non-nullable `DateOnly`, set a `DefaultValue` (or bind `Value`) so a real date is displayed. The calendar itself requires `ViewStart`/`ViewEnd`; without them the grid is not bounded.

## Parameters

The component inherits `ShadcnBase`, so the common `ClassName` and `Props` (unmatched attributes) parameters are also available.

| Name | Type | Default | Description |
|---|---|---|---|
| `StartDate` | `DateOnly?` | `null` | Month to show on first load. Passed through to the inner `Calendar`, which jumps to the first day of that month; when unset the calendar shows the current month. |
| `ViewStart` | `DateOnly` | `default(DateOnly)` | Earliest selectable day, forwarded to the `Calendar`. Days before it render disabled. |
| `ViewEnd` | `DateOnly` | `default(DateOnly)` | Latest selectable day, forwarded to the `Calendar`. Days after it render disabled. |
| `Entries` | `CalendarEntry[]` | `null` | Dates/ranges to highlight in the calendar (single dates or start–end ranges). Built from the current `Value`; not settable as a two-way parameter. |
| `Value` | `DateOnly` | `default(DateOnly)` | Currently selected date. Supports two-way binding via `@bind-Value`; the button renders its short date, or "Select date" when default. |
| `ValueChanged` | `EventCallback<DateOnly>` | `EventCallback<DateOnly>` | Fired with the chosen date when a day is clicked. The popover closes after the callback. |
| `DefaultValue` | `DateOnly` | `default(DateOnly)` | Initial value applied to `Value` on init, used when `Value` is not bound. |

## Examples

Bound to a form value with a two-way `@bind-Value` and explicit view bounds:

```razor
@using ShadcnBlazor.DatePickers

<DatePickers @bind-Value="SelectedDate"
             ViewStart="new DateOnly(2025, 1, 1)"
             ViewEnd="new DateOnly(2025, 12, 31)" />

<p>Selected: @SelectedDate.ToString("g")</p>

@code
{
    private DateOnly SelectedDate { get; set; } = new(2025, 6, 15);
}
```

Starting the calendar on a given month via `StartDate`, with a `DefaultValue` seed:

```razor
@using ShadcnBlazor.DatePickers

<DatePickers ViewStart="new DateOnly(2025, 1, 1)"
             ViewEnd="new DateOnly(2025, 12, 31)"
             StartDate="new DateOnly(2025, 6, 1)"
             DefaultValue="new DateOnly(2025, 6, 15)" />
```

Customizing the trigger button with `ClassName` (merged with the component's base classes) while keeping default behavior:

```razor
@using ShadcnBlazor.DatePickers

<DatePickers ViewStart="new DateOnly(2025, 1, 1)"
             ViewEnd="new DateOnly(2025, 12, 31)"
             DefaultValue="new DateOnly(2025, 6, 15)"
             ClassName="w-64" />
```
