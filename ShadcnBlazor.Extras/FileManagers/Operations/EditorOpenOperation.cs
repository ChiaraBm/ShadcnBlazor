using Microsoft.AspNetCore.Components;
using ShadcnBlazor.Extras.FileManagers.Abstractions;
using ShadcnBlazor.Extras.FileManagers.OpenWindows;

namespace ShadcnBlazor.Extras.FileManagers.Operations;

public sealed class EditorOpenOperation : FsOpenOperationBase
{
    public override Func<FsEntry, bool>? Filter { get; } =
        entry => EditorHelper.CheckExtension(EditorHelper.GetExtension(entry.Name));

    public override int Order => 0;
    public override bool CheckCompatability(IFsAccess fsAccess) => true;

    public override Task<RenderFragment> OpenAsync(
        string workingDirectory,
        FsEntry fsEntry,
        IFsAccess fsAccess,
        IFileManager fileManager
    )
    {
        return Task.FromResult<RenderFragment>(builder =>
        {
            builder.OpenComponent<EditorWindow>(0);
            builder.AddComponentParameter(1, nameof(EditorWindow.FsAccess), fsAccess);
            builder.AddComponentParameter(2, nameof(EditorWindow.FileManager), fileManager);
            builder.AddComponentParameter(3, nameof(EditorWindow.WorkingDirectory), workingDirectory);
            builder.AddComponentParameter(3, nameof(EditorWindow.FsEntry), fsEntry);
            builder.CloseComponent();
        });
    }
}