using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using GameMaker.Core.Runtime;
using GameMaker.Extension.Runtime;
using GameMaker.UI.Runtime;
using TMPro;
using UnityEngine;

namespace GameMaker.Feature.Shop.Runtime
{
    public class UIShop : MonoBehaviour, Core.Runtime.IObserver<BasePlayerData>
    {
        [SerializeField] private BaseUI _rootUI;
        [SerializeField] private ShopID _shopID;
        [SerializeField] private TMP_Text _txtShopTitle;
        [SerializeField] private RectTransform _rectContent;
        [SerializeField] private UIShopItem _shopItemPrefab;
        [SerializeField] private UICountDown _uiCountDown;


        private PlayerShop _playerShop;
        private List<UIShopItem> _uIShopItems = new List<UIShopItem>();

        protected PlayerShop PlayerShop => _playerShop;
        protected IReadOnlyCollection<UIShopItem> UIShopItems => _uIShopItems.AsReadOnly();
        protected virtual void OnEnable()
        {
            _shopItemPrefab.gameObject.SetActive(false);
            _playerShop = _shopID.GetPlayerShop();
            _rootUI.OnShowAction += OnRootUIOnShow;
            _rootUI.OnHiddenAction += OnRootUIOnHidden;

            _txtShopTitle.text = _shopID.GetShopDefinition().GetTitle();
            _playerShop.AddObserver(this);
            _uiCountDown.Init(_playerShop.GetNextRefreshUTCTime());
        }

        protected virtual void OnDisable()
        {
            _playerShop.RemoveObserver(this);
            _rootUI.OnShowAction -= OnRootUIOnShow;
            _rootUI.OnHiddenAction -= OnRootUIOnHidden;
        }
        private void OnRootUIOnShow()
        {
            for (int i = 0; i < PlayerShop.PlayerShopItems.Count; i++)
            {
                var uiShopItem = Instantiate(_shopItemPrefab, _rectContent);
                uiShopItem.gameObject.SetActive(true);
                uiShopItem.OnInit(PlayerShop.PlayerShopItems[i], OnPurchaseItemAsync);
                _uIShopItems.Add(uiShopItem);
            }
        }

        private async UniTask<bool> OnPurchaseItemAsync(string itemReferenceId)
        {
            return await ShopGateway.Manager.PurchaseAsync(_shopID.ID, itemReferenceId, new ShopExtendData("SHOP"));
        }

        private void OnRootUIOnHidden()
        {
            foreach (var uiShopItem in _uIShopItems)
            {
                uiShopItem.OnClear();
                Destroy(uiShopItem.gameObject);
            }
            _uIShopItems.Clear();
        }

        public void OnNotify(ISubject<BasePlayerData> subject, BasePlayerData data)
        {
            _uiCountDown.Init(_playerShop.GetNextRefreshUTCTime());
        }
    }
}
