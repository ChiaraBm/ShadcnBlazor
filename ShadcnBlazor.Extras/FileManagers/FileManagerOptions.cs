using Microsoft.Extensions.DependencyInjection;
using ShadcnBlazor.Extras.FileManagers.Abstractions;

namespace ShadcnBlazor.Extras.FileManagers;

public class FileManagerOptions
{
    private readonly IServiceProvider ServiceProvider;

    public List<FsOpenOperationBase> OpenOperations { get; } = [];
    public List<FsSingleOperationBase> SingleOperations { get; } = [];
    public List<FsMultiOperationBase> MultiOperations { get; } = [];
    public List<FsToolbarOperationBase> ToolbarOperations { get; } = [];

    internal FileManagerOptions(IServiceProvider serviceProvider)
    {
        ServiceProvider = serviceProvider;
    }

    public void AddOpenOperation<T>() where T : FsOpenOperationBase
        => OpenOperations.Add(ServiceProvider.GetRequiredService<T>());
    
    public void AddSingleOperation<T>() where T : FsSingleOperationBase
        => SingleOperations.Add(ServiceProvider.GetRequiredService<T>());
    
    public void AddMultiOperation<T>() where T : FsMultiOperationBase
        => MultiOperations.Add(ServiceProvider.GetRequiredService<T>());
    
    public void AddToolbarOperation<T>() where T : FsToolbarOperationBase
        => ToolbarOperations.Add(ServiceProvider.GetRequiredService<T>());
}