using LucideBlazor;
using Microsoft.AspNetCore.Components;
using ShadcnBlazor.Buttons;
using ShadcnBlazor.Extras.AlertDialogs;
using ShadcnBlazor.Extras.FileManagers.Abstractions;
using ShadcnBlazor.Extras.FileManagers.Dialogs;

namespace ShadcnBlazor.Extras.FileManagers.Operations;

public class NewDirectoryOperation : FsToolbarOperationBase
{
    public override RenderFragment Content { get; } = builder =>
    {
        builder.OpenComponent<FolderIcon>(0);
        builder.CloseComponent();
    };

    public override int Order => -101;

    private readonly AlertDialogService AlertDialogService;

    public NewDirectoryOperation(AlertDialogService alertDialogService)
    {
        AlertDialogService = alertDialogService;
        
        ToolbarButtonSize = ButtonSize.Icon;
    }
    
    public override async Task ExecuteAsync(string workingDirectory, IFsAccess fsAccess, IFileManager fileManager)
    {
        await AlertDialogService.LaunchCustomAsync<NewDirectoryDialog>(parameters =>
        {
            parameters[nameof(NewDirectoryDialog.FsAccess)] = fsAccess;
            parameters[nameof(NewDirectoryDialog.FileManager)] = fileManager;
            parameters[nameof(NewDirectoryDialog.WorkingDirectory)] = workingDirectory;
        });
    }

    public override bool CheckCompatability(IFsAccess fsAccess) => true;
}