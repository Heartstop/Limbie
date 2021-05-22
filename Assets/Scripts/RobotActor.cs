using UnityEngine;

public class RobotActor : MonoBehaviour
{
    private int _maxMotorTorque = 3000;
    public HingeJoint2D facingLimb;
    public HingeJoint2D outerFacingLimb;
    public HingeJoint2D awayLimb;
    public HingeJoint2D outerAwayLimb;

    void Start()
    {
        if(facingLimb != null && outerFacingLimb != null && awayLimb != null && outerAwayLimb != null)
        {
            HingeJoint2D[] hinges = new HingeJoint2D[]{ facingLimb, outerFacingLimb, awayLimb, outerAwayLimb};
            foreach (var hinge in hinges)
            {
                hinge.useMotor = true;
                var motor = hinge.motor;
                motor.maxMotorTorque = _maxMotorTorque;
                hinge.motor = motor;
            }
        }
    }

    void Update()
    {
        
    }
}
