namespace NeogrokCodec;

public record Disconnect(ClientId ClientId) : IFrame
{
    public FrameType FrameType
    {
        get => FrameType.Disconnect;
    }
}