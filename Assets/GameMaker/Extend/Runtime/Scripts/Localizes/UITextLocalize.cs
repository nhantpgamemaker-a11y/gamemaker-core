using System;
using GameMaker.Core.Runtime;
using GameMaker.UI.Runtime;
using TMPro;
using UnityEngine;

namespace GameMaker.Extension.Runtime
{
    public class UITextLocalize : MonoBehaviour
    {
        [SerializeField] private BaseUI _rootUI;
        [SerializeField] private BaseLocalizeMiddleware _localizeMiddleware;
        [SerializeField] private TMP_Text _text;
        private string _rootStringKey;
        protected virtual void OnEnable()
        {
            _rootStringKey = _text.text;
            _rootUI.OnShowAction += OnShow;
        }
        protected virtual void OnDisable()
        {
            _rootUI.OnShowAction -= OnShow;
        }
        private void OnShow()
        {
            _text.text = _localizeMiddleware.LocalizeText(_rootStringKey);
        }
    }
}
