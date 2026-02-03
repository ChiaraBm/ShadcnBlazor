using Microsoft.AspNetCore.Components;

namespace ShadcnBlazor.Extras.FileManagers.Abstractions;

public abstract class FsOpenOperationBase
{
    public abstract Func<FsEntry, bool>? Filter { get; }
    public abstract int Order { get; }
    
    public abstract bool CheckCompatability(IFsAccess fsAccess);

    public abstract Task<RenderFragment> OpenAsync(
        string workingDirectory,
        FsEntry fsEntry,
        IFsAccess fsAccess,
        IFileManager fileManager
    );
}