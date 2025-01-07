using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class CarMovement : MonoBehaviour
    {
        public Rigidbody m_rigidbody;

        public float m_accelerationBase;
        [Range(0.01f, 1f)]
        public float m_accelerationRate;

        [SerializeField]
        private float m_leftRightSteer;
        private Vector3 accelerationVector;

        void FixedUpdate()
        {               
            if (Mathf.Approximately(accelerationVector.z, 0f))
            {
                accelerationVector = Vector3.Lerp(accelerationVector, new Vector3(0, 0, m_accelerationBase), Time.deltaTime * m_accelerationRate);
            }
            else
            {
                accelerationVector = Vector3.Lerp(accelerationVector, new Vector3(0, 0, accelerationVector.z * m_accelerationBase), Time.deltaTime * m_accelerationRate);
            }

            accelerationVector = new Vector3(m_leftRightSteer, m_leftRightSteer, accelerationVector.z);

            m_rigidbody.position += accelerationVector;
        }

        public void OnSteer(InputAction.CallbackContext context)
        {
            float steerValue = context.ReadValue<float>();

            // TODO: Need to figure out steering
            //m_leftRightSteer = steerValue;
        }
    }
}