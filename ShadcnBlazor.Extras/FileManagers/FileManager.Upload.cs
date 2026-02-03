using ShadcnBlazor.Extras.FileManagers.Dialogs;

namespace ShadcnBlazor.Extras.FileManagers;

public partial class FileManager
{
    public async Task LaunchUpdateAsync()
    {
        await DialogService.LaunchAsync<FileUploadDialog>(onConfigure: model =>
        {
            model.ClassName = "sm:max-w-2xl!";
        });
    }
}