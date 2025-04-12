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
        delay = 2;
        bulletTime -= Time.deltaTime;

        if(bulletTime > 0 + delay)
        {
            return;
        }

        bulletTime = timer;

        GameObject bulletObj = Instantiate(enemyBullet, spawnPoint.transform.position, spawnPoint.transform.rotation) as GameObject;
        Rigidbody bulletRig = bulletObj.GetComponent<Rigidbody>();
        if (delay > 0)
        {
            bulletRig.AddForce(bulletRig.transform.forward * enemySpeed);
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
