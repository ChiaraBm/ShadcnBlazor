using Microsoft.Extensions.DependencyInjection;
using ShadcnBlazor.Extras.AlertDialogs;
using ShadcnBlazor.Extras.Alerts;
using ShadcnBlazor.Extras.Dialogs;
using ShadcnBlazor.Extras.FileManagers.Operations;
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

    public static void AddFileManagerOperations(this IServiceCollection collection)
    {
        collection.AddScoped<NewDirectoryOperation>();
        collection.AddScoped<NewFileOperation>();
        collection.AddScoped<UploadOperation>();
        collection.AddScoped<RenameOperation>();
        collection.AddScoped<DeleteOperation>();
        collection.AddScoped<MoveOperation>();
        collection.AddScoped<EditorOpenOperation>();
        collection.AddScoped<DownloadOperation>();
    }
}