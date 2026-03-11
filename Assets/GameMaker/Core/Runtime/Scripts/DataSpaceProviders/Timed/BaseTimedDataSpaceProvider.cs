using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Codice.Client.Commands;
using Cysharp.Threading.Tasks;
using GameMaker.Core.Runtime;
using UnityEngine;

namespace GameMaker.Core.Runtime
{
    public abstract class BaseTimedDataSpaceProvider : IDataSpaceProvider
    {
        public virtual void Dispose()
        {
        }

        public async virtual UniTask<bool> InitAsync(BaseDataSpaceSetting baseDataSpaceSetting)
        {
            return true;
        }

        public abstract UniTask<(bool, List<PlayerTimed>)> GetPlayerTimedAsync();

        public abstract UniTask<(bool, PlayerTimed)> AddTimedAsync(string refId, long amount);
    }
}