namespace NeogrokCodec;

public record Server(string Host, int Port) : IFrame
{
    public FrameType FrameType
    {
        get => FrameType.Server;
    }
}