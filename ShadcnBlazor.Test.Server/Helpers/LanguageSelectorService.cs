using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.JSInterop;
using ShadcnBlazor.Test.Helper;

namespace ShadcnBlazor.Test.Server.Helpers;

public class LanguageSelectorService : ILanguageSelectorService
{
    private readonly IJSRuntime _jsRuntime;

    public LanguageSelectorService(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    public async Task SelectAsync(string key)
    {
        var culture = new CultureInfo(key);

        var value = CookieRequestCultureProvider.MakeCookieValue(
            new RequestCulture(culture, culture)
        );
        
        await _jsRuntime.InvokeVoidAsync("languageSelector.set", value);
    }
}