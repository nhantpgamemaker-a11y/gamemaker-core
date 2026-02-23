using System;
using System.Collections.Generic;
using GameMaker.Core.Editor;
using GameMaker.Core.Runtime;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace GameMaker.Core.Editor
{
    [CustomPropertyDrawer(typeof(BaseDefinitionManager<ConfigDefinition>))]
    public class ConfigDataManagerPropertyHolder : Core.Editor.BaseDataManagerPropertyDrawer<ConfigDefinition>
    {
        protected override BaseDataManagerHolder<ConfigDefinition> CreateBaseDataManagerHolder()
        {
            var asset = UIToolkitLoaderUtils.LoadUXML("DataManagerElement");
            var templateContainer = asset.CloneTree();
            templateContainer.style.height = StyleKeyword.Auto;
            templateContainer.style.flexGrow = 1;
            return new ConfigDataManagerHolder(templateContainer);
        }
    }
    public class ConfigDataManagerHolder : BaseDataManagerHolder<ConfigDefinition>
    {
        public ConfigDataManagerHolder(VisualElement root) : base(root)
        {

        }
        protected override string GetTitle()
        {
            return "Config";
        }

        protected override BaseHolder CreateHolder()
        {
            var asset = Resources.Load<VisualTreeAsset>(
            "ConfigDefinitionElement");
            var templateContainer = asset.CloneTree();
            templateContainer.style.height = StyleKeyword.Auto;
            templateContainer.style.flexGrow = 1;
            return new ConfigDefinitionHolder(asset.CloneTree());
        }
    }
}
