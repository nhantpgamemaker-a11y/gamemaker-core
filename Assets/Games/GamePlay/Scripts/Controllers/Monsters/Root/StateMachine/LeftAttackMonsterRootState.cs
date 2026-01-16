using Game.GamePlay;
using GameMaker.Core.Runtime;
using UnityEngine;

namespace GamePlay.Game
{
    public class LeftAttackMonsterRootState : AttackMonsterRootState
    {
        public override Collider2D GetCollider2D()
        {
            return monsterRootStateMachine.LeftAttackColliderRange;
        }

        public override MonsterRootStateType GetStateType()
        {
            return MonsterRootStateType.LeftAttack;
        }
        public override void OnEnterState(BaseStateData baseStateData = null)
        {
            base.OnEnterState(baseStateData);
            StartAnimation(monsterRootStateMachine.RootMonsterData.RootAnimationData.LeftAttackAnimationHash);
        }
        public override void OnExitState()
        {
            base.OnExitState();
            StopAnimation(monsterRootStateMachine.RootMonsterData.RootAnimationData.LeftAttackAnimationHash);
        }
    }
}