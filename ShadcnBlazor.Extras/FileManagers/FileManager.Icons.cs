using LucideBlazor;
using Microsoft.AspNetCore.Components;

namespace ShadcnBlazor.Extras.FileManagers;

public partial class FileManager
{
    private static RenderFragment GetIcon(string fileName)
    {
        var parts = fileName.Split('.');

        var extension = parts.Length == 1
            ? parts[0].ToLowerInvariant()
            : parts[parts.Length - 1].ToLowerInvariant();

        var type = extension switch
        {
            // Archives
            "zip" or "rar" or "7z" or "tar" or "gz" or "bz2" or "jar" => typeof(FileArchiveIcon),

            // JavaScript/TypeScript
            "js" or "mjs" or "cjs" or "jsx" => typeof(FileCodeIcon),
            "ts" or "tsx" => typeof(FileCodeIcon),

            // HTML
            "html" or "htm" => typeof(FileCodeIcon),

            // JSON
            "json" or "jsonc" or "json5" => typeof(FileBracesIcon),

            // SQL
            "sql" => typeof(FileCodeIcon),

            // YAML
            "yml" or "yaml" => typeof(FileBracesIcon),

            // PHP
            "php" => typeof(FileCodeIcon),

            // XML
            "xml" => typeof(FileBracesIcon),

            // Python
            "py" or "pyc" or "pyd" => typeof(FileCodeIcon),

            // CSS
            "css" or "scss" or "sass" or "less" => typeof(FileCodeIcon),

            // Configuration files
            "txt" or "log" => typeof(FileTextIcon),
            "md" => typeof(FileTextIcon),
            "env" or "properties" or "conf" or "cfg" or "ini" or "toml" => typeof(FileCogIcon),
            "lock" => typeof(FileLockIcon),

            // Java/Kotlin
            "java" or "class" => typeof(FileCodeIcon),
            "kt" or "kts" => typeof(FileCodeIcon),

            // Shell scripts
            "sh" or "bash" => typeof(FileTerminalIcon),
            "bat" or "cmd" or "ps1" => typeof(FileTerminalIcon),

            // Other code files
            "cs" or "csx" => typeof(FileCodeIcon),
            "cpp" or "c" or "h" or "hpp" => typeof(FileCodeIcon),
            "rb" => typeof(FileCodeIcon),
            "go" => typeof(FileCodeIcon),
            "rs" => typeof(FileCodeIcon),
            "swift" => typeof(FileCodeIcon),
            "xaml" => typeof(FileBracesIcon),

            // Documents
            "pdf" => typeof(FileTextIcon),
            "doc" or "docx" or "odt" or "rtf" => typeof(FileTextIcon),

            // Spreadsheets
            "xls" or "xlsx" or "csv" or "ods" => typeof(FileSpreadsheetIcon),

            // Keys/Security
            "key" or "pem" or "cert" or "crt" => typeof(FileKeyIcon),

            // Images
            "jpg" or "jpeg" or "png" or "gif" or "bmp" or "svg" or "webp" or "ico" => typeof(FileImageIcon),

            // Audio
            "mp3" or "wav" or "flac" or "aac" or "ogg" or "m4a" or "wma" => typeof(FileMusicIcon),

            // Video
            "mp4" or "avi" or "mkv" or "mov" or "wmv" or "flv" or "webm" => typeof(FilePlayIcon),

            // Special
            "lnk" or "symlink" => typeof(FileSymlinkIcon),
            "diff" or "patch" => typeof(FileDiffIcon),
            "exe" or "dll" or "so" or "dylib" => typeof(FileCogIcon),

            _ => typeof(FileIcon)
        };

        var className = extension switch
        {
            // Archives - amber
            "zip" or "rar" or "7z" or "tar" or "gz" or "bz2" => "text-amber-500",

            // JavaScript - yellow
            "js" or "mjs" or "cjs" or "jsx" => "text-yellow-400",

            // TypeScript - blue
            "ts" or "tsx" => "text-blue-500",

            // HTML - orange
            "html" or "htm" => "text-orange-500",

            // JSON - yellow
            "json" or "jsonc" or "json5" => "text-yellow-500",

            // SQL - pink
            "sql" => "text-pink-500",

            // YAML - purple
            "yml" or "yaml" => "text-purple-400",

            // PHP - indigo
            "php" => "text-indigo-400",

            // XML - green
            "xml" or "xaml" => "text-green-600",

            // Python - blue
            "py" or "pyc" or "pyd" => "text-blue-400",

            // CSS - blue
            "css" or "scss" or "sass" or "less" => "text-blue-400",

            // Plain text - slate
            "txt" => "text-slate-400",
            "log" => "text-slate-500",
            "md" => "text-slate-300",

            // Configuration - slate/gray
            "env" or "properties" => "text-slate-500",
            "conf" or "cfg" or "ini" => "text-slate-500",
            "toml" => "text-slate-500",
            "lock" => "text-yellow-600",

            // Java - orange/red
            "java" or "class" or "jar" => "text-orange-600",

            // Kotlin - purple
            "kt" or "kts" => "text-purple-600",

            // Shell scripts - green
            "sh" or "bash" => "text-green-500",
            "bat" or "cmd" => "text-green-600",
            "ps1" => "text-blue-600",

            // C# - purple
            "cs" or "csx" => "text-purple-500",

            // C++ - blue
            "cpp" or "c" or "h" or "hpp" => "text-blue-600",

            // Ruby - red
            "rb" => "text-red-500",

            // Go - cyan
            "go" => "text-cyan-400",

            // Rust - orange
            "rs" => "text-orange-500",

            // Swift - orange
            "swift" => "text-orange-400",

            // Documents - various
            "pdf" => "text-red-600",
            "doc" or "docx" or "odt" or "rtf" => "text-blue-600",

            // Spreadsheets - green
            "xls" or "xlsx" or "csv" or "ods" => "text-green-600",

            // Keys/Security - amber
            "key" or "pem" or "cert" or "crt" => "text-amber-600",

            // Images - pink
            "jpg" or "jpeg" or "png" or "gif" or "bmp" or "svg" or "webp" or "ico" => "text-pink-500",

            // Audio - purple
            "mp3" or "wav" or "flac" or "aac" or "ogg" or "m4a" or "wma" => "text-purple-500",

            // Video - red
            "mp4" or "avi" or "mkv" or "mov" or "wmv" or "flv" or "webm" => "text-red-500",

            // Special - cyan/teal
            "lnk" or "symlink" => "text-cyan-500",
            "diff" or "patch" => "text-teal-500",
            "exe" or "dll" or "so" or "dylib" => "text-slate-600",

            _ => "text-slate-400"
        };

        return builder =>
        {
            builder.OpenComponent(0, type);
            builder.AddComponentParameter(1, nameof(IconBase.ClassName), className);
            builder.CloseComponent();
        };
    }
}