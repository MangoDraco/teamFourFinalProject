using KBCore.Refs;
using System;
using System.Collections;
using System.Collections.Generic;
using teamFourFinalProject;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthManager : MonoBehaviour
{
    public float maxHealth;
    public float curHealth;
    public bool dead;
    public GameObject playerPrefab; //add the player in :3
    // Start is called before the first frame update
    public static event Action OnPlayerDamaged;
    public static event Action OnPlayerDeath;
    [SerializeField] SceneManager sceneManager;
    void Start()
    {
        curHealth = maxHealth;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDmg(float dmg)
    {
        curHealth -= dmg;
        OnPlayerDamaged?.Invoke();
        if (curHealth <= 0)
        {
            curHealth = 0;
            OnPlayerDeath?.Invoke();
            Death();
            
        }
    }

    public void Death()
    {
        Debug.Log(dead);
        dead = true;
        Debug.Log(dead);
        playerPrefab.SetActive(false);
        
        
    }

    public void Respawn()
    {
        dead = false;
        curHealth = maxHealth;
        playerPrefab.transform.position = CheckpointSystem.respawnPoint.position;
        playerPrefab.SetActive(true);
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
