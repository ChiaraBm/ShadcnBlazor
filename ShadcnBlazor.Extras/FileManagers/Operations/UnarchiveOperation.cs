using LucideBlazor;
using Microsoft.AspNetCore.Components;
using ShadcnBlazor.Extras.Dialogs;
using ShadcnBlazor.Extras.FileManagers.Abstractions;
using ShadcnBlazor.Extras.FileManagers.Dialogs;
using ShadcnBlazor.Extras.Toasts;

namespace ShadcnBlazor.Extras.FileManagers.Operations;

public sealed class UnarchiveOperation : FsSingleOperationBase
{
    public override Func<FsEntry, bool>? Filter
    {
        get { return entry => ArchiveFormats.Any(x => x.Extensions.Any(extension => entry.Name.EndsWith(extension))); }
    }

    public override RenderFragment Content { get; } = builder =>
    {
        builder.OpenComponent<ArchiveRestoreIcon>(0);
        builder.CloseComponent();
        builder.AddContent(1, "Unarchive");
    };

    public override int Order => 0;

    private ArchiveFormat[] ArchiveFormats = [];

    private readonly DialogService DialogService;
    private readonly ToastService ToastService;

    public UnarchiveOperation(DialogService dialogService, ToastService toastService)
    {
        DialogService = dialogService;
        ToastService = toastService;
    }

    public override async Task ExecuteAsync(
        string workingDirectory,
        FsEntry fsEntry,
        IFsAccess fsAccess,
        IFileManager fileManager
    )
    {
        if (fsAccess is not IArchiveAccess archiveAccess)
            return;

        var format = ArchiveFormats.FirstOrDefault(x => x.Extensions.Any(y => fsEntry.Name.EndsWith(y)));

        if (format == null) // This should never be the case, cause our filter should not even give the option to unarchive when unsupported
        {
            await ToastService.ErrorAsync(
                "Unarchive Error",
                $"Unable to unarchive {fsEntry.Name}: Unsupported format"
            );

            return;
        }

        await DialogService.LaunchAsync<LocationSelectDialog>(parameters =>
        {
            parameters[nameof(LocationSelectDialog.DefaultPath)] = workingDirectory;
            parameters[nameof(LocationSelectDialog.FsAccess)] = fsAccess;
            parameters[nameof(LocationSelectDialog.Title)] = "Select location to unarchive";
            parameters[nameof(LocationSelectDialog.Description)] = "Select location to unarchive the items to";
            parameters[nameof(LocationSelectDialog.OnSubmit)] = async (string selectedPath) =>
            {
                await ToastService.ProgressAsync(
                    "Unarchiving",
                    "Extracting content",
                    async toast =>
                    {
                        await archiveAccess.UnarchiveAsync(
                            UnixPath.Combine(workingDirectory, fsEntry.Name),
                            format,
                            selectedPath,
                            message => toast.UpdateAsync($"Unarchiving item(s) from {fsEntry.Name}", message)
                        );

                        await fileManager.RefreshAsync();
                    }
                );
            };
        });
    }

    public override bool CheckCompatability(IFsAccess fsAccess)
    {
        if (fsAccess is not IArchiveAccess archiveAccess)
            return false;

        ArchiveFormats = archiveAccess.ArchiveFormats;
        return true;
    }
}