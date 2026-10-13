using System.Text.Json.Serialization;

namespace ShadcnBlazor.ScrollArea;

public class ScrollBarScrollLength
{
    [JsonPropertyName("thumbLeft")] public double ThumbLeft { get; set; }
    [JsonPropertyName("thumbTop")] public double ThumbTop { get; set; }
}