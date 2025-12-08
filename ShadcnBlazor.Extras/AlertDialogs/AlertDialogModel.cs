using ShadcnBlazor.AlertDialogs;

namespace ShadcnBlazor.Extras.AlertDialogs;

public class AlertDialogModel
{
    public Dictionary<string, object?> Attributes { get; set; }
    public Type ComponentType { get; set; }
    public AlertDialog Dialog { get; set; }
    public bool CloseOnBackdropClick { get; set; }
}