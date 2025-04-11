using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace teamFourFinalProject
{
    public class GroundChecker : MonoBehaviour
    {
        [SerializeField] float groundDistance = 0.08f;
        [SerializeField] LayerMask groundLayers;

        public bool isGrounded { get; private set; }

        private void Update()
        {
            isGrounded = Physics.SphereCast(origin:transform.position, radius:groundDistance, direction:Vector3.down, out _, groundDistance, (int)groundLayers);
        }
    }
}
