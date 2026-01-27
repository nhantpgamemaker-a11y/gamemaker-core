namespace GameMaker.Core.Runtime
{
    [System.Serializable]
    public class BaseID
    {
        [UnityEngine.SerializeField]
        private string _id;
        public string ID => _id;
    }
}