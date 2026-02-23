using System;
using Cysharp.Threading.Tasks;
using GameMaker.Core.Runtime;
using GameMaker.UI.Runtime;
using LitMotion;
using LitMotion.Extensions;
using UnityEngine;
using UnityEngine.UI;

namespace CatAdventure.GamePlay
{
    public class BootView : BaseView
    {
        public const string VIEW_NAME = "BootView";
        [SerializeField] private RectTransform _rectFill;
        [SerializeField] private RectTransform _rectTargetFillSize;
        [SerializeField] private float _safeFillDuration = 1f;
        [SerializeField] private Button _btnPlay;
        [SerializeField] private RectTransform _rectSlider;
        [SerializeField] private float _waitingTimeShowPlayButton = 0.3f;

        private BootStepController _bootStepController;
        private MotionHandle _fillMotionHandle;
        protected override void OnShow()
        {
            base.OnShow();
            _rectFill.sizeDelta = new Vector2(0f, _rectTargetFillSize.rect.size.y);
            _bootStepController = data as BootStepController;

            _bootStepController.AddProgressEvent(OnProgressHandler);
            _bootStepController.AddFailBootStepEvent(OnFailBootStepHandlerAsync);

            _btnPlay.gameObject.SetActive(false);
            _btnPlay.onClick.AddListener(OnClickPlay);
        }

        protected override void OnHide()
        {
            base.OnHide();
            _bootStepController.RemoveProgressEvent(OnProgressHandler);
            _bootStepController.RemoveFailBootStepEvent(OnFailBootStepHandlerAsync);

            _btnPlay.onClick.RemoveListener(OnClickPlay);
        }

        private void OnClickPlay()
        {
            viewController.ShowAsync(HomeView.VIEW_NAME).Forget();
            _btnPlay.onClick.RemoveListener(OnClickPlay);
        }

        private async UniTask PlayFillAnimation(float targetFillSize)
        {
            if (_fillMotionHandle.IsActive())
            {
                _fillMotionHandle.Cancel();
            }

            _fillMotionHandle = LMotion
                .Create(
                    _rectFill.rect.size,
                    new Vector2(targetFillSize, _rectFill.sizeDelta.y),
                    _safeFillDuration
                )
                .BindToSizeDelta(_rectFill);

            await _fillMotionHandle.ToUniTask(this.GetCancellationTokenOnDestroy());
        }

        private void OnProgressHandler(float clamp01)
        {
            PlayFillAnimation(clamp01 * _rectTargetFillSize.rect.size.x).Forget();
            if (clamp01 == 1)
            {
                WaitAndShowButtonAsync().Forget();
            }
        }

        private async UniTask OnFailBootStepHandlerAsync(FailBootStepData data)
        {
            await UniTask.Yield();
        }

        private async UniTask WaitAndShowButtonAsync()
        {
            await UniTask.Yield(this.GetCancellationTokenOnDestroy());
            await _fillMotionHandle.ToUniTask(this.GetCancellationTokenOnDestroy());
            await UniTask.WaitForSeconds(_waitingTimeShowPlayButton, cancellationToken: this.GetCancellationTokenOnDestroy());
            HideSlider();
            ShowPlayButton();
        }
        
        private void ShowPlayButton()
        {
            _btnPlay.gameObject.SetActive(true);
        }
        private void HideSlider()
        {
            _rectSlider.gameObject.SetActive(false);
        }
    }
}
