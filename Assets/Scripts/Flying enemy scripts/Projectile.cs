using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public AudioSource src;
    public AudioClip impact;

    public void OnTriggerEnter(Collider other)
    {


        if (other.gameObject.tag == "Player")
        {
            src.PlayOneShot(impact);
            Destroy(gameObject);
        }
    }

}
