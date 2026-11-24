---
title: Calendar
description: Single-month calendar grid with month/year navigation, date-click events, and single-date and range highlighting.
tags: [calendar, date-picker, date-range, month, picker]
---

# Calendar

A single-month calendar grid for picking a date on the client. It renders a `table[role="grid"]` of days, a header with previous/next-month buttons and month/year dropdown selectors, and highlights the currently displayed month. Use it when you need a visual date picker that fires a callback with the clicked `DateOnly` and can pre-mark individual dates or whole date ranges. Selection is constrained by `ViewStart` and `ViewEnd`: any day outside that range is rendered disabled and the month/year dropdowns only offer values inside it. Highlights are supplied as `CalendarEntry` records (a single date or a start/end range).

## Usage

```razor
@using ShadcnBlazor.Calendars

<Calendar ViewStart="new DateOnly(2025, 1, 1)"
          ViewEnd="new DateOnly(2025, 12, 31)"
          Entries="new List<CalendarEntry>()" />
```

Passing `Entries` (even an empty collection) is required because the component enumerates it while refreshing its grid.

## Parameters

| Name | Type | Default | Description |
|---|---|---|---|
| `StartDate` | `DateOnly?` | `null` | Month to display on first load. The calendar jumps to the first day of that month; when unset it shows the current month. |
| `OnClick` | `EventCallback<DateOnly>` | `EventCallback<DateOnly>` | Called with the clicked day when the user clicks a day button inside the viewable range. |
| `ViewStart` | `DateOnly` | `default(DateOnly)` | Earliest selectable day. Days before it render disabled and the previous-month button is disabled at this boundary. Effectively required. |
| `ViewEnd` | `DateOnly` | `default(DateOnly)` | Latest selectable day. Days after it render disabled and the next-month button is disabled at this boundary. Effectively required. |
| `Entries` | `IEnumerable<CalendarEntry>` | `null` | Dates or ranges to highlight in the grid. Enumerated during refresh, so it must be non-null (an empty list is fine). |

`Entries` is populated with `CalendarEntry` records, defined in the same namespace: `public record CalendarEntry(DateOnly StartDate, DateOnly EndDate, bool IsSingle)`. An entry with `IsSingle = true` highlights the single date at `StartDate`; an entry with `IsSingle = false` highlights every day from `StartDate` through `EndDate` (start, middle and end cells are styled differently).

## Examples

Calendar with a click handler that stores the selected date:

```razor
@using ShadcnBlazor.Calendars

<Calendar StartDate="new DateOnly(2025, 6, 15)"
          ViewStart="new DateOnly(2025, 1, 1)"
          ViewEnd="new DateOnly(2025, 12, 31)"
          Entries="new List<CalendarEntry>()"
          OnClick="date => Selected = date" />

<p>Selected: @(Selected?.ToString("g") ?? "-")</p>

@code
{
    private DateOnly? Selected;
}
```

Highlighting a single date:

```razor
@using ShadcnBlazor.Calendars

<Calendar ViewStart="new DateOnly(2025, 1, 1)"
          ViewEnd="new DateOnly(2025, 12, 31)"
          Entries="new[]
                  {
                      new CalendarEntry(new DateOnly(2025, 6, 15), new DateOnly(2025, 6, 15), true)
                  }" />
```

Highlighting a date range:

```razor
@using ShadcnBlazor.Calendars

<Calendar ViewStart="new DateOnly(2025, 1, 1)"
          ViewEnd="new DateOnly(2025, 12, 31)"
          Entries="new[]
                  {
                      new CalendarEntry(new DateOnly(2025, 6, 10), new DateOnly(2025, 6, 20), false)
                  }" />
```

Combined usage: a start month plus a mix of a range and a single date, and a click handler:

```razor
@using ShadcnBlazor.Calendars

<Calendar StartDate="new DateOnly(2025, 6, 1)"
          ViewStart="new DateOnly(2025, 1, 1)"
          ViewEnd="new DateOnly(2025, 12, 31)"
          Entries="new[]
                  {
                      new CalendarEntry(new DateOnly(2025, 6, 10), new DateOnly(2025, 6, 20), false),
                      new CalendarEntry(new DateOnly(2025, 6, 25), new DateOnly(2025, 6, 25), true)
                  }"
          OnClick="date => SelectedDate = date" />

@code
{
    private DateOnly? SelectedDate;
}
```
