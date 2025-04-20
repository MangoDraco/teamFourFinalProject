using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace teamFourFinalProject
{
    [CreateAssetMenu(fileName = "CardPlatformPowerup", menuName = "Platformer/Powerups/CardPlatform")]
    public class CardPowerup : PowerupData
    {
        public GameObject redPlat;
        public GameObject blackPlat;
        public float despawnTimer = 5.0f;
        public int cardVal = 0;

        public override void ApplyEffects(PlayerController player)
        {
            Debug.Log("Applying Card Powerup effects");
            var groundChecker = player.GetComponent<GroundChecker>();
            var healthManager = player.GetComponent<HealthManager>();

            if (groundChecker == null || healthManager == null)
            {
                Debug.LogWarning("Missing components on player");
                return;
            }

            if (cardVal == 0)
            {
                healthManager.curHealth += 1;
                player.tempHealth = (int)healthManager.curHealth;
                player.upgradeAppl = true;
            }

            else if (cardVal == 1)
            {
                //player.changeMoveSpeed(3);
                player.upgradeAppl = true;
            }

            Debug.Log("CardPlatform powerup applied");
        }

        public override void RemoveEffects(PlayerController player)
        {
            var groundChecker = player.GetComponent<GroundChecker>();
            var healthManager = player.GetComponent<HealthManager>();

            if (groundChecker == null || healthManager == null)
            {
                Debug.LogWarning("Missing components on player");
                return;
            }

            if (player.upgradeAppl)
            {
                player.upgradeAppl = false;

                if (cardVal == 0 && healthManager.curHealth == player.tempHealth)
                {
                    healthManager.curHealth -= 1;
                }
                else if (cardVal ==1)
                {
                    //player.changeMoveSpeed(-3);
                }
            }
            Debug.Log("CardPlatform powerup removed");
        }

        public void ThrowPlatform(PlayerController player)
        {
            Vector3 spawnPos = player.transform.position + player.transform.forward * 2f;
            GameObject platToSpawn = cardVal == 0 ? redPlat : blackPlat;

            GameObject platform = GameObject.Instantiate(platToSpawn, spawnPos, Quaternion.identity);
            GameObject.Destroy(platform, despawnTimer);

            Debug.Log("Platform thrown");
        }
    }
}
