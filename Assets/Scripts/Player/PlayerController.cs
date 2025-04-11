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
        [SerializeField, Self] GroundChecker groundChecker;
        [SerializeField, Anywhere] CinemachineFreeLook freelookVCam;
        [SerializeField, Anywhere] InputReader input;

        [Header("Settings")]
        [SerializeField] float moveSpeed = 6f;
        [SerializeField] float rotationSpeed = 15f;
        [SerializeField] float smoothTime = 0.2f;

        [Header("Jump Settings")]
        [SerializeField] float jumpForce = 10f;
        [SerializeField] float jumpDuration = 0.5f;
        [SerializeField] float jumpCooldown = 0f;
        [SerializeField] float jumpMaxHeight = 2f;
        [SerializeField] float gravityMultiplier = 3f;

        //Animator
        static readonly int Speed = Animator.StringToHash(name: "Speed");

        float currentSpeed;
        float velocity;
        float jumpVelocity;

        Transform mainCam;

        const float ZeroF = 0f;

        Vector3 movement;

        List<JumpTimer> timers;
        CountdownTimer jumpTimer;
        CountdownTimer jumpCooldownTimer;

        private void Awake()
        {
            mainCam = Camera.main.transform;
            freelookVCam.Follow = transform;
            freelookVCam.LookAt = transform;

            //Invoke event when observed transform is teleported, adjusting freeLookVCam's position accordingly
            freelookVCam.OnTargetObjectWarped(transform, positionDelta: transform.position - freelookVCam.transform.position - Vector3.forward);

            rb.freezeRotation = true;

            //Setting up timers
            jumpTimer = new CountdownTimer(jumpDuration);
            jumpCooldownTimer = new CountdownTimer(jumpCooldown);
            timers = new List<JumpTimer>(capacity:2) { jumpTimer, jumpCooldownTimer };

            jumpTimer.OnTimerStop += () => jumpCooldownTimer.Start();
        }

        private void Start()
        {
            input.EnablePlayerActions();
        }

        void OnEnable()
        {
            input.Jump += OnJump;
        }

        void OnDisable()
        {
            input.Jump -= OnJump;
        }

        void OnJump(bool performed)
        {
            if (performed && !jumpTimer.isRunning && !jumpCooldownTimer.isRunning && groundChecker.isGrounded)
            {
                jumpTimer.Start();
            }

            else if (!performed && jumpTimer.isRunning)
            {
                jumpTimer.Stop();
            }
        }

        private void Update()
        {
            movement = new Vector3(input.Direction.x, y: 0f, z: input.Direction.y);

            HandleAnimator();
            HandleTimers();
        }

        private void FixedUpdate()
        {
            HandleJump();
            HandleMovement();
        }
        void HandleAnimator()
        {
            animator.SetFloat(id: Speed, currentSpeed);
        }

        void HandleTimers()
        {
            foreach (var timer in timers)
            {
                timer.Tick(Time.deltaTime);
            }
        }

        void HandleJump()
        {
            //If not jumping and grounded, keep jump velocity at 0
            if (!jumpTimer.isRunning && groundChecker.isGrounded)
            {
                jumpVelocity = ZeroF;
                jumpTimer.Stop();
                return;
            }

            //If jumping or falling calculate velocity
            if (jumpTimer.isRunning)
            {
                //Progress point for inital burst of velocity
                float launchPoint = 0.9f;
                if (jumpTimer.Progress > launchPoint)
                {
                    //Calculate the velocity required to reach the jump height using physics equations v = sqrt(2gh) (height (h), gravity (g), velocity (v)
                    jumpVelocity = Mathf.Sqrt(f: 2 * jumpMaxHeight * Mathf.Abs(Physics.gravity.y));
                }

                else
                {
                    //Gradually apply less velocity as the jump progresses
                    jumpVelocity += (1 - jumpTimer.Progress) * jumpForce * Time.fixedDeltaTime;
                }
            }

            else
            {
                jumpVelocity += Physics.gravity.y * gravityMultiplier * Time.fixedDeltaTime;
            }

            //Apply Velocity
            rb.velocity = new Vector3(rb.velocity.x, jumpVelocity, rb.velocity.z);
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
