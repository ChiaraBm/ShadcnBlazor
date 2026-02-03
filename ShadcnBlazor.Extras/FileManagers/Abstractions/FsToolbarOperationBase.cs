using Microsoft.AspNetCore.Components;
using ShadcnBlazor.Buttons;
using ShadcnBlazor.Dropdowns;

namespace ShadcnBlazor.Extras.FileManagers.Abstractions;

public abstract class FsToolbarOperationBase : IFsOperation
{
    public abstract RenderFragment Content { get; }
    public abstract int Order { get; }
    
    public string? ToolbarClassName { get; protected set; }
    public string? DropdownClassName { get; protected set; }
    public ButtonVariant ToolbarButtonVariant { get; protected set; } = ButtonVariant.Outline;
    public ButtonSize ToolbarButtonSize { get; protected set; } = ButtonSize.Default;
    public DropdownMenuItemVariant DropdownItemVariant { get; protected set; } = DropdownMenuItemVariant.Default;
    
    public bool IsPrimary { get; protected set; }

    public abstract Task ExecuteAsync(string workingDirectory, IFsAccess fsAccess, IFileManager fileManager);

    public abstract bool CheckCompatability(IFsAccess fsAccess);
}