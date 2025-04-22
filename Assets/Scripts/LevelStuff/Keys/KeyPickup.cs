using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace teamFourFinalProject
{
    public class KeyPickup : MonoBehaviour
    {
        [SerializeField] KeyData keyData;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                if (keyData == null)
                {
                    Debug.LogError("KeyData is not assigned in the inspector!");
                    return;
                }

                if (KeyManager.instance == null)
                {
                    Debug.LogError("KeyManager.instance is null! Is it placed in the scene?");
                    return;
                }

                KeyManager.instance.AddKey(keyData.keyID);
                Debug.Log("Picked up key: " + keyData.keyID);

                Destroy(gameObject);
            }
        }
    }
}
