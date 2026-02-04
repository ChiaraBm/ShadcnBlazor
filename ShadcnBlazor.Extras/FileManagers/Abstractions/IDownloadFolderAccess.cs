namespace ShadcnBlazor.Extras.FileManagers.Abstractions;

public interface IDownloadFolderAccess
{
    public Task<string> GetFolderDownloadUrlAsync(string path);
}