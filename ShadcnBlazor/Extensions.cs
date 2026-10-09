using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using ShadcnBlazor.Interop;
using ShadcnBlazor.ScrollArea;
using TailwindMerge.Extensions;

namespace ShadcnBlazor;

public static class Extensions
{
    public static void AddShadcnBlazor(this IServiceCollection collection)
    {
        collection.TryAddScoped<InteropService>();
        collection.TryAddScoped<PositionService>();
        collection.TryAddScoped<ScrollBarInteropService>();
        collection.AddTailwindMerge();
    }
}