using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointData : MonoBehaviour, IDataPersistence
{
    public Transform[] checkpoints;
    public static Transform respawnPoint;

    private int currentCheckpointIndex = 0;

    void Start()
    {
        respawnPoint = checkpoints[0];
        checkpoints[0].localScale = Vector3.zero;
        transform.position = respawnPoint.position;
    }

    public void LoadData(GameData data)
    {
        currentCheckpointIndex = data.playerCheckpoint;
        respawnPoint = checkpoints[currentCheckpointIndex];
        this.transform.position = respawnPoint.position;
    }

    public void SaveData(ref GameData data)
    {
        data.playerCheckpoint = currentCheckpointIndex;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Checkpoint"))
        {
            for (int i = 0; i < checkpoints.Length; i++)
            {
                if (checkpoints[i].position == other.transform.position)
                {
                    currentCheckpointIndex = i;
                    respawnPoint = checkpoints[i];
                    Debug.Log($"Checkpoint reached: {i}");
                    break;
                }
            }
        }
    }
}
