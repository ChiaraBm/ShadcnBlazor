namespace ShadcnBlazor.Extras;

internal static class Formatter
{
    internal static string FormatSize(long bytes, double conversionStep = 1024)
    {
        if (bytes == 0) return "0 B";
    
        string[] units = ["B", "KB", "MB", "GB", "TB", "PB", "EB"];
        var unitIndex = 0;
        double size = bytes;
    
        while (size >= conversionStep && unitIndex < units.Length - 1)
        {
            size /= conversionStep;
            unitIndex++;
        }
    
        var decimals = unitIndex == 0 ? 0 : 2;
        return $"{Math.Round(size, decimals)} {units[unitIndex]}";
    }
}