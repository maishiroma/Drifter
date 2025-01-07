using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class CarMovement : MonoBehaviour
    {
        public Rigidbody m_rigidbody;

        public float acceleration;
        public float maxAcceleration;

        public Vector3 testPosition;
        public Vector3 accelerationVector;

        private void Start()
        {
            testPosition = m_rigidbody.position;

        }

        void FixedUpdate()
        {
            accelerationVector = Vector3.Lerp(accelerationVector, new Vector3(1f * acceleration, 0, 0), Time.deltaTime);
            accelerationVector = Vector3.ClampMagnitude(accelerationVector, maxAcceleration);
            testPosition += accelerationVector;

            // TODO: Figure out how to move in a straight shot
        }

        public void OnSteer(InputAction.CallbackContext context)
        {
            acceleration *= context.ReadValue<float>();
        }
    }
}