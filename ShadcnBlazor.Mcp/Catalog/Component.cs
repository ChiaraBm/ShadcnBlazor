namespace ShadcnBlazor.Mcp.Catalog;

public sealed record Component
{
    public required string Name { get; init; }

    public required string Package { get; init; }

    public required string Title { get; init; }

    public required string Description { get; init; }

    public required IReadOnlyList<string> Tags { get; init; }

    public required string Docs { get; init; }

    public required IReadOnlyList<SourceFile> Files { get; init; }
}