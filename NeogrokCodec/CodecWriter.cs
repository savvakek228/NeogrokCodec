namespace NeogrokCodec;

using System.Net.Sockets;

public class CodecWriter
{
    private NetworkStream _writer;

    public CodecWriter(Socket socket)
    {
        _writer = new NetworkStream(socket);
    }
    
    
}