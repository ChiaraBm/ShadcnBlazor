using LucideBlazor;
using Microsoft.AspNetCore.Components;
using ShadcnBlazor.Buttons;
using ShadcnBlazor.Extras.AlertDialogs;
using ShadcnBlazor.Extras.FileManagers.Abstractions;
using ShadcnBlazor.Extras.FileManagers.Dialogs;

namespace ShadcnBlazor.Extras.FileManagers.Operations;

public class NewFileOperation : FsToolbarOperationBase
{
    public override RenderFragment Content { get; } = builder =>
    {
        builder.OpenComponent<FileIcon>(0);
        builder.CloseComponent();
    };

    public override int Order => 0;

    private readonly AlertDialogService AlertDialogService;

    public NewFileOperation(AlertDialogService alertDialogService)
    {
        AlertDialogService = alertDialogService;
        
        IsPrimary = true;
        ToolbarButtonSize = ButtonSize.Icon;
    }
    
    public override async Task ExecuteAsync(string workingDirectory, IFsAccess fsAccess, IFileManager fileManager)
    {
        await AlertDialogService.LaunchCustomAsync<NewFileDialog>(parameters =>
        {
            parameters[nameof(NewFileDialog.FsAccess)] = fsAccess;
            parameters[nameof(NewFileDialog.FileManager)] = fileManager;
            parameters[nameof(NewFileDialog.WorkingDirectory)] = workingDirectory;
        });
    }

    public override bool CheckCompatability(IFsAccess fsAccess) => true;
}