using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace teamFourFinalProject
{
    public class GroundChecker : MonoBehaviour
    {
        [SerializeField] float groundDistance = 0.08f;
        [SerializeField] LayerMask groundLayers;
        [SerializeField] LayerMask cardLayers;

        public bool isGrounded {  get; private set; }
        public bool isCardGrounded { get; private set; }
        public bool IsGrounded => isGrounded || isCardGrounded;

        private void Update()
        {
            Vector3 origin = transform.position + Vector3.up * 0.1f;

            isGrounded = Physics.SphereCast(origin: transform.position, radius: groundDistance, direction: Vector3.down, out _, groundDistance, (int)groundLayers);
            isCardGrounded = Physics.SphereCast(origin: transform.position, radius: groundDistance, direction: Vector3.down, out _, groundDistance, cardLayers);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Vector3 origin = transform.position + Vector3.up * 0.1f;
            Gizmos.DrawWireSphere(origin, groundDistance);
            Gizmos.DrawWireSphere(origin + Vector3.down * (groundDistance + 0.1f), groundDistance);
        }
    }
}
