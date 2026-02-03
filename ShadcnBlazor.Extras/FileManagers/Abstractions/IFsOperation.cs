using Microsoft.AspNetCore.Components;

namespace ShadcnBlazor.Extras.FileManagers.Abstractions;

public interface IFsOperation
{
    public RenderFragment Content { get; }
    public int Order { get; }

    public bool CheckCompatability(IFsAccess fsAccess);
}