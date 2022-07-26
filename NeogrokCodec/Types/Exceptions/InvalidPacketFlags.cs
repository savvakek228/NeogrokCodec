namespace NeogrokCodec.Types.Exceptions;

/*
 * Throw this error is nearly impossible, because flags will be trimmed to 3 least significant bits
 */
public class InvalidPacketFlags : Exception
{
    public byte Got { get; }

    public InvalidPacketFlags(byte got)
        : base($"Packet flags is out of range: expected upper bound {(1 << 3) - 1}, got {got}")
    {
        Got = got;
    }
}