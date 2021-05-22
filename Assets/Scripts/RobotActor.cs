using Limbie.Control;
using UnityEngine;

public class RobotActor : MonoBehaviour
{
    private int _maxMotorTorque = 3000;
    public HingeJoint2D facingLimb;
    public HingeJoint2D outerFacingLimb;
    public HingeJoint2D awayLimb;
    public HingeJoint2D outerAwayLimb;

    private Vector3 _resetPosistion;
    private Quaternion _resetRotation;
    private Quaternion _resetRotationFacingLimb;
    private Quaternion _resetRotationOuterFacingLimb;
    private Quaternion _resetRotationAwayLimb;
    private Quaternion _resetTransformOuterAwayLimb;

    public void ResetActor()
    {
        var rigidBodies = GetComponentsInChildren<Rigidbody2D>(); 
        foreach(var rigidBody in rigidBodies){
            rigidBody.velocity = Vector2.zero;
            rigidBody.angularVelocity = 0f;
        }
        this.transform.position = _resetPosistion;

        this.transform.rotation = _resetRotation;

        facingLimb.transform.rotation = _resetRotationFacingLimb;

        outerFacingLimb.transform.rotation = _resetRotationOuterFacingLimb;
        
        awayLimb.transform.rotation = _resetRotationAwayLimb;

        outerAwayLimb.transform.rotation = _resetTransformOuterAwayLimb;

        GetComponent<Programmable>().ResetScriptEngine();
    }

    void Start()
    {
        _resetPosistion = this.transform.position;
        _resetRotation = this.transform.rotation;
        
        if(facingLimb != null && outerFacingLimb != null && awayLimb != null && outerAwayLimb != null)
        {
            _resetRotationFacingLimb = facingLimb.transform.rotation;
            _resetRotationOuterFacingLimb = outerFacingLimb.transform.rotation;
            _resetRotationAwayLimb = awayLimb.transform.rotation;
            _resetTransformOuterAwayLimb = outerAwayLimb.transform.rotation;

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
