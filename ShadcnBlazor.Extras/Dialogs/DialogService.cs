namespace ShadcnBlazor.Extras.Dialogs;

public class DialogService
{
    private DialogLauncher Launcher;

    public Task<DialogModel> LaunchAsync<T>(
        Action<Dictionary<string, object?>>? callback = null,
        Action<DialogModel>? onConfigure = null
    ) where T : DialogBase
        => Launcher.LaunchAsync<T>(callback, onConfigure);

    internal void SetLauncher(DialogLauncher launcher) => Launcher = launcher;
}