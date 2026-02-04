namespace ShadcnBlazor.Extras.FileManagers.Abstractions;

public interface IArchiveAccess
{
    public ArchiveFormat[] ArchiveFormats { get; }

    public Task ArchiveAsync(
        string destination,
        string archiveRootPath,
        ArchiveFormat archiveFormat,
        FsEntry[] entries,
        Func<string, Task> updateProgress
    );

    public Task UnarchiveAsync(
        string path,
        ArchiveFormat archiveFormat,
        string archiveRootPath,
        Func<string, Task> updateProgress
    );
}