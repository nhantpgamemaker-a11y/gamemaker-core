using GameMaker.Core.Runtime;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace GameMaker.Core.Editor
{
    [CustomPropertyDrawer(typeof(BaseDefinitionManager<ItemDefinition>))]
    public class ItemDataManagerPropertyDrawer : Core.Editor.BaseDataManagerPropertyDrawer<ItemDefinition>
    {
        protected override BaseDataManagerHolder<ItemDefinition> CreateBaseDataManagerHolder()
        {
            var asset = UIToolkitLoaderUtils.LoadUXML("DataManagerElement");
            var templateContainer = asset.CloneTree();
            templateContainer.style.height = StyleKeyword.Auto;
            templateContainer.style.flexGrow = 1;
            return new ItemDataManagerHolder(templateContainer);
        }
    }
    public class ItemDataManagerHolder : BaseDataManagerHolder<ItemDefinition>
    {
        public ItemDataManagerHolder(VisualElement root) : base(root)
        {

        }
        protected override string GetTitle()
        {
            return "Item";
        }

        protected override BaseHolder CreateHolder()
        {
            var asset = Resources.Load<VisualTreeAsset>(
            "ItemDefinitionElement");
            var templateContainer = asset.CloneTree();
            templateContainer.style.height = StyleKeyword.Auto;
            templateContainer.style.flexGrow = 1;
            return new ItemDefinitionHolder(asset.CloneTree());
        }
    }
}
