namespace ShadcnBlazor.Extras;

/// <summary>
/// Provides helper methods to create unix compatible paths.
/// Even when your program is running on windows.
/// Inspired by <see cref="Path"/>
/// </summary>
public static class UnixPath
{
    public static readonly char DirectorySeparatorChar = '/';

    /// <summary>
    /// Combines n numbers of parts of a path to one path
    /// </summary>
    /// <param name="paths">Parts of the new path</param>
    /// <returns>Combined path</returns>
    public static string Combine(params string[] paths)
    {
        return string.Join(DirectorySeparatorChar, paths.Select(p => p.Trim(DirectorySeparatorChar)));
    }

    /// <summary>
    /// Returns the file name of a specific path
    /// </summary>
    /// <param name="path">Path to determine the file name from</param>
    /// <returns>Filename or empty string if not found</returns>
    public static string GetFileName(string path)
    {
        if (string.IsNullOrEmpty(path))
            return string.Empty;
        
        var parts = path.Split(DirectorySeparatorChar);
        
        return parts[^1];
    }

    /// <summary>
    /// Returns the file name without the extension
    /// </summary>
    /// <param name="path">Filename or path</param>
    /// <returns>File name without extension</returns>
    public static string GetFileNameWithoutExtension(string path)
    {
        var fileName = GetFileName(path);
        var index = fileName.LastIndexOf('.');
        
        return index >= 0 ? fileName.Substring(0, index) : fileName;
    }

    /// <summary>
    /// Returns the extension of a file
    /// </summary>
    /// <param name="path">Filename or path</param>
    /// <returns>Extension of the file</returns>
    public static string GetExtension(string path)
    {
        var fileName = GetFileName(path);
        var index = fileName.LastIndexOf('.');
        
        return index >= 0 ? fileName.Substring(index) : string.Empty;
    }

    /// <summary>
    /// Returns the directory portion of a file path. This method effectively
    /// removes the last segment of the given file path, i.e. it returns a
    /// string consisting of all characters up to but not including the last
    /// slash ("/") in the file path
    /// </summary>
    /// <param name="path">Path to determine the directory name from</param>
    public static string GetDirectoryName(string path)
    {
        if (string.IsNullOrEmpty(path))
            return string.Empty;
        
        var index = path.LastIndexOf(DirectorySeparatorChar);
        
        return index > 0 ? path.Substring(0, index) : string.Empty;
    }

    /// <summary>
    /// Changes the file extension on a file path to the newly provided extension
    /// </summary>
    /// <param name="path">File path to change the extension of</param>
    /// <param name="newExtension">New extension to change the file path to</param>
    /// <returns></returns>
    public static string ChangeExtension(string path, string newExtension)
    {
        var dir = GetDirectoryName(path);
        
        var fileNameWithoutExt = GetFileNameWithoutExtension(path);
        
        if (!string.IsNullOrEmpty(newExtension) && !newExtension.StartsWith("."))
            newExtension = "." + newExtension;
        
        return Combine(dir, fileNameWithoutExt + newExtension);
    }

    /// <summary>
    /// Gets the full path of a regular path
    /// </summary>
    /// <param name="path">Regular path to process</param>
    /// <param name="basePath">Base aka. root path</param>
    /// <returns>Full path of the <see cref="path"/></returns>
    public static string GetFullPath(string path, string basePath = "/")
    {
        if (IsPathRooted(path))
            return NormalizePath(path);
        
        return NormalizePath(Combine(basePath, path));
    }

    /// <summary>
    /// Checks if the path is rooted
    /// </summary>
    /// <param name="path">Path to check</param>
    public static bool IsPathRooted(string path)
    {
        return !string.IsNullOrEmpty(path) && path[0] == DirectorySeparatorChar;
    }

    /// <summary>
    /// Normalizes the provided path handling ".." and "."
    /// </summary>
    /// <param name="path">Path to normalize</param>
    /// <returns>Normalized path</returns>
    public static string NormalizePath(string path)
    {
        if (string.IsNullOrEmpty(path)) return string.Empty;
        var segments = new Stack<string>();
        foreach (var part in path.Split(DirectorySeparatorChar))
        {
            if (part == "..")
            {
                if (segments.Count > 0) segments.Pop();
            }
            else if (!string.IsNullOrEmpty(part) && part != ".")
            {
                segments.Push(part);
            }
        }
        var normalized = string.Join(DirectorySeparatorChar, segments.Reverse());
        return IsPathRooted(path) ? DirectorySeparatorChar + normalized : normalized;
    }

    /// <summary>
    /// Gets the relative path to the target from the base path
    /// </summary>
    /// <param name="basePath">Base path to create the relative path from</param>
    /// <param name="targetPath">Target path to create the relative path to</param>
    /// <returns>Created relative path</returns>
    public static string GetRelativePath(string basePath, string targetPath)
    {
        var baseParts = NormalizePath(basePath).Trim(DirectorySeparatorChar).Split(DirectorySeparatorChar);
        var targetParts = NormalizePath(targetPath).Trim(DirectorySeparatorChar).Split(DirectorySeparatorChar);
        var commonLength = 0;
        while (commonLength < baseParts.Length && commonLength < targetParts.Length &&
               baseParts[commonLength] == targetParts[commonLength])
        {
            commonLength++;
        }

        var upMoves = Enumerable.Repeat("..", baseParts.Length - commonLength);
        var downMoves = targetParts.Skip(commonLength);
        return string.Join(DirectorySeparatorChar, upMoves.Concat(downMoves));
    }
}