namespace NeogrokCodec.Types.Frames;

public record ServerRequest() : IFrame
{
    public FrameType Type
    {
        get => FrameType.Server;
    }
}