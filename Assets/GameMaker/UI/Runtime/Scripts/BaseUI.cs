
using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace GameMaker.UI.Runtime
{
    public abstract class BaseUI : MonoBehaviour
    {
        [SerializeField] protected RectTransform overlay;
        [SerializeField] protected RectTransform container;
        [SerializeField] private BaseUIAnimation _uiAnimation;
        private UIController _uIController;
        public UIController UIController => _uIController;

        public void SetUIController(UIController uIController)
        {
            _uIController = uIController;
        }
        protected object data = null;

        public event Action OnShowAction;
        public event Action OnHideAction;
        public event Action OnShownAction;
        public event Action OnHiddenAction;
        public void SetData(object data)
        {
            this.data = data;
        }
        internal virtual async UniTask ShowAsync()
        {
            overlay.gameObject.SetActive(true);
            container.gameObject.SetActive(true);
            OnShow();
            OnShowAction?.Invoke();
            await _uiAnimation.ShowAsync();
            OnShown();
            OnShownAction?.Invoke();
        }
        internal virtual async UniTask HideAsync()
        {
            OnHide();
            OnHideAction?.Invoke();
            await _uiAnimation.HideAsync();
            OnHidden();
            OnHiddenAction?.Invoke();
            overlay.gameObject.SetActive(false);
            container.gameObject.SetActive(false);
            this.data = null;
        }
        protected virtual void OnShow()
        {
        }
        protected virtual void OnShown()
        {
        }
        protected virtual void OnHide()
        {
        }
        protected virtual void OnHidden()
        {
        }
    }
}