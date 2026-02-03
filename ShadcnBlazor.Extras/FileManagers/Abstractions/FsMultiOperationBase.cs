using Microsoft.AspNetCore.Components;
using ShadcnBlazor.Buttons;
using ShadcnBlazor.ContextMenus;
using ShadcnBlazor.Dropdowns;

namespace ShadcnBlazor.Extras.FileManagers.Abstractions;

public abstract class FsMultiOperationBase : IFsOperation
{
    public abstract RenderFragment Content { get; }
    public abstract int Order { get; }
    
    public string? DropdownClassName { get; protected set; }
    public DropdownMenuItemVariant DropdownItemVariant { get; protected set; } = DropdownMenuItemVariant.Default;
    
    public string? ToolbarClassName { get; protected set; }
    public ButtonVariant ToolbarButtonVariant { get; protected set; } = ButtonVariant.Outline;
    public ButtonSize ToolbarButtonSize { get; protected set; } = ButtonSize.Default;
    
    public string? ContextMenuClassName { get; protected set; }
    public ContextMenuItemVariant ContextMenuItemVariant { get; protected set; } = ContextMenuItemVariant.Default;

    public abstract Task ExecuteAsync(
        string workingDirectory,
        FsEntry[] fsEntries,
        IFsAccess fsAccess,
        IFileManager fileManager
    );

    public abstract bool CheckCompatability(IFsAccess fsAccess);
}