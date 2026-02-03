using ShadcnBlazor.Extras.FileManagers.Abstractions;

namespace ShadcnBlazor.Extras.FileManagers;

public partial class FileManager
{
    private async Task ExecuteSingleFsOperationAsync(FsSingleOperationBase operation, FsEntry fsEntry)
    {
        var workingDir = new string(CurrentPath);
        await operation.ExecuteAsync(workingDir, fsEntry, FsAccess, this);
    }

    private async Task ExecuteMultiFsOperationAsync(FsMultiOperationBase operation, FsEntry[] entries)
    {
        if(entries.Length == 0)
            return;
        
        var workingDir = new string(CurrentPath);
        await operation.ExecuteAsync(workingDir, entries, FsAccess, this);
    }
}