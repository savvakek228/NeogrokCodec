namespace NeogrokCodec;

public record UpdateRights(int Flags) : IFrame
{
    public FrameType FrameType
    {
        get => FrameType.UpdateRights;
    }

    public bool CanCreateHttp
    {
        get => (Flags & (int)RightsFlags.CanCreateHttp) != 0;
    }
    
    public bool CanCreateTcp
    {
        get => (Flags & (int)RightsFlags.CanCreateTcp) != 0;
    }
    
    public bool CanCreateUdp
    {
        get => (Flags & (int)RightsFlags.CanCreateUdp) != 0;
    }
    
    public bool CanCreateSsh
    {
        get => (Flags & (int)RightsFlags.CanCreateSsh) != 0;
    }
}