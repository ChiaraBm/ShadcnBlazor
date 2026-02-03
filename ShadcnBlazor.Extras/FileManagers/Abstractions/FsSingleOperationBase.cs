using Microsoft.AspNetCore.Components;
using ShadcnBlazor.ContextMenus;
using ShadcnBlazor.Dropdowns;

namespace ShadcnBlazor.Extras.FileManagers.Abstractions;

public abstract class FsSingleOperationBase : IFsOperation
{
    public abstract Func<FsEntry, bool>? Filter { get; }

    public abstract RenderFragment Content { get; }
    public abstract int Order { get; }
    
    public string? DropdownClassName { get; protected set; }
    public DropdownMenuItemVariant DropdownItemVariant { get; protected set; } = DropdownMenuItemVariant.Default;
    public string? ContextMenuClassName { get; protected set; }
    public ContextMenuItemVariant ContextMenuItemVariant { get; protected set; } = ContextMenuItemVariant.Default;
    
    public abstract Task ExecuteAsync(string workingDirectory, FsEntry fsEntry, IFsAccess fsAccess, IFileManager fileManager);
    public abstract bool CheckCompatability(IFsAccess fsAccess);
}