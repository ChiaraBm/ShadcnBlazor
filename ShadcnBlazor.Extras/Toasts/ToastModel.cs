namespace ShadcnBlazor.Extras.Toasts;

public class ToastModel
{
    public Type ComponentType { get; set; }
    public Dictionary<string, object?> Attributes { get; set; }
    public bool IsOpen { get; set; }
}