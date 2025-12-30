namespace Game.GamePlay
{
    [System.Serializable]
    public class LayerData
    {
        [UnityEngine.SerializeField]
        private UnityEngine.LayerMask _groundLayer;
        public UnityEngine.LayerMask GroundLayer => _groundLayer;

         [UnityEngine.SerializeField]
        private UnityEngine.LayerMask _monsterLayer;
        public UnityEngine.LayerMask MonsterLayer => _monsterLayer;
    }
}