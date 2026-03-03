using Cysharp.Threading.Tasks;
using Unity.Services.Core;
using UnityEngine;

namespace GameMaker.Core.Runtime
{
    public class UnityServiceBootStep : BaseBootStep
    {
        protected override async UniTask<bool> PerformBootAsync()
        {
            await UnityServices.InitializeAsync().AsUniTask();
            return true;
        }
    }
}
