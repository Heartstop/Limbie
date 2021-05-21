using MoonSharp.Interpreter;

namespace Limbie.Control.Shared.Out
{
    [MoonSharpUserData]
    public class Limbs
    {
        public Limb facingLimb { get; }
        public Limb outerFacingLimb { get; }
        public Limb awayLimb { get; }
        public Limb outerAwayLimb { get; }
    }
}
