namespace NeogrokCodec.Types.Frames;

public record PingResponse(string ServerName) : IFrame
{
    public FrameType Type
    {
        get => FrameType.Ping;
    }
}