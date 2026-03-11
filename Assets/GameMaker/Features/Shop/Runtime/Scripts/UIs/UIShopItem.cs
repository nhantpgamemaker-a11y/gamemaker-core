using System;
using Cysharp.Threading.Tasks;
using GameMaker.Core.Runtime;
using GameMaker.Extension.Runtime;
using GameMaker.UI.Runtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameMaker.Feature.Shop.Runtime
{
    public class UIShopItem : MonoBehaviour,Core.Runtime.IObserver<BasePlayerData>
    {
        [SerializeField] private GameObject _activeObject;
        [SerializeField] private GameObject _deActiveObject;
        [SerializeField] private BaseUI _rootUI;
        [SerializeField] private TMP_Text _txtAmount;
        [SerializeField] private Image _imgIcon;
        [SerializeField] private TMP_Text _txtPrice;
        [SerializeField] private Image _imgPriceIcon;
        [SerializeField] private TMP_Text _txtTitle;
        [SerializeField] private TMP_Text _txtDescription;

        [SerializeField] private UIButtonAsync _btnBuy;
        private BasePlayerShopItem _playerShopItem;
        private BaseShopItemDefinition _shopItemDefinition;
        protected BasePlayerShopItem playerShopItem => _playerShopItem;
        protected BaseShopItemDefinition shopItemDefinition => _shopItemDefinition;
        protected BaseUI rootUI { get => _rootUI; }
        protected TMP_Text txtRemain { get => _txtAmount;}
        protected Image imgIcon { get => _imgIcon;  }
        protected TMP_Text txtPrice { get => _txtPrice; }
        protected Image imgPriceIcon { get => _imgPriceIcon;  }
        protected TMP_Text txtTitle { get => _txtTitle; }
        protected TMP_Text txtDescription { get => _txtDescription; }
        protected UIButtonAsync btnBuy { get => _btnBuy; }

        private bool _isBuying = false;
        private Func<string, UniTask<bool>> _onPurchaseFunc;
        public virtual void OnInit(BasePlayerShopItem playerShopItem, Func<string, UniTask<bool>> OnPurchaseFunc = null)
        {
            _playerShopItem = playerShopItem;
            _onPurchaseFunc = OnPurchaseFunc;
            _shopItemDefinition = playerShopItem.GetBaseShopItemDefinition();
            _btnBuy.AddListenerAsync(OnBuyButtonClickAsync);
            _playerShopItem.AddObserver(this);
            UpdateUI();
        }
        public virtual void OnClear()
        {
            _playerShopItem.RemoveObserver(this);
            _btnBuy.RemoveListenerAsync(OnBuyButtonClickAsync);
            _playerShopItem = null;
        }

        public virtual void OnNotify(ISubject<BasePlayerData> subject, BasePlayerData data)
        {
            UpdateUI();
        }
        protected virtual void UpdateUI()
        {
            if (_txtAmount != null)
                _txtAmount.text = _shopItemDefinition.GetStringAmount();
            if (_imgIcon != null)
                _imgIcon.sprite = shopItemDefinition.GetIcon();
            if (_txtPrice != null)
                _txtPrice.text = shopItemDefinition.Price.GetAmount().ToString();
            if (_imgPriceIcon != null)
                _imgPriceIcon.sprite = shopItemDefinition.Price.GetCurrencyDefinition().GetIcon();
            if (_txtTitle != null)
                _txtTitle.text = shopItemDefinition.GetTitle();
            if (_txtDescription != null)
                _txtDescription.text = shopItemDefinition.GetDescription();

            _activeObject.gameObject.SetActive(playerShopItem.CanPurchase);
            _deActiveObject.gameObject.SetActive(!playerShopItem.CanPurchase);
        }

        public virtual async UniTask OnBuyButtonClickAsync()
        {
            if (_isBuying)
                return;
            if (!playerShopItem.CanPurchase)
                return;
            await PurchaseAsync();
        }

        private async UniTask<bool> PurchaseAsync()
        {
            _isBuying = true;
            bool status = false;
            if (_onPurchaseFunc != null)
            {
                status = await _onPurchaseFunc.Invoke(playerShopItem.GetID());
                if (status)
                {
                    rootUI.UIController.AlertController.ShowAsync("Purchase Successful").Forget();

                }
                else
                {
                    rootUI.UIController.AlertController.ShowAsync("Purchase Failed").Forget();
                }
            }
            _isBuying = false;
            return status;
        }
    }
}