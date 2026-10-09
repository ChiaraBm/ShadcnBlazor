using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace ShadcnBlazor.ScrollArea;

public class ScrollBarInteropService
{
    private readonly IJSRuntime JsRuntime;

    public ScrollBarInteropService(IJSRuntime jsRuntime)
    {
        JsRuntime = jsRuntime;
    }

    internal async Task<bool> IsContentOverflowingAsync(ElementReference area, ScrollBarOrientation orientation)
    {
        return await JsRuntime.InvokeAsync<bool>(
            "shadcnBlazor.scrollBar.isContentOverflowing",
            area, orientation.ToString().ToLower()
        );
    }

    internal async Task<ScrollBarThumbSize> GetScrollBarThumbSizeAsync(ElementReference track)
    {
        return await JsRuntime.InvokeAsync<ScrollBarThumbSize>("shadcnBlazor.scrollBar.getScrollBarThumbSize",
            track);
    }

    internal async Task<ScrollBarScrollLength> GetScrollBarScrollLengthAsync(ElementReference area,
        ScrollBarOrientation orientation)
    {
        return await JsRuntime.InvokeAsync<ScrollBarScrollLength>(
            "shadcnBlazor.scrollBar.getScrollBarScrollLength",
            area, orientation.ToString().ToLower()
        );
    }

    internal async Task AttachAsync(ElementReference area, ElementReference track,
        ElementReference thumb, ScrollBarOrientation orientation)
    {
        await JsRuntime.InvokeVoidAsync("shadcnBlazor.scrollBar.attachScrollBar", area, track, thumb,
            orientation.ToString().ToLowerInvariant());
    }

    /// <summary>
    /// Can be used to manually Detach all Events from the Scrollbar Component.
    /// </summary>
    /// <param name="thumb">The Scrollbar Thumb</param>
    public async Task DetachAsync(ElementReference thumb)
    {
        await JsRuntime.InvokeVoidAsync("shadcnBlazor.scrollBar.detachScrollBar", thumb);
    }
}