using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class ProjectileHit : MonoBehaviour
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
        else if (other.gameObject.tag == "Wall")
        {
            src.PlayOneShot(impact);
            Destroy(gameObject);
        }
    }

}
