using MoonSharp.Interpreter;

namespace Limbie.Control.Shared.Out
{
    [MoonSharpUserData]
    public class Limbs
    {
        public Limb FacingLimb { get; set; } = new Limb();
        public Limb OuterFacingLimb { get; set; } = new Limb();
        public Limb AwayLimb { get; set; } = new Limb();
        public Limb OuterAwayLimb { get; set; } = new Limb();
    }
}
