using NeogrokCodec.Types.Exceptions;

namespace NeogrokCodec.Types.Flags;

[Flags]
public enum RightsFlags : byte
{
    CanCreateHttp = 1 << 0,
    CanCreateTcp  = 1 << 1,
    CanCreateUdp  = 1 << 2,
    CanCreateSsh  = 1 << 3,
    
    CanSelectHttp = 1 << 4,
    CanSelectUdp  = 1 << 5,
    CanSelectTcp  = 1 << 6,
}

public static class RightsFlagsExt
{
    private const byte IntegerUpperBound = (1 << 7) - 1;

    public static RightsFlags Parse(byte flags)
    {
        if (flags > IntegerUpperBound)
        {
            throw new InvalidRightsFlags(flags);
        }

        return (RightsFlags)flags;
    }
    
    public static bool HasRightsTo(this RightsFlags self, RightsFlags flags)
        => (self & flags) == flags;
}
