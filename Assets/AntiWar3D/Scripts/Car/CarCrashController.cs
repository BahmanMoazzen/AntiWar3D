using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarCrashController : MonoBehaviour
{
    [Tooltip("First Crash Place Is The Buttom of Car")]
    [SerializeField] Transform _ButtonGroundCheck;
    [SerializeField] float _CheckRadiuse;
    [SerializeField] LayerMask _GroundLayer;





    void FixedUpdate()
    {


        Debug.Log(_checkUpsideDown());
    }
    [ContextMenu("Reset Rotation")]
    public void _ResetRotation()
    {
        transform.rotation = Quaternion.Euler(new Vector3(0,transform.localEulerAngles.y,0));
    }

    bool _checkUpsideDown()
    {

        RaycastHit RCH;
        Physics.Raycast(_ButtonGroundCheck.position, Vector3.down, out RCH, _CheckRadiuse, _GroundLayer);
        if (RCH.collider)
        {
            return false;
        }

        return true;
    }
    private void OnDrawGizmos()
    {

    }
}
