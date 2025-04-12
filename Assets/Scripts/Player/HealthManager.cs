using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public int maxHealth;
    public int curHealth;
    public GameObject playerPrefab; //add the player in :3
    // Start is called before the first frame update
    void Start()
    {
        curHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (curHealth == 0)
        {
            Death();
        }
    }

    void TakeDmg(int dmg)
    {
        if (curHealth > 0)
        {
            curHealth -= dmg;
        }
        else
        {
            Death();
        }
    }

    void Death()
    {
        Destroy(gameObject);
        Debug.Log("You have died");
    }

    void Respawn()
    {
        Instantiate(playerPrefab, CheckpointSystem.respawnPoint.position, Quaternion.identity);
    }

    bool Heal()
    {
        if(curHealth == maxHealth)
        {
            return false;
        }
        else
        {
            curHealth += 1;
            return true;
        }
    }

    //Health Pickup Manager
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Health Pickup")
        {
            bool charHealed = Heal();
            if (charHealed)
            {
                Destroy(collision.gameObject);
                charHealed = false;
            }

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Deathplane")
        {
            Death();
        }

    }
}
