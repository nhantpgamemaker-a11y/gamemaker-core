using System;
using Cysharp.Threading.Tasks;
using GameMaker.Extension.Runtime;
using UnityEngine;

namespace GameMaker.IAP.Runtime
{

    public class UIRestoreButton : MonoBehaviour
    {
        [UnityEngine.SerializeField]
        private UIButtonAsync _button;
        private void OnEnable()
        {
            _button.AddListenerAsync(OnClickAsync);
        }
        private void OnDisable()
        {
            _button.AddListenerAsync(OnClickAsync);
        }

        private async UniTask OnClickAsync()
        {
            await IAPRuntimeManager.Instance.RestorePurchases();
        }
    }
}
