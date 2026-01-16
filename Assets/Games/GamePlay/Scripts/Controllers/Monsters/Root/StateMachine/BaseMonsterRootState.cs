using System;
using GameMaker.Core.Runtime;
using GamePlay.Game;
using UnityEngine;

namespace Game.GamePlay
{
    public abstract class BaseMonsterRootState : MonoBehaviour, IState<MonsterRootStateType>
    {
        public abstract MonsterRootStateType GetStateType();
        protected MonsterRootStateMachine monsterRootStateMachine;

        public virtual void OnEnterState(BaseStateData baseStateData = null)
        {

        }
        public virtual void OnPhysicUpdate()
        {
            
        }

        public virtual void OnExitState()
        {
           
        }

        public void SetStateMachine(IStateMachine<MonsterRootStateType> stateMachine)
        {
            monsterRootStateMachine = stateMachine as MonsterRootStateMachine;
        }

       
        internal protected virtual void OnAnimationStartEventHandle()
        {
            
        }

        internal protected virtual void OnAnimationTransitionEventHandle()
        {
            
        }

        internal protected virtual void OnAnimationEndEventHandle()
        {
            monsterRootStateMachine.ChangeState(MonsterRootStateType.Idling);
        }
        public void StartAnimation(int hashId)
        {
            monsterRootStateMachine.StartAnimation(hashId);
        }
        public void StopAnimation(int hashId)
        {
            monsterRootStateMachine.StopAnimation(hashId);
        }
    }
}