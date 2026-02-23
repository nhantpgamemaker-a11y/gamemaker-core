using UnityEngine;
using Cysharp.Threading.Tasks;
using GameMaker.Core.Runtime;
using System;

namespace GameMaker.UI.Runtime
{
    public class AlertController : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas;
        [SerializeField] ObjectPoolManager _poolManager;
        private UIController _uiController;

        public UIController UIController => _uiController;
        public async UniTask ShowAsync(string text, string poolingName = "")
        {
            BaseAlert alert = null;
            if (poolingName == string.Empty)
            {
                alert = _poolManager.Get<BaseAlert>(BaseAlert.BASE_ALERT_NAME);
            }
            else
            {
                alert = _poolManager.Get<BaseAlert>(poolingName);
            }
            alert.gameObject.transform.parent = _canvas.gameObject.transform;
            alert.gameObject.transform.localPosition = Vector3.zero;
            alert.SetData(text);
            await alert.ShowAsync();
            _poolManager.Release(alert);
        }

        public void OnInit(UIController uIController)
        {
            _uiController = uIController;
        }
    }
}