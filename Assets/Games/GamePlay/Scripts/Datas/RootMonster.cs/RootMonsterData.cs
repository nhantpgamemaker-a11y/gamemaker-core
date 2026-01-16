using UnityEngine;

namespace GamePlay.Game
{
    [System.Serializable]
    public class RootMonsterData
    {
        [UnityEngine.SerializeField]
        private RootAnimationData _rootAnimationData;
         [UnityEngine.SerializeField]
        private LayerMask _playerLayerMask;
        public RootAnimationData RootAnimationData => _rootAnimationData;
        public LayerMask PlayerLayerMask => _playerLayerMask;

        public void OnInit()
        {
            _rootAnimationData.OnInit();
        }
    }
    [System.Serializable]
    public class RootAnimationData
    {
        [UnityEngine.SerializeField]
        private string _idlingAnimationName = "Idle";
        [UnityEngine.SerializeField]
        private string _leftAttackAnimationName = "LeftAttack";
        [UnityEngine.SerializeField]
        private string _rightAttackAnimationName = "RightAttack";

        public int IdlingAnimationHash { get; private set; }
        public int LeftAttackAnimationHash { get; private set; }
        public int RightAttachAnimationHash { get; private set; }
        public void OnInit()
        {
            IdlingAnimationHash = Animator.StringToHash(_idlingAnimationName);
            LeftAttackAnimationHash = Animator.StringToHash(_leftAttackAnimationName);
            RightAttachAnimationHash = Animator.StringToHash(_rightAttackAnimationName);
        }
    }
}