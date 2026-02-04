namespace ShadcnBlazor.Extras.FileManagers.Abstractions;

public interface ICombineAccess
{
    public Task CombineAsync(string destination, string[] paths);
}