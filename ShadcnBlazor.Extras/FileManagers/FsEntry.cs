namespace ShadcnBlazor.Extras.FileManagers;

public record FsEntry(string Name, FsEntryType Type, long Size, FsEntryPermissions Permissions, DateTimeOffset CreatedAt, DateTimeOffset UpdatedAt);

public enum FsEntryType
{
    File = 0,
    Folder = 1
}

public enum FsEntryPermissions
{
    None = 0,
    Read = 1,
    ReadWrite = 2
}