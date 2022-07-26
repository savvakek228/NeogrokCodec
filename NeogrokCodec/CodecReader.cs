using System.Text;

namespace NeogrokCodec;

using System.Net.Sockets;

public class CodecReader
{
    private NetworkStream _reader;

    public CodecReader(Socket sock)
    {
        _reader = new NetworkStream(sock);
    }

    public async Task<IFrame> ReadFrameAsync()
    {
        var data = await ReadByteAsync();
        var type = new PacketType((FrameType)(data >> 3), (PacketTypeFlags)(data & 0b111));
        switch (type.Type)
        {
            case FrameType.Error:
                return new ErrorFrame((Error)(await ReadByteAsync()));
            
            case FrameType.Authorize:
                throw new NotImplementedException();
            
            case FrameType.Forward:
                var clientId = await ReadClientIdAsync(type.Flags);
                var length = await ReadPacketLengthAsync(type.Flags);

                return new Forward(clientId, await ReadExactAsync(length));

            case FrameType.Server:
                return new Server("0.0.0.0", await ReadU16LeAsync());
            
            case FrameType.Ping:
                return new Ping(await ReadStringAsync());
            
            case FrameType.Connect:
                return new Connect(await ReadClientIdAsync(type.Flags));
            case FrameType.Disconnect:
                return new Disconnect(await ReadClientIdAsync(type.Flags));
            
            default:
                throw new NotImplementedException();
        }
        
        throw new NotImplementedException();
    }

    protected async Task<string> ReadStringAsync()
    {
        var length = await ReadByteAsync();
        var data = await ReadExactAsync(length);

        return Encoding.UTF8.GetString(data);
    }

    protected async Task<ClientId> ReadClientIdAsync(PacketTypeFlags flags)
    {
        return new ClientId(await ReadVariadicAsync(flags, PacketTypeFlags.CShort));
    }

    protected Task<int> ReadPacketLengthAsync(PacketTypeFlags flags)
    {
        return ReadVariadicAsync(flags, PacketTypeFlags.Short);
    }

    protected async Task<int> ReadVariadicAsync(PacketTypeFlags presentFlags, PacketTypeFlags neededFlags)
    {
        if ((presentFlags & neededFlags) == neededFlags)
        {
            return await ReadByteAsync();
        }

        return await ReadU16LeAsync();
    }

    protected async Task<int> ReadU16LeAsync()
    {
        var buffer = await ReadExactAsync(2);
        return (buffer[1] << 8) | buffer[0];
    }

    protected async Task<byte> ReadByteAsync()
    {
        return (await ReadExactAsync(1))[0];
    }

    protected async Task<byte[]> ReadExactAsync(int count)
    {
        var buffer = new byte[count];
        var read = 0;

        while (read < count)
        {
            var chunkRead = await _reader.ReadAsync(buffer, read, count - read);
            read += chunkRead;
        }

        return buffer;
    }
}