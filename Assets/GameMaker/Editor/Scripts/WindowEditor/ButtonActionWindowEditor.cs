using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace GameMaker.Core.Editor
{
    public class ButtonActionWindowEditor : EditorWindow
    {
        public static EditorWindow ShowWindow(string title, List<ActionData> actionDatas)
        {
            var window = CreateInstance<ButtonActionWindowEditor>();
            window.titleContent = new GUIContent("Action");
            window.InitUI(title, actionDatas);
            window.ShowModalUtility();
            return window;
        }
        private void InitUI(string title, List<ActionData> actionDatas)
        {
            rootVisualElement.Clear();

            var asset = Resources.Load<VisualTreeAsset>("BaseActionElement");
            var root = asset.CloneTree();
            rootVisualElement.Add(root);
            foreach(var actionData in actionDatas)
            {
                actionData.Action += () => Close();
            }
            new BaseActionHolder(
                root,
                title,
                actionDatas
            );
        }
    }
}
