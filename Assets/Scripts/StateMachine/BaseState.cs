using UnityEngine;

namespace teamFourFinalProject
{
    public abstract class BaseState : IState
    {
        protected readonly PlayerController player;
        protected readonly Animator animator;

        protected static readonly int LocomotionHash = Animator.StringToHash(name: "Locomotion");
        protected static readonly int JumpHash = Animator.StringToHash(name: "Jump");

        protected const float crossFadeDuration = 0.1f;

        protected BaseState(PlayerController player, Animator animator)
        {
            this.player = player;
            this.animator = animator;
        }
        public virtual void OnEnter()
        {
            //noop
        }

        public virtual void Update()
        {
            //noop
        }

        public virtual void FixedUpdate()
        {
            //noop
        }

        public virtual void OnExit()
        {
            //noop
        }
    }
}
