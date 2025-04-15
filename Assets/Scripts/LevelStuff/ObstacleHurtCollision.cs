using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleHurtCollision : MonoBehaviour
{
   private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("I'm doing damage to you :<");
            collision.gameObject.GetComponent<HealthManager>().TakeDmg(1);
        }
    }
}
