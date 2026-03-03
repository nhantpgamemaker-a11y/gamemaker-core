using System;
using Cysharp.Threading.Tasks;
using GameMaker.Extension.Runtime;
using GameMaker.UI.Runtime;
using UnityEngine;

namespace CatAdventure.GamePlay
{
    [RequireComponent(typeof(UIButtonAsync))]
    public class UISettingButton : MonoBehaviour
    {
        [SerializeField] private BaseUI _uiRoot;
        [SerializeField] private UIButtonAsync _button;
        public void OnEnable()
        {
            _button.AddListenerAsync(OnClickAsync);
        }
        public void OnDisable()
        {
            _button.RemoveListenerAsync(OnClickAsync);
        }

        private async UniTask OnClickAsync()
        {
            await _uiRoot.UIController.PopupController.ShowAsync<SettingPopup>(SettingPopup.POPUP_NAME);
        }
    }
}
