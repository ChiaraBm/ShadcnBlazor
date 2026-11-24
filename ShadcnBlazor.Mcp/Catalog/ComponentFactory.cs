namespace ShadcnBlazor.Mcp.Catalog;

public static class ComponentFactory
{
    public static IReadOnlyList<Component> BuildComponents(
        IReadOnlyList<SourceFile> sources, IReadOnlyDictionary<string, string> instructions
    )
    {
        return sources
            .Where(file => file.Path.Split('/').Length >= 4)
            .GroupBy(file => CatalogKeyHelpers.Combine(CatalogKeyHelpers.PackageOf(file.Path),
                CatalogKeyHelpers.ComponentFolder(file.Path)))
            .Where(group => group.Any(file => file.Path.EndsWith(".razor", StringComparison.OrdinalIgnoreCase)))
            .Select(group => BuildFromGroup(group, instructions))
            .ToArray();
    }

    private static Component BuildFromGroup(
        IGrouping<string, SourceFile> group,
        IReadOnlyDictionary<string, string> instructions
    )
    {
        var sample = group.First().Path;

        var package = CatalogKeyHelpers.PackageOf(sample);
        var folder = CatalogKeyHelpers.ComponentFolder(sample);

        return BuildComponent(
            package,
            folder,
            group.ToArray(),
            instructions.GetValueOrDefault(group.Key)
        );
    }

    private static Component BuildComponent(
        string package,
        string folder, SourceFile[] files,
        string? instruction
    )
    {
        var name = CatalogKeyHelpers.ToKebabCase(folder);
        var label = package == ComponentCatalogService.CorePackage ? "Core" : "Extra";
        var description = $"{label} component folder \"{folder}\" with {files.Length} file{(files.Length == 1 ? "" : "s")}.";

        string docs;
        string title;
        string[] tags;

        if (instruction is null)
        {
            title = folder;
            docs = GenerateDocs(package, folder, files.Select(file => file.Path));
            tags = [package, CatalogKeyHelpers.ToKebabCase(package)];
        }

        else
        {
            var document = Markdown.Parse(instruction);

            title = document.FrontMatter.GetValueOrDefault("title") is { Length: > 0 } docTitle
                    ? docTitle
                    : Markdown.Title(document.Body) ?? folder;

            description = document.FrontMatter.GetValueOrDefault("description") is { Length: > 0 } docDescription
                    ? docDescription
                    : Markdown.FirstParagraph(document.Body) ?? description;

            tags = ParseTags(document.FrontMatter.GetValueOrDefault("tags")) is { Length: > 0 } parsed
                    ? parsed
                    : [package, CatalogKeyHelpers.ToKebabCase(package)];

            docs = document.Body;
        }

        return new Component
        {
            Name = name,
            Package = package,
            Title = title,
            Description = description,
            Tags = tags,
            Docs = docs,
            Files = files,
        };
    }

    private static string[] ParseTags(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return [];

        var tagList = value.Trim().Trim('[', ']');

        return tagList.Split(',', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
    }

    private static string GenerateDocs(string package, string folder, IEnumerable<string> paths)
    {
        var packageLabel = package == ComponentCatalogService.CorePackage ? "core" : "extras";
        var fileList = string.Join('\n', paths.Select(path => $"- {path}"));

        return $"No instruction doc is available for \"{folder}\" from the {packageLabel} package. " +
               $"Install it with `dotnet add package {package}`.\n\n## Files\n\n{fileList}";
    }
}