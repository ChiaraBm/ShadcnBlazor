using LucideBlazor;
using Microsoft.AspNetCore.Components;
using ShadcnBlazor.Extras.AlertDialogs;
using ShadcnBlazor.Extras.FileManagers.Abstractions;
using ShadcnBlazor.Extras.FileManagers.Dialogs;

namespace ShadcnBlazor.Extras.FileManagers.Operations;

public sealed class RenameOperation : FsSingleOperationBase
{
    public override Func<FsEntry, bool>? Filter { get; } = _ => true;

    public override RenderFragment Content { get; } = builder =>
    {
        builder.OpenComponent<PencilIcon>(0);
        builder.CloseComponent();
        
        builder.AddContent(1, "Rename");
    };

    public override int Order => 0;

    private readonly AlertDialogService AlertDialogService;
    
    public RenameOperation(AlertDialogService alertDialogService)
    {
        AlertDialogService = alertDialogService;
    }
    
    public override async Task ExecuteAsync(string workingDirectory, FsEntry fsEntry, IFsAccess fsAccess, IFileManager fileManager)
    {
        await AlertDialogService.LaunchCustomAsync<RenameDialog>(parameters =>
        {
            parameters[nameof(RenameDialog.FsAccess)] = fsAccess;
            parameters[nameof(RenameDialog.FsEntry)] = fsEntry;
            parameters[nameof(RenameDialog.FileManager)] = fileManager;
            parameters[nameof(RenameDialog.WorkingDirectory)] = workingDirectory;
        });
    }

    public override bool CheckCompatability(IFsAccess fsAccess) => true;
}