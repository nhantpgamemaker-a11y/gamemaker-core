using System;
using System.Collections.Generic;
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
        [MenuItem("GameMaker/Setting")]
        public static CoreWindowEditor ShowExample()
        {
            CoreWindowEditor wnd = GetWindow<CoreWindowEditor>();
            wnd.titleContent = new GUIContent("GameMaker Setting");
            wnd.Show();
            return wnd;
        }
        public void CreateGUI()
        {
            var tabContentHolders = TypeUtils.GetAllConcreteDerivedTypes(typeof(BaseTabContentHolder))
            .Where(x => x.GetCustomAttribute<CoreTabContextAttribute>() != null)
            .Select(x => Activator.CreateInstance(x, rootVisualElement) as BaseTabContentHolder)
            .OrderBy(x => x.GetIndex())
            .ToList();

            var csharpCustomStyledTabView = new TabView();
            foreach(var tabHolder in tabContentHolders)
            {
                var tab = new Tab(tabHolder.GetTitle());
                tab.Add(tabHolder.GetTabView());
                csharpCustomStyledTabView.Add(tab);
            }
            rootVisualElement.Add(csharpCustomStyledTabView);
        }
    }
}
