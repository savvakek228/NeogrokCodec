namespace NeogrokCodec;

public record Forward(ClientId ClientId, byte[] Data) : IFrame
{
    public FrameType FrameType
    {
        get => FrameType.Forward;
    }
}