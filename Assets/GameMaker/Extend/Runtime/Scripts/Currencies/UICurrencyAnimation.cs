using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace GameMaker.Core.Runtime
{
    public class UILongCurrencyAnimation : UICurrency,IUICollection
    {
        [UnityEngine.SerializeField] private BaseCollectionAnimationController _collectionAnimationController;
        [SerializeField] protected RectTransform _initAnimationRect;
        [SerializeField] protected RectTransform _targetAnimationRect;
        private long _currentAmount;
        private long _animationCurrencyAmount;
        private long _currentActive = 0;
        public long CurrentActive => _currentActive;
        public long CurrentAmount { get => _currentAmount; }
        public long AnimationCurrencyAmount { get => _animationCurrencyAmount; }

        
        public override void Init()
        {
            base.Init();
            UICollectionAnimation.Instance.Add(currencyID.ID, this);
            _currentAmount = (long)playerCurrency.GetValue();
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
            
            long gap = (long)playerCurrency.GetValue() - _currentAmount;
            if (gap < 0)
            {
                _animationCurrencyAmount += gap;
                _currentAmount += gap;
                UpdateUI(playerCurrency);
            }
            else
            {
                _currentActive++;
                _currentAmount = (long)playerCurrency.GetValue();
                var currencyDefinition = playerCurrency.GetDefinition() as BaseCurrencyDefinition;
                var task = _collectionAnimationController.PlayAsync(currencyDefinition.GetIcon(), gap, _initAnimationRect.position, _targetAnimationRect, OnCollectionAction);
                await task;
                _currentActive--;
            }
        }
        public void OnCollectionAction(long amount)
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
                txtAmount.text = playerCurrency.GetValue().ToString();
                _animationCurrencyAmount = (long)playerCurrency.GetValue();
            }
            else
            {
                PlayAnimationAsync(playerCurrency).Forget();
            }
        }
        #endregion
    }
}
