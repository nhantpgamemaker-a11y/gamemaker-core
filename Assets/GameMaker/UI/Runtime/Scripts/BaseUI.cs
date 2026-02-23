
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace GameMaker.UI.Runtime
{
    public abstract class BaseUI : MonoBehaviour
    {
        [SerializeField] protected RectTransform overlay;
        [SerializeField] protected RectTransform container;
        [SerializeField] private BaseUIAnimation _uiAnimation;
        protected object data = null;
        public void SetData(object data)
        {
            this.data = data;
        }
        internal virtual async UniTask ShowAsync()
        {
            overlay.gameObject.SetActive(true);
            container.gameObject.SetActive(true);
            OnShow();
            await _uiAnimation.ShowAsync();
            OnShown();
        }
        internal virtual async UniTask HideAsync()
        {
            OnHide();
            await _uiAnimation.HideAsync();
            OnHidden();
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