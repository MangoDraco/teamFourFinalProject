using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    [System.Serializable]
    public class GameData : MonoBehaviour
    {
        public int curHealth;

        //the values defined in this will be default values for when game loads in

        public GameData()
        {
            this.curHealth = 5;
        }
    }
