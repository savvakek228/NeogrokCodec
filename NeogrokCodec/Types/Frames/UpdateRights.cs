using NeogrokCodec.Types.Flags;

namespace NeogrokCodec.Types.Frames;

public record UpdateRights(RightsFlags Flags) : IFrame
{
    public FrameType Type
    {
        get => FrameType.UpdateRights;
    }
}