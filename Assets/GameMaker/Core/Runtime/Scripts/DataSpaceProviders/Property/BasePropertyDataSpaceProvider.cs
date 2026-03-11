using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;

namespace GameMaker.Core.Runtime
{
    public abstract class BasePropertyDataSpaceProvider : IDataSpaceProvider
    {
        public virtual async UniTask<bool> InitAsync(BaseDataSpaceSetting baseDataSpaceSetting)
        {
            return true;
        }
        public virtual void Dispose()
        {
        }
        public abstract UniTask<bool> AddAsync(string id, string value);
        public abstract UniTask<bool> SetAsync(string id, string value);
        public abstract UniTask<(bool, List<PlayerProperty>)> GetPlayerPropertiesAsync();
    }
}