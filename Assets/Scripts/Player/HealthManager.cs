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
    public GameObject playerPrefab; //add the player in :3
    // Start is called before the first frame update
    public Transform playerTransform;
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
        Debug.Log("You have died");
        playerPrefab.SetActive(false);
        sceneManager.LoadNextScene("gameOver");
        
    }

    public void Respawn()
    {
        playerPrefab.SetActive(true);
        playerTransform.position = CheckpointSystem.respawnPoint.position;
        //Instantiate(playerPrefab, CheckpointSystem.respawnPoint.position, Quaternion.identity);
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
