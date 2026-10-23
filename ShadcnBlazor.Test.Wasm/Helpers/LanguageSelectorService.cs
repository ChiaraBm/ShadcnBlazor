using Microsoft.JSInterop;
using ShadcnBlazor.Test.Helper;

namespace ShadcnBlazor.Test.Wasm.Helpers;

public class LanguageSelectorService : ILanguageSelectorService
{
    private readonly IJSRuntime _jsRuntime;

    public LanguageSelectorService(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    public async Task SelectAsync(string key)
        => await _jsRuntime.InvokeVoidAsync("languageSelector.set", key);
}