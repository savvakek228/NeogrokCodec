using System.Net.Sockets;
using System.Text;
using NeogrokCodec.Config;
using NeogrokCodec.Types;
using NeogrokCodec.Types.Flags;

namespace NeogrokCodec.Codec;

public class CodecWriter
{
    private NetworkStream _writer;
    private ushort CompressionThreshold { get; }

    public CodecWriter(NetworkStream writer)
    {
        _writer = writer;
        CompressionThreshold = 64;
    }

    public CodecWriter(NetworkStream writer, ushort compressionThreshold)
    {
        _writer = writer;
        CompressionThreshold = compressionThreshold;
    }

    public async Task WriteForwardAsync(byte[] data, ClientId clientId)
    {
        PacketFlags flags = 0;
        var buffer = new byte[5];  // header buffer
        var offset = 1;
        
        if (data.Length >= CompressionThreshold)
        {
            // TODO: Add ZStd compression
        }
        if (clientId.IsShort)
        {
            buffer[offset] = clientId.ShortId;
            flags |= PacketFlags.CShort;

            offset += 1;
        }
        else
        {
            buffer[offset]     = (byte)((clientId.Id >> 0) & 0xff);
            buffer[offset + 1] = (byte)((clientId.Id >> 8) & 0xff);

            offset += 2;
        }

        if (data.Length <= 0xFF)
        {
            buffer[offset] = (byte)buffer.Length;
            flags |= PacketFlags.Short;

            offset += 1;
        }
        else
        {
            var length = buffer.Length;
            
            buffer[offset]     = (byte)((length >> 0) & 0xff);
            buffer[offset + 1] = (byte)((length >> 8) & 0xff);

            offset += 2;
        }

        buffer[0] = EncodeFrameType(FrameType.Forward, flags);
        await _writer.WriteAsync(buffer, 0, offset);
        await _writer.WriteAsync(data, 0, data.Length);
    }

    public async Task WriteAuthAsync(string magic)
    {
        var buffer = Encoding.UTF8.GetBytes(magic);
        var length = (byte)buffer.Length;

        await _writer.WriteAsync(new[]
        {
            EncodeFrameType(FrameType.Auth, 0),
            length,
        });
        await _writer.WriteAsync(buffer);
    }

    public async Task WriteServerResponseAsync(ushort port)
    {
        await _writer.WriteAsync(new[]
        {
            EncodeFrameType(FrameType.Server, 0),
            (byte)((port >> 0) & 0xff),
            (byte)((port >> 8) & 0xff)
        });
    }

    public async Task WriteServerRequestAsync(ushort port)
    {
        if (port == 0)
        {
            await WriteByteAsync(EncodeFrameType(FrameType.Server, PacketFlags.Short));
        }
        else
        {
            await _writer.WriteAsync(new[]
            {
                EncodeFrameType(FrameType.Server, PacketFlags.CShort),
                (byte)((port >> 0) & 0xff),
                (byte)((port >> 8) & 0xff)
            });
        }
    }

    public async Task WriteErrorAsync(ErrorCode code)
    {
        await _writer.WriteAsync(new[]
        {
            EncodeFrameType(FrameType.Error, 0),
            (byte)code,
        });
    }

    public async Task WriteSyncResponseAsync(CompressionConfig config)
    {
        var single = BitConverter.SingleToInt32Bits(config.FractionalProfit * 100);
        await _writer.WriteAsync(new[]
        {
            EncodeFrameType(FrameType.Connect, 0),
            config.Level,
            
            (byte)((single >> 0 ) & 0xff),
            (byte)((single >> 8 ) & 0xff),
            (byte)((single >> 16) & 0xff),
            (byte)((single >> 24) & 0xff),
            
            (byte)((config.Threshold >> 0) & 0xff),
            (byte)((config.Threshold >> 8) & 0xff)
        });
    }

    public Task WriteSyncRequestAsync()
        => WriteByteAsync(EncodeFrameType(FrameType.Sync, 0));

    public async Task WritePingResponseAsync(string pong)
    {
        var buffer = Encoding.UTF8.GetBytes(pong);
        var length = (byte)buffer.Length;

        await _writer.WriteAsync(new[]
        {
            EncodeFrameType(FrameType.Ping, 0),
            length,
        });
        await _writer.WriteAsync(buffer);
    }

    public Task WritePingRequestAsync()
        => WriteByteAsync(EncodeFrameType(FrameType.Ping, 0));

    public Task WriteConnectAsync(ClientId clientId)
        => WriteVariadicPacketAsync(clientId.Id, FrameType.Connect);

    public Task WriteDisconnectAsync(ClientId clientId)
        => WriteVariadicPacketAsync(clientId.Id, FrameType.Disconnect);

    private async Task WriteVariadicPacketAsync(ushort value, FrameType frameType)
    {
        var isShort = value < 0xFF;
        var encoded = EncodeFrameType(frameType, isShort ? PacketFlags.Short : 0);

        var buffer = new byte[3];
        var length = 2;
        
        buffer[0] = encoded;
        buffer[1] = (byte)(value & 0xff);
        
        if (!isShort)
        {
            buffer[2] = (byte)(value >> 8);
            length = 3;
        }

        await _writer.WriteAsync(buffer, 0, length);
    }

    private async Task WriteByteAsync(byte data) => 
        await _writer.WriteAsync(new[] { data });
    
    private static byte EncodeFrameType(FrameType frameType, PacketFlags flags)
        => (byte)(((byte)frameType << 3) | (byte)flags);
}

