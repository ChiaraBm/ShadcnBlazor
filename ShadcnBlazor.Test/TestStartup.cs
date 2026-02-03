using Microsoft.Extensions.DependencyInjection;
using ShadcnBlazor.Extras;

namespace ShadcnBlazor.Test;

public static class TestStartup
{
    public static void AddTest(this IServiceCollection services)
    {
        services.AddShadcnBlazor();
        services.AddShadcnBlazorExtras();
        services.AddFileManagerOperations();
    }
}