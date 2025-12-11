using Microsoft.Extensions.DependencyInjection;
using ShadcnBlazor.Extras.AlertDialogs;
using ShadcnBlazor.Extras.Alerts;
using ShadcnBlazor.Extras.Dialogs;
using ShadcnBlazor.Extras.Toasts;

namespace ShadcnBlazor.Extras;

public static class Extensions
{
    public static void AddShadcnBlazorExtras(this IServiceCollection collection)
    {
        collection.AddScoped<AlertDialogService>();
        collection.AddScoped<AlertService>();
        collection.AddScoped<ToastService>();
        collection.AddScoped<DialogService>();
    }
}