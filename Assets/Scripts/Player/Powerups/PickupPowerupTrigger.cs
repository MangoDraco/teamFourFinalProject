using UnityEngine;

namespace teamFourFinalProject
{
    public class PickupPowerupTrigger : MonoBehaviour
    {
        public PowerupData powerupData;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                PlayerController player = other.GetComponent<PlayerController>();
                if (player != null && powerupData != null)
                {
                    player.PickupPowerup(powerupData);

                    var respawner = GetComponent<PowerupRespawner>();
                    if (respawner != null)
                    {
                        respawner.OnPickedUp();
                    }
                }
            }
        }
    }
}
