using ShadcnBlazor.Extras.Editors;

namespace ShadcnBlazor.Extras.FileManagers;

public static class EditorHelper
{
    private static readonly Dictionary<string, EditorLanguage> AllowedExtensions = new()
    {
        // JavaScript/TypeScript
        ["js"] = EditorLanguage.Javascript,
        ["mjs"] = EditorLanguage.Javascript,
        ["cjs"] = EditorLanguage.Javascript,
        ["ts"] = EditorLanguage.Javascript,
        ["jsx"] = EditorLanguage.Javascript,
        ["tsx"] = EditorLanguage.Javascript,
    
        // HTML
        ["html"] = EditorLanguage.Html,
        ["htm"] = EditorLanguage.Html,
    
        // JSON
        ["json"] = EditorLanguage.Json,
        ["jsonc"] = EditorLanguage.Json,
        ["json5"] = EditorLanguage.Json,
    
        // SQL
        ["sql"] = EditorLanguage.Sql,
    
        // YAML
        ["yml"] = EditorLanguage.Yaml,
        ["yaml"] = EditorLanguage.Yaml,
    
        // PHP
        ["php"] = EditorLanguage.Php,
    
        // XML
        ["xml"] = EditorLanguage.Xml,
    
        // Python
        ["py"] = EditorLanguage.Python,
    
        // CSS
        ["css"] = EditorLanguage.Css,
        ["scss"] = EditorLanguage.Css,
        ["sass"] = EditorLanguage.Css,
        ["less"] = EditorLanguage.Css,
    
        // Configuration files
        ["txt"] = EditorLanguage.None,
        ["md"] = EditorLanguage.None,
        ["env"] = EditorLanguage.None,
        ["properties"] = EditorLanguage.None,
        ["conf"] = EditorLanguage.None,
        ["cfg"] = EditorLanguage.None,
        ["ini"] = EditorLanguage.None,
        ["toml"] = EditorLanguage.None,
        ["lock"] = EditorLanguage.None,
        ["log"] = EditorLanguage.None,
    
        // Java/Kotlin (Minecraft plugins)
        ["java"] = EditorLanguage.None,
        ["kt"] = EditorLanguage.None,
        ["kts"] = EditorLanguage.None,
    
        // Shell scripts
        ["sh"] = EditorLanguage.None,
        ["bash"] = EditorLanguage.None,
        ["bat"] = EditorLanguage.None,
        ["cmd"] = EditorLanguage.None,
        ["ps1"] = EditorLanguage.None,
    };

    public static bool TryGetLanguage(string extension, out EditorLanguage editorLanguage)
        => AllowedExtensions.TryGetValue(extension, out editorLanguage);
    
    public static EditorLanguage GetLanguage(string extension)
        => AllowedExtensions.GetValueOrDefault(extension);

    public static bool CheckExtension(string extension) => AllowedExtensions.ContainsKey(extension);

    public static string GetExtension(string fileName)
    {
        var parts = fileName.Split('.');

        return parts.Length == 1 ? parts[0] : parts[parts.Length - 1];
    }
}