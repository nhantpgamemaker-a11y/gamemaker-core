using System;
using System.Reflection;
using Cysharp.Threading.Tasks;

namespace GameMaker.Core.Runtime
{
    [System.Serializable]
    public class BaseRuntimeDataManager
    {
        public BaseRuntimeDataManager()
        {
            
        }
        public virtual async UniTask<bool> InitializeAsync(IDataSpaceProvider[] dataSpaceProviders,PlayerDataManager[] playerDataManagers)
        {
            return true;
        }
    }
}