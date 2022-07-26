namespace NeogrokCodec;

public enum FrameType
{
    Ping         = 1,
    Sync         = 6,
    
    UpdateRights = 8,
    
    Server       = 7,
    Authorize    = 2,  // unimplemented, sent only by server
    Connect      = 3,
    Forward      = 4,
    Disconnect   = 5,
    Error        = 0,
}