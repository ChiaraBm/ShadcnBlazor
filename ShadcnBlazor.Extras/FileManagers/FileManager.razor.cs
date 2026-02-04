using Microsoft.AspNetCore.Components;
using ShadcnBlazor.Extras.FileManagers.Abstractions;
using ShadcnBlazor.Extras.FileManagers.Operations;

namespace ShadcnBlazor.Extras.FileManagers;

public partial class FileManager
{
    [Parameter] public IFsAccess FsAccess { get; set; }
    [Parameter] public Action<FileManagerOptions>? OnConfigure { get; set; }

    private FsOpenOperationBase[] OpenOperations;
    private FsMultiOperationBase[] MultiOperations;
    private FsSingleOperationBase[] SingleOperations;
    private FsToolbarOperationBase[] ToolbarOperations;

    protected override void OnInitialized()
    {
        if (!string.IsNullOrEmpty(DefaultPath))
            CurrentPath = DefaultPath;

        var options = new FileManagerOptions(ServiceProvider);
        OnConfigure?.Invoke(options);

        OpenOperations = options
            .OpenOperations
            .OrderBy(x => x.Order)
            .Where(x => x.CheckCompatability(FsAccess))
            .ToArray();
        
        MultiOperations = options
            .MultiOperations
            .OrderBy(x => x.Order)
            .Where(x => x.CheckCompatability(FsAccess))
            .ToArray();
        
        SingleOperations = options
            .SingleOperations
            .OrderBy(x => x.Order)
            .Where(x => x.CheckCompatability(FsAccess))
            .ToArray();
        
        ToolbarOperations = options
            .ToolbarOperations
            .OrderBy(x => x.Order)
            .Where(x => x.CheckCompatability(FsAccess))
            .ToArray();
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if(!firstRender)
            return;

        await LoadAsync();
    }

    private async Task OnDragEnterAsync()
    {
        var uploadOperation = ToolbarOperations
            .OfType<UploadOperation>()
            .FirstOrDefault();
        
        if(uploadOperation == null)
            return;

        var workingDir = new string(CurrentPath);
        await uploadOperation.ExecuteAsync(workingDir, FsAccess, this);
    }
}