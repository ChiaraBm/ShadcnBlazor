using ModelContextProtocol;
using ShadcnBlazor.Mcp.Resources;

namespace ShadcnBlazor.Mcp.Catalog;

public sealed class ComponentCatalogService
{
    public IReadOnlyList<Component> AllComponents { get; }

    public const string CorePackage = "ShadcnBlazor";
    public const string ExtrasPackage = "ShadcnBlazor.Extras";

    private const string SourcePrefix = "src/";
    private const string InstructionsPrefix = "instructions/";

    private static readonly char[] WhiteSpaceSeparators = [' ', '\t', '\n', '\r'];

    public ComponentCatalogService(EmbeddedResourceService resources)
    {
        var sources = new List<SourceFile>();
        var instructions = new Dictionary<string, string>(StringComparer.Ordinal);

        foreach (var path in resources.Paths)
        {
            if (path.StartsWith(SourcePrefix, StringComparison.Ordinal))
            {
                var content = resources.Read(path);
                sources.Add(new SourceFile(path, content.Length, content));
            }

            else if (path.StartsWith(InstructionsPrefix, StringComparison.Ordinal) &&
                     TryGetInstructionsKey(path, out var key))
                instructions[key] = resources.Read(path);
        }

        AllComponents =
            ComponentFactory.BuildComponents(sources, instructions)
                .OrderBy(component => component.Package == ExtrasPackage ? 1 : 0)
                .ThenBy(component => component.Name, StringComparer.Ordinal)
                .ToArray();
    }

    public Component Find(string name, string? package)
    {
        var component = MatchByName(name)
            .FirstOrDefault(component => package is null || component.Package == package);

        if (component is null)
            throw new McpException(
                package is null
                    ? $"\"{name}\" not found."
                    : $"\"{name}\" not found in package \"{package}\"."
            );

        return component;
    }

    public IReadOnlyList<string> Packages(string name)
        => MatchByName(name).Select(component => component.Package).Distinct().ToList();

    public string ReadSource(SourceFile file) => file.Content;

    public IReadOnlyList<Component> Search(string query, int limit)
    {
        var terms = query.Split(WhiteSpaceSeparators, StringSplitOptions.RemoveEmptyEntries);

        var maxItems = Math.Clamp(limit, 1, 50);

        return AllComponents
            .Select(component => (component, Score: terms.Sum(term => CalculateScore(component, term))))
            .Where(scored => scored.Score > 0)
            .OrderByDescending(scored => scored.Score)
            .ThenBy(scored => scored.component.Name, StringComparer.Ordinal)
            .Take(maxItems)
            .Select(scored => scored.component)
            .ToArray();
    }

    public IReadOnlyList<string> Suggest(string name)
    {
        var key = CatalogKeyHelpers.Normalize(name);

        return AllComponents
            .Select(component => component.Name)
            .Distinct()
            .Where(candidate => Matches(candidate, key))
            .Take(5)
            .ToArray();
    }

    private Component[] MatchByName(string name)
    {
        foreach (var variant in CatalogKeyHelpers.Variants(CatalogKeyHelpers.Normalize(name)))
        {
            var matches = AllComponents
                .Where(component => CatalogKeyHelpers.Normalize(component.Name) == variant)
                .ToArray();

            if (matches.Length != 0)
                return matches;
        }

        return [];
    }

    private static int CalculateScore(Component component, string term)
    {
        var key = CatalogKeyHelpers.Normalize(term);
        var name = CatalogKeyHelpers.Normalize(component.Name);

        var score = 0;

        if (name == key)
            score += 100;

        else if (name.Contains(key, StringComparison.Ordinal))
            score += 30;

        if (component.Tags.Any(tag => tag.Equals(term, StringComparison.OrdinalIgnoreCase)))
            score += 20;

        if (component.Description.Contains(term, StringComparison.OrdinalIgnoreCase))
            score += 10;

        if (component.Docs.Contains(term, StringComparison.OrdinalIgnoreCase))
            score += 5;

        return score;
    }

    private static bool Matches(string candidate, string key)
    {
        return CatalogKeyHelpers.Normalize(candidate).Contains(key, StringComparison.Ordinal)
               || key.Contains(CatalogKeyHelpers.Normalize(candidate), StringComparison.Ordinal)
               || candidate.Split('-').Any(word => word.Contains(key, StringComparison.Ordinal));
    }

    private static bool TryGetInstructionsKey(string path, out string key)
    {
        key = string.Empty;

        if (path.Split('/') is not [_, var folder, var fileName]
            || !fileName.EndsWith(".md", StringComparison.Ordinal))
            return false;

        key = CatalogKeyHelpers.Combine(folder, fileName[..^".md".Length]);
        return true;
    }
}