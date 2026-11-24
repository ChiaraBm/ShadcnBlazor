using ModelContextProtocol.Protocol;
using ShadcnBlazor.Mcp.Catalog;
using ShadcnBlazor.Mcp.Resources;
using ShadcnBlazor.Mcp.Tools;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHealthChecks();

builder.Services
        .AddMcpServer(options =>
        {
            options.ServerInfo = new Implementation
                    { Name = "shadcnblazor-mcp", Title = "ShadcnBlazor", Version = "1.0.0" };
            options.ServerInstructions =
                    "Use search_components to find ShadcnBlazor components, get_component_docs for their usage, " +
                    "parameters and files, and get_component_source to read their implementation. " +
                    "Components exist in two packages: ShadcnBlazor (core) and ShadcnBlazor.Extras (extras); " +
                    "when a name exists in both, pass the `package` parameter to pick one.";
        })
        .WithTools<ComponentTools>()
        .WithHttpTransport();

builder.Services.AddSingleton<EmbeddedResourceService>();
builder.Services.AddSingleton<ComponentCatalogService>();
builder.Services.AddTransient<ComponentTools>();

var app = builder.Build();

app.MapMcp("/mcp");
app.MapHealthChecks("/healthz");

app.Services.GetRequiredService<ComponentCatalogService>();

app.Run();