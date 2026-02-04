using LucideBlazor;
using Microsoft.AspNetCore.Components;
using ShadcnBlazor.Buttons;
using ShadcnBlazor.ContextMenus;
using ShadcnBlazor.Dropdowns;
using ShadcnBlazor.Extras.AlertDialogs;
using ShadcnBlazor.Extras.FileManagers.Abstractions;

namespace ShadcnBlazor.Extras.FileManagers.Operations;

public class DeleteOperation : FsMultiOperationBase
{
    public override RenderFragment Content { get; } = builder =>
    {
        builder.OpenComponent<TrashIcon>(0);
        builder.CloseComponent();

        builder.AddContent(1, "Delete");
    };

    public override int Order => 10;

    private readonly AlertDialogService AlertDialogService;

    public DeleteOperation(AlertDialogService alertDialogService)
    {
        AlertDialogService = alertDialogService;

        DropdownItemVariant = DropdownMenuItemVariant.Destructive;
        ContextMenuItemVariant = ContextMenuItemVariant.Destructive;
        ToolbarButtonVariant = ButtonVariant.Destructive;
    }

    public override async Task ExecuteAsync(
        string workingDirectory,
        FsEntry[] fsEntries,
        IFsAccess fsAccess,
        IFileManager fileManager
    )
    {
        var itemsList = fsEntries.Length > 3
            ? string.Join(", ", fsEntries.Take(3).Select(x => x.Name)) + $" and {fsEntries.Length - 3} more"
            : string.Join(", ", fsEntries.Select(x => x.Name));

        await AlertDialogService.ConfirmDangerAsync(
            $"Deletion of {fsEntries.Length} item(s)",
            $"Do you really want to delete these item(s): {itemsList}",
            async () =>
            {
                foreach (var entry in fsEntries)
                    await fsAccess.DeleteAsync(UnixPath.Combine(workingDirectory, entry.Name));

                await fileManager.RefreshAsync();
            }
        );
    }

    public override bool CheckCompatability(IFsAccess fsAccess) => true;
}