using GameMaker.Core.Editor;
using GameMaker.Core.Runtime;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace GameMaker.Core.Editor
{
    [CustomPropertyDrawer(typeof(BaseDefinitionManager<TimedDefinition>))]
    public class TimedDataManagerPropertyDrawer : Core.Editor.BaseDataManagerPropertyDrawer<TimedDefinition>
    {
        protected override BaseDataManagerHolder<TimedDefinition> CreateBaseDataManagerHolder()
        {
            var asset = UIToolkitLoaderUtils.LoadUXML("DataManagerElement");
            var templateContainer = asset.CloneTree();
            templateContainer.style.height = StyleKeyword.Auto;
            templateContainer.style.flexGrow = 1;
            return new TimedDataManagerHolder(templateContainer);
        }
    }
    public class TimedDataManagerHolder : BaseDataManagerHolder<TimedDefinition>
    {
        public TimedDataManagerHolder(VisualElement root) : base(root)
        {
        }
        protected override string GetTitle()
        {
            return "Timed";
        }

        protected override BaseHolder CreateHolder()
        {
            var asset = Resources.Load<VisualTreeAsset>(
            "TimedDefinitionElement");
            var templateContainer = asset.CloneTree();
            templateContainer.style.height = StyleKeyword.Auto;
            templateContainer.style.flexGrow = 1;
            return new TimedDefinitionHolder(asset.CloneTree());
        }
    }
}