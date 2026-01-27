using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace GameMaker.Core.Editor
{
    public class ConfirmWindowEditor : EditorWindow
    {
        private Action _onConfirm;
        private Action _onCancel;
        public static void ShowWindow(string title, string description, Action OnConfirmAction, Action OnCancelAction)
        {
            var window = CreateInstance<ConfirmWindowEditor>();
            window._onConfirm = OnConfirmAction;
            window._onCancel = OnCancelAction;
            window.titleContent = new GUIContent("Confirm");
            window.InitUI(title, description);
            window.ShowModalUtility();
        }
        private void InitUI(string title, string description)
        {
            rootVisualElement.Clear();

            var asset = Resources.Load<VisualTreeAsset>("ConfirmElement");
            var root = asset.CloneTree();
            rootVisualElement.Add(root);

            new ConfirmHolder(
                root,
                title,
                description,
                OnConfirm,
                OnCancel
            );
        }
        private void OnConfirm()
        {
            _onConfirm?.Invoke();
            Close();
        }

        private void OnCancel()
        {
            _onCancel?.Invoke();
            Close();
        }
    }
}