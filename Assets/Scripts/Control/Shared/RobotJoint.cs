using MoonSharp.Interpreter;
using UnityEngine;

namespace Limbie.Control.Shared
{
    [MoonSharpUserData]
    public class RobotJoint
    {
        public RobotJoint(HingeJoint2D hinge)
        {
            Angle = hinge.jointAngle;
            Speed = hinge.jointSpeed;
        }

        public float Angle { get; }

        public float Speed { get; }
    }
}
