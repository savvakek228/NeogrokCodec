namespace NeogrokCodec;

public record Sync(int CompressionLevel, float CompressionProfit, int CompressionThreshold) : IFrame
{
    public FrameType FrameType
    {
        get => FrameType.Sync;
    }
};