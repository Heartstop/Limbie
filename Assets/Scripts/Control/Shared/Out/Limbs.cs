using MoonSharp.Interpreter;

namespace Limbie.Control.Shared.Out
{
    [MoonSharpUserData]
    public class Limbs
    {
        public Limb Facing { get; set; } = new Limb();
        public Limb OuterFacing { get; set; } = new Limb();
        public Limb Away { get; set; } = new Limb();
        public Limb OuterAway { get; set; } = new Limb();
    }
}
