namespace ShadcnBlazor.Dropdowns;

public interface IDropdownMenuParent
{
    public bool IsOpen { get; }
    public Task ChangeFocusAsync(IDropdownMenuItem item);
    public Task CloseMenuAsync();
}