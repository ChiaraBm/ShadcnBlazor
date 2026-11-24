namespace ShadcnBlazor.Mcp.Resources;

public sealed class EmbeddedResourceService
{
    public IReadOnlyList<string> Paths { get; }

    private readonly Dictionary<string, string> _byPath;

    public EmbeddedResourceService()
    {
        var assembly = typeof(EmbeddedResourceService).Assembly;

        _byPath = assembly.GetManifestResourceNames()
            .ToDictionary(
                name => name.Replace('\\', '/'),
                name => ReadAllText(assembly.GetManifestResourceStream(name)!),
                StringComparer.Ordinal);

        Paths = [.. _byPath.Keys.OrderBy(name => name, StringComparer.Ordinal)];
    }

    public string Read(string path)
        => _byPath[path];

    private static string ReadAllText(Stream stream)
    {
        using var reader = new StreamReader(stream);
        return reader.ReadToEnd();
    }
}