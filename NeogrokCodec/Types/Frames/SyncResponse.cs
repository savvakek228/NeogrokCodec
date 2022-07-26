using NeogrokCodec.Config;

namespace NeogrokCodec.Types.Frames;

public record SyncResponse(CompressionConfig Compression) : IFrame
{
    public FrameType Type
    {
        get => FrameType.Sync;
    }
}