namespace NeogrokCodec.Types.Frames;

public record ServerResponse(string Host, ushort Port) : IFrame
{
    public FrameType Type
    {
        get => FrameType.Server;
    }
}