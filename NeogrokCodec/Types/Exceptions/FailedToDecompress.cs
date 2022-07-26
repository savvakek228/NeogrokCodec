namespace NeogrokCodec.Types.Exceptions;

public class FailedToDecompress : Exception
{
    private bool FromServer { get; }

    public FailedToDecompress(bool fromServer)
        : base("Failed to decompress zstd buffer")
    {
        FromServer = fromServer;
    }
}