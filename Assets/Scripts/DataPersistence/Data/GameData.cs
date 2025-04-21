using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    [System.Serializable]
    public class GameData
    {
        public int curHealth;
        public int playerCheckpoint;

        public int currentLevel;
        public int unlockedLevels;

        public bool[] keysCollected;

        //the values defined in this will be default values for when game loads in

        public GameData()
        {
            this.curHealth = 5; //Player health
            this.playerCheckpoint = 0; //Checkpoints reached during levels
            this.currentLevel = 0; //HUB
            this.unlockedLevels = 1; //Level 1 is unlocked
            this.keysCollected = new bool[3]; //3 Keys
        }
    }
