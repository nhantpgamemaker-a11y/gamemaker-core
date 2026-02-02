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
        public abstract UniTask<bool> AddStatAsync(string id, long value);

        public abstract UniTask<bool> SetAttributeAsync(string id, string value);
        public abstract UniTask<(bool, List<PlayerProperty>)> GetPlayerPropertiesAsync();
        public abstract UniTask<bool> SetStatAsync(string id, long value);

        
    }
}