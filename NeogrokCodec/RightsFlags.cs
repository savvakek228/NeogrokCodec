namespace NeogrokCodec;

public enum RightsFlags
{
    // Create
    CanCreateHttp = 1 << 0,
    CanCreateTcp  = 1 << 1,
    CanCreateUdp  = 1 << 2,
    CanCreateSsh  = 1 << 3,
    
    // Select
    CanSelectHttp = 1 << 4,
    CanSelectUdp  = 1 << 5,
    CanSelectTcp  = 1 << 6,
}