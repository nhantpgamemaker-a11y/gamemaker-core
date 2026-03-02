using System;
using Cysharp.Threading.Tasks;
using GameMaker.Core.Runtime;
using GameMaker.IAP.Runtime;
using GameMaker.Sound.Runtime;
using GameMaker.UI.Runtime;
using UnityEngine;
using UnityEngine.UI;

namespace CatAdventure.GamePlay
{
    public class HomeView : BaseView
    {
        public const string VIEW_NAME = "HomeView";
        [SerializeField] private Button _playButton;
        [SerializeField] private CurrencyID _currencyID;
        [SerializeField] private SoundID _soundID;
        [SerializeField] private IAPGroupDefinitionID _iAPGroupID;
        [SerializeField] private IAPDefinitionID _iAPID;
        [SerializeField] private TimedID _timedID;
        [SerializeField] private PropertyID _propertyID;
        [SerializeField] private ConfigID _configID;
        [SerializeField] private ConditionID _conditionID;
        protected override void OnShow()
        {
            base.OnShow();
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
        }

        [ContextMenu("Add PlayerCurrency")]
        private void AddPlayerCurrency()
        {
            _currencyID.AddCurrencyAsync(100, null).Forget();
        }
        [ContextMenu("Add PlayerProperty")]
        private void AddPlayerProperty()
        {
            _propertyID.SetPropertyAsync(((_propertyID.GetPlayerProperty() as PlayerStat).Value +1).ToString(), null).Forget();
        }
    }
}