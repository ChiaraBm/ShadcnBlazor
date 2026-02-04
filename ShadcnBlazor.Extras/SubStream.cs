namespace ShadcnBlazor.Extras;

// Source: https://github.com/ChiaraBm/MoonCore/blob/main/Library/MoonCore/Common/SubStream.cs

/// <summary>
/// Allows you to access to part of a stream as a separate stream. Useful for chunked uploading from a stream
/// where every chunk you upload is just a SubStream instance. Async and sync operations are supported. Write
/// operations are not supported
/// </summary>
internal class SubStream : Stream
{
    private readonly Stream InnerStream;
    private readonly long InnerLenght;
    private long InnerPosition;

    /// <summary>
    /// Creates a sub-stream that limits read access to a specified number of bytes.
    /// </summary>
    /// <param name="innerStream">The underlying stream to read from.</param>
    /// <param name="innerLenght">The maximum number of bytes that can be read from this sub-stream.</param>
    public SubStream(Stream innerStream, long innerLenght)
    {
        InnerStream = innerStream;
        InnerLenght = innerLenght;
        InnerPosition = 0;
    }

    /// <inheritdoc />
    public override bool CanRead => InnerStream.CanRead;

    /// <inheritdoc />
    public override bool CanSeek => InnerStream.CanSeek;

    /// <inheritdoc />
    public override bool CanWrite => false;

    /// <inheritdoc />
    public override long Length => InnerLenght;

    /// <inheritdoc />
    public override long Position
    {
        get => InnerPosition;
        set
        {
            if (value < 0 || value > InnerLenght)
                throw new ArgumentException("Position must be between 0 and Length.", nameof(value));
            
            InnerPosition = value;
        }
    }

    /// <inheritdoc />
    public override void Flush()
    {
        throw new NotSupportedException();
    }

    /// <inheritdoc />
    public override int Read(byte[] buffer, int offset, int count)
    {
        if (buffer == null)
            throw new ArgumentNullException(nameof(buffer));
        
        if (offset < 0 || count < 0 || offset + count > buffer.Length)
            throw new ArgumentException("Offset and count are invalid for the buffer.");

        // Limit the read to remaining bytes in the sub-stream
        var remainingBytes = InnerLenght - InnerPosition;
        if (remainingBytes <= 0)
            return 0;

        var bytesToRead = (int)Math.Min(count, remainingBytes);

        var bytesRead = InnerStream.Read(buffer, offset, bytesToRead);
        InnerPosition += bytesRead;

        return bytesRead;
    }

    /// <inheritdoc />
    public override async Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
    {
        if (buffer == null)
            throw new ArgumentNullException(nameof(buffer));
        
        if (offset < 0 || count < 0 || offset + count > buffer.Length)
            throw new ArgumentException("Offset and count are invalid for the buffer.");

        // Limit the read to remaining bytes in the sub-stream
        var remainingBytes = InnerLenght - InnerPosition;
        if (remainingBytes <= 0)
            return 0;

        var bytesToRead = (int)Math.Min(count, remainingBytes);

        var bytesRead = await InnerStream.ReadAsync(buffer, offset, bytesToRead, cancellationToken);
        InnerPosition += bytesRead;

        return bytesRead;
    }

    /// <inheritdoc />
    public override long Seek(long offset, SeekOrigin origin)
    {
        var newPosition = origin switch
        {
            SeekOrigin.Begin => offset,
            SeekOrigin.Current => InnerPosition + offset,
            SeekOrigin.End => InnerLenght + offset,
            _ => throw new ArgumentException("Invalid seek origin.", nameof(origin))
        };

        if (newPosition < 0 || newPosition > InnerLenght)
            throw new IOException("Seek would position outside the sub-stream bounds.");

        InnerPosition = newPosition;
        return InnerPosition;
    }

    /// <inheritdoc />
    public override void SetLength(long value)
    {
        throw new NotSupportedException("SubStream is read-only.");
    }

    /// <inheritdoc />
    public override void Write(byte[] buffer, int offset, int count)
    {
        throw new NotSupportedException("SubStream is read-only.");
    }
}