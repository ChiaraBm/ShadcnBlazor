using System.Globalization;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace ShadcnBlazor.Interop;

public class PositionService
{
    private readonly InteropService InteropService;

    public PositionService(InteropService interopService)
    {
        InteropService = interopService;
    }

    public async Task<Position> PositionAtWithinViewportAsync(
        ElementReference element,
        double cursorX,
        double cursorY
    )
    {
        var rect = await InteropService.GetBoundingBoxAsync(element);

        const double offsetX = 10; // distance from cursor
        const double offsetY = 10;
        const double margin = 8; // min distance from viewport edges
        const double tolerance = 2; // accounts for subpixel differences

        var x = cursorX + offsetX;
        var y = cursorY + offsetY;

        // Viewport size
        var viewportSize = await InteropService.GetViewportSizeAsync();

        // If dropdown overflowed to the right, move left
        if (x + rect.Width + margin > viewportSize.Width - tolerance)
        {
            x = Math.Max(margin, cursorX - rect.Width - offsetX);
        }

        // If dropdown overflowed to the bottom, move up
        if (y + rect.Height + margin > viewportSize.Height - tolerance)
        {
            y = Math.Max(margin, cursorY - rect.Height - offsetY);
        }

        // Prevent going off-screen entirely
        x = Math.Min(Math.Max(x, margin), viewportSize.Width - rect.Width - margin);
        y = Math.Min(Math.Max(y, margin), viewportSize.Height - rect.Height - margin);

        return new Position(x, y);
    }

    public async Task<Position> PositionAroundAsync(
        ElementReference aroundElement,
        ElementReference elementToPosition,
        PositionSide side,
        PositionAlignment alignment = PositionAlignment.Start,
        double offset = 4
    )
    {
        var triggerRect = await InteropService.GetBoundingBoxAsync(aroundElement);
        var submenuRect = await InteropService.GetBoundingBoxAsync(elementToPosition);

        const double margin = 8; // distance from viewport edges
        const double tolerance = 2;

        var viewportSize = await InteropService.GetViewportSizeAsync();

        Position position;

        switch (side)
        {
            case PositionSide.Right:
                position = PositionRight(triggerRect, submenuRect, viewportSize, offset, margin, tolerance, alignment);
                if (position.X + submenuRect.Width + margin > viewportSize.Width - tolerance)
                    position = PositionLeft(triggerRect, submenuRect, viewportSize, offset, margin, tolerance,
                        alignment);
                break;

            case PositionSide.Left:
                position = PositionLeft(triggerRect, submenuRect, viewportSize, offset, margin, tolerance, alignment);
                if (position.X < margin)
                    position = PositionRight(triggerRect, submenuRect, viewportSize, offset, margin, tolerance,
                        alignment);
                break;

            case PositionSide.Bottom:
                position = PositionBottom(triggerRect, submenuRect, viewportSize, offset, margin, tolerance, alignment);
                if (position.Y + submenuRect.Height + margin > viewportSize.Height - tolerance)
                    position = PositionTop(triggerRect, submenuRect, viewportSize, offset, margin, tolerance,
                        alignment);
                break;

            case PositionSide.Top:
                position = PositionTop(triggerRect, submenuRect, viewportSize, offset, margin, tolerance, alignment);
                if (position.Y < margin)
                    position = PositionBottom(triggerRect, submenuRect, viewportSize, offset, margin, tolerance,
                        alignment);
                break;

            default:
                position = PositionRight(triggerRect, submenuRect, viewportSize, offset, margin, tolerance, alignment);
                break;
        }

        // Clamp within viewport
        position.X = Math.Min(Math.Max(position.X, margin), viewportSize.Width - submenuRect.Width - margin);
        position.Y = Math.Min(Math.Max(position.Y, margin), viewportSize.Height - submenuRect.Height - margin);

        return position;
    }

    private static double AlignVertical(DomRect trigger, DomRect submenu, PositionAlignment alignment)
    {
        return alignment switch
        {
            PositionAlignment.Start => trigger.Top,
            PositionAlignment.Center => trigger.Top + (trigger.Height / 2) - (submenu.Height / 2),
            PositionAlignment.End => trigger.Bottom - submenu.Height,
            _ => trigger.Top
        };
    }

    private static double AlignHorizontal(DomRect trigger, DomRect submenu, PositionAlignment alignment)
    {
        return alignment switch
        {
            PositionAlignment.Start => trigger.Left,
            PositionAlignment.Center => trigger.Left + (trigger.Width / 2) - (submenu.Width / 2),
            PositionAlignment.End => trigger.Right - submenu.Width,
            _ => trigger.Left
        };
    }


    private static Position PositionRight(
        DomRect trigger,
        DomRect submenu,
        ViewportSize viewport,
        double offset,
        double margin,
        double tolerance,
        PositionAlignment alignment
    )
    {
        var x = trigger.Right + offset;
        var y = AlignVertical(trigger, submenu, alignment);

        // Clamp vertical overflow
        if (y + submenu.Height + margin > viewport.Height - tolerance)
            y = viewport.Height - submenu.Height - margin;

        return new Position(x, y);
    }


    private static Position PositionLeft(
        DomRect trigger,
        DomRect submenu,
        ViewportSize viewport,
        double offset,
        double margin,
        double tolerance,
        PositionAlignment alignment
    )
    {
        var x = trigger.Left - submenu.Width - offset;
        var y = AlignVertical(trigger, submenu, alignment);

        if (y + submenu.Height + margin > viewport.Height - tolerance)
            y = viewport.Height - submenu.Height - margin;

        return new Position(x, y);
    }

    private static Position PositionBottom(
        DomRect trigger,
        DomRect submenu,
        ViewportSize viewport,
        double offset,
        double margin,
        double tolerance,
        PositionAlignment alignment
    )
    {
        var x = AlignHorizontal(trigger, submenu, alignment);
        var y = trigger.Bottom + offset;

        if (x + submenu.Width + margin > viewport.Width - tolerance)
            x = viewport.Width - submenu.Width - margin;

        return new Position(x, y);
    }


    private static Position PositionTop(
        DomRect trigger,
        DomRect submenu,
        ViewportSize viewport,
        double offset,
        double margin,
        double tolerance,
        PositionAlignment alignment
    )
    {
        var x = AlignHorizontal(trigger, submenu, alignment);
        var y = trigger.Top - submenu.Height - offset;

        if (x + submenu.Width + margin > viewport.Width - tolerance)
            x = viewport.Width - submenu.Width - margin;

        return new Position(x, y);
    }

    public record Position
    {
        public double X { get; set; }
        public double Y { get; set; }

        public Position(double x, double y)
        {
            X = x;
            Y = y;
        }

        public string ToCssStyle()
        {
            return $"left: {X.ToString(CultureInfo.InvariantCulture)}px; top: {Y.ToString(CultureInfo.InvariantCulture)}px";
        }

        public const string OffCanvasCss = "left: -1000px; top: -1000px";
    }
}