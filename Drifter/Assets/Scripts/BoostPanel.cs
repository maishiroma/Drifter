using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

namespace GameMechanics
{
    public class BoostPanel : MonoBehaviour
    {
        public float boostPower;

        Coroutine currOne;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.transform.root.tag == "Player")
            {
                ArcadeCarMovement currMovement = other.gameObject.transform.root.GetComponent<ArcadeCarMovement>();

                if (currOne != null)
                {
                    currMovement.StopCoroutine(currOne);
                }
                currMovement.BoostMode(boostPower, false);
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.transform.root.tag == "Player")
            {
                ArcadeCarMovement currMovement = other.gameObject.transform.root.GetComponent<ArcadeCarMovement>();
                currMovement.BoostMode(boostPower, true);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.transform.root.tag == "Player")
            {
                ArcadeCarMovement currMovement = other.gameObject.transform.root.GetComponent<ArcadeCarMovement>();
                currOne = StartCoroutine(currMovement.StopBostMode());
            }
        }
    }
}