using Game.GamePlay;
using GameMaker.Core.Runtime;
using UnityEngine;

namespace GamePlay.Game
{
    public class RightAttackMonsterRootState : AttackMonsterRootState
    {
        public override Collider2D GetCollider2D()
        {
            return monsterRootStateMachine.RightAttackColliderRange;
        }

        public override MonsterRootStateType GetStateType()
        {
            return MonsterRootStateType.RightAttack;
        }
        public override void OnEnterState(BaseStateData baseStateData = null)
        {
            base.OnEnterState(baseStateData);
            StartAnimation(monsterRootStateMachine.RootMonsterData.RootAnimationData.RightAttachAnimationHash);
        }
        public override void OnExitState()
        {
            base.OnExitState();
            StopAnimation(monsterRootStateMachine.RootMonsterData.RootAnimationData.RightAttachAnimationHash);
        }
    }
}