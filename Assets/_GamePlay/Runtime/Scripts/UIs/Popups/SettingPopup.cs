using Cysharp.Threading.Tasks;
using GameMaker.Extension.Runtime;
using GameMaker.UI.Runtime;
using UnityEngine;

namespace CatAdventure.GamePlay
{
    public class SettingPopup : BasePopup
    {
        public const string POPUP_NAME = "SettingPopup";
        [SerializeField] private UIButtonAsync _btnClose;

        protected override void OnShown()
        {
            base.OnShown();
            _btnClose.AddListenerAsync(OnCloseButtonClickedAsync);
        }
        protected override void OnHide()
        {
            base.OnHide();
            _btnClose.RemoveListenerAsync(OnCloseButtonClickedAsync);
        }
        private async UniTask OnCloseButtonClickedAsync()
        {
            await popupController.HideAsync(this);
        }
    }
}
