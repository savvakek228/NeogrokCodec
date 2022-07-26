namespace NeogrokCodec;

public enum FrameType
{
    Ping,
    Sync,
    
    UpdateRights,
    
    Server,
    Authorize,  // unimplemented, sent only by server
    Connect,
    Forward,
    Disconnect,
    Error,
}