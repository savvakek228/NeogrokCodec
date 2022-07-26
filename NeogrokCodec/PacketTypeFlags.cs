namespace NeogrokCodec;

[Flags]
public enum PacketTypeFlags
{
    Compressed = 1 << 0,  // The packet is compressed using the ZStandard algorithm
    Short      = 1 << 1,  // Length has 1 byte length
    CShort     = 1 << 2,  // Client id has 1 byte length
}