using ShadcnBlazor.Dialogs;

namespace ShadcnBlazor.Extras.Dialogs;

public class DialogModel
{
    public Type ComponentType { get; set; }
    public Dictionary<string, object?> Attributes { get; set; }
    public Dialog Dialog { get; set; }
    public string ClassName { get; set; }
    public bool CloseOnBackdropClick { get; set; }
    public bool ShowCloseButton { get; set; } = true;
}