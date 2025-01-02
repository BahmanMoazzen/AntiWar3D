using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSticker : MonoBehaviour
{
    [SerializeField] Transform _CameraPos;
    Camera _MainCam;
    [SerializeField] float _CameraRepositioningSpeed = 1f;
    private void Start()
    {
        _MainCam = Camera.main;
    }
    private void Update()
    {
        _MainCam.transform.position = Vector3.MoveTowards(_MainCam.transform.position, _CameraPos.position, _CameraRepositioningSpeed);

        _MainCam.transform.rotation = Quaternion.Lerp(_MainCam.transform.rotation, _CameraPos.rotation, _CameraRepositioningSpeed);
    }
}
