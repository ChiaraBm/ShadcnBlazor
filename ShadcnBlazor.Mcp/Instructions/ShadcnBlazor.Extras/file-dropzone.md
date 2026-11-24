---
title: File Dropzone
description: Drag-and-drop upload of files and folders to a Stream callback, with a live progress toast for each transferred file.
tags: [file-upload, drag-and-drop, dropzone, stream-upload, progress-toast]
---

# File Dropzone

A drag-and-drop upload surface from `ShadcnBlazor.Extras`. Wrap any content in a `<FileDropzone>` and it becomes a drop target: dropped files and folders are streamed to your `UploadCallback` one file at a time, and a hidden `<InputFile>` lets users click to browse as well. While a transfer runs, the component automatically opens an `UploadToast` (a Sonner-based progress toast) and reports per-file progress back to it through its public `UpdateAsync` method, finishing with a "files uploaded" info toast. Use it anywhere you need an upload control — file managers, dashboards, or a single upload dialog — and it handles recursive folder drops for you.

The component initializes its drop behavior by calling the `ShadcnBlazorExtras.initializeFileDropElement` JS function, so the host page must load the Extras `interop.js` script: `<script src="/_content/ShadcnBlazor.Extras/interop.js" defer></script>`. A `<ToastLauncher />` (from `ShadcnBlazor.Extras.Toasts`) must also be present in the UI for the progress and completion toasts to appear, and `ToastService` is registered as scoped by `AddShadcnBlazorExtras()`.

## Usage

```razor
@using ShadcnBlazor.Extras.FileDropzone
@using ShadcnBlazor.Extras.Toasts

<FileDropzone UploadCallback="OnUploadAsync">
    <div class="flex size-48 flex-col items-center justify-center rounded-xl border-2 border-dashed border-input text-muted-foreground">
        Drop files or folders here, or click to browse
    </div>
</FileDropzone>

<ToastLauncher />

@code
{
    private async Task OnUploadAsync(string path, Stream stream)
    {
        await using var memory = new MemoryStream();
        await stream.CopyToAsync(memory);
        // persist the file from `memory` (the full path identifies it)
    }
}
```

## Parameters

| Name | Type | Default | Description |
|---|---|---|---|
| `ChildContent` | `RenderFragment` | `null` | `FileDropzone`. The drop-zone content, rendered inside the outer `<label>`. That label doubles as the file-picker trigger, so clicking anywhere in `ChildContent` opens the hidden `<InputFile>` dialog. |
| `UploadCallback` | `Func<string, Stream, Task>` | `null` | `FileDropzone`. Invoked once per uploaded file with its path (folders dropped from the desktop are walked recursively, so nested files arrive with a full path) and an open read stream that must be consumed before the method returns. |
| `ClassName` | `string` | `null` | `FileDropzone` (inherited from `ShadcnBase`). Tailwind classes merged via `Cn(...)` and applied to the outer `<label>` element. |
| `Props` | `Dictionary<string, object>?` | `null` | `FileDropzone` (inherited from `ShadcnBase`, `CaptureUnmatchedValues = true`). Any unmatched attribute is applied to the `<label>` via `@attributes="Props"`, which is how `@ondragenter` / `@ondragleave` handlers can be attached to the drop surface. |
| `Callback` | `Func<UploadToast, Task>` | `null` | `UploadToast`. Invoked on the toast's first render with the toast instance. The toast stays open (launched with `hideDelay: -1`) until the callback completes, after which it closes itself. |
| `Model` | `ToastModel` | `null` | `UploadToast` (inherited from `ToastBase`). The toast model; `ToastBase.CloseAsync()` closes the toast through `ToastService.CloseAsync(Model)`. |

## Examples

A styled drop surface with drag-state feedback and a live progress toast, adapted from the showcase. The `@ondragenter` / `@ondragleave` handlers are captured by `Props` and forwarded to the `<label>`, and the upload callback reads the stream in 1 MiB chunks while calling `Dropzone.UpdateAsync` to drive the progress bar:

```razor
@using System.Buffers
@using LucideBlazor
@using ShadcnBlazor.Cards
@using ShadcnBlazor.Emptys
@using ShadcnBlazor.Extras.FileDropzone
@using ShadcnBlazor.Extras.Toasts

<FileDropzone @ref="Dropzone" UploadCallback="HandleUploadAsync"
              @ondragenter="OnDragEnterAsync" @ondragleave="OnDragLeaveAsync">
    <Card>
        <CardContent ClassName="items-center justify-center text-center">
            @if (IsDragging)
            {
                <Empty>
                    <EmptyHeader>
                        <EmptyMedia Variant="EmptyMediaVariant.Icon">
                            <PackageOpenIcon/>
                        </EmptyMedia>
                        <EmptyTitle>Drop to start upload</EmptyTitle>
                        <EmptyDescription>
                            Drop your files and folders to start uploading them
                        </EmptyDescription>
                    </EmptyHeader>
                </Empty>
            }
            else
            {
                <Empty>
                    <EmptyHeader>
                        <EmptyMedia Variant="EmptyMediaVariant.Icon">
                            <CloudUploadIcon/>
                        </EmptyMedia>
                        <EmptyTitle>Click to select files to upload</EmptyTitle>
                        <EmptyDescription>
                            Or drop them on here. Files and folders are supported
                        </EmptyDescription>
                    </EmptyHeader>
                </Empty>
            }
        </CardContent>
    </Card>
</FileDropzone>

<ToastLauncher />

@code
{
    private FileDropzone Dropzone;
    private bool IsDragging;

    private async Task HandleUploadAsync(string path, Stream stream)
    {
        if (IsDragging)
        {
            IsDragging = false;
            await InvokeAsync(StateHasChanged);
        }

        var name = Path.GetFileName(path);
        var buffer = ArrayPool<byte>.Shared.Rent(1024 * 1024);

        while (stream.Position != stream.Length)
        {
            _ = await stream.ReadAsync(buffer, 0, 1024 * 1024);

            await Dropzone.UpdateAsync(
                name,
                stream.Position,
                stream.Length
            );
        }

        ArrayPool<byte>.Shared.Return(buffer);
    }

    private async Task OnDragEnterAsync()
    {
        IsDragging = true;
        await InvokeAsync(StateHasChanged);
    }

    private async Task OnDragLeaveAsync()
    {
        IsDragging = false;
        await InvokeAsync(StateHasChanged);
    }
}
```

Writing uploads to disk, including the parent directories of dropped folders, adapted from the file-manager upload dialog. The destination is derived from the streamed path, and a size limit is enforced before the transfer begins:

```razor
@using System.Buffers
@using LucideBlazor
@using ShadcnBlazor.Cards
@using ShadcnBlazor.Emptys
@using ShadcnBlazor.Extras.FileDropzone
@using ShadcnBlazor.Extras.Toasts

@inject ToastService ToastService

<FileDropzone @ref="Dropzone" ClassName="p-3" UploadCallback="HandleUploadAsync"
              @ondragenter="OnDragEnterAsync" @ondragleave="OnDragLeaveAsync">
    <Card>
        <CardContent ClassName="items-center justify-center text-center">
            <Empty>
                <EmptyHeader>
                    <EmptyMedia Variant="EmptyMediaVariant.Icon">
                        <CloudUploadIcon/>
                    </EmptyMedia>
                    <EmptyTitle>Upload files and folders</EmptyTitle>
                    <EmptyDescription>Click to select, or drop directly on the card.</EmptyDescription>
                </EmptyHeader>
            </Empty>
        </CardContent>
    </Card>
</FileDropzone>

<ToastLauncher />

@code
{
    private FileDropzone Dropzone;
    private const long UploadLimit = 100 * 1024 * 1024; // 100 MiB

    private async Task HandleUploadAsync(string path, Stream stream)
    {
        if (stream.Length > UploadLimit)
        {
            await ToastService.ErrorAsync("Upload failed", $"{Path.GetFileName(path)} exceeds the size limit.");
            return;
        }

        var fileName = Path.GetFileName(path);
        var destination = Path.Combine(AppContext.BaseDirectory, "uploads", path);

        Directory.CreateDirectory(Path.GetDirectoryName(destination)!);

        await using var file = File.Create(destination);

        var buffer = ArrayPool<byte>.Shared.Rent(1024 * 1024);

        while (stream.Position < stream.Length)
        {
            _ = await stream.ReadAsync(buffer, 0, 1024 * 1024);
            await file.WriteAsync(buffer.AsMemory(0, 1024 * 1024));

            await Dropzone.UpdateAsync(
                fileName,
                stream.Position,
                stream.Length
            );
        }

        ArrayPool<byte>.Shared.Return(buffer);
    }

    private async Task OnDragEnterAsync() => await InvokeAsync(StateHasChanged);
    private async Task OnDragLeaveAsync() => await InvokeAsync(StateHasChanged);
}
```

A minimal in-memory upload that validates the size limit, streams the file into a `MemoryStream`, and reports completion or failure through the toast service:

```razor
@using ShadcnBlazor.Extras.FileDropzone
@using ShadcnBlazor.Extras.Toasts

@inject ToastService ToastService

<FileDropzone UploadCallback="OnUploadAsync">
    <div class="flex size-48 cursor-pointer flex-col items-center justify-center rounded-xl border-2 border-dashed border-input text-muted-foreground">
        Drop files here, or click to browse
    </div>
</FileDropzone>

<ToastLauncher />

@code
{
    private const long UploadLimit = 50 * 1024 * 1024; // 50 MiB

    private async Task OnUploadAsync(string path, Stream stream)
    {
        if (stream.Length > UploadLimit)
        {
            await ToastService.ErrorAsync("Upload failed", $"{Path.GetFileName(path)} is too large.");
            return;
        }

        await using var memory = new MemoryStream();
        await stream.CopyToAsync(memory);

        // `memory` now holds the file bytes; `path` identifies where it came from.
        await ToastService.SuccessAsync("Uploaded", Path.GetFileName(path));
    }
}
```
