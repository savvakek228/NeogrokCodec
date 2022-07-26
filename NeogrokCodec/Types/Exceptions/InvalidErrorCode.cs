namespace NeogrokCodec.Types.Exceptions;

public class InvalidErrorCode : Exception
{
    private byte Got { get; }

    public InvalidErrorCode(byte got)
        : base($"Error code is out of range: expected upper bound 10, got {got}")
    {
        Got = got;
    }
}