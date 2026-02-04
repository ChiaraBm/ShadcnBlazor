namespace ShadcnBlazor.Extras.FileManagers.Abstractions;

public interface IDownloadFileAccess
{
    public Task<string> GetFileDownloadUrlAsync(string path);
}