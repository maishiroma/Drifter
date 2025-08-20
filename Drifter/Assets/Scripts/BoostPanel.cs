using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

namespace GameMechanics
{
    public class BoostPanel : MonoBehaviour
    {
        public float boostPower;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.transform.root.tag == "Player")
            {
                CarMovementTryTwo currMovement = other.gameObject.transform.root.GetComponent<CarMovementTryTwo>();

                Vector3 boostVelocity = currMovement.carRB.transform.forward * boostPower;
                currMovement.carRB.velocity += boostVelocity;
                currMovement.isBoosting = true;
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.transform.root.tag == "Player")
            {
                CarMovementTryTwo currMovement = other.gameObject.transform.root.GetComponent<CarMovementTryTwo>();

                Vector3 boostForce = currMovement.carRB.transform.forward * boostPower;
                currMovement.carRB.AddForce(boostForce, ForceMode.Acceleration);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.transform.root.tag == "Player")
            {
                CarMovementTryTwo currMovement = other.gameObject.transform.root.GetComponent<CarMovementTryTwo>();

                currMovement.isBoosting = false;
            }
        }
    }
}