using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace GameMaker.Core.Runtime
{
    public class UICurrencyAnimation : UICurrency,IUICollection
    {
        [UnityEngine.SerializeField] private BaseCollectionAnimationController _collectionAnimationController;
        [SerializeField] protected RectTransform _initAnimationRect;
        [SerializeField] protected RectTransform _targetAnimationRect;
        private float _currentAmount;
        private float _animationCurrencyAmount;
        private float _currentActive = 0;
        public float CurrentActive => _currentActive;
        public float CurrentAmount { get => _currentAmount;}
        public float AnimationCurrencyAmount { get => _animationCurrencyAmount; }

        protected override void OnValidate()
        {
            this.gameObject.name = $"UICurrencyAnimation-{currencyID.GetBaseCurrencyDefinition().GetName()}";
        }
        public override void Init()
        {
            base.Init();
            UICollectionAnimation.Instance.Add(currencyID.ID, this);
            _currentAmount = float.Parse(playerCurrency.Value);
            _animationCurrencyAmount = _currentAmount;
            _currentActive = _currentAmount;
            UpdateUI(playerCurrency);
        }
        public override void Clear()
        {
            base.Clear();
            UICollectionAnimation.Instance.Remove(currencyID.ID,this);
        }
        public async UniTask PlayAnimationAsync(BasePlayerCurrency playerCurrency)
        {
            float gap = float.Parse(playerCurrency.Value) - _currentAmount;
            if (gap < 0)
            {
                _animationCurrencyAmount -= gap;
                UpdateUI(playerCurrency);
            }
            else
            {
                _currentActive++;
                _currentAmount = float.Parse(playerCurrency.Value);
                var currencyDefinition = playerCurrency.GetDefinition() as BaseCurrencyDefinition;
                var task = _collectionAnimationController.PlayAsync(currencyDefinition.GetIcon(), gap, _initAnimationRect.position, _targetAnimationRect, OnCollectionAction);
                await task;
                _currentActive--;
            }
        }
        public void OnCollectionAction(float amount)
        {
            _animationCurrencyAmount += amount;
            UpdateUI(playerCurrency);
        }
        protected override void UpdateUI(BasePlayerCurrency playerCurrency)
        {
            txtAmount.text = _animationCurrencyAmount.ToString();
        }
        #region IObserver<BasePlayerData>
        public override void OnNotify(ISubject<BasePlayerData> subject, BasePlayerData data)
        {
            if (!UICollectionAnimation.Instance.IsLast(currencyID.ID, this))
            {
                txtAmount.text = playerCurrency.Value.ToString();
                _animationCurrencyAmount = float.Parse(playerCurrency.Value);
            }
            else
            {
                PlayAnimationAsync(playerCurrency).Forget();
            }
        }
        #endregion
    }
}
