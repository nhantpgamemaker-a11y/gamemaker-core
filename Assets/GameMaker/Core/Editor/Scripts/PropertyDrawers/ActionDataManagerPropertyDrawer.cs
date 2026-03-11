using GameMaker.Core.Editor;
using GameMaker.Core.Runtime;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace GameMaker.Core.Editor
{
    [CustomPropertyDrawer(typeof(BaseDefinitionManager<ActionDefinition>))]
    public class ActionDataManagerPropertyDrawer : Core.Editor.BaseDataManagerPropertyDrawer<ActionDefinition>
    {
        protected override BaseDataManagerHolder<ActionDefinition> CreateBaseDataManagerHolder()
        {
            var asset = UIToolkitLoaderUtils.LoadUXML("DataManagerElement");
            var templateContainer = asset.CloneTree();
            templateContainer.style.height = StyleKeyword.Auto;
            templateContainer.style.flexGrow = 1;
            return new ActionDataManagerHolder(templateContainer);
        }
    }
    public class ActionDataManagerHolder : BaseDataManagerHolder<ActionDefinition>
    {
        public ActionDataManagerHolder(VisualElement root) : base(root)
        {

        }
        protected override string GetTitle()
        {
            return "Action";
        }

        protected override BaseHolder CreateHolder()
        {
            var asset = UIToolkitLoaderUtils.LoadUXML(
            "ActionDefinitionElement");
            var templateContainer = asset.CloneTree();
            templateContainer.style.height = StyleKeyword.Auto;
            templateContainer.style.flexGrow = 1;
            return new ActionDefinitionHolder(asset.CloneTree());
        }
    }
}