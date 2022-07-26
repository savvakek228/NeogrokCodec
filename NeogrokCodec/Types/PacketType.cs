using NeogrokCodec.Types.Flags;

namespace NeogrokCodec.Types;

public record PacketType(PacketFlags Flags, FrameType Type);