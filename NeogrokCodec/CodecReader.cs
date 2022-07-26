namespace NeogrokCodec;

using System.Net.Sockets;

public class CodecReader
{
    private NetworkStream _reader;

    public CodecReader(Socket sock)
    {
        _reader = new NetworkStream(sock, false);
    }
}