using ShadcnBlazor.Extras.Toasts.Designs;

namespace ShadcnBlazor.Extras.Toasts;

public class ToastService
{
    private ToastLauncher Launcher;

    public async Task<ToastModel> InfoAsync(string title, string description, int hideDelay = 5000)
    {
        return await Launcher.LaunchAsync<InfoToast>(attributes =>
        {
            attributes[nameof(InfoToast.Title)] = title;
            attributes[nameof(InfoToast.Description)] = description;
        }, hideDelay);
    }
    
    public async Task<ToastModel> SuccessAsync(string title, string description, int hideDelay = 5000)
    {
        return await Launcher.LaunchAsync<SuccessToast>(attributes =>
        {
            attributes[nameof(SuccessToast.Title)] = title;
            attributes[nameof(SuccessToast.Description)] = description;
        }, hideDelay);
    }
    
    public async Task<ToastModel> WarningAsync(string title, string description, int hideDelay = 5000)
    {
        return await Launcher.LaunchAsync<WarningToast>(attributes =>
        {
            attributes[nameof(WarningToast.Title)] = title;
            attributes[nameof(WarningToast.Description)] = description;
        }, hideDelay);
    }
    
    public async Task<ToastModel> ErrorAsync(string title, string description, int hideDelay = 5000)
    {
        return await Launcher.LaunchAsync<ErrorToast>(attributes =>
        {
            attributes[nameof(ErrorToast.Title)] = title;
            attributes[nameof(ErrorToast.Description)] = description;
        }, hideDelay);
    }

    public async Task<ToastModel> ProgressAsync(string title, string description, Func<ProgressToast, Task> callback)
    {
        return await Launcher.LaunchAsync<ProgressToast>(attributes =>
        {
            attributes[nameof(ProgressToast.Title)] = title;
            attributes[nameof(ProgressToast.Description)] = description;
            attributes[nameof(ProgressToast.Callback)] = callback;
        });
    }

    public Task<ToastModel> LaunchCustomAsync<T>(
        Action<Dictionary<string, object?>>? callback = null,
        int hideDelay = -1
    ) where T : ToastBase
    {
        return Launcher.LaunchAsync<T>(callback, hideDelay);
    }

    public Task CloseAsync(ToastModel model) => Launcher.CloseAsync(model);

    internal void SetLauncher(ToastLauncher launcher) => Launcher = launcher;
}