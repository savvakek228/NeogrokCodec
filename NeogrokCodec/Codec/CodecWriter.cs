using System.Net.Sockets;
using NeogrokCodec.Types;

namespace NeogrokCodec.Codec;

public class CodecWriter
{
    private Side Side { get; }
    private NetworkStream _writer;

    public CodecWriter(NetworkStream writer, Side side)
    {
        Side = side;
        _writer = writer;
    }
    
    
}