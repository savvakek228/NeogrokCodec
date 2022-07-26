namespace NeogrokCodec.Types.Exceptions;

public class InvalidRightsFlags : Exception
{
    private byte Got { get; }

    public InvalidRightsFlags(byte got)
        : base($"Rights flags is out of range: expected upper bound {(1 << 7) - 1}, got {got} instead")
    {
        Got = got;
    }
}