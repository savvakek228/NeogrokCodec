namespace NeogrokCodec;

public record ErrorFrame(Error Code) : IFrame
{
    public FrameType FrameType
    {
        get => FrameType.Error;
    }
};