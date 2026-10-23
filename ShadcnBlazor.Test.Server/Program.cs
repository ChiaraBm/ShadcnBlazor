using Microsoft.AspNetCore.Localization;
using ShadcnBlazor.Test;
using ShadcnBlazor.Test.Helper;
using ShadcnBlazor.Test.Server.Helpers;
using ShadcnBlazor.Test.Server.UI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddTest();

builder.Services.AddScoped(_ => new HttpClient());

builder.Services.AddScoped<ILanguageSelectorService, LanguageSelectorService>();

var app = builder.Build();

app.UseRequestLocalization(new RequestLocalizationOptions()
    .AddSupportedCultures("en-US", "de")
    .AddSupportedUICultures("en-US", "de")
    .AddInitialRequestCultureProvider(new CookieRequestCultureProvider())
);

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
}

app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddAdditionalAssemblies(typeof(TestStartup).Assembly);

app.Run();