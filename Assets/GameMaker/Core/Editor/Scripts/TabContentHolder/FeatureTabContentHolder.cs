using System;
using System.Linq;
using System.Reflection;
using GameMaker.Core.Runtime;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace GameMaker.Core.Editor
{
    [CoreTabContext]
    public class FeatureTabContentHolder : BaseTabContentHolder
    {
        private TemplateContainer _templateContainer;
        public FeatureTabContentHolder(VisualElement root) : base(root)
        {
            _templateContainer = new TemplateContainer();
        }

        public override int GetIndex()
        {
            return int.MaxValue;
        }

        public override VisualElement GetTabView()
        {
            var tabContentHolders = TypeUtils.GetAllConcreteDerivedTypes(typeof(BaseFeatureTabContentHolder))
            .Where(x => x.GetCustomAttribute<FeatureTabContentAttribute>() != null)
            .Select(x => Activator.CreateInstance(x, _templateContainer) as BaseFeatureTabContentHolder)
            .OrderBy(x => x.GetIndex())
            .ToList();

            var csharpCustomStyledTabView = new TabView();
            foreach (var tabHolder in tabContentHolders)
            {
                var tab = new Tab(tabHolder.GetTitle());
                tab.Add(tabHolder.GetTabView());
                csharpCustomStyledTabView.Add(tab);
            }
            _templateContainer.Add(csharpCustomStyledTabView);
            return _templateContainer;
        }

        public override string GetTitle()
        {
            return "FEATURE";
        }
    }
}
