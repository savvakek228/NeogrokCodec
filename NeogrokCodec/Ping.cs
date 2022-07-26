namespace NeogrokCodec;

public record Ping(string PingText) : IFrame
{
    public FrameType FrameType
    {
        get => FrameType.Ping;
    }
}