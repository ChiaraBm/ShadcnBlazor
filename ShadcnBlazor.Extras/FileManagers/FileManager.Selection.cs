namespace ShadcnBlazor.Extras.FileManagers;

public partial class FileManager
{
    private readonly List<FsEntry> SelectedEntries = new();

    private async Task ToggleAllAsync(bool toggle)
    {
        SelectedEntries.Clear();
        
        if(toggle)
            SelectedEntries.AddRange(CurrentEntries);

        await InvokeAsync(StateHasChanged);
    }

    private async Task ToggleAsync(FsEntry fsEntry, bool toggle)
    {
        if (toggle)
            SelectedEntries.Add(fsEntry);
        else
            SelectedEntries.Remove(fsEntry);
        
        await InvokeAsync(StateHasChanged);
    }

    public async Task ClearSelectionAsync()
    {
        SelectedEntries.Clear();
        await InvokeAsync(StateHasChanged);
    }
}