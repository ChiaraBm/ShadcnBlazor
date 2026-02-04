using ShadcnBlazor.Extras.FileManagers;
using ShadcnBlazor.Extras.FileManagers.Abstractions;

namespace ShadcnBlazor.Test.ExtraShowcases.Services;

public partial class InMemoryFsAccess : IFsAccess, IDownloadFileAccess, IDownloadFolderAccess, ICombineAccess
{
    private readonly Dictionary<string, FsNode> _nodes = new();
    private const char PathSeparator = '/';

    public InMemoryFsAccess()
    {
        var now = DateTimeOffset.UtcNow;
        
        // Initialize with the root directory
        _nodes["/"] = new FsNode
        {
            Name = "/",
            Type = FsEntryType.Folder,
            CreatedAt = now,
            UpdatedAt = now,
            Permissions = FsEntryPermissions.ReadWrite
        };

        // Create default directory structure
        CreateDefaultDirectory("/documents", now);
        CreateDefaultDirectory("/images", now);
        CreateDefaultDirectory("/downloads", now);
        CreateDefaultDirectory("/projects", now);
        CreateDefaultDirectory("/projects/blazor-app", now);

        // Create default files
        CreateDefaultFile("/documents/readme.txt", 
            "Welcome to the In-Memory File System!\n\nThis is a sample text file.\nYou can create, read, update, and delete files.", 
            now);
        
        CreateDefaultFile("/documents/notes.txt", 
            "Meeting Notes - 2026-02-02\n\n- Implement file manager interface\n- Add in-memory storage\n- Test all operations", 
            now);
        
        CreateDefaultFile("/projects/blazor-app/Program.cs", 
            "using Microsoft.AspNetCore.Components.Web;\nusing Microsoft.AspNetCore.Components.WebAssembly.Hosting;\n\nvar builder = WebAssemblyHostBuilder.CreateDefault(args);\nbuilder.RootComponents.Add<App>(\"#app\");\n\nawait builder.Build().RunAsync();", 
            now);
        
        CreateDefaultFile("/projects/blazor-app/README.md", 
            "# Blazor Application\n\nA sample Blazor WebAssembly project.\n\n## Features\n- Component-based architecture\n- Client-side rendering\n- Modern UI framework", 
            now);
        
        CreateDefaultFile("/downloads/sample.json", 
            "{\n  \"name\": \"Sample Data\",\n  \"version\": \"1.0\",\n  \"items\": [\n    { \"id\": 1, \"value\": \"First\" },\n    { \"id\": 2, \"value\": \"Second\" }\n  ]\n}", 
            now);
    }

    private void CreateDefaultDirectory(string path, DateTimeOffset timestamp)
    {
        var dirName = GetFileName(path);
        _nodes[path] = new FsNode
        {
            Name = dirName,
            Type = FsEntryType.Folder,
            CreatedAt = timestamp,
            UpdatedAt = timestamp,
            Permissions = FsEntryPermissions.ReadWrite
        };
    }

    private void CreateDefaultFile(string path, string content, DateTimeOffset timestamp)
    {
        var fileName = GetFileName(path);
        _nodes[path] = new FsNode
        {
            Name = fileName,
            Type = FsEntryType.File,
            Data = System.Text.Encoding.UTF8.GetBytes(content),
            CreatedAt = timestamp,
            UpdatedAt = timestamp,
            Permissions = FsEntryPermissions.ReadWrite
        };
    }

    public Task<IEnumerable<FsEntry>> ListAsync(string path)
    {
        var normalizedPath = NormalizePath(path);
        
        if (!_nodes.TryGetValue(normalizedPath, out var node))
        {
            throw new DirectoryNotFoundException($"Directory not found: {path}");
        }

        if (node.Type != FsEntryType.Folder)
        {
            throw new InvalidOperationException($"Path is not a directory: {path}");
        }

        var prefix = normalizedPath == "/" ? "/" : normalizedPath + "/";
        var entries = _nodes
            .Where(kvp => kvp.Key.StartsWith(prefix) && kvp.Key != normalizedPath)
            .Where(kvp => 
            {
                var relativePath = kvp.Key.Substring(prefix.Length);
                return !relativePath.Contains('/'); // Only direct children
            })
            .Select(kvp => new FsEntry(
                kvp.Value.Name,
                kvp.Value.Type,
                kvp.Value.Data?.Length ?? 0,
                kvp.Value.Permissions,
                kvp.Value.CreatedAt,
                kvp.Value.UpdatedAt
            ));

        return Task.FromResult(entries);
    }

    public Task CreateFileAsync(string path)
    {
        var normalizedPath = NormalizePath(path);
        
        if (_nodes.ContainsKey(normalizedPath))
        {
            throw new IOException($"File already exists: {path}");
        }

        var parentPath = GetParentPath(normalizedPath);
        EnsureDirectoryExists(parentPath);

        var fileName = GetFileName(normalizedPath);
        _nodes[normalizedPath] = new FsNode
        {
            Name = fileName,
            Type = FsEntryType.File,
            Data = Array.Empty<byte>(),
            CreatedAt = DateTimeOffset.UtcNow,
            UpdatedAt = DateTimeOffset.UtcNow,
            Permissions = FsEntryPermissions.ReadWrite
        };

        return Task.CompletedTask;
    }

    public Task CreateDirectoryAsync(string path)
    {
        var normalizedPath = NormalizePath(path);
        
        if (_nodes.ContainsKey(normalizedPath))
        {
            throw new IOException($"Directory already exists: {path}");
        }

        var parentPath = GetParentPath(normalizedPath);
        if (parentPath != "/")
        {
            EnsureDirectoryExists(parentPath);
        }

        var dirName = GetFileName(normalizedPath);
        _nodes[normalizedPath] = new FsNode
        {
            Name = dirName,
            Type = FsEntryType.Folder,
            CreatedAt = DateTimeOffset.UtcNow,
            UpdatedAt = DateTimeOffset.UtcNow,
            Permissions = FsEntryPermissions.ReadWrite
        };

        return Task.CompletedTask;
    }

    public Task<Stream> ReadAsync(string path)
    {
        var normalizedPath = NormalizePath(path);
        
        if (!_nodes.TryGetValue(normalizedPath, out var node))
        {
            throw new FileNotFoundException($"File not found: {path}");
        }

        if (node.Type != FsEntryType.File)
        {
            throw new InvalidOperationException($"Path is not a file: {path}");
        }

        if (node.Permissions == FsEntryPermissions.None)
        {
            throw new UnauthorizedAccessException($"No read permission: {path}");
        }

        var stream = new MemoryStream(node.Data ?? Array.Empty<byte>());
        return Task.FromResult<Stream>(stream);
    }

    public async Task WriteAsync(string path, Stream dataStream)
    {
        var normalizedPath = NormalizePath(path);
        
        if (!_nodes.TryGetValue(normalizedPath, out var node))
        {
            var parentPath = GetParentPath(normalizedPath);
            EnsureDirectoryExists(parentPath);

            var fileName = GetFileName(normalizedPath);
            
            node = new FsNode
            {
                Name = fileName,
                Type = FsEntryType.File,
                Data = Array.Empty<byte>(),
                CreatedAt = DateTimeOffset.UtcNow,
                UpdatedAt = DateTimeOffset.UtcNow,
                Permissions = FsEntryPermissions.ReadWrite
            };

            _nodes[normalizedPath] = node;
        }

        if (node.Type != FsEntryType.File)
        {
            throw new InvalidOperationException($"Path is not a file: {path}");
        }

        if (node.Permissions != FsEntryPermissions.ReadWrite)
        {
            throw new UnauthorizedAccessException($"No write permission: {path}");
        }

        using var memoryStream = new MemoryStream();
        await dataStream.CopyToAsync(memoryStream);
        
        node.Data = memoryStream.ToArray();
        node.UpdatedAt = DateTimeOffset.UtcNow;
    }

    public Task MoveAsync(string oldPath, string newPath)
    {
        var normalizedOldPath = NormalizePath(oldPath);
        var normalizedNewPath = NormalizePath(newPath);
        
        if (!_nodes.TryGetValue(normalizedOldPath, out var node))
        {
            throw new FileNotFoundException($"Source not found: {oldPath}");
        }

        if (_nodes.ContainsKey(normalizedNewPath))
        {
            throw new IOException($"Destination already exists: {newPath}");
        }

        var newParentPath = GetParentPath(normalizedNewPath);
        EnsureDirectoryExists(newParentPath);

        // If moving a directory, move all its children
        if (node.Type == FsEntryType.Folder)
        {
            var prefix = normalizedOldPath == "/" ? "/" : normalizedOldPath + "/";
            var childrenToMove = _nodes
                .Where(kvp => kvp.Key.StartsWith(prefix) || kvp.Key == normalizedOldPath)
                .ToList();

            foreach (var child in childrenToMove)
            {
                var relativePath = child.Key.Substring(normalizedOldPath.Length);
                var newChildPath = normalizedNewPath + relativePath;
                
                _nodes[newChildPath] = child.Value;
                _nodes.Remove(child.Key);
            }
        }
        else
        {
            _nodes[normalizedNewPath] = node;
            _nodes.Remove(normalizedOldPath);
        }

        node.Name = GetFileName(normalizedNewPath);
        node.UpdatedAt = DateTimeOffset.UtcNow;

        return Task.CompletedTask;
    }

    public Task DeleteAsync(string path)
    {
        var normalizedPath = NormalizePath(path);
        
        if (!_nodes.TryGetValue(normalizedPath, out var node))
        {
            throw new FileNotFoundException($"Path not found: {path}");
        }

        if (normalizedPath == "/")
        {
            throw new InvalidOperationException("Cannot delete root directory");
        }

        // If deleting a directory, delete all its children
        if (node.Type == FsEntryType.Folder)
        {
            var prefix = normalizedPath + "/";
            var childrenToDelete = _nodes
                .Where(kvp => kvp.Key.StartsWith(prefix))
                .Select(kvp => kvp.Key)
                .ToList();

            foreach (var child in childrenToDelete)
            {
                _nodes.Remove(child);
            }
        }

        _nodes.Remove(normalizedPath);
        return Task.CompletedTask;
    }

    private string NormalizePath(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
        {
            throw new ArgumentException("Path cannot be empty", nameof(path));
        }

        // Ensure path starts with /
        if (!path.StartsWith("/"))
        {
            path = "/" + path;
        }

        // Remove trailing / except for root
        if (path.Length > 1 && path.EndsWith("/"))
        {
            path = path.TrimEnd('/');
        }

        // Remove duplicate /
        while (path.Contains("//"))
        {
            path = path.Replace("//", "/");
        }

        return path;
    }

    private string GetParentPath(string path)
    {
        var lastSeparator = path.LastIndexOf(PathSeparator);
        if (lastSeparator <= 0)
        {
            return "/";
        }
        return path.Substring(0, lastSeparator);
    }

    private string GetFileName(string path)
    {
        var lastSeparator = path.LastIndexOf(PathSeparator);
        return lastSeparator >= 0 ? path.Substring(lastSeparator + 1) : path;
    }

    private void EnsureDirectoryExists(string path)
    {
        if (!_nodes.TryGetValue(path, out var node))
        {
            throw new DirectoryNotFoundException($"Parent directory not found: {path}");
        }

        if (node.Type != FsEntryType.Folder)
        {
            throw new InvalidOperationException($"Parent path is not a directory: {path}");
        }
    }

    private class FsNode
    {
        public string Name { get; set; } = string.Empty;
        public FsEntryType Type { get; set; }
        public byte[]? Data { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
        public FsEntryPermissions Permissions { get; set; }
    }

    public Task<string> GetFileDownloadUrlAsync(string path)
    {
        var normalizedPath = NormalizePath(path);
        
        if (!_nodes.TryGetValue(normalizedPath, out var node))
        {
            throw new FileNotFoundException($"File not found: {path}");
        }

        if (node.Type != FsEntryType.File)
        {
            throw new InvalidOperationException($"Path is not a file: {path}");
        }

        if (node.Permissions == FsEntryPermissions.None)
        {
            throw new UnauthorizedAccessException($"No read permission: {path}");
        }

        var data = node.Data ?? [];
        var base64 = Convert.ToBase64String(data);
        
        var encodedFileName = Uri.EscapeDataString(node.Name);
        
        var dataUrl = $"data:application/octet-stream;name={encodedFileName};base64,{base64}";
        return Task.FromResult(dataUrl);
    }
    
    public async Task<string> GetFolderDownloadUrlAsync(string path)
    {
        var normalizedPath = NormalizePath(path);
        
        if (!_nodes.TryGetValue(normalizedPath, out var node))
        {
            throw new DirectoryNotFoundException($"Folder not found: {path}");
        }

        if (node.Type != FsEntryType.Folder)
        {
            throw new InvalidOperationException($"Path is not a folder: {path}");
        }

        // Create a ZIP archive in memory
        using var zipStream = new MemoryStream();
        using (var archive = new System.IO.Compression.ZipArchive(zipStream, System.IO.Compression.ZipArchiveMode.Create, true))
        {
            // Get all files in the folder (recursively)
            var prefix = normalizedPath == "/" ? "/" : normalizedPath + "/";
            var filesToZip = _nodes
                .Where(kvp => kvp.Key.StartsWith(prefix) && kvp.Value.Type == FsEntryType.File)
                .ToList();

            foreach (var file in filesToZip)
            {
                // Calculate relative path for the zip entry
                var relativePath = file.Key.Substring(prefix.Length);
                if (string.IsNullOrEmpty(relativePath))
                {
                    continue;
                }

                var entry = archive.CreateEntry(relativePath, System.IO.Compression.CompressionLevel.Optimal);
                await using var entryStream = entry.Open();
                var fileData = file.Value.Data ?? Array.Empty<byte>();
                await entryStream.WriteAsync(fileData, 0, fileData.Length);
            }
        }

        // Convert ZIP to base64 data URL
        zipStream.Position = 0;
        var zipBytes = zipStream.ToArray();
        var base64 = Convert.ToBase64String(zipBytes);
        
        // Create filename for the ZIP archive
        var folderName = node.Name == "/" ? "root" : node.Name;
        var zipFileName = Uri.EscapeDataString($"{folderName}.zip");
        
        var dataUrl = $"data:application/zip;name={zipFileName};base64,{base64}";
        
        return dataUrl;
    }
    
    public async Task CombineAsync(string destination, string[] paths)
    {
        if (paths == null || paths.Length == 0)
        {
            throw new ArgumentException("At least one source path must be provided", nameof(paths));
        }

        var normalizedDestination = NormalizePath(destination);
        var normalizedPaths = paths.Select(NormalizePath).ToArray();

        // Validate all source paths exist and are files
        foreach (var sourcePath in normalizedPaths)
        {
            if (!_nodes.TryGetValue(sourcePath, out var sourceNode))
            {
                throw new FileNotFoundException($"Source file not found: {sourcePath}");
            }

            if (sourceNode.Type != FsEntryType.File)
            {
                throw new InvalidOperationException($"Source path is not a file: {sourcePath}");
            }

            if (sourceNode.Permissions == FsEntryPermissions.None)
            {
                throw new UnauthorizedAccessException($"No read permission: {sourcePath}");
            }
        }

        // Ensure parent directory exists
        var parentPath = GetParentPath(normalizedDestination);
        EnsureDirectoryExists(parentPath);

        // Combine all file data
        using var combinedStream = new MemoryStream();
        
        foreach (var sourcePath in normalizedPaths)
        {
            var sourceNode = _nodes[sourcePath];
            var sourceData = sourceNode.Data ?? Array.Empty<byte>();
            await combinedStream.WriteAsync(sourceData, 0, sourceData.Length);
        }

        // Create or update the destination file
        var destinationFileName = GetFileName(normalizedDestination);
        var now = DateTimeOffset.UtcNow;

        if (_nodes.TryGetValue(normalizedDestination, out var existingNode))
        {
            if (existingNode.Type != FsEntryType.File)
            {
                throw new InvalidOperationException($"Destination path is not a file: {destination}");
            }

            if (existingNode.Permissions != FsEntryPermissions.ReadWrite)
            {
                throw new UnauthorizedAccessException($"No write permission: {destination}");
            }

            existingNode.Data = combinedStream.ToArray();
            existingNode.UpdatedAt = now;
        }
        else
        {
            _nodes[normalizedDestination] = new FsNode
            {
                Name = destinationFileName,
                Type = FsEntryType.File,
                Data = combinedStream.ToArray(),
                CreatedAt = now,
                UpdatedAt = now,
                Permissions = FsEntryPermissions.ReadWrite
            };
        }
    }
}