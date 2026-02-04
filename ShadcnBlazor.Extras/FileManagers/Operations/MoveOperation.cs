using LucideBlazor;
using Microsoft.AspNetCore.Components;
using ShadcnBlazor.Extras.Dialogs;
using ShadcnBlazor.Extras.FileManagers.Abstractions;
using ShadcnBlazor.Extras.FileManagers.Dialogs;

namespace ShadcnBlazor.Extras.FileManagers.Operations;

public class MoveOperation : FsMultiOperationBase
{
    public override RenderFragment Content { get; } = builder =>
    {
        builder.OpenComponent<HandIcon>(0);
        builder.CloseComponent();
        builder.AddContent(1, "Move");
    };

    public override int Order => 2;

    private readonly DialogService DialogService;
    
    public MoveOperation(DialogService dialogService)
    {
        DialogService = dialogService;
    }
    
    public override async Task ExecuteAsync(string workingDirectory, FsEntry[] fsEntries, IFsAccess fsAccess, IFileManager fileManager)
    {
        await DialogService.LaunchAsync<LocationSelectDialog>(paramaters =>
        {
            paramaters[nameof(LocationSelectDialog.DefaultPath)] = workingDirectory;
            paramaters[nameof(LocationSelectDialog.FsAccess)] = fsAccess;
            paramaters[nameof(LocationSelectDialog.Title)] = $"Move {fsEntries.Length} item(s)";
            paramaters[nameof(LocationSelectDialog.Description)] = "Select a location to move the item(s) to";
            paramaters[nameof(LocationSelectDialog.OnSubmit)] = async (string selectedPath) =>
            {
                foreach (var fsEntry in fsEntries)
                {
                    await fsAccess.MoveAsync(
                        UnixPath.Combine(workingDirectory, fsEntry.Name),
                        UnixPath.Combine(selectedPath, fsEntry.Name)
                    );
                }

                await fileManager.RefreshAsync();
            };
        });
    }

    public override bool CheckCompatability(IFsAccess fsAccess) => true;
}