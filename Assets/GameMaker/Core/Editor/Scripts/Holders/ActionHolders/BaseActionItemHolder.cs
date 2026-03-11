using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

namespace GameMaker.Core.Editor
{
    public class BaseActionItemHolder : BaseHolder
    {
        private Button _actionButton;
        private Label _titleLabel;
        public BaseActionItemHolder(VisualElement root) : base(root)
        {
            _actionButton = root.Q<Button>("ActionButton");
        }
        public virtual void Bind(ActionData actionData)
        {
            _actionButton.text = actionData.Title;
            _actionButton.clicked += actionData.Action;
        }
    }
}
