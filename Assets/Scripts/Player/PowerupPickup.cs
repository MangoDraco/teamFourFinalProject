using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupPickup : MonoBehaviour
{
    //public PlayerController.PowerupType powerupType;  For revised player controller

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                player.PickupPowerup(powerupType);
                Destroy(gameObject);
            }
        }
    }
}
