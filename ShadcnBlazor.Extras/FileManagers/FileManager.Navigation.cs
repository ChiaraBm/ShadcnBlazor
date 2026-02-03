using Microsoft.AspNetCore.Components;

namespace ShadcnBlazor.Extras.FileManagers;

public partial class FileManager
{
    [Parameter] public string DefaultPath { get; set; }
    
    public string CurrentPath { get; private set; } = "/";
    
    private FsEntry[] CurrentEntries = [];
    private bool HasLoaded = false;

    private async Task LoadAsync(bool silent = false)
    {
        if (!silent)
        {
            HasLoaded = false;
            await InvokeAsync(StateHasChanged);
        }

        var loadedEntries = await FsAccess.ListAsync(CurrentPath);
        
        CurrentEntries = loadedEntries
            .OrderByDescending(x => x.Type)
            .ThenBy(x => x.Name)
            .ToArray();

        HasLoaded = true;
        await InvokeAsync(StateHasChanged);
    }

    private async Task OnFileClick(FsEntry entry)
    {
        if (entry.Type == FsEntryType.File)
        {
            
        }
        else if (entry.Type == FsEntryType.Folder)
        {
            CurrentPath = UnixPath.GetFullPath(UnixPath.Combine(CurrentPath, entry.Name));
            await LoadAsync();
        }
    }

    private async Task NavigateToAsync(string path)
    {
        CurrentPath = path;
        await LoadAsync();
    }

    private async Task GoUpAsync()
    {
        CurrentPath = UnixPath.GetFullPath(UnixPath.Combine(CurrentPath, ".."));
        await LoadAsync();
    }

    public async Task RefreshAsync(bool silent = false) => await LoadAsync(silent);
}