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
            Rect main = EditorGUIUtility.GetMainWindowPosition();
            Vector2 size = new Vector2(400, 200);

            float x = main.x + (main.width - size.x) * 0.5f;
            float y = main.y + (main.height - size.y) * 0.5f;

            window.position = new Rect(x, y, size.x, size.y);

            window.ShowModalUtility();
            return window;
        }
        private void InitUI(string title, List<ActionData> actionDatas)
        {
            rootVisualElement.Clear();

            var asset = UIToolkitLoaderUtils.LoadUXML("BaseActionElement");
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
