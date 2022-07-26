namespace NeogrokCodec.Types.Frames;

public record PingRequest() : IFrame
{
    public FrameType Type
    {
        get => FrameType.Ping;
    }
}