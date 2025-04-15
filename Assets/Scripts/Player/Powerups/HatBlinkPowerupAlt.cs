using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatBlinkPowerupAlt : MonoBehaviour
{
    public Transform player, destination;
    public GameObject playerg;
    public HealthManager healthManager;
    public GameObject gameObject;
    private void OnTriggerEnter(Collider other)
    {
        /*
        if (other.CompareTag("Player"))
        {
            playerg.SetActive(false);
            player.position = destination.position;
            playerg.SetActive(true);
            gameObject.SetActive(false);
        }
        */
    }
    void RespawnPowerup()
    {
        //this.SetActive(true);
    }
}
