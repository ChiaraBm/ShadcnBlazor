using LucideBlazor;
using Microsoft.AspNetCore.Components;
using ShadcnBlazor.Extras.FileManagers.Abstractions;
using ShadcnBlazor.Extras.Toasts;

namespace ShadcnBlazor.Extras.FileManagers.Operations;

public class DownloadOperation : FsMultiOperationBase
{
    private readonly NavigationManager NavigationManager;
    private readonly ToastService ToastService;
    
    public override RenderFragment Content { get; } = builder =>
    {
        builder.OpenComponent<HardDriveDownloadIcon>(0);
        builder.CloseComponent();
        builder.AddContent(1, "Download");
    };

    public override int Order => 0;

    public DownloadOperation(NavigationManager navigationManager, ToastService toastService)
    {
        NavigationManager = navigationManager;
        ToastService = toastService;
    }
    
    public override async Task ExecuteAsync(string workingDirectory, FsEntry[] fsEntries, IFsAccess fsAccess, IFileManager fileManager)
    {
        foreach (var fsEntry in fsEntries)
            await HandleDownloadAsync(workingDirectory, fsEntry, fsAccess);
    }

    private async Task HandleDownloadAsync(string workingDirectory, FsEntry fsEntry, IFsAccess fsAccess)
    {
        if (fsEntry.Type == FsEntryType.File && fsAccess is IDownloadFileAccess downloadFileAccess)
        {
            var path = UnixPath.Combine(workingDirectory, fsEntry.Name);
            var url = await downloadFileAccess.GetFileDownloadUrlAsync(path);
            
            NavigationManager.NavigateTo(url, true);
            return;
        }

        if (fsEntry.Type == FsEntryType.Folder && fsAccess is IDownloadFolderAccess downloadFolderAccess)
        {
            var path = UnixPath.Combine(workingDirectory, fsEntry.Name);
            var url = await downloadFolderAccess.GetFolderDownloadUrlAsync(path);
            
            NavigationManager.NavigateTo(url, true);
            return;
        }

        await ToastService.ErrorAsync(
            "Download Error",
            $"Unable to download {fsEntry.Name}: Not supported"
        );
    }

    public override bool CheckCompatability(IFsAccess fsAccess) =>
        fsAccess is IDownloadFileAccess or IDownloadFolderAccess;
}