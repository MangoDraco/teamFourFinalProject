using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    [System.Serializable]
    public class GameData
    {
        public int curHealth;
        public int playerCheckpoint;

        //the values defined in this will be default values for when game loads in

        public GameData()
        {
            this.curHealth = 5;
            this.playerCheckpoint = 0;
        }
    }
