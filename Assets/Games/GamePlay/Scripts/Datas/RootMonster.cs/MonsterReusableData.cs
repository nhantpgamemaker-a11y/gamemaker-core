namespace GamePlay.Game
{
    [System.Serializable]
    public class MonsterReusableData
    {
        [UnityEngine.SerializeField]
        private float _hp = 100f;
        public float Hp
        {
            get => _hp;
            set => _hp = value;
        }
    }
}