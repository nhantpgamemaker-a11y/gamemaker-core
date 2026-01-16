using System;
using GameMaker.Core.Runtime;
using GamePlay.Game;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.GamePlay
{
    public abstract class PlayerAttackState : PlayerGroundState
    {
        [SerializeField] protected AudioClip _slatSfx;
        protected bool _onTransition = false;
        public override void OnEnterState(BaseStateData baseStateData = null)
        {
            _onTransition = false;
            base.OnEnterState(baseStateData);
            StartAnimation(playerStateMachine.PlayerData.AnimationData.AttackAnimationHash);
            ResetVelocity();
            playerStateMachine.AudioSource.PlayOneShot(_slatSfx);
        }
        public override void OnExitState()
        {
            _onTransition = false;
            base.OnExitState();
            StopAnimation(playerStateMachine.PlayerData.AnimationData.AttackAnimationHash);
        }
        internal override void OnAnimationStartEventHandle()
        {
            base.OnAnimationStartEventHandle();
            ContactFilter2D filter = new ContactFilter2D();
            filter.useLayerMask = true;
            filter.layerMask = playerStateMachine.PlayerData.LayerData.MonsterLayer;

            Collider2D[] contactColliders = new Collider2D[1];
            int contactAmount = Physics2D.OverlapCollider(playerStateMachine.AttackRangeCheckCollider, filter, contactColliders);
            if (contactAmount != 0)
            {
                foreach(var collider in contactColliders)
                {
                    var takeDame = collider.transform.GetComponentInParent<ITakeDame>();
                    if (takeDame != null)
                    {
                        var center = collider.bounds.center;
                        Vector3 direction = center - this.transform.position;
                        direction.y = 0f;
                        direction.z = 0f;
                        takeDame.TakeDame(30f,direction, center);
                    }
                }
            }
        }
        internal override void OnAnimationTransitionEventHandle()
        {
            _onTransition = true;
        }

        internal override void OnAnimationEndEventHandle()
        {
            if (playerStateMachine.PlayerReusableData.GetControlDirection() != Vector2.zero)
            {
                playerStateMachine.ChangeState(PlayerStateType.Running);
            }
            else
            {
                playerStateMachine.ChangeState(PlayerStateType.Idling);
            }
        }
        protected override void RegisterInputAction()
        {
            playerStateMachine.PlayerInputAction.Player.Attack.started += OnAttackStarted;
        }
        protected override void UnRegisterInputAction()
        {
            playerStateMachine.PlayerInputAction.Player.Attack.started -= OnAttackStarted;
        }
        private void OnAttackStarted(InputAction.CallbackContext context)
        {
            if (!_onTransition) return;
            NextAttack();
        }
        public virtual void NextAttack()
        {
            
        }
    }
}
