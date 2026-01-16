using UnityEngine;

namespace Game.GamePlay
{
    [System.Serializable]
    public class AnimationData
    {
        [SerializeField]
        private string _idlingAnimationName = "Idling";
        [SerializeField]
        private string _runningAnimationName = "Running";
        [SerializeField]
        private string _jumpingAnimationName = "Jumping";
        [SerializeField]
        private string _fallingAnimationName = "Falling";
        [SerializeField]
        private string _groundAnimationName = "Ground";
        [SerializeField]
        private string _airborneAnimationName = "Airborne";
        [SerializeField]
        private string _landingAnimationName = "Landing";
        [SerializeField]
        private string _moveAnimationName = "Move";
        [SerializeField]
        private string _directionAnimationName = "Direction";
        [SerializeField]
        private string _attackAnimationName = "Attack";
        [SerializeField]
        private string _attack1AnimationName = "Attack_1";
        [SerializeField]
        private string _attack2AnimationName = "Attack_2";
        [SerializeField]
        private string _attack3AnimationName = "Attack_3";
        [SerializeField]
        private string _takeDameAnimationName = "TakeDame";

        public int IdlingAnimationHash { get; private set; }
        public int RunningAnimationHash { get; private set; }
        public int JumpingAnimationHash { get; private set; }
        public int FallingAnimationHash { get; private set; }
        public int GroundAnimationHash { get; private set; }
        public int AirborneAnimationHash { get; private set; }
        public int LandingAnimationHash { get; private set; }
        public int MoveAnimationHash { get; private set; }
        public int DirectionAnimationHash { get; private set; }

        public int AttackAnimationHash { get; private set; }
        public int Attack1AnimationHash { get; private set; }
        public int Attack2AnimationHash { get; private set; }
        public int Attack3AnimationHash { get; private set; }
        public int TakeDameAnimationHash { get; private set; }
        

        public void OnInit()
        {
            IdlingAnimationHash = Animator.StringToHash(_idlingAnimationName);
            RunningAnimationHash = Animator.StringToHash(_runningAnimationName);
            JumpingAnimationHash = Animator.StringToHash(_jumpingAnimationName);
            FallingAnimationHash = Animator.StringToHash(_fallingAnimationName);
            GroundAnimationHash = Animator.StringToHash(_groundAnimationName);
            AirborneAnimationHash = Animator.StringToHash(_airborneAnimationName);
            LandingAnimationHash = Animator.StringToHash(_landingAnimationName);
            MoveAnimationHash = Animator.StringToHash(_moveAnimationName);
            DirectionAnimationHash = Animator.StringToHash(_directionAnimationName);
            AttackAnimationHash = Animator.StringToHash(_attackAnimationName);
            Attack1AnimationHash = Animator.StringToHash(_attack1AnimationName);
            Attack2AnimationHash = Animator.StringToHash(_attack2AnimationName);
            Attack3AnimationHash = Animator.StringToHash(_attack3AnimationName);
            TakeDameAnimationHash = Animator.StringToHash(_takeDameAnimationName);
        }
    }
}