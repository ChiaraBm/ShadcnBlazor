using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace ShadcnBlazor.Interop;

public class InteropService
{
    private readonly IJSRuntime JsRuntime;

    public InteropService(IJSRuntime jsRuntime)
    {
        JsRuntime = jsRuntime;
    }
    
    public async Task<DomRect> GetBoundingBoxAsync(ElementReference element)
    {
        return await JsRuntime.InvokeAsync<DomRect>("shadcnBlazor.getBoundingBox", element);
    }

    public async Task<ViewportSize> GetViewportSizeAsync()
    {
        return await JsRuntime.InvokeAsync<ViewportSize>("shadcnBlazor.getViewport");
    }

    public async Task<bool> GetMatchMediaAsync(string query)
    {
        return await JsRuntime.InvokeAsync<bool>("shadcnBlazor.getMatchMedia", query);
    }
}