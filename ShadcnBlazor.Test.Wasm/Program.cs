using System.Globalization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.JSInterop;
using ShadcnBlazor.Test;
using ShadcnBlazor.Test.Helper;
using ShadcnBlazor.Test.Wasm.Helpers;
using ShadcnBlazor.Test.Wasm.UI;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddTest();

builder.Services.AddScoped(_ => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddScoped<ILanguageSelectorService, LanguageSelectorService>();

var app = builder.Build();

var jsService = app.Services.GetRequiredService<IJSRuntime>();
var language = await jsService.InvokeAsync<string?>("languageSelector.get");

if (!string.IsNullOrEmpty(language))
{
    var cultureInfo = new CultureInfo(language);
    
    CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
    CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;
}

await app.RunAsync();