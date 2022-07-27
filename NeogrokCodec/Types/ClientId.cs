namespace NeogrokCodec.Types;

public record ClientId(ushort Id) {
    public bool IsShort { get => Id <= 0xFF; }
    public byte ShortId
    {
        get => (byte)Id;
    }
}