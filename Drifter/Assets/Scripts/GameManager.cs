using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GlobalGameSystem
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;
        
        [Header("Prefabs")]
        public GameObject playerCameraPrefab;

        [Space]
        [Header("Dynamic References")]
        public GameObject playerRef;
        public GameObject playerCamera;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(Instance);
            }
            else
            {
                Destroy(this);
            }
        }

        private void Start()
        {
            if (playerRef == null)
            {
                playerRef = GameObject.FindGameObjectWithTag("Player");
            }

            if (playerCamera == null)
            {
                playerCamera = Instantiate(playerCameraPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            }
        }
    }
}

