namespace NeogrokCodec.Types.Frames;

public record Connect(ClientId Id) : IFrame
{
    public FrameType Type
    {
        get => FrameType.Connect;
    }
}