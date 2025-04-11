using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using KBCore.Refs;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

namespace teamFourFinalProject
{


    public class PlayerController : ValidatedMonoBehaviour
    {
        [Header("References")]
        [SerializeField, Self] Animator animator;
        [SerializeField, Self] Rigidbody rb;
        [SerializeField, Anywhere] CinemachineFreeLook freelookVCam;
        [SerializeField, Anywhere] InputReader input;

        [Header("Settings")]
        [SerializeField] float moveSpeed = 6f;
        [SerializeField] float rotationSpeed = 15f;
        [SerializeField] float smoothTime = 0.2f;

        //Animator
        static readonly int Speed = Animator.StringToHash(name: "Speed");

        float currentSpeed;
        float velocity;

        Transform mainCam;

        const float ZeroF = 0f;

        Vector3 movement;

        private void Awake()
        {
            mainCam = Camera.main.transform;
            freelookVCam.Follow = transform;
            freelookVCam.LookAt = transform;

            //Invoke event when observed transform is teleported, adjusting freeLookVCam's position accordingly
            freelookVCam.OnTargetObjectWarped(transform, positionDelta: transform.position - freelookVCam.transform.position - Vector3.forward);

            rb.freezeRotation = true;
        }

        private void Start()
        {
            input.EnablePlayerActions();
        }

        private void Update()
        {
            movement = new Vector3(input.Direction.x, y: 0f, z: input.Direction.y);

            HandleAnimator();
        }

        private void FixedUpdate()
        {
            //HandleJump();
            HandleMovement();
        }

        void HandleMovement()
        {
            //Rotate movement direction to match camera rotation
            var adjustedDirection = Quaternion.AngleAxis(mainCam.eulerAngles.y, Vector3.up) * movement;
            if (adjustedDirection.magnitude > ZeroF)
            {
                HandleRotation(adjustedDirection);
                HandleHorizontalMovement(adjustedDirection);
                SmoothSpeed(adjustedDirection.magnitude);
            }
            else
            {
                SmoothSpeed(ZeroF);

                //Reset horizontal velocity
                rb.velocity = new Vector3(x: ZeroF, rb.velocity.y, z: ZeroF);
            }
        }

        void HandleAnimator()
        {
            animator.SetFloat(id: Speed, currentSpeed);
        }

        void HandleHorizontalMovement(Vector3 adjustedDirection)
        {
            //Move the player
            Vector3 velocity = adjustedDirection * moveSpeed * Time.fixedDeltaTime;
            rb.velocity = new Vector3(velocity.x, rb.velocity.y, velocity.z);
        }

        void HandleRotation(Vector3 adjustedDirection)
        {
            //Adjust rotation to match movement direction
            var targetRotation = Quaternion.LookRotation(adjustedDirection);
            transform.rotation = Quaternion.RotateTowards(from: transform.rotation, to: targetRotation, maxDegreesDelta: rotationSpeed * Time.deltaTime);
            transform.LookAt(worldPosition: transform.position + adjustedDirection);
        }

        void SmoothSpeed(float value)
        {
            currentSpeed = Mathf.SmoothDamp(current: currentSpeed, target: value, ref velocity, smoothTime);
        }
    }
}
