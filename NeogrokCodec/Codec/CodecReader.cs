using System.Net.Sockets;
using System.Text;
using NeogrokCodec.Config;
using NeogrokCodec.Types;
using NeogrokCodec.Types.Exceptions;
using NeogrokCodec.Types.Flags;

namespace NeogrokCodec.Codec;

public class CodecReader
{
    private Side Side { get; }
    private NetworkStream _reader;

    public CodecReader(Side side, NetworkStream reader)
    {
        Side = side;
        _reader = reader;
    }

    public async Task<IFrame> ReadFrameAsync()
    {
        var pktTypeData = await ReadByteAsync();
        var pktType = new PacketType(PacketFlagsExt.ParseFlags((byte)(pktTypeData & 0b111)), FrameTypeExt.Parse((byte)(pktTypeData >> 3)));

        switch (pktType.Type)
        {
            case FrameType.Server:
                switch (Side)
                {
                    case Side.Client:
                        return new ServerResponse("0.0.0.0", await ReadU16LeAsync());
                    case Side.Server:
                        if (pktType.Flags.HasFlag(PacketFlags.CShort))
                        {
                            return new ServerRequest(TransportProtocol.Tcp, await ReadU16LeAsync());
                        } else if (pktType.Flags.HasFlag(PacketFlags.Short))
                        {
                            return new ServerRequest(TransportProtocol.Tcp, 0);
                        } else if (pktType.Flags.HasFlag(PacketFlags.Compressed))
                        {
                            return new ServerRequest(TransportProtocolExt.Parse(await ReadByteAsync()), 0);
                        }
                        else
                        {
                            return new ServerRequest(TransportProtocolExt.Parse(await ReadByteAsync()),
                                await ReadU16LeAsync());
                        }
                }

                break;
            
            case FrameType.Sync:
                switch (Side)
                {
                    case Side.Client:
                        return await ReadSyncResponseAsync();
                    case Side.Server:
                        return new SyncRequest();
                }

                break;
            
            case FrameType.Forward:
                var clientId = await ReadClientIdAsync(pktType.Flags);
                var length = await ReadPacketLengthAsync(pktType.Flags);

                if (pktType.Flags.HasFlag(PacketFlags.Compressed))
                {
                    // TODO: Implement decompression
                    throw new NotImplementedException();
                }

                return new Forward(await ReadExactAsync(length), clientId);
            
            case FrameType.Auth:
                switch (Side)
                {
                    case Side.Client:
                        throw new UnsupportedType(Side.Client);
                    case Side.Server:
                        return new Auth(await ReadPrefixedBytesAsync());
                }

                break;
            
            case FrameType.Connect:
                switch (Side)
                {
                    case Side.Client:
                        return new Connect(await ReadClientIdAsync(pktType.Flags));
                    case Side.Server:
                        throw new UnsupportedType(Side.Server);
                }

                break;
            
            case FrameType.Disconnect:
                return new Disconnect(await ReadClientIdAsync(pktType.Flags));
            
            case FrameType.Error:
                return new Error(await ReadErrorCodeAsync());
            
            case FrameType.Ping:
                switch (Side)
                {
                    case Side.Client:
                        return new PingResponse(await ReadStringAsync());
                    case Side.Server:
                        return new PingRequest();
                }

                break;
            
            case FrameType.UpdateRights:
                switch (Side)
                {
                    case Side.Client:
                        return new UpdateRights(RightsFlagsExt.Parse(await ReadByteAsync()));
                    case Side.Server:
                        throw new UnsupportedType(Side.Client);
                }

                break;
        }
        
        throw new NotImplementedException();
    }

    private async Task<float> ReadFloat32Async()
    {
        var buffer = await ReadExactAsync(4);
        return BitConverter.ToSingle(buffer);
    }

    private async Task<SyncResponse> ReadSyncResponseAsync()
    {
        var level = await ReadByteAsync();
        var profit = await ReadFloat32Async();
        var threshold = await ReadU16LeAsync();

        return new SyncResponse(new CompressionConfig(threshold, profit / 100, level));
    }

    private async Task<ErrorCode> ReadErrorCodeAsync()
    {
        return ErrorCodeExt.Parse(await ReadByteAsync());
    }

    private async Task<ClientId> ReadClientIdAsync(PacketFlags flags)
        => new ClientId(await ReadVariadicAsync(flags, PacketFlags.CShort));

    private Task<ushort> ReadPacketLengthAsync(PacketFlags flags)
        => ReadVariadicAsync(flags, PacketFlags.Short);

    private async Task<string> ReadStringAsync()
    {
        var buffer = await ReadPrefixedBytesAsync();

        return Encoding.UTF8.GetString(buffer);
    }

    private async Task<byte[]> ReadPrefixedBytesAsync()
    {
        var length = await ReadByteAsync();
        return await ReadExactAsync(length);
    }
    
    private async Task<ushort> ReadVariadicAsync(PacketFlags flags, PacketFlags needed)
    {
        if ((flags & needed) == needed)
        {
            return await ReadByteAsync();
        }

        return await ReadU16LeAsync();
    }

    private async Task<ushort> ReadU16LeAsync()
    {
        var buffer = await ReadExactAsync(2);
        return (ushort)((buffer[1] << 8) | buffer[0]);
    }
    
    private async Task<byte> ReadByteAsync()
        => (await ReadExactAsync(1))[0];

    private async Task<byte[]> ReadExactAsync(int length)
    {
        var read = 0;
        var buffer = new byte[length];

        while (read < length)
        {
            var chunkRead = await _reader.ReadAsync(buffer, read, length - read);
            if (chunkRead == 0)
            {
                throw new InvalidEndOfStream();
            }
            
            read += chunkRead;
        }
        
        return buffer;
    }
}