using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace GameMaker.UI.Runtime
{
    public enum PopupShowType
    {
        Parallel,
        Sequence
    }

    public class PopupController : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas;

        private Dictionary<BasePopup, AsyncOperationHandle<GameObject>> _popupLockup = new();

        private Queue<BasePopup> _waitingPopupQueue = new();

        private List<BasePopup> _instancePopupList = new();

        private UIController _uiController;

        public UIController UIController => _uiController;

        internal void OnInit(UIController uIController)
        {
            _uiController = uIController;
        }

        public async UniTask<T> ShowAsync<T>(string popupName,object data = null, PopupShowType showType = PopupShowType.Parallel) where T: BasePopup
        {
            var (basePopupPrefab, loadAsyncOperationHandle) = await GetPopupAsync(popupName);
            var popup = Instantiate(basePopupPrefab, _canvas.transform);
            popup.OnInit(this);
            _popupLockup[popup] = loadAsyncOperationHandle;
            popup.SetData(data);
            if (showType == PopupShowType.Sequence)
            {
                popup.gameObject.SetActive(false);
                _waitingPopupQueue.Enqueue(popup);
            }
            else
            {
                _instancePopupList.Add(popup);
                await popup.ShowAsync();
            }
            return popup as T;
        }
        public async UniTask HideAsync(BasePopup basePopup)
        {
            await basePopup.HideAsync();
            _instancePopupList.Remove(basePopup);
            if (_popupLockup.TryGetValue(basePopup, out var operationHandle))
            {
                _popupLockup.Remove(basePopup);
                Destroy(basePopup.gameObject);
                operationHandle.Release();
            }
        }
        public BasePopup GetPopup<T>() where T : BasePopup
        {
            var popup = _instancePopupList.FirstOrDefault(x => x.GetType() == typeof(T));
            if (!popup) popup = _waitingPopupQueue.FirstOrDefault(x => x.GetType() == typeof(T));
            return popup;
        }
        private async UniTask<(BasePopup basePopup, AsyncOperationHandle<GameObject> loadAsyncOperationHandle)> GetPopupAsync(string popupName)
        {
            var loadAsyncOperationHandle = Addressables.LoadAssetAsync<GameObject>(popupName);
            var popupObjectPrefab = await UniTask.RunOnThreadPool(async () => await loadAsyncOperationHandle.ToUniTask());
            var popupPrefab = popupObjectPrefab.GetComponent<BasePopup>();
            await UniTask.SwitchToMainThread();
            return (popupPrefab, loadAsyncOperationHandle);
        }

        #region  Unity Method
        private void Update()
        {
            if(_instancePopupList.Count ==0 && _waitingPopupQueue.Count > 0)
            {
                var popup = _waitingPopupQueue.Dequeue();
                _instancePopupList.Add(popup);
                _ = popup.ShowAsync();
            }
        }
        #endregion
    }
}