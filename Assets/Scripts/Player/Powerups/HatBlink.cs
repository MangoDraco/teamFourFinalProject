using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace teamFourFinalProject
{
    [CreateAssetMenu(fileName = "HatBlinkPowerup", menuName = "Platformer/Powerups/HatBlink")]
    public class HatBlink : PowerupData
    {
        public override void ApplyEffects(PlayerController player)
        {
            player.SetInvulnerable(true);
            player.SetPassThroughEnemies(true);
            //player.DashForward(true);
        }

        public override void RemoveEffects(PlayerController player)
        {
            player.SetInvulnerable(false);
            player.SetPassThroughEnemies(false);
            //player.DashForward(false);
        }
    }
}
