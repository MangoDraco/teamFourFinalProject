using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace teamFourFinalProject
{
    public class KeyPickup : MonoBehaviour
    {
        public string keyID;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                //Add the key to the player's inventory through the KeyManager
                KeyManager.instance.AddKey(keyID);

                //Destroy the key object after pickup (or deactivate it)
                Destroy(gameObject);
            }
        }
    }
}
