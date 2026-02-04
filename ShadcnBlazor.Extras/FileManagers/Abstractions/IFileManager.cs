namespace ShadcnBlazor.Extras.FileManagers.Abstractions;

public interface IFileManager
{
    public Task RefreshAsync(bool silent = false);
    public Task CloseOpenWindowAsync();
}