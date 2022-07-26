namespace NeogrokCodec.Types.Frames;

public record Auth(byte[] Magic) : IFrame
{
    public FrameType Type
    {
        get => FrameType.Auth;
    }
}