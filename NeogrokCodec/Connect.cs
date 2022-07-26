namespace NeogrokCodec;

public record Connect(ClientId ClientId) : IFrame
{
    public FrameType FrameType
    {
        get => FrameType.Connect;
    }
}