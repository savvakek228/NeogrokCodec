namespace NeogrokCodec.Types.Exceptions;

public class UnsupportedType : Exception
{
    private Side Side { get; }

    public UnsupportedType(Side side)
        : base("Unsupported packet type by current side settings")
    {
        Side = side;
    }
}