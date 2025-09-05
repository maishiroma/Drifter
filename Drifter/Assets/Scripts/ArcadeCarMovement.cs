using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    [System.Serializable]
    public class WheelPositions
    {
        public GameObject wheelPosition;
        public ParticleSystem wheelSmoke;
    }
    
    public class ArcadeCarMovement : MonoBehaviour
    {
        [Header("Tires")]
        public WheelPositions[] wheeelData;
        public GameObject wheelParticles;

        [Header("States")]
        public bool isBraking;
        public bool isAccelerating;
        public float steerInput;
        public bool isBoosting;

        [Header("General Attributes")]
        public float maxBrakeMod;
        public float maxAccelerateMod;
        public float maxTopSpeed;
        public float maxReverseSpeed;
        public AnimationCurve steeringCurve;
       
        // Private Variables
        float carSpeed;
        float currAccelerationMod;
        float currLerpedSpeed;
        float origDrag;
        Rigidbody carRB;

        private void Start()
        {
            carRB = gameObject.GetComponent<Rigidbody>();
            InstantidateSmoke();

            currAccelerationMod = 0f;
            currLerpedSpeed = 0.1f;
            origDrag = carRB.drag;
        }

        private void FixedUpdate()
        {
            carSpeed = carRB.velocity.magnitude;
            currLerpedSpeed = DetermineLerpSpeed(currLerpedSpeed);

            ApplyMotor();
            ApplyBrake();
            ApplySteering();
            ApplyDrag();
            CheckSmoke();
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
            if (isAccelerating && !isBoosting)
            {
                currAccelerationMod = Mathf.Lerp(currAccelerationMod, maxAccelerateMod, currLerpedSpeed);
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
                currAccelerationMod = Mathf.Lerp(currAccelerationMod, -maxBrakeMod, currLerpedSpeed);
                if (carSpeed <= maxReverseSpeed)
                {
                    moveForce = carRB.transform.forward * currAccelerationMod;
                    carRB.AddForce(moveForce, ForceMode.Force);
                }
            }
        }

        private void ApplySteering()
        {
            float steeringAngle = steerInput * steeringCurve.Evaluate(carSpeed);
            carRB.AddTorque(carRB.transform.up * steeringAngle);
        }

        private void ApplyDrag()
        {
            if (!isAccelerating && !isBraking)
            {
                currAccelerationMod = Mathf.Lerp(0f, currAccelerationMod, 0.01f);
                carRB.drag = Mathf.Lerp(origDrag, 5, 0.01f);
            }
            else
            {
                carRB.drag = origDrag;
            }
        }

        private float DetermineLerpSpeed(float existingOne)
        {
            float results = 0.1f;
            if (Mathf.Sign(currAccelerationMod) < 0)
            {
                // Car is in "reverse"
                results = Mathf.Clamp(Mathf.Lerp(existingOne, 0.1f, 1f), 0.01f, 0.1f);
            }
            else if (carSpeed > maxTopSpeed / 2)
            {
                // Over Half of TopSpeed
                results = Mathf.Clamp(Mathf.Lerp(existingOne, 0.01f, 0.1f), 0.01f, 0.1f);
            }
            else if (carSpeed < maxTopSpeed / 2 && carSpeed > 0)
            {
                // Below Half of Top Speed
                results = Mathf.Clamp(Mathf.Lerp(existingOne, 0.01f,  0.1f), 0.01f, 0.1f);
            }

            print("Lerp Speed:"  + results);
            return results;
        }

        private void InstantidateSmoke()
        {
            foreach (WheelPositions currOne in wheeelData)
            {
                Vector3 newSmokePos = new Vector3(currOne.wheelPosition.transform.position.x, currOne.wheelPosition.transform.position.y - 0.1f, currOne.wheelPosition.transform.position.z);
                currOne.wheelSmoke = Instantiate(wheelParticles, newSmokePos, Quaternion.identity, gameObject.transform).GetComponent<ParticleSystem>();
            }
        }

        private void CheckSmoke()
        {
            for (int i = 0; i < wheeelData.Length; i++)
            { 
                if (currAccelerationMod > 0 && isAccelerating)
                {
                    wheeelData[i].wheelSmoke.Play();
                }
                else
                {
                    wheeelData[i].wheelSmoke.Stop();
                }
            }
        }
    
        public void BoostMode(float boostPower, bool isConstant)
        {
            if (!isBoosting)
            {
                isBoosting = true;
            }

            Vector3 boostVelocity = carRB.transform.forward * boostPower;
            if (isConstant)
            {
                carRB.velocity += boostVelocity;
            }
            else
            {
                carRB.AddForce(boostVelocity, ForceMode.Acceleration);
            }
        }

        public IEnumerator StopBostMode()
        {
            if(isBoosting)
            {
                Vector3 topSpeed = carRB.velocity.normalized * maxTopSpeed;
                while (carSpeed > maxTopSpeed)
                {
                    carRB.velocity = Vector3.Lerp(carRB.velocity, topSpeed, 0.1f);
                    yield return null;
                }
                isBoosting = false;
            }
            yield return null;
        }
    }
}