using MoonSharp.Interpreter;

namespace Limbie.Control.Shared.Out
{
    [MoonSharpUserData]
    public class Limbs
    {
        public Limb facingLimb { get; set; } = new Limb();
        public Limb outerFacingLimb { get; set; } = new Limb();
        public Limb awayLimb { get; set; } = new Limb();
        public Limb outerAwayLimb { get; set; } = new Limb();
    }
}
