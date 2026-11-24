---
title: File Manager
description: A file manager UI with breadcrumbs, selection checkboxes, and configurable operations for managing files and folders.
tags: [file-manager, ifaccess, operations, upload-download, archive]
---

# File Manager

The FileManagers folder provides `FileManager`, a complete file browser for Blazor: a breadcrumb navigation bar, a table of `FsEntry` rows with selection checkboxes, a per-row dropdown menu, a right-click context menu, and a toolbar that adapts to the current selection. Use it when your app needs a UI for listing, creating, renaming, moving, deleting, uploading, downloading, or archiving files on any storage backend. The component is storage-agnostic — you implement `IFsAccess` — and every feature is an optional, configurable operation (single, multi, toolbar, or open-window) registered through `FileManagerOptions` via the `OnConfigure` callback. Files with supported text extensions open in the built-in CodeMirror `EditorWindow`; uploads over `Options.WriteLimit` are split into chunks and reassembled with `ICombineAccess`, and dropping files directly onto the grid triggers the `UploadOperation`.

## Usage

```razor
@using ShadcnBlazor.Extras.FileManagers
@using ShadcnBlazor.Extras.FileManagers.Abstractions
@using ShadcnBlazor.Extras.FileManagers.Operations

<FileManager ClassName="bg-card" FsAccess="Access" OnConfigure="OnConfigure" />

@code
{
    private IFsAccess Access;   // any implementation of IFsAccess

    private void OnConfigure(FileManagerOptions options)
    {
        options.AddToolbarOperation<NewFileOperation>();
        options.AddToolbarOperation<UploadOperation>();
        options.AddSingleOperation<RenameOperation>();
        options.AddMultiOperation<DeleteOperation>();
    }
}
```

## Parameters

### FileManager

| Name | Type | Default | Description |
| --- | --- | --- | --- |
| `FsAccess` | `IFsAccess` | — (required) | The storage backend used for all file operations (`ListAsync`, `ReadAsync`, `WriteAsync`, `MoveAsync`, `DeleteAsync`, `CreateFileAsync`, `CreateDirectoryAsync`). Operations that are incompatible with the given access are filtered out automatically via `CheckCompatability`. |
| `OnConfigure` | `Action<FileManagerOptions>?` | `null` | Callback invoked on initialization with the freshly created `FileManagerOptions`. Use it to register operations (`AddOpenOperation<T>()`, `AddSingleOperation<T>()`, `AddMultiOperation<T>()`, `AddToolbarOperation<T>()`) and to set `WriteLimit` / `UploadLimit` (both default to `-1`, i.e. unlimited). |
| `DefaultPath` | `string` | `null` | The initial folder to open. When empty or `null`, the file manager starts at `/`. |
| `ClassName` | `string` | `null` | Inherited from `ShadcnBase`. Extra CSS classes, merged with `overflow-hidden rounded-md border` and applied to the outer container (which also hosts drag-and-drop upload). |
| `Props` | `Dictionary<string, object>?` | `null` | Inherited from `ShadcnBase`. Unmatched attributes captured from the component tag; spread onto the inner `Table`. |

In addition to the parameters, `FileManager` implements `IFileManager` and exposes public instance members: `Options` (the configured `FileManagerOptions`), `CurrentPath`, `RefreshAsync(bool silent = false)`, `CloseOpenWindowAsync()`, and `ClearSelectionAsync()`.

Built-in operations in `ShadcnBlazor.Extras.FileManagers.Operations`: `NewDirectoryOperation` and `NewFileOperation` (toolbar), `UploadOperation` (toolbar, also handles drop-onto-grid uploads), `RenameOperation` and `UnarchiveOperation` (single, the latter filtered to archive extensions reported by `IArchiveAccess`), `DeleteOperation`, `DownloadOperation` (requires `IDownloadFileAccess` or `IDownloadFolderAccess`), `MoveOperation`, and `ArchiveOperation` (requires `IArchiveAccess`) (multi), and `EditorOpenOperation` (open-window).

### Dialogs and open windows

`ArchiveDialog` (launched by `ArchiveOperation`)

| Name | Type | Default | Description |
| --- | --- | --- | --- |
| `FsAccess` | `IArchiveAccess` | — (required) | The archive-capable storage backend. `ArchiveFormats` is used to populate the format `Select`. |
| `FileManager` | `IFileManager` | — (required) | The owning file manager; `RefreshAsync()` is called after a successful archive. |
| `WorkingDirectory` | `string` | — (required) | The folder the archive is created in. |
| `FsEntries` | `FsEntry[]` | — (required) | The entries to be archived. |

`LocationSelectDialog` (launched by `MoveOperation` and `UnarchiveOperation`)

| Name | Type | Default | Description |
| --- | --- | --- | --- |
| `DefaultPath` | `string?` | `null` | Initial folder to display; `null` starts at `/`. |
| `Title` | `string` | — (required) | Dialog title. |
| `Description` | `string` | — (required) | Dialog description. |
| `Filter` | `Func<string, FsEntry, bool>?` | `null` | Returns `false` for rows that should be dimmed and unclickable (e.g. the original location of moved entries). |
| `OnSubmit` | `Func<string, Task>` | — (required) | Invoked with the selected path when the user confirms. |
| `FsAccess` | `IFsAccess` | — (required) | The storage backend listing the folder contents. |

`NewDirectoryDialog` and `NewFileDialog` (launched by `NewDirectoryOperation` and `NewFileOperation`)

| Name | Type | Default | Description |
| --- | --- | --- | --- |
| `FsAccess` | `IFsAccess` | — (required) | The storage backend; calls `CreateDirectoryAsync` / `CreateFileAsync`. |
| `FileManager` | `IFileManager` | — (required) | The owning file manager; `RefreshAsync()` is called after creation. |
| `WorkingDirectory` | `string` | — (required) | The folder the new directory/file is created in. |

`RenameDialog` (launched by `RenameOperation`)

| Name | Type | Default | Description |
| --- | --- | --- | --- |
| `FsAccess` | `IFsAccess` | — (required) | The storage backend; renames via `MoveAsync`. |
| `FileManager` | `IFileManager` | — (required) | The owning file manager; `RefreshAsync()` is called after the rename. |
| `WorkingDirectory` | `string` | — (required) | The folder the entry lives in. |
| `FsEntry` | `FsEntry` | — (required) | The entry to rename; its `Name` pre-fills the form. |

`UploadDialog` (launched by `UploadOperation`)

| Name | Type | Default | Description |
| --- | --- | --- | --- |
| `FsAccess` | `IFsAccess` | — (required) | The storage backend receiving uploaded files. |
| `FileManager` | `IFileManager` | — (required) | The owning file manager; `RefreshAsync()` is called after upload, and its `Options.WriteLimit` / `Options.UploadLimit` are enforced. |
| `WorkingDirectory` | `string` | — (required) | The folder files are uploaded into. |
| `WriteLimit` | `long` | `-1` | Declared but the dialog reads `FileManager.Options.WriteLimit`; when `FsAccess` is not an `ICombineAccess`, values over the limit are rejected. |
| `UploadLimit` | `long` | `-1` | Declared but the dialog reads `FileManager.Options.UploadLimit`; larger files are rejected with an error toast. |

`EditorWindow` (rendered instead of the file table by `EditorOpenOperation`)

| Name | Type | Default | Description |
| --- | --- | --- | --- |
| `FsAccess` | `IFsAccess` | — (required) | The storage backend; reads the file via `ReadAsync` and saves via `WriteAsync`. |
| `FileManager` | `IFileManager` | — (required) | The owning file manager; `CloseOpenWindowAsync()` returns to the file table (with an unsaved-changes confirmation). |
| `WorkingDirectory` | `string` | — (required) | The folder the file lives in. |
| `FsEntry` | `FsEntry` | — (required) | The file to open; shown in the header card with its formatted size. |

## Examples

### Full operation set with upload and write limits

Adapted from the test showcase (`FileManagerSc1.razor`); `InMemoryFsAccess` there is the demo implementation of `IFsAccess` (including `IArchiveAccess` and `ICombineAccess`):

```razor
@using ShadcnBlazor.Extras.FileManagers
@using ShadcnBlazor.Extras.FileManagers.Abstractions
@using ShadcnBlazor.Extras.FileManagers.Operations
@using ShadcnBlazor.Test.ExtraShowcases.Services

@inject IJSRuntime JsRuntime

<FileManager ClassName="bg-card" FsAccess="Access" OnConfigure="OnConfigure" />

@code
{
    private IFsAccess Access;

    protected override void OnInitialized()
    {
        Access = new InMemoryFsAccess(JsRuntime);
    }

    private void OnConfigure(FileManagerOptions options)
    {
        options.AddToolbarOperation<NewDirectoryOperation>();
        options.AddToolbarOperation<NewFileOperation>();
        options.AddToolbarOperation<UploadOperation>();
        options.AddSingleOperation<RenameOperation>();
        options.AddSingleOperation<UnarchiveOperation>();
        options.AddMultiOperation<DeleteOperation>();
        options.AddMultiOperation<DownloadOperation>();
        options.AddMultiOperation<MoveOperation>();
        options.AddMultiOperation<ArchiveOperation>();
        options.AddOpenOperation<EditorOpenOperation>();

        options.WriteLimit = 1024 * 1024 * 20;
        options.UploadLimit = 1024 * 1024 * 1024;
    }
}
```

### Minimal file manager with a default path

Only the essentials registered; the file manager opens at the given `DefaultPath`:

```razor
@using ShadcnBlazor.Extras.FileManagers
@using ShadcnBlazor.Extras.FileManagers.Abstractions
@using ShadcnBlazor.Extras.FileManagers.Operations

<FileManager FsAccess="Access"
             DefaultPath="/projects"
             OnConfigure="OnConfigure" />

@code
{
    private IFsAccess Access;   // your IFsAccess implementation

    private void OnConfigure(FileManagerOptions options)
    {
        options.AddSingleOperation<RenameOperation>();
        options.AddMultiOperation<MoveOperation>();
        options.AddMultiOperation<DeleteOperation>();
    }
}
```

### Programmatic control: refresh, clear selection, close an open window

`FileManager` implements `IFileManager`, so an `@ref` gives access to `RefreshAsync`, `ClearSelectionAsync`, and `CloseOpenWindowAsync`:

```razor
@using ShadcnBlazor.Buttons
@using ShadcnBlazor.Extras.FileManagers
@using ShadcnBlazor.Extras.FileManagers.Abstractions
@using ShadcnBlazor.Extras.FileManagers.Operations

<FileManager @ref="FileManagerRef"
             ClassName="bg-card"
             FsAccess="Access"
             OnConfigure="OnConfigure" />

<div class="flex gap-2">
    <Button OnClick="() => FileManagerRef.RefreshAsync()">Refresh</Button>
    <Button OnClick="() => FileManagerRef.ClearSelectionAsync()">Clear selection</Button>
    <Button OnClick="() => FileManagerRef.CloseOpenWindowAsync()">Back to files</Button>
</div>

@code
{
    private FileManager? FileManagerRef;
    private IFsAccess Access;   // your IFsAccess implementation

    private void OnConfigure(FileManagerOptions options)
    {
        options.AddOpenOperation<EditorOpenOperation>();   // open text files in the EditorWindow
        options.AddMultiOperation<DeleteOperation>();
    }
}
```

### Custom single operation registered from the same razor file

Subclass `FsSingleOperationBase` (filter, content, order, execute, compatibility) and register it with `AddSingleOperation<T>` — the pattern used by every built-in operation:

```razor
@using ShadcnBlazor.Extras.FileManagers
@using ShadcnBlazor.Extras.FileManagers.Abstractions
@using ShadcnBlazor.Extras.FileManagers.Operations
@using ShadcnBlazor.Extras.Toasts

@inject ToastService ToastService

<FileManager ClassName="bg-card" FsAccess="Access" OnConfigure="OnConfigure" />

@code
{
    private IFsAccess Access;   // your IFsAccess implementation

    private void OnConfigure(FileManagerOptions options)
    {
        options.AddSingleOperation<FileCopyOperation>();
        options.AddMultiOperation<DeleteOperation>();
    }

    private class FileCopyOperation : FsSingleOperationBase
    {
        public override Func<FsEntry, bool>? Filter { get; } =
            entry => entry.Type == FsEntryType.File;

        public override RenderFragment Content { get; } = builder =>
        {
            builder.AddContent(0, "Copy");
        };

        public override int Order => 5;

        public override async Task ExecuteAsync(
            string workingDirectory,
            FsEntry fsEntry,
            IFsAccess fsAccess,
            IFileManager fileManager
        )
        {
            var source = UnixPath.Combine(workingDirectory, fsEntry.Name);
            var target = UnixPath.Combine(workingDirectory, $"{fsEntry.Name}-copy");

            await using var stream = await fsAccess.ReadAsync(source);
            await fsAccess.WriteAsync(target, stream);

            await ToastService.SuccessAsync("Copy file", $"{fsEntry.Name} was copied.");
        }

        public override bool CheckCompatability(IFsAccess fsAccess) => true;
    }
}
```
