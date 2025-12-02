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
        return await JsRuntime.InvokeAsync<DomRect>("interop.getBoundingBox", element);
    }

    public async Task<ViewportSize> GetViewportSizeAsync()
    {
        return await JsRuntime.InvokeAsync<ViewportSize>("interop.getViewport");
    }

    public async Task<bool> GetMatchMediaAsync(string query)
    {
        return await JsRuntime.InvokeAsync<bool>("interop.getMatchMedia", query);
    }
}