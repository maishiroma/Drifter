using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GlobalGameSystem;

namespace Player
{
    public class CameraController : MonoBehaviour
    {
        [Header("Camera Properties")]
        public Vector3 offset;
        public float cameraSpeed;
        public float forwardMod = -5f;
        
        // Private Variables
        private Transform playerRef;
        private  Rigidbody playerRB;
        
        private void Start()
        {
            playerRef = GameManager.Instance.playerRef.transform.GetChild(0).transform;
            playerRB = playerRef.GetComponent<Rigidbody>();
        }

        private void LateUpdate()
        {
            if (playerRef != null)
            {
                Vector3 playerForward = (playerRB.velocity + playerRB.transform.forward).normalized;
                transform.position = Vector3.Lerp(
                    transform.position,
                    playerRef.position + playerRef.transform.TransformVector(offset) + playerForward * forwardMod,
                    cameraSpeed * Time.deltaTime);
                transform.LookAt(playerRef);
            }
        }
    }
}