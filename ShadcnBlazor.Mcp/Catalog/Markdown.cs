namespace ShadcnBlazor.Mcp.Catalog;

public static class Markdown
{
    private const string Fence = "---";
    private const string ClosingFence = "\n---";

    public static MarkdownDocument Parse(string text)
    {
        var frontMatter = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        if (!TryFindFrontMatterEnd(text, out var end))
            return new MarkdownDocument(frontMatter, text.Trim());

        foreach (var line in text[Fence.Length..end].Split('\n'))
        {
            var colon = line.IndexOf(':');
            if (colon <= 0)
                continue;

            var key = line[..colon].Trim();
            var value = line[(colon + 1)..].Trim().Trim('"', '\'');

            frontMatter[key] = value;
        }

        var body = text[(end + ClosingFence.Length)..];

        return new MarkdownDocument(
            frontMatter,
            body.Trim()
        );
    }

    public static string? Title(string markdown)
        => Headings(markdown).FirstOrDefault(heading => heading.Level == 1)?.Text ?? null;

    public static string? FirstParagraph(string markdown)
    {
        var insideCodeBlock = false;

        foreach (var rawLine in markdown.Split('\n'))
        {
            var line = rawLine.Trim();

            if (line.StartsWith("```", StringComparison.Ordinal))
            {
                insideCodeBlock = !insideCodeBlock;
                continue;
            }

            var isText = !insideCodeBlock && line.Length > 0 && !line.StartsWith('#');
            if (isText)
                return line;
        }

        return null;
    }

    private static bool TryFindFrontMatterEnd(string text, out int end)
    {
        end = -1;

        if (!text.StartsWith(Fence, StringComparison.Ordinal))
            return false;

        end = text.IndexOf(ClosingFence, Fence.Length, StringComparison.Ordinal);
        if (end < 0)
            return false;

        var afterFence = end + ClosingFence.Length;

        return afterFence == text.Length || text[afterFence] is '\n' or '\r';
    }

    private static IEnumerable<MarkdownHeading> Headings(string markdown)
    {
        var lines = markdown.Split('\n');
        var insideCodeBlock = false;

        for (var lineNumber = 0; lineNumber < lines.Length; lineNumber++)
        {
            var line = lines[lineNumber].TrimStart();

            if (line.StartsWith("```", StringComparison.Ordinal))
            {
                insideCodeBlock = !insideCodeBlock;
                continue;
            }

            if (!insideCodeBlock && TryParseHeading(line, out var level, out var text))
                yield return new MarkdownHeading(level, text, lineNumber);
        }
    }

    private static bool TryParseHeading(string line, out int level, out string text)
    {
        level = line.Length - line.TrimStart('#').Length;
        text = line[level..].Trim();

        var isValidLevel = level is >= 1 and <= 6;
        var isFollowedBySpace = level == line.Length || line[level] == ' ';
        return isValidLevel && isFollowedBySpace;
    }

    private sealed record MarkdownHeading(int Level, string? Text, int Line);

    public sealed record MarkdownDocument(Dictionary<string, string> FrontMatter, string Body);
}