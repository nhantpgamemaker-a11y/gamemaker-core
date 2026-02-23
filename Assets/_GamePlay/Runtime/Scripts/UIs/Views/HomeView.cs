using System;
using Cysharp.Threading.Tasks;
using GameMaker.Core.Runtime;
using GameMaker.Sound.Runtime;
using GameMaker.UI.Runtime;
using UnityEngine;
using UnityEngine.UI;

namespace CatAdventure.GamePlay
{
    public class HomeView : BaseView
    {
        public const string VIEW_NAME = "HomeView";
        [SerializeField] private UICurrency[] _uiCurrencies;
        [SerializeField] private Button _playButton;
        [SerializeField] private CurrencyID _currencyID;
        [SerializeField] private SoundID _soundID;
        protected override void OnShow()
        {
            base.OnShow();
            foreach (var currency in _uiCurrencies)
            {
                currency.Init();
            }
            _playButton.onClick.AddListener(OnClickPlay);
            SoundRuntimeManager.Instance.PlayLoopFade(_soundID.GetSoundDefinition());
        }
        protected override void OnHide()
        {
            base.OnHide();
            _playButton.onClick.RemoveListener(OnClickPlay);
            SoundRuntimeManager.Instance.StopLoopFade(_soundID.GetSoundDefinition());
        }

        private void OnClickPlay()
        {
            viewController.ShowAsync(MapView.VIEW_NAME,ViewShowType.Before).Forget();
        }

        protected override void OnHidden()
        {
            base.OnHidden();
            foreach (var currency in _uiCurrencies)
            {
                currency.Clear();
            }
        }

        [ContextMenu("Add PlayerCurrency")]
        private void AddPlayerCurrency()
        {
            _currencyID.AddCurrencyAsync("100",null).Forget();
        }
    }
}