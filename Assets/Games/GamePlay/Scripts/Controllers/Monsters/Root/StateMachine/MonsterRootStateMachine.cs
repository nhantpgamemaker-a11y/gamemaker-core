using System;
using System.Collections.Generic;
using System.Linq;
using GameMaker.Core.Runtime;
using GamePlay.Game;
using UnityEngine;

namespace Game.GamePlay
{
    public enum MonsterRootStateType
    {
        Idling,
        RightAttack,
        LeftAttack,
        TakeDame
    }
    [System.Serializable]
    public class MonsterRootStateMachine : BaseStateMachine<MonsterRootStateType>
    {
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private Animator _animator;
        [SerializeField] private RootMonsterData _rootMonsterData;
        [SerializeField] private MonsterReusableData _monsterReusableData;
        [SerializeField] private Collider2D _leftAttackColliderRange;
        [SerializeField] private Collider2D _rightAttackColliderRange;
        protected BaseMonsterRootState CurrentState => currentState as BaseMonsterRootState;
        private MonsterRootController _monsterRootController;
        public RootMonsterData RootMonsterData => _rootMonsterData;
        public MonsterReusableData MonsterReusableData => _monsterReusableData;
        public Collider2D LeftAttackColliderRange => _leftAttackColliderRange;
        public Collider2D RightAttackColliderRange => _rightAttackColliderRange;
        public AudioSource AudioSource => _audioSource;
        public void OnInit()
        {
            var states = _monsterRootController.gameObject.GetComponents<BaseMonsterRootState>().ToList();
            OnInit(states.Cast<IState<MonsterRootStateType>>().ToList());
        }
        public override void OnInit(List<IState<MonsterRootStateType>> states)
        {
            base.OnInit(states);
            _rootMonsterData.OnInit();
            ChangeState(MonsterRootStateType.Idling);
        }
        public virtual void OnPhysicUpdate()
        {
            CurrentState?.OnPhysicUpdate();
        }

        internal void BindController(MonsterRootController monsterRootController)
        {
            _monsterRootController = monsterRootController;
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

        internal void StartAnimation(int hashId)
        {
            _animator.SetBool(hashId, true);
        }

        internal void StopAnimation(int hashId)
        {
            _animator.SetBool(hashId, false);
        }
    }
}