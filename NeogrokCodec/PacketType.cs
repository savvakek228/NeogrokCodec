namespace NeogrokCodec;

public record PacketType(FrameType Type, PacketTypeFlags Flags)
{
    public bool IsShort
    {
        get => (Flags & PacketTypeFlags.Short) == PacketTypeFlags.Short;
    }
    
    public bool IsCShort
    {
        get => (Flags & PacketTypeFlags.CShort) == PacketTypeFlags.CShort;
    }

    public bool IsCompressed
    {
        get => (Flags & PacketTypeFlags.Compressed) == PacketTypeFlags.Compressed;
    }
}