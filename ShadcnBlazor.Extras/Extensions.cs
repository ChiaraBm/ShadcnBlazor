using Microsoft.Extensions.DependencyInjection;
using ShadcnBlazor.Extras.AlertDialogs;
using ShadcnBlazor.Extras.Alerts;

namespace ShadcnBlazor.Extras;

public static class Extensions
{
    public static void AddShadcnBlazorExtras(this IServiceCollection collection)
    {
        collection.AddScoped<AlertDialogService>();
        collection.AddScoped<AlertService>();
    }
}