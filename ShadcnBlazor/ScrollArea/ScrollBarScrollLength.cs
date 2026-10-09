using System.Text.Json.Serialization;

namespace ShadcnBlazor.ScrollArea;

public class ScrollBarScrollLength
{
    [JsonPropertyName("ThumbLeft")] public double ThumbLeft { get; set; }
    [JsonPropertyName("ThumbTop")] public double ThumbTop { get; set; }
}