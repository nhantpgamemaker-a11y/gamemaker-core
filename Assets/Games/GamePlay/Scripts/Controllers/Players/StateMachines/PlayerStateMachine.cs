using System;
using System.Collections.Generic;
using GameMaker.Core.Runtime;
using UnityEngine;
using System.Linq;

namespace Game.GamePlay
{
    public enum PlayerStateType
    {
        Idling,
        Running,
        Purificating,
        Buffing,
        Jumping,
        Falling,

        Landing,
        Attacking_1,
        Attacking_2,
        Attacking_3,
        TakeDame
    }
    [System.Serializable]
    public class PlayerStateMachine : BaseStateMachine<PlayerStateType>
    {
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private Collider2D _collider2D;
        [SerializeField] private Collider2D _groundCheckCollider;
        [SerializeField] private Collider2D _attackRangeCheckCollider;
        [SerializeField] private Animator _animator;
        [SerializeField] private PlayerData _playerData;
        [SerializeField] private PlayerReusableData _playerReusableData;
        private PlayerInputAction _playerInputAction;
        public PlayerInputAction PlayerInputAction
        {
            get
            {
                if (_playerInputAction == null)
                {
                    _playerInputAction = new PlayerInputAction();
                }
                return _playerInputAction;
            }
        }
        private BasePlayerState CurrentState => this.currentState as BasePlayerState;
        protected PlayerController playerController;
        public PlayerController PlayerController => playerController;
        public Rigidbody2D Rigidbody { get => _rigidbody; }
        public Collider2D Collider2D { get => _collider2D; }
        public Collider2D GroundCheckCollider { get => _groundCheckCollider; }
        public Collider2D AttackRangeCheckCollider { get => _attackRangeCheckCollider; }
        public Animator Animator { get => _animator; }
        public PlayerData PlayerData { get => _playerData; }
        public PlayerReusableData PlayerReusableData { get => _playerReusableData; }
        public AudioSource AudioSource => _audioSource;
        public virtual void OnEnable()
        {
            PlayerInputAction.Enable();
        }
        public virtual void OnDisable()
        {
            PlayerInputAction.Disable();
        }
        public void OnInit()
        {
            var states = playerController.gameObject.GetComponents<BasePlayerState>().ToList();
            OnInit(states.Cast<IState<PlayerStateType>>().ToList());
        }

        public void BindPlayerController(PlayerController playerController)
        {
            this.playerController = playerController;
        }
        
        public override void OnInit(List<IState<PlayerStateType>> states)
        {
            PlayerData.OnInit();
            base.OnInit(states);
            ChangeState(PlayerStateType.Idling);
        }

        public void OnPhysicUpdate()
        {
            CurrentState?.OnPhysicUpdate();
        }

        public void OnUpdate()
        {
            ReadPlayerInput();
            CurrentState?.OnUpdate();
        }
        
        private void ReadPlayerInput()
        {
            var controlDirection = PlayerInputAction.Player.Move.ReadValue<Vector2>();
            _playerReusableData.SetControlDirection(controlDirection);
        }

        internal void StartAnimation(int hashId)
        {
            var animator = _animator;
            animator.SetBool(hashId, true);
        }

        internal void StopAnimation(int hashId)
        {
            var animator = _animator;
            animator.SetBool(hashId, false);
        }

        internal void SetFloatAnimation(int hashId, float value)
        {
            var animator = _animator;
            animator.SetFloat(hashId, value);
        }
        internal Vector2 GetCenterBody()
        {
            return _collider2D.bounds.center;
        }
        internal Vector2 GetMinPoint()
        {
            return _groundCheckCollider.bounds.center + new Vector3(0, -_groundCheckCollider.bounds.extents.y);
        }

        internal void OnAnimationStartEventHandle()
        {
            CurrentState.OnAnimationStartEventHandle();
        }

        internal void OnAnimationTransitionEventHandle()
        {
            CurrentState.OnAnimationTransitionEventHandle();
        }

        internal void OnAnimationEndEventHandle()
        {
            CurrentState.OnAnimationEndEventHandle();
        }
    }
}
