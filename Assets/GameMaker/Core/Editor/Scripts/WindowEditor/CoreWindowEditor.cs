using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using GameMaker.Core.Runtime;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace GameMaker.Core.Editor
{
    public class CoreWindowEditor : EditorWindow
    {
        [MenuItem("GameMaker/Setting",priority = 1)]
        public static CoreWindowEditor ShowWindowEditor()
        {
            CoreWindowEditor wnd = GetWindow<CoreWindowEditor>();
            wnd.titleContent = new GUIContent("GameMaker Setting");
            wnd.Show();
            return wnd;
        }
        public void CreateGUI()
        {
            var tabContentHolders = TypeUtils.GetAllConcreteDerivedTypes_Editor(typeof(BaseTabContentHolder))
            .Where(x => x.GetCustomAttribute<CoreTabContextAttribute>() != null)
            .Select(x => Activator.CreateInstance(x, rootVisualElement) as BaseTabContentHolder)
            .OrderBy(x => x.GetIndex())
            .ToList();

            var csharpCustomStyledTabView = new TabView();
            foreach (var tabHolder in tabContentHolders)
            {
                var tab = new Tab(tabHolder.GetTitle());
                tab.Add(tabHolder.GetTabView());
                csharpCustomStyledTabView.Add(tab);
            }
            rootVisualElement.Add(csharpCustomStyledTabView);
        }

        [MenuItem("GameMaker/Clear LocalData")]
        public static void ClearLocalData()
        {
            var path = Application.persistentDataPath;

            if (!Directory.Exists(path))
            {
                GameMaker.Core.Runtime.Logger.LogWarning("LocalData path does not exist: " + path);
                return;
            }

            if (!EditorUtility.DisplayDialog(
                    "Clear Local Data",
                    $"Are you sure you want to delete all local data?\n\n{path}",
                    "Yes",
                    "Cancel"))
            {
                return;
            }

            try
            {
                Directory.Delete(path, true);
                Directory.CreateDirectory(path);

                GameMaker.Core.Runtime.Logger.Log("LocalData cleared: " + path);
            }
            catch (System.Exception e)
            {
                GameMaker.Core.Runtime.Logger.LogError("Failed to clear LocalData: " + e);
            }
        }

        [MenuItem("GameMaker/Open LocalData Location")]
        public static void OpenLocalData()
        {
            var path = Application.persistentDataPath;

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            EditorUtility.RevealInFinder(path);
        }
        
    }
}
