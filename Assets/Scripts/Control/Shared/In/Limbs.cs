using MoonSharp.Interpreter;

namespace Limbie.Control.Shared.In
{
    [MoonSharpUserData]
    public class Limbs
    {
        public Limbs(RobotActor robotActor, bool mirrored)
        {
            Facing = new Limb(robotActor.facingLimb, mirrored);
            OuterFacing = new Limb(robotActor.outerFacingLimb, mirrored);
            Away = new Limb(robotActor.awayLimb, mirrored);
            OuterAway = new Limb(robotActor.outerAwayLimb, mirrored);
        }

        public Limb Facing { get; }
        public Limb OuterFacing { get; }
        public Limb Away { get; }
        public Limb OuterAway { get; }
    }
}
