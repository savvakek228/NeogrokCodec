namespace NeogrokCodec;

public enum Error
{
    Unimplemented,          // This feature currently is not implemented
    AccessDenied,           // Access to this feature is denied (or wrong authorization)
    BindError,              // Server can't bind specified port
    UnknownPacket,          // Packet type is incorrect
    UnsupportedPacket,      // Packet is not support by Client/Server side
    
    TooLongBuffer,          // Too long buffer specified in the Packet frame
    DecompressionError,     // ZStd decompression error
    
    ServerIsAlreadyCreated, // Session already initialized
    InternalError,          // Error on server side
    
    StateError,             // Sent if client tries to access features without required steps before
    
    NoSuchClient,           // Client could not be found
                            // Usually this error is ok, because synchronization between client and server
                            // has some latency.
}