using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour, IDataPersistence
{
    public int requiredKeyIndex;
    private bool keyOwned = false;

    public GameObject lockedDoorVisual;
    public GameObject unlockedDoorVisual;

    public void LoadData(GameData data)
    {
        keyOwned = data.keysCollected[requiredKeyIndex];

        if (keyOwned)
        {
            lockedDoorVisual.SetActive(false);
            unlockedDoorVisual.SetActive(true);
        }

        else
        {
            lockedDoorVisual.SetActive(true);
            unlockedDoorVisual.SetActive(false);
        }
    }

    public void SaveData(ref GameData data) { }

    public void OnTriggerEnter(Collider other)
    {
        if (keyOwned && other.CompareTag("Player"))
        {
            SceneManager.instance.LoadLevel(requiredKeyIndex + 1); //Assuming HUB is 0
        }
    }
}
