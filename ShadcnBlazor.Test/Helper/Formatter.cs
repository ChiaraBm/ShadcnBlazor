using System.Text.RegularExpressions;

namespace ShadcnBlazor.Test.Helper;

internal static partial class Formatter
{
    public static string InsertSpaceBeforeCapital(string input)
    {
        if (string.IsNullOrEmpty(input))
            return input;
        
        return SpaceBeforeCapitalRegex().Replace(input, " ");
    }

    [GeneratedRegex("(?<=[a-z])(?=[A-Z])")]
    private static partial Regex SpaceBeforeCapitalRegex();
}