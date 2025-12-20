using Codice.Client.Common;
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
            await OnShowAsync();
            await _uiAnimation.ShowAsync();
            await OnShownAsync();
        }
        internal virtual async UniTask HideAsync()
        {
            await OnHideAsync();
            await _uiAnimation.HideAsync();
            await OnHiddenAsync();
            overlay.gameObject.SetActive(false);
            container.gameObject.SetActive(false);
            this.data = null;
        }
        protected virtual async UniTask OnShowAsync()
        {
            await UniTask.Yield(); 
        }
        protected virtual async UniTask OnShownAsync()
        {
            await UniTask.Yield(); 
        }
        protected virtual async UniTask OnHideAsync()
        {
            await UniTask.Yield(); 
        }
        protected virtual async UniTask OnHiddenAsync()
        {
            await UniTask.Yield(); 
        }
    }
}