namespace NeogrokCodec.Types.Exceptions;

public class InvalidPacketType : Exception
{
    private ushort Type { get; }

    public InvalidPacketType(ushort type)
        : base($"Invalid packet of type 0x{type:X}")
    {
        Type = type;
    }
}