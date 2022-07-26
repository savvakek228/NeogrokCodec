namespace NeogrokCodec.Types.Frames;

public record SyncRequest() : IFrame
{
    public FrameType Type
    {
        get => FrameType.Sync;
    }
}