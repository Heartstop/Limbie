using MoonSharp.Interpreter;

namespace Limbie.Control.Shared
{
    [MoonSharpUserData]
    public class Limbs
    {
        public Limbs(RobotActor robotActor)
        {
            facingLimb = new RobotJoint(robotActor.facingLimb);
            outerFacingLimb = new RobotJoint(robotActor.outerFacingLimb);
            awayLimb = new RobotJoint(robotActor.awayLimb);
            outerAwayLimb = new RobotJoint(robotActor.outerAwayLimb);
        }

        public RobotJoint facingLimb { get; }
        public RobotJoint outerFacingLimb { get; }
        public RobotJoint awayLimb { get; }
        public RobotJoint outerAwayLimb { get; }
    }
}
