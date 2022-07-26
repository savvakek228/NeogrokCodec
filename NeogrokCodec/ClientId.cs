namespace NeogrokCodec;

public record ClientId(int Id)
{
    public bool IsShort
    {
        get => Id <= 0xFF;
    }
}