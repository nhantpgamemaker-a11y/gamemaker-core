using Cysharp.Threading.Tasks;

namespace GameMaker.Core.Runtime
{
    [System.Serializable]
    public abstract class BaseLocalData
    {
        private LocalDataManager _localDataManger;
        internal void SetManager(LocalDataManager localDataManager)
        {
            _localDataManger = localDataManager;
        }
        internal virtual void OnCreate()
        {

        }
        internal virtual void OnLoad()
        {

        }
        public async UniTask SaveAsync()
        {
            await _localDataManger.SaveAsync(this.GetType());
        }
    }
}
