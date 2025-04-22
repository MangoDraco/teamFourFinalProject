using KBCore.Refs;
using System;
using System.Collections;
using System.Collections.Generic;
using teamFourFinalProject;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;

public class HealthManager : MonoBehaviour, IDataPersistence
{
    public float maxHealth;
    public float curHealth;
    private int lives;
    public TextMeshProUGUI livesText;
    public bool dead;
    public GameObject playerPrefab; //add the player in :3
    // Start is called before the first frame update
    public static event Action OnPlayerDamaged;
    public static event Action OnPlayerDeath;
    [SerializeField] SceneManager sceneManager;
    [SerializeField] AudioSource hurt;
    [SerializeField] AudioClip hurtSound;
    void Start()
    {
        curHealth = maxHealth;
        lives = 3;
        livesText.text = "x" + lives;


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadData(GameData data)
    {
        this.curHealth = Mathf.Clamp(data.curHealth, 1, (int)maxHealth);
    }

    public void SaveData(ref GameData data)
    {
        data.curHealth = (int)this.curHealth;
    }

    public void TakeDmg(float dmg)
    {
        curHealth -= dmg;
        hurt.Play();
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
        if(lives <= 0)
        {
            Debug.Log(dead);
            dead = true;
            Debug.Log(dead);
            playerPrefab.SetActive(false);
        }
        else
        {
            curHealth = maxHealth;
            playerPrefab.transform.position = CheckpointSystem.respawnPoint.position;
        }
        
    }

    public void MapDeath()
    {
         playerPrefab.SetActive(false);
         curHealth -= 1;
        dead = true;
    }

    public void Respawn()
    {
        if (dead)
        {
            curHealth = maxHealth;
        }
        dead = false;
        OnPlayerDamaged?.Invoke();
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
            MapDeath();
        }

    }
}
