using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public enum WheelTypePosition
    {
        FrontLeft,
        FrontRight,
        RearLeft,
        RearRight
    }

    [System.Serializable]
    public class WheelData
    {
        public WheelCollider collider;
        public MeshRenderer renderer;
        public WheelTypePosition typePosition;
    }

    public class CarMovementTryTwo : MonoBehaviour
    {
        [Header("Car Wheels Data Structure")]
        public WheelData[] wheelsData;

        [Header("Car Body Components")]
        public Rigidbody carRB;

        [Header("Car Controls")]
        public bool isAccelerating;
        public bool isBraking;
        public float steerInput;
        public AnimationCurve steeringCurve;

        [Header("Car Stats")]
        public float motorPower;
        public float brakePower;

        //Private Variables
        private float carSpeed;
        private float slipAngle;

        private void FixedUpdate()
        {
            carSpeed = carRB.velocity.magnitude;
            
            ApplyMotor();
            ApplyBrake();
            ApplySteering();
            ApplyWheelPositions();
        }

        public void OnAccelerate(InputAction.CallbackContext context)
        {
            if (context.ReadValue<float>() == 1)
            {
                isAccelerating = true;
            }
            else
            {
                isAccelerating = false;
            }
            print("Accelerating: " + isAccelerating);
        }

        public void OnBrake(InputAction.CallbackContext context)
        {
            if (context.ReadValue<float>() == 1)
            {
                isBraking = true;
            }
            else
            {
                isBraking = false;
            }
            print("Braking: " + isBraking);
        }

        public void OnSteer(InputAction.CallbackContext context)
        {
            steerInput = context.ReadValue<float>();
            print("Steering: " + steerInput);
        }

        private void ApplyMotor()
        {
            float acceleration = isAccelerating ? 1.0f : 0f;
            if (CheckSlipping() && isBraking)
            {
                acceleration = -1f;
            }

            foreach (WheelData currOne in wheelsData)
            {
                if (currOne.typePosition == WheelTypePosition.RearLeft || currOne.typePosition == WheelTypePosition.RearRight)
                {
                    currOne.collider.motorTorque = motorPower * acceleration;
                }
            }
        }

        private void ApplyBrake()
        {
            float braking = isBraking ? 1.0f : 0f;
            if (CheckSlipping() && isBraking)
            {
                braking = 0f;
            }

            foreach (WheelData currOne in wheelsData)
            {
                if (currOne.typePosition == WheelTypePosition.FrontLeft || currOne.typePosition == WheelTypePosition.FrontRight)
                {
                    currOne.collider.brakeTorque = braking * (brakePower * 0.7f);
                }
                else if (currOne.typePosition == WheelTypePosition.RearLeft || currOne.typePosition == WheelTypePosition.RearRight)
                {
                    currOne.collider.brakeTorque = braking * (brakePower * 0.3f);
                }
            }
        }

        private bool CheckSlipping()
        {
            slipAngle = Vector3.Angle(transform.forward, carRB.velocity - transform.forward);
            if (slipAngle < 120.0f)
            {
                return false;
            }
            return true;
        }

        private void ApplySteering()
        {
            float steeringAngle = steerInput * steeringCurve.Evaluate(carSpeed);
            foreach (WheelData currOne in wheelsData)
            {
                if (currOne.typePosition == WheelTypePosition.FrontLeft || currOne.typePosition == WheelTypePosition.FrontRight)
                {
                    currOne.collider.steerAngle = steeringAngle;
                }
            }
        }

        private void ApplyWheelPositions()
        {
            for (int index = 0; index < wheelsData.Length; index++)
            {
                UpdateWheel(wheelsData[index].collider, wheelsData[index].renderer);
            }
        }

        private void UpdateWheel(WheelCollider col, MeshRenderer mesh)
        {
            // This allows us to get the position and quat of the current collider
            Quaternion quat;
            Vector3 position;
            col.GetWorldPose(out position, out quat);

            // We then apply it to the wheel itself
            mesh.transform.position = position;
            mesh.transform.rotation = quat;
        }
    }

}