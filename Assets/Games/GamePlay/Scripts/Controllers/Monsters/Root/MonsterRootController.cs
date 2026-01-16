using System;
using GamePlay.Game;
using UnityEngine;

namespace Game.GamePlay
{
    public class MonsterRootController: MonoBehaviour, ITakeDame
    {
        [SerializeField] private MonsterRootStateMachine _monsterStateMachine;
         
         public void Start()
        {
            OnInitState();
        }
        public void FixedUpdate()
        {
            _monsterStateMachine.OnPhysicUpdate();
        }
        private void OnInitState()
        {
            _monsterStateMachine.BindController(this);
            _monsterStateMachine.OnInit();
        }
         #region  Exposed Method
        public void OnAnimationStartEvent()
        {
            _monsterStateMachine.OnAnimationStartEventHandle();
        }
        public void OnAnimationTransitionEvent()
        {
            _monsterStateMachine.OnAnimationTransitionEventHandle();
        }
        public void OnAnimationEndEvent()
        {
            _monsterStateMachine.OnAnimationEndEventHandle();
        }

        public void TakeDame(float amount, Vector3 direction, Vector3 position)
        {
            if (_monsterStateMachine.MonsterReusableData.Hp < 0) return;
            _monsterStateMachine.ChangeState(MonsterRootStateType.TakeDame, new DameStateData(amount, direction, position));
        }
        #endregion
    }
}