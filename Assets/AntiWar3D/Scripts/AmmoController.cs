using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class AmmoController : MonoBehaviour
{
    [SerializeField] float _DriveForce;
    //[SerializeField] Vector3 _ForcePlace;
    Rigidbody _RB;

    private void Start()
    {
        Debug.Log(transform.forward);
        _RB = GetComponent<Rigidbody>();
        Destroy(gameObject, 1f);
    }
    private void Update()
    {

        
    }
    private void FixedUpdate()
    {
        _RB.velocity = transform.forward * _DriveForce;
    }
}
