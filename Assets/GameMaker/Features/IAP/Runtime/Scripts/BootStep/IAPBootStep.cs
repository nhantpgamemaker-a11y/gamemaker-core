using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using GameMaker.Core.Runtime;
using UnityEngine;

namespace GameMaker.IAP.Runtime
{
    public class IAPBootStep : BaseBootStep
    {
        protected override async UniTask<bool> PerformBootAsync()
        {
            bool status = await IAPRuntimeManager.Instance.InitAsync();
            return status;
        }
    }
}
