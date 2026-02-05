window.ShadcnBlazorExtras = {
    initializeFileDropElement: function (element, callbackElement) {

        const uploadCallback = async function (path, file) {
            const streamRef = DotNet.createJSStreamReference(file);

            await callbackElement.invokeMethodAsync("OnUploadFile", path, streamRef);
        }

        const handleEntry = async function (entry, path = '') {
            if (entry.isFile) {
                const file = await new Promise((resolve, reject) => {
                    entry.file(resolve, reject);
                });

                const fullPath = path ? `${path}/${file.name}` : file.name;

                if (file.size === 0)
                    return;

                await uploadCallback(fullPath, file);
            } else if (entry.isDirectory) {
                const reader = entry.createReader();
                const entries = await new Promise((resolve, reject) => {
                    reader.readEntries(resolve, reject);
                });

                for (const childEntry of entries) {
                    const childPath = path ? `${path}/${entry.name}` : entry.name;
                    await handleEntry(childEntry, childPath);
                }
            }
        }

        const handleDragEvent = async function (dragEvent) {

            // Prevent default
            dragEvent.preventDefault();
            dragEvent.stopPropagation();

            const droppedItems = dragEvent.dataTransfer?.items || [];

            // First extract all webkit entries while DataTransfer is still accessible
            // cause when we do our async interop with blazor it will be gone.
            // Don't ask why... I don't know either

            const fileSystemEntries = [];
            const files = [];

            for (let i = 0; i < droppedItems.length; i++) {
                const droppedItem = droppedItems[i];

                if (droppedItem.kind !== 'file')
                    continue;

                const fileSystemEntry = droppedItem.webkitGetAsEntry();

                if (fileSystemEntry) {
                    fileSystemEntries.push(fileSystemEntry);
                } else {
                    // Fallback for browsers without webkitGetAsEntry support.
                    // I dunno who would use such an outdated browser but whatever
                    const file = droppedItem.getAsFile();

                    if (!file) {
                        console.log("Unable to get file or entry from item", droppedItem);
                        continue;
                    }

                    files.push(file);
                }
            }

            await callbackElement.invokeMethodAsync("OnUploadStarted");

            // Second process all collected file system entries
            for (const entry of fileSystemEntries) {
                await handleEntry(entry);
            }

            // Third process fallback files
            for (const file of files) {
                await uploadCallback(file.name, file);
            }
            
            await callbackElement.invokeMethodAsync("OnUploadCompleted");
        };

        const preventDefaults = (e) => {
            e.preventDefault();
        };

        ['dragenter', 'dragover', 'dragleave', 'drop'].forEach((eventName) => {
            element.addEventListener(eventName, preventDefaults, false);
        });

        element.addEventListener('drop', async (e) => {
            const items = e.dataTransfer.items;

            if (!items) return;

            await handleDragEvent(e);
        });
    },
    submitForm: function (id) {
        document.getElementById(id).requestSubmit();
    }
}