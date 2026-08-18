<div align="center">
  <img alt="ShadcnBlazor Logo" src="https://raw.githubusercontent.com/ChiaraBm/ShadcnBlazor/refs/heads/main/Assets/logo.svg" width="120" />
  <h1>ShadcnBlazor</h1>
  <p><strong>A Blazor port of the popular <a href="https://ui.shadcn.com/">shadcn/ui</a> component library.</strong></p>
  <p>
    <a href="https://www.nuget.org/packages/ShadcnBlazor"><img alt="NuGet version: ShadcnBlazor" src="https://img.shields.io/nuget/v/ShadcnBlazor?label=ShadcnBlazor" /></a>
    <a href="https://www.nuget.org/packages/ShadcnBlazor.Extras"><img alt="NuGet version: ShadcnBlazor.Extras" src="https://img.shields.io/nuget/v/ShadcnBlazor.Extras?label=ShadcnBlazor.Extras" /></a>
    <img alt=".NET 9" src="https://img.shields.io/badge/.NET-9.0-512BD4?logo=dotnet&logoColor=white" />
    <img alt="License: MIT" src="https://img.shields.io/badge/license-MIT-blue" />
    <a href="https://shadcnblazor.chiarabm.dev"><img alt="Live demo" src="https://img.shields.io/badge/demo-live-0ea5e9" /></a>
  </p>
</div>

## What is ShadcnBlazor?

ShadcnBlazor brings the shadcn/ui component style to Blazor. You get the same component names, the same look and the same Tailwind-based theming, just written for .NET. Every component can be tried in the [live demo](https://shadcnblazor.chiarabm.dev).

The library ships as two NuGet packages:

- **`ShadcnBlazor`**: the core component set
- **`ShadcnBlazor.Extras`**: extra components that don't exist in shadcn/ui

Styling is Tailwind CSS v4, so theming, custom variants and dark mode work the way you'd expect from the original.

## Quick start

1. Install the package:

   ```bash
   dotnet add package ShadcnBlazor
   ```

2. Set up Tailwind in a `Styles` folder inside your project:

   ```bash
   mkdir Styles && cd Styles
   npm init -y
   npm install tailwindcss @tailwindcss/postcss postcss postcss-cli tw-animate-css
   ```

   and add a `postcss.config.mjs`:

   ```js
   export default {
     plugins: {
       "@tailwindcss/postcss": {},
     }
   }
   ```

3. Add a `styles.css` to `Styles/` that pulls in the base styles shipped with the package:

   ```css
   @import "tailwindcss";
   @import "tw-animate-css";

   @import "../bin/ShadcnBlazor/scrollbar.css";
   @import "../bin/ShadcnBlazor/default-theme.css";
   @source "../bin/ShadcnBlazor/ShadcnBlazor.map";

   @source "../**/*.razor";
   @source "../**/*.cs";
   @source "../**/*.html";

   @custom-variant dark (&:is(.dark *));

   @layer base {
       * {
           @apply border-border outline-ring/50;
       }
       body {
           @apply bg-background text-foreground;
       }
   }
   ```

4. Wire it up:

   ```html
   <!-- index.html -->
   <link rel="stylesheet" href="/style.min.css" />
   <script src="/_content/ShadcnBlazor/interop.js" defer></script>
   ```

   ```razor
   <!-- MainLayout.razor -->
   <PortalOutlet />
   ```

   ```csharp
   // Program.cs
   using ShadcnBlazor;

   builder.Services.AddShadcnBlazor();
   ```

5. Build your solution, then the styles and leave the watch running while you develop:

   ```bash
   npx postcss styles.css -o ../wwwroot/style.min.css --watch
   ```

The full step-by-step guide lives on the [installation page](https://shadcnblazor.chiarabm.dev/installation).

## Components

The core package covers most of the shadcn/ui set:

| Category | Components |
| --- | --- |
| Forms & inputs | Button, Button Group, Checkbox, Field, Input, Input Group, Input OTP, Label, Native Select, Radio Group, Select, Slider, Switch, Toggle, Toggle Group |
| Overlays & menus | Alert Dialog, Command, Context Menu, Dialog, Drawer, Dropdown Menu, Hover Card, Menubar, Popover, Sheet |
| Navigation | Breadcrumb, Pagination, Tabs |
| Data display | Table, Data Table |
| Feedback & status | Alert, Badge, Empty, Progress, Skeleton, Sonner, Spinner |
| Layout & misc | Accordion, Avatar, Calendar, Card, Carousel, Collapsible, Item, Kbd, Separator |

Every component has a working demo on the [components page](https://shadcnblazor.chiarabm.dev/components).

## Extras

`ShadcnBlazor.Extras` has no shadcn/ui equivalent. These components exist because Blazor apps needed them:

| Component | What it does |
| --- | --- |
| Combobox | Searchable select that loads items asynchronously based on the search term |
| Editor | CodeMirror-based code editor |
| File Dropzone | Drag & drop file uploads, including chunked uploads |
| File Manager | Full file browser with navigation, selection, context menu and toolbar |
| Form Handler | Enhanced `EditForm` with validation summary and a submit button |
| Input Tags | Tag input |
| Lazy Loader | Keeps a loading indicator up until your load callback finishes |
| Toast | Toast notifications with a few different designs |
| Worker Button | Button that runs an async handler and shows a spinner while it works |
| Crash Handler | Error boundary that renders a crash UI instead of blowing up the page |
| Alert / Dialog / Alert Dialog | Service-based launchers so you can show them from anywhere via an injected service |

Browse them on the [extras page](https://shadcnblazor.chiarabm.dev/extras).

## Roadmap

Currently in progress:

- Chart
- Navigation Menu
- Resizable
- Scroll Area

## License

ShadcnBlazor is released under the [MIT License](LICENSE.md).
