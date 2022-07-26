namespace NeogrokCodec.Types.Frames;

public record Error(ErrorCode Code) : IFrame
{
    public FrameType Type
    {
        get => FrameType.Error;
    }
}