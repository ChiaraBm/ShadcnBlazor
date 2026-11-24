using System.ComponentModel;
using System.Text;
using ModelContextProtocol;
using ModelContextProtocol.Server;
using ShadcnBlazor.Mcp.Catalog;
using Component = ShadcnBlazor.Mcp.Catalog.Component;

namespace ShadcnBlazor.Mcp.Tools;

[McpServerToolType]
public sealed class ComponentTools
{
    private const int MaxSourceLength = 60_000;

    private readonly ComponentCatalogService _catalogService;

    public ComponentTools(ComponentCatalogService catalogService)
        => _catalogService = catalogService;

    [McpServerTool(Name = "list_components", Destructive = false, Idempotent = true, OpenWorld = false,
        ReadOnly = true)]
    [Description(
        "Lists all ShadcnBlazor components from both packages: ShadcnBlazor (core) and ShadcnBlazor.Extras (extras)")]
    public string ListComponents()
        => FormatComponents(_catalogService.AllComponents);

    [McpServerTool(Name = "search_components", Destructive = false, Idempotent = true, OpenWorld = false,
        ReadOnly = true)]
    [Description(
        "Searches ShadcnBlazor components by name, tags, description and documentation. " +
        "Components are matched in both the ShadcnBlazor and ShadcnBlazor.Extras packages")]
    public string SearchComponents(string query,
        [Description("Maximum number of results to return (1–50, default 10)")]
        int limit = 10
    )
    {
        var matches = _catalogService.Search(query, limit);

        if (matches.Count == 0)
            return $"No components match \"{query}\"." + SuggestionOrListHint(query);

        return FormatComponents(matches);
    }

    [McpServerTool(Name = "get_component_docs", Destructive = false, Idempotent = true, OpenWorld = false,
        ReadOnly = true)]
    [Description("Gets the full documentation of a ShadcnBlazor component: usage, parameters and files.")]
    public string GetComponentDocs(string name,
        [Description(
            "Required only when the component exists in both packages: \"ShadcnBlazor\" or \"ShadcnBlazor.Extras\""
        )]
        string? package = null
    )
    {
        return Resolve(name, package).Docs;
    }

    [McpServerTool(Name = "get_component_source", Destructive = false, Idempotent = true, OpenWorld = false,
        ReadOnly = true)]
    [Description(
        "Gets the source code of a ShadcnBlazor component. Returns all of its files " +
        "from both the ShadcnBlazor and ShadcnBlazor.Extras packages " +
        "unless a package is specified"
    )]
    public string GetComponentSource(
        string name,
        [Description(
            "A single file to return, matched by full path or trailing path segment; omit for all files (capped at 60,000 characters)"
        )]
        string? file = null,
        [Description(
            "Required only when the component exists in both packages: \"ShadcnBlazor\" or \"ShadcnBlazor.Extras\""
        )]
        string? package = null
    )
    {
        var component = Resolve(name, package);

        return file is null
                ? ReadFilesWithinLimit(component.Files)
                : ReadSingleFile(component.Files, file);
    }

    private Component Resolve(string name, string? package)
    {
        var packages = _catalogService.Packages(name);

        if (package is not null)
        {
            return packages.Contains(package, StringComparer.Ordinal)
                    ? _catalogService.Find(name, package)
                    : throw NotFound(name, package);
        }

        return packages.Count switch
        {
            0 => throw NotFound(name, package),
            1 => _catalogService.Find(name, package),
            _ => throw new McpException($"\"{name}\" exists in multiple packages: {string.Join(", ", packages)}. " +
                    "Pass the `package` parameter to pick one.")
        };
    }

    private McpException NotFound(string name, string? package)
    {
        var message = package is null
                ? $"\"{name}\" not found."
                : $"\"{name}\" not found in package \"{package}\".";

        return new McpException(message + SuggestionOrListHint(name));
    }

    private string SuggestionOrListHint(string name)
    {
        var suggestions = _catalogService.Suggest(name);

        return suggestions.Count == 0
                ? " Use list_components to see all components."
                : $" Did you mean: {string.Join(", ", suggestions)}.";
    }

    private string ReadSingleFile(IReadOnlyList<SourceFile> files, string file)
    {
        var target = file.Replace('\\', '/');

        var match = files.FirstOrDefault(f => f.Path == target)
                ?? files.FirstOrDefault(f => f.Path.EndsWith("/" + target, StringComparison.Ordinal));

        if (match is null)
            throw new McpException(
                $"\"{file}\" is not a file of this component. Available files:\n{FormatFileList(files)}");

        return ReadAndFormat(match);
    }

    private string ReadFilesWithinLimit(IReadOnlyList<SourceFile> files)
    {
        var included = new List<SourceFile> { files[0] };
        var total = files[0].Length;
        var omitted = new List<SourceFile>();

        foreach (var file in files.Skip(1))
        {
            if (total + file.Length > MaxSourceLength)
            {
                omitted.Add(file);
                continue;
            }

            included.Add(file);
            total += file.Length;
        }

        var builder = new StringBuilder();

        foreach (var file in included)
            builder.Append(ReadAndFormat(file)).Append('\n');

        if (omitted.Count > 0)
        {
            builder.Append(
                    $"Omitted {omitted.Count} file{(omitted.Count == 1 ? "" : "s")} to stay within {MaxSourceLength} characters:\n")
                .Append(FormatFileList(omitted))
                .Append("\n\nrequest them with `file`.\n");
        }

        return builder.ToString();
    }

    private string ReadAndFormat(SourceFile file)
        => FormatSource(file, _catalogService.ReadSource(file));

    private static string FormatComponents(IEnumerable<Component> components)
        => string.Join('\n', components.Select(FormatComponent));

    private static string FormatComponent(Component component)
        => $"- **{component.Title}** (`{component.Name}`) [{component.Package}]: {component.Description}";

    private static string FormatFileList(IEnumerable<SourceFile> files)
        => string.Join('\n', files.Select(file => $"- {file.Path}"));

    private static string FormatSource(SourceFile file, string content)
        => $"### {file.Path}\n\n```{LanguageOf(file.Path)}\n{content}\n```";

    private static string LanguageOf(string path)
        => path.EndsWith(".razor", StringComparison.Ordinal) ? "razor"
            : path.EndsWith(".cs", StringComparison.Ordinal) ? "csharp"
            : path.EndsWith(".css", StringComparison.Ordinal) ? "css"
            : "text";
}