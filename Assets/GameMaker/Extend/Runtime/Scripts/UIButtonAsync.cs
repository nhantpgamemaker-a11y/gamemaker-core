using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace GameMaker.Extension.Runtime
{
    [RequireComponent(typeof(Button))]
    public class UIButtonAsync : MonoBehaviour
    {
        private Button _button;
        private List<Func<UniTask>> _asyncActions = new();
        private bool _isPress = false;
        public Button Button
        {
            get
            {
                if (_button == null)
                    _button = GetComponent<Button>();
                return _button;
            }
        }
        private void Awake()
        {
            Button.onClick.AddListener(OnClick);
        }
        private void OnDestroy()
        {
            Button.onClick.RemoveListener(OnClick);
        }

        private void OnClick()
        {
            OnClickAsync().Forget();
        }
        private async UniTask OnClickAsync()
        {
            if (_isPress)
            {
                GameMaker.Core.Runtime.Logger.LogWarning("[UIButtonAsync]Button is already processing click event.");
                return;
            }
            _isPress = true;
            var tasks = new List<UniTask>();
            foreach (var asyncAction in _asyncActions.ToList())
            {
                tasks.Add(asyncAction());
            }
            await UniTask.WhenAll(tasks);
            _isPress = false;
        }

        public void AddListenerAsync(Func<UniTask> asyncAction)
        {
            _asyncActions.Add(asyncAction);
        }
        public void RemoveListenerAsync(Func<UniTask> asyncAction)
        {
            _asyncActions.Remove(asyncAction);
        }
    }
}
