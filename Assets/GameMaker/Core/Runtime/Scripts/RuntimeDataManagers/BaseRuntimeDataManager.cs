using System;
using System.Reflection;
using Cysharp.Threading.Tasks;

namespace GameMaker.Core.Runtime
{
    [System.Serializable]
    [TypeCache]
    public abstract class BaseRuntimeDataManager
    {
        public virtual async UniTask<bool> InitializeAsync(IDataSpaceProvider[] dataSpaceProviders,PlayerDataManager[] playerDataManagers)
        {
            return true;
        }
    }
}