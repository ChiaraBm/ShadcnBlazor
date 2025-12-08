namespace ShadcnBlazor.Menubar;

public interface IMenubarParent
{
    public bool IsOpen { get; }
    public Task ChangeFocusAsync(IMenubarItem item);
    public Task CloseMenuAsync();
}