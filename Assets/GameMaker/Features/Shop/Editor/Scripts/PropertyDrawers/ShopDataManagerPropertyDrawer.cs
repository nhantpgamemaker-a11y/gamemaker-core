using GameMaker.Core.Editor;
using GameMaker.Core.Runtime;
using GameMaker.Feature.Shop.Runtime;
using UnityEditor;
using UnityEngine.UIElements;

namespace GameMaker.Feature.Shop.Editor
{
    [CustomPropertyDrawer(typeof(BaseDefinitionManager<ShopDefinition>))]
    public class ShopDataManagerPropertyDrawer : Core.Editor.BaseDataManagerPropertyDrawer<ShopDefinition>
    {
        protected override BaseDataManagerHolder<ShopDefinition> CreateBaseDataManagerHolder()
        {
            var asset = UIToolkitLoaderUtils.LoadUXML("DataManagerElement");
            var templateContainer = asset.CloneTree();
            templateContainer.style.height = StyleKeyword.Auto;
            templateContainer.style.flexGrow = 1;
            return new ShopDataManagerHolder(templateContainer);
        }
    }
    public class ShopDataManagerHolder : BaseDataManagerHolder<ShopDefinition>
    {
        public ShopDataManagerHolder(VisualElement root) : base(root)
        {
        }
        override protected string GetTitle()
        {
            return "Shop";
        }

        protected override BaseHolder CreateHolder()
        {
            var asset = UIToolkitLoaderUtils.LoadUXML("ShopDefinitionElement");
            var templateContainer = asset.CloneTree();
            templateContainer.style.height = StyleKeyword.Auto;
            templateContainer.style.flexGrow = 1;
            return new ShopDefinitionHolder(asset.CloneTree());
        }
    }
}