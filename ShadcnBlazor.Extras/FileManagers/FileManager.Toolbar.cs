using ShadcnBlazor.Extras.FileManagers.Abstractions;

namespace ShadcnBlazor.Extras.FileManagers;

public partial class FileManager
{
    private async Task ExecuteToolbarOperationAsync(FsToolbarOperationBase operation)
    {
        var workingDir = new string(CurrentPath);

        await operation.ExecuteAsync(workingDir, FsAccess, this);
    }
}