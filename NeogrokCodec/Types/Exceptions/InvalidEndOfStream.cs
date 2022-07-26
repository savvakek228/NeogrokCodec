namespace NeogrokCodec.Types.Exceptions;

public class InvalidEndOfStream : Exception
{
    public InvalidEndOfStream()
        : base("Remote server has disconnected")
    {
        
    }
}