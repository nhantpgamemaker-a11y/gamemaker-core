using System;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace GameMaker.Core.Editor
{
    public class ConfirmHolder : BaseHolder
    {
        private Label _title;
        private Label _description;
        private Button _btnConfirmButton;
        private Button _btnCancelButton;

        public ConfirmHolder(VisualElement root,
                                        string title,
                                        string description,
                                        Action confirmAction,
                                        Action cancelAction) : base(root)
        {
            _title = root.Q<Label>("TitleLabel");
            _description = root.Q<Label>("DescriptionLabel");
            _btnConfirmButton = root.Q<Button>("ConfirmButton");
            _btnCancelButton = root.Q<Button>("CancelButton");

            _title.text = title;
            _description.text = description;
            _btnConfirmButton.clicked += confirmAction;
            _btnCancelButton.clicked += cancelAction;
        }
    }
}
