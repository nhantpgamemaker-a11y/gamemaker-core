using System;
using GameMaker.Core.Runtime;
using GameMaker.UI.Runtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameMaker.Extension.Runtime
{
    public class UIIconLocalize : MonoBehaviour
    {
        [SerializeField] private BaseUI _rootUI;
        [SerializeField] private BaseLocalizeMiddleware _localizeMiddleware;
        [SerializeField] private Image _img;
        private string _rootStringKey;
        protected virtual void OnEnable()
        {
            _rootStringKey = _img.name;
            _rootUI.OnShowAction += OnShow;
        }
        protected virtual void OnDisable()
        {
            _rootUI.OnShowAction -= OnShow;
        }
        private void OnShow()
        {
            _img.sprite = _localizeMiddleware.LocalizeIcon(_rootStringKey);
        }
    }
}
