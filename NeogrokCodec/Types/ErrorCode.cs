using NeogrokCodec.Types.Exceptions;

namespace NeogrokCodec.Types;

public enum ErrorCode : byte
{
    Unimplemented           = 0,
    AccessDenied            = 1,
    BindError               = 2,
    UnknownPkt              = 3,
    UnsupportedPkt          = 4,
    TooLongBuffer           = 5,
    DecompressErr           = 6,
    ServerIsAlreadyCreated  = 7,
    InternalError           = 8,
    StateError              = 9,
    NoSuchClient            = 10,
}

public static class ErrorCodeExt
{
    public static ErrorCode Parse(byte code)
    {
        if (code > 10)
        {
            throw new InvalidErrorCode(code);
        }

        return (ErrorCode)code;
    }
}
