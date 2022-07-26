using NeogrokCodec.Types.Exceptions;

namespace NeogrokCodec.Types;

public enum FrameType : byte
{
    Error           = 0,
    Ping            = 1,
    Auth            = 2,
    
    Connect         = 3,
    Forward         = 4,
    Disconnect      = 5,
    
    Sync            = 6,
    Server          = 7,
    
    UpdateRights    = 8,
}

public static class FrameTypeExt
{
    public static FrameType Parse(byte type)
    {
        if (type > 8)
        {
            throw new InvalidPacketType(type);
        }

        return (FrameType)type;
    }
}
