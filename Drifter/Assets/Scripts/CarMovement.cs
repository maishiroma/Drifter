using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class CarMovement : MonoBehaviour
    {
        [Header("External Components")]
        public Rigidbody m_rigidbody;

        [Header("Car Modifiers")]
        [Tooltip("The base acceleration for the vehical; from stop to getting it going")]
        public float m_accelerationBase;
        [Tooltip("How fast does the acceleraton of this car go?")]
        [Range(0.01f, 1f)]
        public float m_accelerationRate;
        [Tooltip("The maximum speed of the vehical. This controls how fast this car goes")]
        public float m_maxSpeed;
        [Tooltip("How fast does the car turn?")]
        public float m_steerSpeed;
        [Tooltip("Think of this as acceleration for steering; how fast does moving left and right take into account?")]
        public float m_steerResponsiveness;
        [Tooltip("How fast does this car slow down when NOT accelerating?")]
        public float m_drag;

        // Private Variables
        [Space]
        [Header("Private Variables")]
        [SerializeField]
        private float m_currentSpeed;
        [SerializeField]
        private Vector3 m_accelerationVector;
        [SerializeField]
        private float m_leftRightSteer;

        private void Start()
        {
            if (m_rigidbody == null)
            {
                m_rigidbody = GetComponent<Rigidbody>();
            }

            // This makes the vehical not tip when driving
            // Unity makes this by default at the center mass which works for the most part
            m_rigidbody.centerOfMass = new Vector3(0, -0.5f, 0);
        }

        private void FixedUpdate()
        {
            // Makes the car move forward based on the car direction
            m_currentSpeed = Vector3.Dot(m_rigidbody.velocity, transform.forward);

            ApplyAcceleration();
            ApplySteering();
            ApplyDrag();
        }

        public void OnSteer(InputAction.CallbackContext context)
        {
            float steerValue = context.ReadValue<float>();
            // Allows us to see how strong we can make the steering
            m_leftRightSteer = Mathf.Lerp(m_leftRightSteer, steerValue, m_steerResponsiveness);
            // Steering can have negative values, so we want to preserve them   
            m_leftRightSteer = Mathf.Clamp(m_leftRightSteer, -1f, 1f);
        }

        private void ApplyAcceleration()
        {
            if (Mathf.Approximately(m_accelerationVector.z, 0f))
            {
                m_accelerationVector = Vector3.Lerp(m_accelerationVector, new Vector3(0, 0, m_accelerationBase), Time.fixedDeltaTime * m_accelerationRate);
            }
            Vector3 forwardForce = transform.forward * m_accelerationVector.z;

            if (Mathf.Abs(m_currentSpeed) < m_maxSpeed)
            {
                // This uses the Physics Engine instead of moving the object via a set position
                m_rigidbody.AddForce(forwardForce, ForceMode.Acceleration);
            }
        }

        private void ApplySteering()
        {
            if (Mathf.Abs(m_currentSpeed) > 0.1f)  // Only steer when moving
            {
                float turnAmount = m_leftRightSteer * m_steerSpeed * Time.fixedDeltaTime;

                if (turnAmount == 0f)
                {
                    // Force the rotation to STOP
                    m_rigidbody.angularVelocity = Vector3.zero;
                }
                else
                {
                    m_rigidbody.AddTorque(Vector3.forward * turnAmount);
                }

            }
        }

        private void ApplyDrag()
        {
            Vector3 dragForce = -m_rigidbody.velocity * m_drag;
            // Naturl slowdown on the car
            m_rigidbody.AddForce(dragForce, ForceMode.Acceleration);
        }

    }
}