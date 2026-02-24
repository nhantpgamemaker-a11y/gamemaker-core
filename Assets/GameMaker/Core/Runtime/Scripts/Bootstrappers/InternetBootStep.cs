using Cysharp.Threading.Tasks;
using UnityEngine;

namespace GameMaker.Core.Runtime
{
    public class InternetBootStep : BaseBootStep
    {
        protected async override UniTask<bool> PerformBootAsync()
        {
            var i = InternetService.Instance;
            var status = await i.CheckInternet();
            if (status)
            {
                i.StartChecking().Forget();
            }
            return status;
        }
    }
}
