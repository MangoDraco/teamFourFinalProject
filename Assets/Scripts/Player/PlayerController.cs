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
        [SerializeField, Self] CharacterController controller;
        [SerializeField, Self] Animator animator;
        [SerializeField, Anywhere] CinemachineFreeLook freelookVCam;
        [SerializeField, Anywhere] InputReader input;

        [Header("Settings")]
        [SerializeField] float moveSpeed = 6f;
        [SerializeField] float rotationSpeed = 15f;
        [SerializeField] float smoothTime = 0.2f;

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
        }

        private void Update()
        {
            HandleMovement();
            //UpdateAnimator();
        }

        void HandleMovement()
        {
            var movementDirection = new Vector3(input.Direction.x, y: 0f, z: input.Direction.y).normalized;
            //Rotate movement direction to match camera rotation
            var adjustedDirection = Quaternion.AngleAxis(mainCam.eulerAngles.y, Vector3.up) * movementDirection;
            if (adjustedDirection.magnitude > ZeroF)
            {
                HandleRotation(adjustedDirection);
                HandleCharacterController(adjustedDirection);
                SmoothSpeed(adjustedDirection.magnitude);
            }
            else
            {
                SmoothSpeed(ZeroF);
            }
        }

        void HandleCharacterController(Vector3 adjustedDirection)
        {
            //Move the player
            var adjustedMovement = adjustedDirection * (moveSpeed * Time.deltaTime);
            controller.Move(adjustedMovement);
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
