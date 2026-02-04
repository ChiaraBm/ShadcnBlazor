using LucideBlazor;
using Microsoft.AspNetCore.Components;
using ShadcnBlazor.Extras.AlertDialogs;
using ShadcnBlazor.Extras.FileManagers.Abstractions;
using ShadcnBlazor.Extras.FileManagers.Dialogs;

namespace ShadcnBlazor.Extras.FileManagers.Operations;

public sealed class ArchiveOperation : FsMultiOperationBase
{
    public override RenderFragment Content { get; } = builder =>
    {
        builder.OpenComponent<ArchiveIcon>(0);
        builder.CloseComponent();
        builder.AddContent(1, "Archive");
    };

    public override int Order => 0;

    private readonly AlertDialogService AlertDialogService;
    
    public ArchiveOperation(AlertDialogService alertDialogService)
    {
        AlertDialogService = alertDialogService;
    }
    
    public override async Task ExecuteAsync(string workingDirectory, FsEntry[] fsEntries, IFsAccess fsAccess, IFileManager fileManager)
    {
        if(fsAccess is not IArchiveAccess archiveAccess)
            return;
        
        await AlertDialogService.LaunchCustomAsync<ArchiveDialog>(parameters =>
        {
            parameters[nameof(ArchiveDialog.FsAccess)] = archiveAccess;
            parameters[nameof(ArchiveDialog.FileManager)] = fileManager;
            parameters[nameof(ArchiveDialog.WorkingDirectory)] = workingDirectory;
            parameters[nameof(ArchiveDialog.FsEntries)] = fsEntries;
        });
    }

    public override bool CheckCompatability(IFsAccess fsAccess) => fsAccess is IArchiveAccess;
}