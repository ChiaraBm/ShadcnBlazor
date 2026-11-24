FROM mcr.microsoft.com/dotnet/aspnet:10.0-noble-chiseled AS base

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build

## Download depenencies:

# Install nuget packages
ARG BUILD_CONFIGURATION=Release

COPY ["ShadcnBlazor.Mcp/ShadcnBlazor.Mcp.csproj", "/src/ShadcnBlazor.Mcp/"]
COPY ["ShadcnBlazor.Extras/ShadcnBlazor.Extras.csproj", "/src/ShadcnBlazor.Extras/"]
COPY ["ShadcnBlazor/ShadcnBlazor.csproj", "/src/ShadcnBlazor/"]

WORKDIR /src/ShadcnBlazor.Mcp
RUN dotnet restore "ShadcnBlazor.Mcp.csproj"

# Copy over all source files
WORKDIR /src
COPY . .

# Build application
WORKDIR "/src/ShadcnBlazor.Mcp"
RUN dotnet build "./ShadcnBlazor.Mcp.csproj" -c $BUILD_CONFIGURATION -o /app/build
RUN dotnet publish "./ShadcnBlazor.Mcp.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final

LABEL org.opencontainers.image.source=https://github.com/ChiaraBm/ShadcnBlazor

WORKDIR /app

# Copy over compiled application
COPY --from=build /app/publish/ /app

ENTRYPOINT ["dotnet", "ShadcnBlazor.Mcp.dll"]
