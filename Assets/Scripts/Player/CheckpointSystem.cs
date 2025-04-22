using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointSystem : MonoBehaviour
{
    public Transform[] checkpoints;
    public static Transform respawnPoint; //do not put anything in from the editor
    // Start is called before the first frame update
    void Start()
    {
        respawnPoint = checkpoints[0].transform; //start at the beginning of the level
        checkpoints[0].localScale = Vector3.zero;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("I entered the checkpoint!");
        if (other.gameObject.tag == "Checkpoint")
        {
            for (int i = 0; i < checkpoints.Length; i++)
            {
                if (checkpoints[i].position == other.gameObject.transform.position)
                {
                    respawnPoint = checkpoints[i];
                }
            }
        }
    }
}
