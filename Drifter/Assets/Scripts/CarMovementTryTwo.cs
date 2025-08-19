using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using GlobalGameSystem;

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
        public ParticleSystem smokeEffect;
    }

    public class CarMovementTryTwo : MonoBehaviour
    {
        [Header("Car Wheels Data Structure")]
        public WheelData[] wheelsData;

        [Header("Car Body Components")]
        public Rigidbody carRB;
        public GameObject tireParticles;

        [Header("Car Controls")]
        public bool isAccelerating;
        public bool isBraking;
        public float steerInput;
        public AnimationCurve steeringCurve;

        [Header("Car Stats")]
        public float motorPower;
        public float brakePower;

        [Header("Car Asthetics")]
        public float slipAllowance = 0.1f;

        //Private Variables
        private float carSpeed;
        private float slipAngle;
        private List<WheelData> rearWheels = new List<WheelData>();
        private List<WheelData> frontWheels = new List<WheelData>();

        private void Start()
        {
            SortWheelsIntoLists();
            InstantidateSmoke();
        }

        private void FixedUpdate()
        {
            carSpeed = carRB.velocity.magnitude;
            
            ApplyMotor();
            ApplyBrake();
            ApplySteering();
            CheckSmoke();
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

            foreach (WheelData currOne in rearWheels)
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

            foreach (WheelData currOne in rearWheels)
            {
                currOne.collider.brakeTorque = braking * (brakePower * 0.3f);
            }
            foreach (WheelData currOne in frontWheels)
            {
                currOne.collider.brakeTorque = braking * (brakePower * 0.7f);
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
            foreach (WheelData currOne in frontWheels)
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

        private void InstantidateSmoke()
        {
            foreach (WheelData currOne in wheelsData)
            {
                Vector3 newSmokePos = new Vector3(currOne.collider.transform.position.x, currOne.collider.transform.position.y - 0.1f, currOne.collider.transform.position.z);
                currOne.smokeEffect = Instantiate(tireParticles, newSmokePos, Quaternion.identity, currOne.collider.transform).GetComponent<ParticleSystem>();
            }
        }

        private void CheckSmoke()
        {
            WheelHit[] wheelHits = new WheelHit[wheelsData.Length];
            for (int i = 0; i < wheelsData.Length; i++)
            {
                WheelData currOne = wheelsData[i];
                currOne.collider.GetGroundHit(out wheelHits[i]);
            }

            for (int i = 0; i < wheelHits.Length; i++)
            {
                if ((Mathf.Abs(wheelHits[i].sidewaysSlip) + Mathf.Abs(wheelHits[i].forwardSlip) > slipAllowance))
                {
                    wheelsData[i].smokeEffect.Play();
                }
                else
                {
                    wheelsData[i].smokeEffect.Stop();
                }
            }
        }

        private void SortWheelsIntoLists()
        {
            for (int currCounter = 0; currCounter < wheelsData.Length; currCounter++)
            {
                WheelData currOne = wheelsData[currCounter];
                if (currOne.typePosition == WheelTypePosition.RearLeft || currOne.typePosition == WheelTypePosition.RearRight)
                {
                    rearWheels.Add(currOne);
                }
                else if (currOne.typePosition == WheelTypePosition.FrontLeft || currOne.typePosition == WheelTypePosition.FrontRight)
                {
                    frontWheels.Add(currOne);
                }
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