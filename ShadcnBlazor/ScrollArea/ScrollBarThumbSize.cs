using System.Text.Json.Serialization;

namespace ShadcnBlazor.ScrollArea;

public class ScrollBarThumbSize
{
    [JsonPropertyName("scrollBarHorizontalSize")] public double ScrollBarHorizontalSize { get; set; }
    [JsonPropertyName("scrollBarVerticalSize")] public double ScrollBarVerticalSize { get; set; }
}