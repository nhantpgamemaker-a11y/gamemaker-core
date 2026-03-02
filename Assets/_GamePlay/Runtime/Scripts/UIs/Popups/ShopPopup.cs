using System;
using Cysharp.Threading.Tasks;
using GameMaker.Core.Runtime;
using GameMaker.UI.Runtime;
using UnityEngine;
using UnityEngine.UI;

namespace CatAdventure.GamePlay
{
    public class ShopPopup : BasePopup
    {
        public const string POPUP_NAME = "ShopPopup";
        [SerializeField] private Button _btnClose;
        protected override void OnShow()
        {
            base.OnShow();
        }
        protected override void OnShown()
        {
            base.OnShown();
            _btnClose.onClick.AddListener(OnClickClose);
        }
        protected override void OnHide()
        {
            base.OnHide();
            _btnClose.onClick.RemoveListener(OnClickClose);
        }
        private void OnClickClose()
        {
            popupController.HideAsync(this).Forget();
        }
    }
}
