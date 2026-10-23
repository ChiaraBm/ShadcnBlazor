using Microsoft.AspNetCore.Components;

namespace ShadcnBlazor.Extras.FileManagers;

public partial class FileManager
{
    private bool ShowOpenScreen;
    private RenderFragment OpenWindow;

    private async Task OpenFileAsync(FsEntry fsEntry)
    {
        var openOperation = OpenOperations.FirstOrDefault(x =>
            x.Filter == null || x.Filter.Invoke(fsEntry)
        );

        if (openOperation == null)
        {
            await ToastService.ErrorAsync(
                Translations.FileManagers_Open_UnableToOpenTitle,
                string.Format(Translations.FileManagers_Open_UnableToOpenDescription, fsEntry.Name)
            );
            return;
        }

        var workingDir = new string(CurrentPath);

        OpenWindow = await openOperation.OpenAsync(
            workingDir,
            fsEntry,
            FsAccess,
            this
        );

        ShowOpenScreen = true;
        await InvokeAsync(StateHasChanged);
    }

    public async Task CloseOpenWindowAsync()
    {
        ShowOpenScreen = false;
        await InvokeAsync(StateHasChanged);
    }
}