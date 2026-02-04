using System.Text;
using ShadcnBlazor.Extras.FileManagers;
using ShadcnBlazor.Extras.FileManagers.Abstractions;

namespace ShadcnBlazor.Test.ExtraShowcases.Services;

public partial class InMemoryFsAccess : IArchiveAccess
{
    public ArchiveFormat[] ArchiveFormats { get; } =
    [
        new("zip", "ZIP Archive", [".zip"]),
        new ArchiveFormat("tar", "TAR Archive", [".tar"])
    ];

    // IArchiveAccess implementation
    public async Task ArchiveAsync(
        string destination,
        string archiveRootPath,
        ArchiveFormat archiveFormat,
        FsEntry[] entries,
        Func<string, Task> updateProgress)
    {
        var normalizedDestination = NormalizePath(destination);
        var normalizedRootPath = NormalizePath(archiveRootPath);

        await updateProgress("Preparing archive...");

        // Collect all files to archive
        var filesToArchive = new List<(string path, byte[] data)>();

        foreach (var entry in entries)
        {
            var entryPath = NormalizePath($"{archiveRootPath}/{entry.Name}");

            if (entry.Type == FsEntryType.File)
            {
                if (_nodes.TryGetValue(entryPath, out var node) && node.Type == FsEntryType.File)
                {
                    filesToArchive.Add((entry.Name, node.Data ?? Array.Empty<byte>()));
                }
            }
            else if (entry.Type == FsEntryType.Folder)
            {
                // Recursively collect all files in the folder
                var prefix = entryPath + "/";
                var folderFiles = _nodes
                    .Where(kvp => kvp.Key.StartsWith(prefix) && kvp.Value.Type == FsEntryType.File)
                    .ToList();

                foreach (var file in folderFiles)
                {
                    var relativePath = entry.Name + "/" + file.Key.Substring(prefix.Length);
                    filesToArchive.Add((relativePath, file.Value.Data ?? Array.Empty<byte>()));
                }
            }
        }

        await updateProgress($"Archiving {filesToArchive.Count} file(s)...");

        byte[] archiveData;

        if (archiveFormat.Identifier == "zip")
        {
            archiveData = await CreateZipArchive(filesToArchive, updateProgress);
        }
        else if (archiveFormat.Identifier == "tar")
        {
            archiveData = await CreateTarArchive(filesToArchive, updateProgress);
        }
        else
        {
            throw new NotSupportedException($"Archive format '{archiveFormat.Identifier}' is not supported");
        }

        // Write the archive to destination
        using var stream = new MemoryStream(archiveData);
        await WriteAsync(normalizedDestination, stream);

        await updateProgress("Archive created successfully!");
    }

    public async Task UnarchiveAsync(
        string path,
        ArchiveFormat archiveFormat,
        string archiveRootPath,
        Func<string, Task> updateProgress)
    {
        var normalizedPath = NormalizePath(path);
        var normalizedRootPath = NormalizePath(archiveRootPath);

        await updateProgress("Reading archive...");

        if (!_nodes.TryGetValue(normalizedPath, out var node) || node.Type != FsEntryType.File)
        {
            throw new FileNotFoundException($"Archive file not found: {path}");
        }

        var archiveData = node.Data ?? Array.Empty<byte>();

        List<(string path, byte[] data)> extractedFiles;

        if (archiveFormat.Identifier == "zip")
        {
            extractedFiles = await ExtractZipArchive(archiveData, updateProgress);
        }
        else if (archiveFormat.Identifier == "tar")
        {
            extractedFiles = await ExtractTarArchive(archiveData, updateProgress);
        }
        else
        {
            throw new NotSupportedException($"Archive format '{archiveFormat.Identifier}' is not supported");
        }

        await updateProgress($"Extracting {extractedFiles.Count} file(s)...");

        // Create all necessary directories first
        var directories = new HashSet<string>();
        foreach (var file in extractedFiles)
        {
            var filePath = NormalizePath($"{normalizedRootPath}/{file.path}");
            var parentPath = GetParentPath(filePath);

            var current = parentPath;
            while (current != "/" && !directories.Contains(current))
            {
                directories.Add(current);
                current = GetParentPath(current);
            }
        }

        foreach (var dir in directories.OrderBy(d => d.Length))
        {
            if (!_nodes.ContainsKey(dir))
            {
                var dirName = GetFileName(dir);
                _nodes[dir] = new FsNode
                {
                    Name = dirName,
                    Type = FsEntryType.Folder,
                    CreatedAt = DateTimeOffset.UtcNow,
                    UpdatedAt = DateTimeOffset.UtcNow,
                    Permissions = FsEntryPermissions.ReadWrite
                };
            }
        }

        // Write all files
        foreach (var file in extractedFiles)
        {
            var filePath = NormalizePath($"{normalizedRootPath}/{file.path}");
            var fileName = GetFileName(filePath);

            _nodes[filePath] = new FsNode
            {
                Name = fileName,
                Type = FsEntryType.File,
                Data = file.data,
                CreatedAt = DateTimeOffset.UtcNow,
                UpdatedAt = DateTimeOffset.UtcNow,
                Permissions = FsEntryPermissions.ReadWrite
            };
        }

        await updateProgress("Extraction complete!");
    }

    private async Task<byte[]> CreateZipArchive(List<(string path, byte[] data)> files,
        Func<string, Task> updateProgress)
    {
        using var zipStream = new MemoryStream();
        using (var archive =
               new System.IO.Compression.ZipArchive(zipStream, System.IO.Compression.ZipArchiveMode.Create, true))
        {
            for (int i = 0; i < files.Count; i++)
            {
                var file = files[i];
                await updateProgress($"Adding {file.path} ({i + 1}/{files.Count})...");

                var entry = archive.CreateEntry(file.path, System.IO.Compression.CompressionLevel.Optimal);
                await using var entryStream = entry.Open();
                await entryStream.WriteAsync(file.data, 0, file.data.Length);
            }
        }

        return zipStream.ToArray();
    }

    private async Task<List<(string path, byte[] data)>> ExtractZipArchive(byte[] archiveData,
        Func<string, Task> updateProgress)
    {
        var extractedFiles = new List<(string path, byte[] data)>();

        using var zipStream = new MemoryStream(archiveData);
        using var archive = new System.IO.Compression.ZipArchive(zipStream, System.IO.Compression.ZipArchiveMode.Read);

        for (int i = 0; i < archive.Entries.Count; i++)
        {
            var entry = archive.Entries[i];

            // Skip directories
            if (entry.FullName.EndsWith("/"))
                continue;

            await updateProgress($"Extracting {entry.FullName} ({i + 1}/{archive.Entries.Count})...");

            using var entryStream = entry.Open();
            using var ms = new MemoryStream();
            await entryStream.CopyToAsync(ms);

            extractedFiles.Add((entry.FullName, ms.ToArray()));
        }

        return extractedFiles;
    }

    private async Task<byte[]> CreateTarArchive(List<(string path, byte[] data)> files,
        Func<string, Task> updateProgress)
    {
        using var tarStream = new MemoryStream();

        for (int i = 0; i < files.Count; i++)
        {
            var file = files[i];
            await updateProgress($"Adding {file.path} ({i + 1}/{files.Count})...");

            // Create TAR header (512 bytes)
            var header = new byte[512];

            // File name (100 bytes)
            var nameBytes = Encoding.ASCII.GetBytes(file.path);
            Array.Copy(nameBytes, 0, header, 0, Math.Min(nameBytes.Length, 100));

            // File mode (8 bytes) - 0644
            var modeBytes = Encoding.ASCII.GetBytes("0000644 ");
            Array.Copy(modeBytes, 0, header, 100, 8);

            // Owner user ID (8 bytes)
            var uidBytes = Encoding.ASCII.GetBytes("0000000 ");
            Array.Copy(uidBytes, 0, header, 108, 8);

            // Owner group ID (8 bytes)
            var gidBytes = Encoding.ASCII.GetBytes("0000000 ");
            Array.Copy(gidBytes, 0, header, 116, 8);

            // File size (12 bytes) - octal
            var sizeOctal = Convert.ToString(file.data.Length, 8).PadLeft(11, '0') + " ";
            var sizeBytes = Encoding.ASCII.GetBytes(sizeOctal);
            Array.Copy(sizeBytes, 0, header, 124, 12);

            // Modification time (12 bytes) - octal timestamp
            var timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            var timeOctal = Convert.ToString(timestamp, 8).PadLeft(11, '0') + " ";
            var timeBytes = Encoding.ASCII.GetBytes(timeOctal);
            Array.Copy(timeBytes, 0, header, 136, 12);

            // Checksum (8 bytes) - initially filled with spaces
            Array.Fill<byte>(header, (byte)' ', 148, 8);

            // Type flag (1 byte) - '0' for regular file
            header[156] = (byte)'0';

            // Calculate checksum
            int checksum = 0;
            for (int j = 0; j < 512; j++)
            {
                checksum += header[j];
            }

            var checksumOctal = Convert.ToString(checksum, 8).PadLeft(6, '0') + "\0 ";
            var checksumBytes = Encoding.ASCII.GetBytes(checksumOctal);
            Array.Copy(checksumBytes, 0, header, 148, 8);

            // Write header
            await tarStream.WriteAsync(header, 0, 512);

            // Write file data
            await tarStream.WriteAsync(file.data, 0, file.data.Length);

            // Pad to 512-byte boundary
            var padding = (512 - (file.data.Length % 512)) % 512;
            if (padding > 0)
            {
                var paddingBytes = new byte[padding];
                await tarStream.WriteAsync(paddingBytes, 0, padding);
            }
        }

        // Write two 512-byte blocks of zeros to mark end of archive
        var endMarker = new byte[1024];
        await tarStream.WriteAsync(endMarker, 0, 1024);

        return tarStream.ToArray();
    }

    private async Task<List<(string path, byte[] data)>> ExtractTarArchive(byte[] archiveData,
        Func<string, Task> updateProgress)
    {
        var extractedFiles = new List<(string path, byte[] data)>();

        using var tarStream = new MemoryStream(archiveData);
        var fileCount = 0;

        while (tarStream.Position < tarStream.Length)
        {
            // Read header (512 bytes)
            var header = new byte[512];
            var bytesRead = await tarStream.ReadAsync(header, 0, 512);

            if (bytesRead < 512)
                break;

            // Check if this is the end marker (all zeros)
            if (header.All(b => b == 0))
                break;

            // Extract file name
            var nameBytes = new byte[100];
            Array.Copy(header, 0, nameBytes, 0, 100);
            var fileName = Encoding.ASCII.GetString(nameBytes).TrimEnd('\0', ' ');

            if (string.IsNullOrWhiteSpace(fileName))
                break;

            // Extract file size (octal string)
            var sizeBytes = new byte[12];
            Array.Copy(header, 124, sizeBytes, 0, 12);
            var sizeOctal = Encoding.ASCII.GetString(sizeBytes).Trim('\0', ' ');
            var fileSize = Convert.ToInt32(sizeOctal, 8);

            // Extract type flag
            var typeFlag = (char)header[156];

            // Skip directories
            if (typeFlag == '5' || fileName.EndsWith("/"))
            {
                continue;
            }

            fileCount++;
            await updateProgress($"Extracting {fileName} ({fileCount})...");

            // Read file data
            var fileData = new byte[fileSize];
            await tarStream.ReadAsync(fileData, 0, fileSize);

            extractedFiles.Add((fileName, fileData));

            // Skip padding to 512-byte boundary
            var padding = (512 - (fileSize % 512)) % 512;
            if (padding > 0)
            {
                tarStream.Position += padding;
            }
        }

        return extractedFiles;
    }
}