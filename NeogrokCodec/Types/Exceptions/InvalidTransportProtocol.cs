namespace NeogrokCodec.Types.Exceptions;

public class InvalidTransportProtocol : Exception
{
    private byte Got { get; }

    public InvalidTransportProtocol(byte got)
        : base($"Invalid transport protocol representation: 0x{got:X}")
    {
        Got = got;
    }
}