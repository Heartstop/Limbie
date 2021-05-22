using MoonSharp.Interpreter;

namespace Limbie.Control.Shared.In
{
    [MoonSharpUserData]
    public class Limbs
    {
        public Limbs(RobotActor robotActor)
        {
            Facing = new RobotJoint(robotActor.facingLimb);
            OuterFacing = new RobotJoint(robotActor.outerFacingLimb);
            Away = new RobotJoint(robotActor.awayLimb);
            OuterAway = new RobotJoint(robotActor.outerAwayLimb);
        }

        public RobotJoint Facing { get; }
        public RobotJoint OuterFacing { get; }
        public RobotJoint Away { get; }
        public RobotJoint OuterAway { get; }
    }
}
