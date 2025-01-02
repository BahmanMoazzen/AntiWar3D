using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace AntiWar3D
{
    public class CarInputController : MonoBehaviour
    {
        CarController _carController;
        [SerializeField] CarInputTypes _controllerType;
        private void OnEnable()
        {
            _carController = GetComponent<CarController>();
        }


        void FixedUpdate()
        {
            float motor = 0, steering = 0;
            switch (_controllerType)
            {
                case CarInputTypes.KeyBoard:


                    _carController._Brake(Input.GetButton("Brake"));

                    motor = Input.GetAxis("Vertical");
                    steering = Input.GetAxis("Horizontal");
                    

                    break;
                case CarInputTypes.JoyStick:
                    break;
                case CarInputTypes.OnScreen:
                    break;


            }


            _carController._Drive(motor, steering);

        }
    }
    public enum CarInputTypes { KeyBoard, JoyStick, OnScreen }
}