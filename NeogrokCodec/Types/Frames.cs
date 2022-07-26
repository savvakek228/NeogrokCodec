using NeogrokCodec.Config;
using NeogrokCodec.Types;
using NeogrokCodec.Types.Flags;

namespace NeogrokCodec.Types;

public record Auth(byte[] Magic) : IFrame;
public record Connect(ClientId Id) : IFrame;
public record Disconnect(ClientId Id) : IFrame;
public record Error(ErrorCode Code) : IFrame;
public record Forward(byte[] Data, ClientId Destination) : IFrame;
public record PingRequest() : IFrame;
public record PingResponse(string ServerName) : IFrame;
public record ServerResponse(string Host, ushort Port) : IFrame;
public record SyncRequest() : IFrame;
public record SyncResponse(CompressionConfig Compression) : IFrame;
public record UpdateRights(RightsFlags Flags) : IFrame;

public record ServerRequest(TransportProtocol Protocol, ushort Port) : IFrame
{
    public bool IsAnyPort
    {
        get => Port == 0;
    }
}

