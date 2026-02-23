using System;
using Cysharp.Threading.Tasks;
using GameMaker.UI.Runtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CatAdventure.GamePlay
{
    public class PlayPopupData
    {
        private int _level;
        public int Level => _level;
        public PlayPopupData(int level)
        {
            _level = level;
        }
    }
    public class PlayPopup : BasePopup
    {
        public const string POPUP_NAME = "PlayPopup";
        [SerializeField] private Button _btnClose;
        [SerializeField] private Button _btnPlay;
        [SerializeField] private TMP_Text _txtHeader;

        protected override void OnShow()
        {
            base.OnShow();
            _txtHeader.text = (data as PlayPopupData).Level.ToString()+" - LEVEL";
        }
        protected override void OnShown()
        {
            base.OnShown();
            _btnClose.onClick.AddListener(OnClickClose);
            _btnPlay.onClick.AddListener(OnClickPlay);
        }
        protected override void OnHide()
        {
            base.OnHide();
            _btnClose.onClick.RemoveListener(OnClickClose);
            _btnPlay.onClick.RemoveListener(OnClickPlay);
        }

        private void OnClickPlay()
        {
            popupController.UIController.ViewController.ShowAsync(GameView.VIEW_NAME).Forget();
            popupController.HideAsync(this).Forget();
        }

        private void OnClickClose()
        {
            popupController.HideAsync(this).Forget();
        }
    }
}
