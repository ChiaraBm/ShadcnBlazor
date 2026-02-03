using Microsoft.AspNetCore.Components.Web;

namespace ShadcnBlazor.Extras.FileManagers;

public partial class FileManager
{
    private FsEntry? ContextMenuFsEntry;
    
    private async Task OpenContextMenuAsync(FsEntry fsEntry, MouseEventArgs args)
    {
        ContextMenuFsEntry = fsEntry;
        await ContextMenu.OpenAsync(args.ClientX, args.ClientY);
    }
}