namespace ShadcnBlazor.Extras.FileManagers.Abstractions;

public interface IFileManager
{
    public FileManagerOptions Options { get; }
    public Task RefreshAsync(bool silent = false);
    public Task CloseOpenWindowAsync();
}