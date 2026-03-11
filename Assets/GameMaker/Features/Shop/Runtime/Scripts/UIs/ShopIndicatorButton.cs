using System;
using Cysharp.Threading.Tasks;
using GameMaker.UI.Runtime;
using UnityEngine;
using UnityEngine.UI;

namespace GameMaker.Feature.Shop.Runtime
{
    public class ShopIndicatorButton : MonoBehaviour
    {
        [SerializeField] private BaseUI _uiRoot;
        [SerializeField] private string _shopNamePopup = "ShopPopup";
        [SerializeField] private Button _btn;
        void OnEnable()
        {
            _btn.onClick.AddListener(OnClick);
        }
        void OnDisable()
        {
            _btn.onClick.RemoveListener(OnClick);
        }

        private void OnClick()
        {
            _uiRoot.UIController.PopupController.ShowAsync<BasePopup>(_shopNamePopup).Forget();
        }
    }
}
