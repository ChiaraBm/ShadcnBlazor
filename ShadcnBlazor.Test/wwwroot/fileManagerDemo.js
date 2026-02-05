window.fileManagerDemo = {
    download: async function (fileName, streamRef) {
        // Get the response from the stream reference
        const response = await streamRef.stream();
        const reader = response.getReader();

        // Show file picker to choose save location
        const handle = await window.showSaveFilePicker({
            suggestedName: fileName
        });

        // Create a writable stream and wait for it to be ready
        const writableStream = await handle.createWritable();

        // Function to pump data from the reader to the writable stream
        const pump = async () => {
            const { done, value } = await reader.read();
            if (done) {
                await writableStream.close();
                console.log("done");
                return;
            }
            await writableStream.write(value);
            return pump(); // Call pump again for the next chunk
        };

        // Start pumping data
        await pump();
    }
}