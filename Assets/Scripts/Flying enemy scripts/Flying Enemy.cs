using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : MonoBehaviour, IStompable
{
    public Transform player;

    [SerializeField] private float timer = 5;
    float delay;
    private float bulletTime;

    public GameObject enemyBullet;
    public Transform spawnPoint;
    public float enemySpeed;
    bool CanShoot;

    public void Start()
    {
       
    }

    private void Update()
    {
        if(CanShoot == true)
        {
            ShootAtPlayer();
            FirstShotDelay();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.name == "Player")
        {
            Debug.Log("Player Detected Firing");
            CanShoot = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.name == "Player")
        {
            Debug.Log("Player Out of Range");
            CanShoot = false;
        }
    }

    public void FirstShotDelay()
    {
        delay -= Time.deltaTime;
        delay = 2;
    }

    void ShootAtPlayer()
    {
        bulletTime -= Time.deltaTime; //decrease bullet time

        if(bulletTime > 0 + delay)
        {
            return;
        }

        bulletTime = timer; //bullets lifetime

        GameObject bulletObj = Instantiate(enemyBullet, spawnPoint.transform.position, spawnPoint.transform.rotation) as GameObject; //spawning the projectile
        Rigidbody bulletRig = bulletObj.GetComponent<Rigidbody>();
        if (delay > 0)
        {
            bulletRig.AddForce(bulletRig.transform.forward * enemySpeed); //shooting the projectile
        }
        Destroy(bulletObj, 5f);
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    public void OnStomped()
    {

    }

}
