using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder.MeshOperations;

namespace teamFourFinalProject
{
    [CreateAssetMenu(fileName = "CardPlatformPowerup", menuName = "Platformer/Powerups/CardPlatform")]
    public class CardPowerup : PowerupData
    {
        public GameObject redPlat;
        public GameObject blackPlat;

        public GameObject ghostPlat;
        private GameObject activeGhost;

        public LayerMask wallLayerMask;

        public float despawnTimer = 5.0f;
        public float spawnDistance = 3f;
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
                else if (cardVal == 1)
                {
                    //player.changeMoveSpeed(-3);
                }
            }
            Debug.Log("CardPlatform powerup removed");
        }

        public void ThrowPlatform(PlayerController player)
        {
            Camera cam = Camera.main;
            Ray ray = new Ray(cam.transform.position, cam.transform.forward); // Center of camera view
            RaycastHit hit;

            GameObject platToSpawn = cardVal == 0 ? redPlat : blackPlat;
            Vector3 spawnPos;
            Quaternion rotation;

            // Check if there's a wall to stick to
            if (Physics.Raycast(ray, out hit, 10f, wallLayerMask))
            {
                spawnPos = hit.point + hit.normal * 0.05f; // Slightly off wall to prevent clipping
                rotation = Quaternion.LookRotation(-hit.normal);
                Debug.Log("Platform thrown and aligned to wall surface.");
            }
            else
            {
                // No wall hit — place in front of player, using flat camera forward direction
                Vector3 flatForward = cam.transform.forward;
                flatForward.y = 0f;
                flatForward.Normalize();

                spawnPos = player.transform.position + flatForward * 3f;
                rotation = Quaternion.LookRotation(-flatForward);
                Debug.Log("No wall detected. Platform placed in front of player.");
            }

            // Instantiate and destroy after timer
            GameObject platform = GameObject.Instantiate(platToSpawn, spawnPos, rotation);
            GameObject.Destroy(platform, despawnTimer);

            // Optional: Visual debug line
            Debug.DrawRay(cam.transform.position, cam.transform.forward * 5f, Color.red, 2f);
        }

        public void UpdateGhost(PlayerController player)
        {
            Camera cam = Camera.main;
            Ray ray = new Ray(cam.transform.position, cam.transform.forward);
            RaycastHit hit;

            Vector3 ghostPos;
            Quaternion ghostRot;

            if (Physics.Raycast(ray, out hit, 10f, wallLayerMask))
            {
                ghostPos = hit.point + hit.normal * 0.05f;
                ghostRot = Quaternion.LookRotation(-hit.normal);
            }
            else
            {
                Vector3 flatForward = cam.transform.forward;
                flatForward.y = 0f;
                flatForward.Normalize();

                ghostPos = player.transform.position + flatForward * 3f;
                ghostRot = Quaternion.LookRotation(-flatForward);
            }

            if (activeGhost == null)
            {
                GameObject platToUse = cardVal == 0 ? redPlat : blackPlat;
                activeGhost = GameObject.Instantiate(platToUse);
                SetGhostAppearance(activeGhost);
            }

            activeGhost.transform.position = ghostPos;
            activeGhost.transform.rotation = ghostRot;

            Debug.DrawRay(cam.transform.position, cam.transform.forward * 5f, Color.green, 2f);
        }

        public void HideGhostPreview()
        {
            if (activeGhost != null)
            {
                GameObject.Destroy(activeGhost);
                activeGhost = null;
            }
        }

        private void SetGhostAppearance(GameObject ghost)
        {
            foreach (var renderer in ghost.GetComponentsInChildren<Renderer>())
            {
                renderer.material = Resources.Load<Material>("GhostMaterial");
            }

            foreach (var collider in ghost.GetComponentsInChildren<Collider>())
            {
                collider.enabled = false;
            }
        }
    }
}
