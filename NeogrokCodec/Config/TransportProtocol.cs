using NeogrokCodec.Types.Exceptions;

namespace NeogrokCodec.Config;

public enum TransportProtocol
{
    Udp  = 0,
    Http = 1,
    Tcp  = 2,
}

public static class TransportProtocolExt
{
    public static TransportProtocol Parse(byte data)
    {
        if (data > 2)
        {
            throw new InvalidTransportProtocol(data);
        }

        return (TransportProtocol)data;
    }
}
