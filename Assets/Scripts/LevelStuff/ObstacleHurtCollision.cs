using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleHurtCollision : MonoBehaviour
{
    public bool playerHurt;
    
   private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.CompareTag("Player") && !playerHurt)
        {
            Debug.Log("I'm doing damage to you :<");
            collision.gameObject.GetComponent<HealthManager>().TakeDmg(1);
        }
    }
    
}
