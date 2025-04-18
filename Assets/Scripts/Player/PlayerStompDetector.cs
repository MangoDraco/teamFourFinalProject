using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace teamFourFinalProject
{
    public class PlayerStompDetector : MonoBehaviour
    {
        public float stompBounceForce = 12f;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                Rigidbody rb = GetComponentInParent<Rigidbody>();
                if (rb != null && rb.velocity.y < 0f)
                {
                    IStompable stompable = other.GetComponent<IStompable>();
                    if (stompable != null)
                    {
                        stompable.Die();
                    }
                }

                //Bounce
                rb.velocity = new Vector3(rb.velocity.x, stompBounceForce, rb.velocity.z);
            }
        }
    }
}
