using MoonSharp.Interpreter;

namespace Limbie.Control.Shared.In
{
    [MoonSharpUserData]
    public class Limbs
    {
        public Limbs(RobotActor robotActor)
        {
            FacingLimb = new RobotJoint(robotActor.facingLimb);
            OuterFacingLimb = new RobotJoint(robotActor.outerFacingLimb);
            AwayLimb = new RobotJoint(robotActor.awayLimb);
            OuterAwayLimb = new RobotJoint(robotActor.outerAwayLimb);
        }

        public RobotJoint FacingLimb { get; }
        public RobotJoint OuterFacingLimb { get; }
        public RobotJoint AwayLimb { get; }
        public RobotJoint OuterAwayLimb { get; }
    }
}
