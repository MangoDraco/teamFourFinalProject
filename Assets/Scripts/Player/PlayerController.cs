using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using KBCore.Refs;
using Unity.PlasticSCM.Editor.WebApi;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace teamFourFinalProject
{


    public class PlayerController : ValidatedMonoBehaviour
    {
        [Header("References")]
        //For some reason Animator is causing issues with jump. Has been omitted for now
        //[SerializeField, Self] Animator animator;
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
        [SerializeField] float jumpMaxHeight = 5f;
        [SerializeField] float gravityMultiplier = 3f;
        [SerializeField] int maxJumpCount = 2;
        [SerializeField] float doubleJumpMultiplier = 2;
        int currentJumpCount = 0;
        private bool canDoubleJump = false;
        private bool doubleJumpRequested = false;
        private Animator animator;

        //If making a different way to attack
        /*[Header("Attack Settings")]
        [SerializeField] float attackCooldown = 0.5f;
        [SerializeField] float attackDistance = 1f;
        [SerializeField] int attackDamage = 10;*/

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
        CountdownTimer attackTimer;

        StateMachine stateMachine;

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

            //jumpTimer.OnTimerStart =+ () => jumpVelocity = jumpForce;
            //jumpTimer.OnTimerStop += () => jumpCooldownTimer.Start();

            //State Machine
            stateMachine = new StateMachine();

            //Declare States
            //var locomotionState = new LocomotionState(player: this, animator);
            //var jumpState = new JumpState(player: this, animator);

            //Define transitions
            //At(from: locomotionState, to: jumpState, condition: new FuncPredicate(() => jumpTimer.isRunning));
            //At(from: jumpState, to: locomotionState, condition: new FuncPredicate(() => groundChecker.isGrounded && !jumpTimer.isRunning));

            //Set initial state
            //stateMachine.SetState(locomotionState);
        }

        //void At(IState from, IState to, IPredicate condition) => stateMachine.AddTransition(from, to, condition);
        //void Any(IState to, IPredicate condition) => stateMachine.AddAnyTransition(to, condition);

        private void Start()
        {
            input.EnablePlayerActions();
            animator = GetComponentInChildren<Animator>();
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
            if (!performed) return;
            
            if (groundChecker.isGrounded)
            {
                animator.SetBool("isJumping", true);
                animator.SetBool("isFalling", true);
                jumpTimer.Start();
                currentJumpCount = 1;
                rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
                canDoubleJump = true;
            }

            else if (canDoubleJump && currentJumpCount < maxJumpCount)
            {
                doubleJumpRequested = true;
                currentJumpCount++;
                canDoubleJump = false;
                Debug.Log("Double jump");
            }
        }

        //Left just in case we want to revert to original
        /*void OnJump(bool performed)
        {
            Debug.Log($"Jump Input: {performed}, JumpTimerRunning: {jumpTimer.isRunning}, CooldownRunning: {jumpCooldownTimer.isRunning}, Grounded: {groundChecker.isGrounded}");
            if (performed && !jumpTimer.isRunning && !jumpCooldownTimer.isRunning && groundChecker.isGrounded)
            {
                Debug.Log("Jump started!");
                jumpTimer.Start();
                currentJumpCount++;
                Debug.Log($"Jump {currentJumpCount} / {maxJumpCount}");
                canDoubleJump = true;
            }

            else if (!performed && jumpTimer.isRunning && canDoubleJump && currentJumpCount < maxJumpCount)
            {
                Debug.Log("Jump stopped early");
                jumpTimer.Stop();

                float doubleJumpForce = jumpForce * doubleJumpMultiplier;
                rb.velocity = new Vector3(rb.velocity.x, doubleJumpForce, rb.velocity.z);

                currentJumpCount++;
                canDoubleJump = false;
            }
        }*/

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
            //animator.SetFloat(id: Speed, currentSpeed);
        }

        void HandleTimers()
        {
            foreach (var timer in timers)
            {
                timer.Tick(Time.deltaTime);
            }
        }

        public void HandleJump()
        {
            if (doubleJumpRequested)
            {
                animator.SetBool("isDoubleJumping", true);
                
                float doubleJumpHeight = jumpMaxHeight * doubleJumpMultiplier;
                float doubleJumpVelocity = Mathf.Sqrt(2 * doubleJumpHeight * Mathf.Abs(Physics.gravity.y));
                //jumpVelocity = jumpForce * doubleJumpMultiplier;
                jumpVelocity = doubleJumpVelocity;
                doubleJumpRequested = false;
            }

            else if (!groundChecker.isGrounded)
            {
                jumpVelocity += Physics.gravity.y * gravityMultiplier * Time.fixedDeltaTime;
            }

            //If not jumping and grounded, keep jump velocity at 0
            if (!jumpTimer.isRunning && groundChecker.isGrounded)
            {
                animator.SetBool("isFalling", false);
                animator.SetBool("isJumping", false);
                animator.SetBool("isDoubleJumping", false);
                jumpVelocity = ZeroF;
                jumpTimer.Stop();
                currentJumpCount = 0;
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
                    jumpVelocity = Mathf.Sqrt(f: 2 * (jumpMaxHeight * doubleJumpMultiplier) * Mathf.Abs(Physics.gravity.y));
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

        public void HandleMovement()
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
                animator.SetBool("isFalling", false);
                animator.SetBool("isWalking", false);
            }
        }

        void HandleHorizontalMovement(Vector3 adjustedDirection)
        {
            //Move the player
            //animator.SetBool("isFalling", false);
            animator.SetBool("isWalking", true);
            Vector3 velocity = adjustedDirection * moveSpeed * Time.fixedDeltaTime;
            rb.velocity = new Vector3(velocity.x, rb.velocity.y, velocity.z);
            Debug.Log(velocity);
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
