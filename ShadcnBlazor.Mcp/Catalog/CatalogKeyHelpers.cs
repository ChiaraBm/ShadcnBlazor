using System.Text.RegularExpressions;

namespace ShadcnBlazor.Mcp.Catalog;

public static partial class CatalogKeyHelpers
{
    public static string Normalize(string value)
        => new(value.Where(char.IsLetterOrDigit).Select(char.ToLowerInvariant).ToArray());

    public static string ToKebabCase(string value)
        => LowerToUpperRegex().Replace(value, "$1-$2").ToLowerInvariant();

    public static string Combine(string package, string name)
        => $"{Normalize(package)}/{Normalize(name)}";

    public static IEnumerable<string> Variants(string name)
    {
        yield return name;

        if (name.Length > 1 && name.EndsWith('s'))
            yield return name[..^1];

        yield return name + "s";
    }

    public static string PackageOf(string path)
        => path.Split('/')[1];

    public static string ComponentFolder(string path)
        => path.Split('/')[2];

    [GeneratedRegex("([a-z0-9])([A-Z])", RegexOptions.Compiled)]
    private static partial Regex LowerToUpperRegex();
}