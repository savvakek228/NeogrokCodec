namespace NeogrokCodec.Types.Frames;

public record Forward(byte[] Data, ClientId Destination) : IFrame
{
    public FrameType Type
    {
        get => FrameType.Forward;
    }
}