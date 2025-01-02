using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

[System.Serializable]
public class AxleInfo
{
    public WheelCollider leftWheel;
    

    public WheelCollider rightWheel;
    
    public bool motor;
    public bool steering;

}
public class CarController : MonoBehaviour
{
    
    [Header("Axel Info")]
    [SerializeField] List<AxleInfo> axleInfos;
    [SerializeField] float maxMotorTorque;
    [SerializeField] float maxSteeringAngle;
    [SerializeField] float maxBrakeTorque;
    [SerializeField] Transform _CenterOfMass;
    



    public void _ApplyLocalPositionToVisuals(WheelCollider collider)
    {
        if (collider.transform.childCount == 0)
        {
            return;
        }

        Transform visualWheel = collider.transform.GetChild(0);

        Vector3 position;
        Quaternion rotation;
        collider.GetWorldPose(out position, out rotation);

        visualWheel.transform.position = position;
        visualWheel.transform.rotation = rotation;
    }
    private void Start()
    {
        
        if (_CenterOfMass != null)
            GetComponent<Rigidbody>().centerOfMass = _CenterOfMass.position;
    }
    
    public void _Brake(bool iIsBraking)
    {
        foreach (AxleInfo axleInfo in axleInfos)
        {
            if (iIsBraking)
            {
                axleInfo.rightWheel.brakeTorque = axleInfo.leftWheel.brakeTorque = maxBrakeTorque;
            }
            else
            {
                axleInfo.rightWheel.brakeTorque = axleInfo.leftWheel.brakeTorque = 0;
            }
            _ApplyLocalPositionToVisuals(axleInfo.leftWheel);
            _ApplyLocalPositionToVisuals(axleInfo.rightWheel);
        }
    }

    public void _Drive(float iMotor,float iSteering)
    {
        float steering = maxSteeringAngle * iSteering;
        float motor = maxMotorTorque * iMotor;

        foreach (AxleInfo axleInfo in axleInfos)
        {
            
            if (axleInfo.steering)
            {
                axleInfo.leftWheel.steerAngle = steering;
                axleInfo.rightWheel.steerAngle = steering;
            }
            if (axleInfo.motor)
            {
                axleInfo.leftWheel.motorTorque = motor;

                axleInfo.rightWheel.motorTorque = motor;
            }
            _ApplyLocalPositionToVisuals(axleInfo.leftWheel);
            _ApplyLocalPositionToVisuals(axleInfo.rightWheel);
        }
    }
}
