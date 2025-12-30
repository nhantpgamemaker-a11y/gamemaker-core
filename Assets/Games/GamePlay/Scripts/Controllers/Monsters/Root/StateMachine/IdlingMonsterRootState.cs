using GameMaker.Core.Runtime;
using UnityEngine;

namespace Game.GamePlay
{
    public class IdlingMonsterRootState : BaseMonsterRootState
    {
        public override MonsterRootStateType GetStateType()
        {
            return MonsterRootStateType.Idling;
        }
        public override void OnEnterState(BaseStateData baseStateData = null)
        {
            base.OnEnterState(baseStateData);
            StartAnimation(monsterRootStateMachine.RootMonsterData.RootAnimationData.IdlingAnimationHash);
        }
        public override void OnPhysicUpdate()
        {
            ContactFilter2D filter = new ContactFilter2D();
            filter.useLayerMask = true;
            filter.layerMask = monsterRootStateMachine.RootMonsterData.PlayerLayerMask;

            Collider2D[] contactColliders = new Collider2D[1];
            int contactAmount = Physics2D.OverlapCollider(monsterRootStateMachine.LeftAttackColliderRange, filter, contactColliders);
            if (contactAmount != 0)
            {
                monsterRootStateMachine.ChangeState(MonsterRootStateType.LeftAttack);
                return;
            }
            contactAmount = Physics2D.OverlapCollider(monsterRootStateMachine.RightAttackColliderRange, filter, contactColliders);
            if (contactAmount != 0)
            {
                monsterRootStateMachine.ChangeState(MonsterRootStateType.RightAttack);
            }
        }
        public override void OnExitState()
        {
            base.OnExitState();
            StopAnimation(monsterRootStateMachine.RootMonsterData.RootAnimationData.IdlingAnimationHash);
        }
    }
}