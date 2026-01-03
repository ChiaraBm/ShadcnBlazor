FROM cgr.dev/chainguard/nginx AS base

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

## Setup environment:

# Install nodejs and npm for building tailwind styles
RUN apt update; apt install nodejs npm -y

## Download depenencies:

# Install nuget packages
ARG BUILD_CONFIGURATION=Release

COPY ["ShadcnBlazor.Test/ShadcnBlazor.Test.csproj", "/src/ShadcnBlazor.Test/"]
COPY ["ShadcnBlazor.Extras/ShadcnBlazor.Extras.csproj", "/src/ShadcnBlazor.Extras/"]
COPY ["ShadcnBlazor/ShadcnBlazor.csproj", "/src/ShadcnBlazor/"]
WORKDIR /src/ShadcnBlazor.Test
RUN dotnet restore "ShadcnBlazor.Test.csproj"

# Install npm packages
COPY ["ShadcnBlazor.Test/Styles/package.json", "/src/ShadcnBlazor.Test/Styles/"]
WORKDIR "/src/ShadcnBlazor.Test/Styles"
RUN npm i

COPY ["ShadcnBlazor.Extras/CodeMirror/package.json", "/src/ShadcnBlazor.Extras/CodeMirror/"]
WORKDIR "/src/ShadcnBlazor.Extras/CodeMirror"
RUN npm i

# Copy over all source files
WORKDIR /src
COPY . .

# Build tailwind styles
WORKDIR "/src/ShadcnBlazor.Test/Styles"
RUN npm run tailwind-build

# Build code mirror bundle
WORKDIR "/src/ShadcnBlazor.Extras/CodeMirror"
RUN npm run build

# Build application
WORKDIR "/src/ShadcnBlazor.Test"
RUN dotnet build "./ShadcnBlazor.Test.csproj" -c $BUILD_CONFIGURATION -o /app/build


FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ShadcnBlazor.Test.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final

LABEL org.opencontainers.image.source=https://github.com/ChiaraBm/ShadcnBlazor

# Copy over compiled application
COPY --from=publish /app/publish/wwwroot/ /usr/share/nginx/html

# Copy single page app nginx config
COPY ShadcnBlazor.Test/Docker/nginx.conf /etc/nginx/nginx.conf
