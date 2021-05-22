using MoonSharp.Interpreter;
using UnityEngine;

namespace Limbie.Control.Shared.In
{
    [MoonSharpUserData]
    public class Limb
    {
        public Limb(HingeJoint2D hinge, bool mirrored)
        {
            Angle = mirrored ? -hinge.jointAngle : hinge.jointAngle;
            Speed = mirrored ? -hinge.jointSpeed : hinge.jointSpeed;
        }

        public float Angle { get; }

        public float Speed { get; }
    }
}
