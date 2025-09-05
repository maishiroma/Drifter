using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class ArcadeCarMovement : MonoBehaviour
    {

        bool isBraking;
        bool isAccelerating;
        float steerInput;

        public float maxBrakeMod;
        public float maxAccelerateMod;
        public float maxTopSpeed;
        public float maxReverseSpeed;

        float carSpeed;
        float currAccelerationMod;
        float currLerpedSpeed;

        Rigidbody carRB;

        private void Start()
        {
            carRB = gameObject.GetComponent<Rigidbody>();

            currAccelerationMod = 0f;
            currLerpedSpeed = 0.1f;
        }

        private void FixedUpdate()
        {
            carSpeed = carRB.velocity.magnitude;
            currLerpedSpeed = DetermineLerpSpeed(currLerpedSpeed);

            ApplyMotor();
            ApplyBrake();
            //ApplySteering();
            //CheckSmoke();
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
            Vector3 moveForce = Vector3.zero;
            if (isBraking)
            {
                currAccelerationMod = Mathf.Lerp(currAccelerationMod, -maxBrakeMod, Time.deltaTime);
                if (carSpeed >= maxReverseSpeed)
                {
                    moveForce = carRB.transform.forward * currAccelerationMod;
                    carRB.AddForce(moveForce, ForceMode.Force);
                }
            }
            else if (isAccelerating)
            {
                currAccelerationMod = Mathf.Lerp(currAccelerationMod, maxAccelerateMod, Time.deltaTime);
                if (carSpeed <= maxTopSpeed)
                {
                    moveForce = carRB.transform.forward * currAccelerationMod;
                    carRB.AddForce(moveForce, ForceMode.Force);
                }
            }
        }

        private void ApplyBrake()
        {
            Vector3 moveForce = Vector3.zero;
            if (isBraking)
            {
                currAccelerationMod = Mathf.Lerp(currAccelerationMod, -maxBrakeMod, 0.01f);
                if (currAccelerationMod <= maxBrakeMod)
                {
                    moveForce = carRB.transform.forward * currAccelerationMod;
                    carRB.AddForce(moveForce, ForceMode.Force);
                }
            }
        }

        private float DetermineLerpSpeed(float existingOne)
        {
            float results = 0.1f;
            if (carSpeed > maxTopSpeed / 2)
            {
                // Over Half of TopSpeed
                results = Mathf.Max(existingOne, Mathf.Lerp(existingOne, 0.01f, 0.01f));
            }
            else if (carSpeed < maxTopSpeed / 2)
            {
                // Below Half of Top Speed
                results = Mathf.Max(existingOne, Mathf.Lerp(existingOne, 0.01f, 0.1f));
            }
            else if (carSpeed <  maxTopSpeed * 0.1)
            {
                // When the car is really slow
                results = Mathf.Max(existingOne, Mathf.Lerp(existingOne, 0.01f, 0.5f));
            }

            print("Lerp Speed:"  + results);
            return results;
        }
    }
}