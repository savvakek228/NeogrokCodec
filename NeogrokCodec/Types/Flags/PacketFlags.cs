using NeogrokCodec.Types.Exceptions;

namespace NeogrokCodec.Types.Flags;

[Flags]
public enum PacketFlags : byte
{
    Compressed = 1 << 0,  // Payload is compressed using ZStd
    Short      = 1 << 1,  // Payload length has 1 byte length
    CShort     = 1 << 2,  // Client id has 1byte length
}

public static class PacketFlagsExt
{
    private const byte IntegerUpperBound = (1 << 3) - 1;
    
    public static PacketFlags ParseFlags(byte flags)
    {
        if (flags > IntegerUpperBound)
        {
            throw new InvalidPacketFlags(flags);
        }

        return (PacketFlags)flags;
    }
}
