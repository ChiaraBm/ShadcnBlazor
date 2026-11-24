---
title: Editor
description: CodeMirror-based code editor with syntax highlighting for 16 languages, exposed via IJSRuntime interop.
tags: [code-editor, codemirror, syntax-highlighting, jsinterop]
---

# Editor

The Editors folder provides `Editor`, a full code editor built on CodeMirror that is embedded in a Blazor component through `IJSRuntime` interop. On its first render it calls the `CodeMirrorAdapter.init` JavaScript function, which mounts a CodeMirror `EditorView` inside a `<div class="cm-wrapper">` element. Use it when you need an in-browser editor for source code, configuration, or other text with syntax highlighting instead of a plain `InputText`. Notable features: an `EditorLanguage` enum with 16 supported languages, `GetValueAsync()`/`SetValueAsync()` for reading and updating the document from .NET, and proper disposal of the JS instance via `IAsyncDisposable`.

The component requires the prebuilt CodeMirror interop script (the `CodeMirrorAdapter` global, shipped as `wwwroot/codemirror-bundle.js` in the `ShadcnBlazor.Extras` package) to be loaded on the page, e.g. `<script src="_content/ShadcnBlazor.Extras/codemirror-bundle.js"></script>`.

## Usage

```razor
@using ShadcnBlazor.Extras.Editors

<Editor InitialValue="Hello from CodeMirror!" />
```

## Parameters

### Editor

| Name | Type | Default | Description |
| --- | --- | --- | --- |
| `InitialValue` | `string` | `""` | Initial document content. Passed to `CodeMirrorAdapter.init` as the starting `doc` when the CodeMirror view is created on the first render. |
| `Language` | `EditorLanguage` | `EditorLanguage.None` | Syntax-highlighting language. When not `None`, the enum name is passed to the adapter lowercased (e.g. `csharp`). Enum values: `None`, `Javascript`, `Html`, `Json`, `Sql`, `Yaml`, `Php`, `Xml`, `Python`, `Css`, `Shell`, `Dockerfile`, `Go`, `Lua`, `Properties`, `Csharp`, `Nix`. |

In addition to the parameters, `Editor` exposes two public instance methods (not `[Parameter]` properties):

- `GetValueAsync()` — returns `Task<string>` with the current document content. Throws `NullReferenceException` if called before the editor instance has been initialized.
- `SetValueAsync(string value)` — returns `Task` and replaces the entire document content. Throws `NullReferenceException` if called before the editor instance has been initialized.

The component implements `IAsyncDisposable` and disposes the `DotNetObjectReference` and the CodeMirror `IJSObjectReference` on disposal.

## Examples

### Syntax highlighting and saving the content

```razor
@using ShadcnBlazor.Extras.Editors   @* Editor component and EditorLanguage enum *@

<Editor @ref="EditorRef"
        Language="EditorLanguage.Csharp"
        InitialValue="class Program\n{\n    static void Main() { }\n}\n" />

<Button OnClick="Save">Save</Button>

@code
{
    private Editor? EditorRef;

    private async Task Save()
    {
        var content = await EditorRef!.GetValueAsync();
        // Persist `content` wherever you need to.
    }
}
```

### Loading a template into the editor with SetValueAsync

```razor
@using ShadcnBlazor.Extras.Editors

<Editor @ref="EditorRef" Language="EditorLanguage.Python" InitialValue="print('start')" />

<Button OnClick="LoadTemplate">Load template</Button>

@code
{
    private Editor? EditorRef;

    private const string Template =
        "def fibonacci(n):\n" +
        "    a, b = 0, 1\n" +
        "    while a < n:\n" +
        "        print(a)\n" +
        "        a, b = b, a + b\n\n" +
        "fibonacci(10)\n";

    private async Task LoadTemplate()
    {
        await EditorRef!.SetValueAsync(Template);
    }
}
```

### YAML configuration editor with a value round-trip

```razor
@using ShadcnBlazor.Extras.Editors

<Editor @ref="EditorRef"
        Language="EditorLanguage.Yaml"
        InitialValue="services:\n  api:\n    port: 8080" />

<Button OnClick="Validate">Validate</Button>

<p>@LastValidation</p>

@code
{
    private Editor? EditorRef;
    private string LastValidation = "";

    private async Task Validate()
    {
        var yaml = await EditorRef!.GetValueAsync();
        LastValidation = string.IsNullOrWhiteSpace(yaml) ? "Empty document." : "Document is not empty.";
    }
}
```

### SQL editor that resets to the initial script

```razor
@using ShadcnBlazor.Extras.Editors

<Editor @ref="EditorRef"
        Language="EditorLanguage.Sql"
        InitialValue="SELECT id, name FROM users WHERE active = 1;" />

<Button OnClick="ResetEditor">Reset</Button>

@code
{
    private Editor? EditorRef;
    private const string InitialScript = "SELECT id, name FROM users WHERE active = 1;";

    private async Task ResetEditor()
    {
        await EditorRef!.SetValueAsync(InitialScript);
    }
}
```
