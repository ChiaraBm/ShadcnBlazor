using LucideBlazor;
using Microsoft.AspNetCore.Components;
using ShadcnBlazor.Buttons;
using ShadcnBlazor.Extras.Dialogs;
using ShadcnBlazor.Extras.FileManagers.Abstractions;
using ShadcnBlazor.Extras.FileManagers.Dialogs;

namespace ShadcnBlazor.Extras.FileManagers.Operations;

public sealed class UploadOperation : FsToolbarOperationBase
{
    public override RenderFragment Content { get; } = builder =>
    {
        builder.OpenComponent<UploadIcon>(0);
        builder.CloseComponent();
        builder.AddContent(1, "Upload");
    };

    public override int Order => 1;

    private readonly DialogService DialogService;

    public UploadOperation(DialogService dialogService)
    {
        DialogService = dialogService;

        IsPrimary = true;
        ToolbarButtonVariant = ButtonVariant.Default;
    }

    public override async Task ExecuteAsync(string workingDirectory, IFsAccess fsAccess, IFileManager fileManager)
    {
        await DialogService.LaunchAsync<UploadDialog>(
            parameters =>
            {
                parameters[nameof(UploadDialog.FsAccess)] = fsAccess;
                parameters[nameof(UploadDialog.FileManager)] = fileManager;
                parameters[nameof(UploadDialog.WorkingDirectory)] = workingDirectory;
            },
            onConfigure: model => { model.ClassName = "sm:max-w-2xl!"; }
        );
    }

    public override bool CheckCompatability(IFsAccess fsAccess) => true;
}