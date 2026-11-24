---
title: Breadcrumb
description: Accessible breadcrumb trail for hierarchical navigation with links, separators, an ellipsis and a highlighted current page.
tags: [breadcrumb, navigation, links, separator, ellipsis]
---

# Breadcrumb

A composable set of components for rendering an accessible breadcrumb trail that shows the current page's position in a hierarchy and lets the visitor jump back to any ancestor. Use it to orient users within deeply nested content such as file managers, documentation trees, or settings. The `<Breadcrumb>` wrapper renders a `<nav aria-label="Breadcrumbs">` containing a `<BreadcrumbList>` of `<BreadcrumbItem>`s; items hold either a navigable `<BreadcrumbLink>` (which can render a custom element through its `Slot` template), the non-clickable `<BreadcrumbPage>` marker for the current page, a `<BreadcrumbSeparator>` (defaulting to a chevron icon), or a `<BreadcrumbEllipsis>` placeholder for collapsed segments.

## Usage

```razor
@using ShadcnBlazor.Breadcrumbs

<Breadcrumb>
    <BreadcrumbList>
        <BreadcrumbItem>
            <BreadcrumbLink href="/">Home</BreadcrumbLink>
        </BreadcrumbItem>
        <BreadcrumbSeparator />
        <BreadcrumbItem>
            <BreadcrumbPage>Dashboard</BreadcrumbPage>
        </BreadcrumbItem>
    </BreadcrumbList>
</Breadcrumb>
```

## Parameters

| Name | Type | Default | Description |
|---|---|---|---|
| `ChildContent` (Breadcrumb) | `RenderFragment` | `null` | The inner content of the `<nav>` wrapper, normally a `<BreadcrumbList>`. |
| `ChildContent` (BreadcrumbList) | `RenderFragment` | `null` | The inner content of the `<ol>` list, normally `<BreadcrumbItem>` and `<BreadcrumbSeparator>` elements. |
| `ChildContent` (BreadcrumbItem) | `RenderFragment` | `null` | The inner content of the `<li>` item, normally a `<BreadcrumbLink>` or `<BreadcrumbPage>`. |
| `ChildContent` (BreadcrumbLink) | `RenderFragment` | `null` | The content displayed inside the rendered link element. |
| `Slot` (BreadcrumbLink) | `RenderFragment<Dictionary<string, object?>>?` | `null` | When set, this template is invoked with the computed attributes instead of rendering the default `<a>`; use it to render a different element (e.g. a `<button>`) and apply the attributes with `@attributes="context"`. |
| `ChildContent` (BreadcrumbPage) | `RenderFragment` | `null` | The content of the non-clickable current-page `<span>` (rendered with `aria-current="page"`). |
| `ChildContent` (BreadcrumbSeparator) | `RenderFragment?` | `null` | Optional custom separator content. When `null`, the component renders a `<ChevronRightIcon />` by default. |

Every component in this folder inherits from `ShadcnBase`, so the base parameters are also available on each one. `ClassName` (a `string`) is merged into the element's Tailwind classes, and `Props` (a `Dictionary<string, object>?` with `CaptureUnmatchedValues = true`) collects any unmatched attributes and spreads them onto the rendered element.

## Examples

Standard breadcrumb where each link passes an `href` through the inherited `Props` dictionary, and the whole trail is styled with `ClassName` on the wrapper:

```razor
@using ShadcnBlazor.Breadcrumbs

<Breadcrumb ClassName="w-fit rounded-lg border px-3 py-2">
    <BreadcrumbList>
        <BreadcrumbItem>
            <BreadcrumbLink href="/">Home</BreadcrumbLink>
        </BreadcrumbItem>
        <BreadcrumbSeparator />
        <BreadcrumbItem>
            <BreadcrumbLink href="/library">Library</BreadcrumbLink>
        </BreadcrumbItem>
        <BreadcrumbSeparator />
        <BreadcrumbItem>
            <BreadcrumbPage>Widgets</BreadcrumbPage>
        </BreadcrumbItem>
    </BreadcrumbList>
</Breadcrumb>
```

Breadcrumbs with Lucide icons placed next to the link text and the current page:

```razor
@using LucideBlazor
@using ShadcnBlazor.Breadcrumbs

<Breadcrumb>
    <BreadcrumbList>
        <BreadcrumbItem>
            <BreadcrumbLink href="/">
                <HomeIcon ClassName="size-4" />
            </BreadcrumbLink>
        </BreadcrumbItem>
        <BreadcrumbSeparator />
        <BreadcrumbItem>
            <BreadcrumbLink ClassName="flex items-center gap-1.5" href="/projects">
                <FolderIcon ClassName="size-4" />
                Projects
            </BreadcrumbLink>
        </BreadcrumbItem>
        <BreadcrumbSeparator />
        <BreadcrumbItem>
            <BreadcrumbPage ClassName="flex items-center gap-1.5">
                <FileTextIcon ClassName="size-4" />
                Overview
            </BreadcrumbPage>
        </BreadcrumbItem>
    </BreadcrumbList>
</Breadcrumb>
```

Slot-based links where the rendered element is a plain `<a>`, combined with a `<BreadcrumbEllipsis>` wrapped in a `<DropdownMenuTrigger>` and a `<BreadcrumbPage>` for the current item:

```razor
@using ShadcnBlazor.Breadcrumbs
@using ShadcnBlazor.Dropdowns
@using ShadcnBlazor.Interop

<Breadcrumb>
    <BreadcrumbList>
        <BreadcrumbItem>
            <BreadcrumbLink>
                <Slot>
                    <a href="/" @attributes="context">Home</a>
                </Slot>
            </BreadcrumbLink>
        </BreadcrumbItem>
        <BreadcrumbSeparator/>
        <BreadcrumbItem>
            <DropdownMenu>
                <DropdownMenuTrigger>
                    <Slot>
                        <button class="flex items-center gap-1 hover:cursor-pointer" @attributes="context">
                            <BreadcrumbEllipsis ClassName="size-4"/>
                        </button>
                    </Slot>
                </DropdownMenuTrigger>

                <DropdownMenuContent Align="PositionAlignment.Start">
                    <DropdownMenuItem>Documentation</DropdownMenuItem>
                    <DropdownMenuItem>Themes</DropdownMenuItem>
                    <DropdownMenuItem>GitHub</DropdownMenuItem>
                </DropdownMenuContent>
            </DropdownMenu>
        </BreadcrumbItem>
        <BreadcrumbSeparator/>
        <BreadcrumbItem>
            <BreadcrumbLink>
                <Slot>
                    <a href="/docs/components" @attributes="context">Components</a>
                </Slot>
            </BreadcrumbLink>
        </BreadcrumbItem>
        <BreadcrumbSeparator/>
        <BreadcrumbItem>
            <BreadcrumbPage>Breadcrumb</BreadcrumbPage>
        </BreadcrumbItem>
    </BreadcrumbList>
</Breadcrumb>
```

Programmatic trail built from a path: Slot-based links navigate via `@onclick` (no real `href`), the separator carries a custom `/` child, and the deepest segment is rendered as a `<BreadcrumbPage>`:

```razor
@using ShadcnBlazor.Breadcrumbs

<Breadcrumb>
    <BreadcrumbList>
        <BreadcrumbItem>
            <BreadcrumbLink>
                <Slot>
                    <a href="#" @onclick="@(() => NavigateAsync("/"))" @onclick:preventDefault
                       @attributes="context">
                        Home
                    </a>
                </Slot>
            </BreadcrumbLink>
        </BreadcrumbItem>

        <BreadcrumbSeparator>
            /
        </BreadcrumbSeparator>

        <BreadcrumbItem>
            <BreadcrumbLink>
                <Slot>
                    <a href="#" @onclick="@(() => NavigateAsync("/projects"))" @onclick:preventDefault
                       @attributes="context">
                        Projects
                    </a>
                </Slot>
            </BreadcrumbLink>
        </BreadcrumbItem>

        <BreadcrumbSeparator>
            /
        </BreadcrumbSeparator>

        <BreadcrumbItem>
            <BreadcrumbPage>Overview</BreadcrumbPage>
        </BreadcrumbItem>
    </BreadcrumbList>
</Breadcrumb>

@code
{
    private void NavigateAsync(string path)
    {
        // handle navigation to the given path
    }
}
```
