using ShadcnBlazor.Extras.AlertDialogs.AlertDesigns;

namespace ShadcnBlazor.Extras.AlertDialogs;

public class AlertDialogService
{
    private AlertDialogLauncher Launcher;
    
    internal void SetLauncher(AlertDialogLauncher launcher) => Launcher = launcher;

    public async Task<AlertDialogModel> ConfirmAsync(string title, string description, Func<Task> onConfirm)
    {
        return await Launcher.LaunchAsync<ConfirmAlertDialog>(attributes =>
        {
            attributes[nameof(ConfirmAlertDialog.Title)] = title;
            attributes[nameof(ConfirmAlertDialog.Description)] = description;
            attributes[nameof(ConfirmAlertDialog.OnConfirm)] = onConfirm;
        });
    }

    public async Task<AlertDialogModel> InfoAsync(string title, string description)
    {
        return await Launcher.LaunchAsync<InfoAlertDialog>(attributes =>
        {
            attributes[nameof(InfoAlertDialog.Title)] = title;
            attributes[nameof(InfoAlertDialog.Description)] = description;
        });
    }
    
    public async Task<AlertDialogModel> SuccessAsync(string title, string description)
    {
        return await Launcher.LaunchAsync<SuccessAlertDialog>(attributes =>
        {
            attributes[nameof(InfoAlertDialog.Title)] = title;
            attributes[nameof(InfoAlertDialog.Description)] = description;
        });
    }
    
    public async Task<AlertDialogModel> WarningAsync(string title, string description)
    {
        return await Launcher.LaunchAsync<WarningAlertDialog>(attributes =>
        {
            attributes[nameof(InfoAlertDialog.Title)] = title;
            attributes[nameof(InfoAlertDialog.Description)] = description;
        });
    }
    
    public async Task<AlertDialogModel> ErrorAsync(string title, string description)
    {
        return await Launcher.LaunchAsync<ErrorAlertDialog>(attributes =>
        {
            attributes[nameof(InfoAlertDialog.Title)] = title;
            attributes[nameof(InfoAlertDialog.Description)] = description;
        });
    }

    public Task CloseAsync(AlertDialogModel dialogModel) => Launcher.CloseAsync(dialogModel);

    public Task<AlertDialogModel> LaunchCustomAsync<T>(Action<Dictionary<string, object?>>? callback = null)
        where T : AlertDialogBase
        => Launcher.LaunchAsync<T>(callback);
}