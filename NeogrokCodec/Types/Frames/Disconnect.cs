namespace NeogrokCodec.Types.Frames;

public record Disconnect(ClientId Id) : IFrame
{
    public FrameType Type
    {
        get => FrameType.Disconnect;
    }
}