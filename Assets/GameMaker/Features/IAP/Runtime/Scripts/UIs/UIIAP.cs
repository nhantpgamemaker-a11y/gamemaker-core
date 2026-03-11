using System;
using Cysharp.Threading.Tasks;
using GameMaker.Extension.Runtime;
using GameMaker.UI.Runtime;
using TMPro;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.UI;

namespace GameMaker.IAP.Runtime
{
    [DefaultExecutionOrder(99)]
    public class UIIAP : MonoBehaviour
    {
        [SerializeField] private BaseUI _rootUI;
        [SerializeField] private IAPDefinitionID _iapDefinitionID;
        [SerializeField] private Image _imgIcon;
        [SerializeField] private TMP_Text _txtTitle;
        [SerializeField] private TMP_Text _txtDescription;
        [SerializeField] private TMP_Text _txtPrice;
        [SerializeField] private UIButtonAsync _btnBuy;
        [SerializeField] private UIBundle _uiBundle;
        private IAPDefinition _iapDefinition;
        private Product _product;
        protected UIBundle uIBundle => _uiBundle;
        protected Product product => _product;
        public void SetIAPDefinitionID(IAPDefinitionID iapDefinitionID)
        {
            _iapDefinitionID = iapDefinitionID;
            _iapDefinition = _iapDefinitionID.GetIAPDefinition();
        }
        public void SetIAPDefinition(IAPDefinition iapDefinition)
        {
            _iapDefinition = iapDefinition;
        }

        protected virtual void OnEnable()
        {
            var definition = _iapDefinitionID.GetIAPDefinition();
            if (definition == null)
            {
                definition = _iapDefinition;
            }
            if (definition == null) return;
            
            _uiBundle?.SetBundleDefinition(definition.Reward);
            _product = IAPRuntimeManager.Instance.GetProductByDefinitionID(definition.GetID());
            if (_product == null) return;
            if (_txtPrice != null)
            {
                _txtPrice.text = _product.metadata.localizedPrice + _product.metadata.isoCurrencyCode;
            }
            if (_txtTitle != null)
            {
                _txtTitle.text = definition.GetTitle();
            }
            if(_imgIcon!=null)
            {
                _imgIcon.sprite = definition.GetIcon();
            }
            _btnBuy.AddListenerAsync(OnBuyButtonClickAsync);
        }
        protected virtual void OnDisable()
        {
            if (_iapDefinition == null) return;
            _btnBuy.RemoveListenerAsync(OnBuyButtonClickAsync);
        }

        private async UniTask OnBuyButtonClickAsync()
        {
            IAPRuntimeManager.Instance.PurchaseAsync(_iapDefinition.GetID()).Forget();
        }
    }
}