namespace ShadcnBlazor.Extras.FileManagers.Abstractions;

public interface IFsAccess
{
    public Task<IEnumerable<FsEntry>> ListAsync(string path);

    public Task CreateFileAsync(string path);
    public Task CreateDirectoryAsync(string path);

    public Task<Stream> ReadAsync(string path);
    public Task WriteAsync(string path, Stream dataStream);

    public Task MovAsync(string oldPath, string newPath);
    public Task DeleteAsync(string path);
}