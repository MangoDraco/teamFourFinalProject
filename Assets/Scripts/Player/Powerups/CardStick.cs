using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardStick : MonoBehaviour
//this is meant to go ON the card prefabs
{
    // Start is called before the first frame update
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 9) //check if it hits a wall
        {
            gameObject.transform.SetParent(collision.gameObject.transform, false);
        }
    }
}
