using ShadcnBlazor.Extras.Alerts.Designs;

namespace ShadcnBlazor.Extras.Alerts;

public class AlertService
{
    private AlertLauncher Launcher;

    internal void SetLauncher(AlertLauncher launcher) => Launcher = launcher;

    public async Task<AlertModel> InfoAsync(string title, string description, int hideDelay = -1)
    {
        return await Launcher.LaunchAsync<InfoAlert>(attributes =>
        {
            attributes[nameof(InfoAlert.Title)] = title;
            attributes[nameof(InfoAlert.Description)] = description;
        }, hideDelay);
    }
    
    public async Task<AlertModel> SuccessAsync(string title, string description, int hideDelay = -1)
    {
        return await Launcher.LaunchAsync<SuccessAlert>(attributes =>
        {
            attributes[nameof(InfoAlert.Title)] = title;
            attributes[nameof(InfoAlert.Description)] = description;
        }, hideDelay);
    }
    
    public async Task<AlertModel> WarningAsync(string title, string description, int hideDelay = -1)
    {
        return await Launcher.LaunchAsync<WarningAlert>(attributes =>
        {
            attributes[nameof(InfoAlert.Title)] = title;
            attributes[nameof(InfoAlert.Description)] = description;
        }, hideDelay);
    }
    
    public async Task<AlertModel> ErrorAsync(string title, string description, int hideDelay = -1)
    {
        return await Launcher.LaunchAsync<ErrorAlert>(attributes =>
        {
            attributes[nameof(InfoAlert.Title)] = title;
            attributes[nameof(InfoAlert.Description)] = description;
        }, hideDelay);
    }
    
    public Task<AlertModel> LaunchCustomAsync<T>(Action<Dictionary<string, object?>>? callback = null, int hideDelay = -1) where T : AlertBase
        => Launcher.LaunchAsync<T>(callback, hideDelay);
    
    public Task CloseAsync(AlertModel model) => Launcher.CloseAsync(model);
}