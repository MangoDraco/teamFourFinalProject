using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyManager : MonoBehaviour, IDataPersistence
{
    //Attach to keys!

    public int keyIndex;

    private bool collected = false;

    public void LoadData(GameData data)
    {
        collected = data.keysCollected[keyIndex];
        if (collected)
        {
            gameObject.SetActive(false);
        }
    }

    public void SaveData(ref GameData data)
    {
        data.keysCollected[keyIndex] = collected;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!collected && other.CompareTag("Player"))
        {
            collected = true;
            gameObject.SetActive(false);
            Debug.Log("Collected key " + keyIndex);

            DataPersistenceManager.instance.SaveGame();
        }
    }
}
