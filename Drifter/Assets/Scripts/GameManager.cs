using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GlobalGameSystem
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;
        
        public Camera playerCamera;

        private void Awake()
        {
            if (Instance != null)
            {
                Instance = this;
                DontDestroyOnLoad(Instance);
            }
            else
            {
                Destroy(this);
            }
        }
    }
}

