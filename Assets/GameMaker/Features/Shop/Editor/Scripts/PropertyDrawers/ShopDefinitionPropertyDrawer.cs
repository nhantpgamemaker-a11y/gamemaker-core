using GameMaker.Core.Editor;
using GameMaker.Feature.Shop.Runtime;
using UnityEditor;
using UnityEngine.UIElements;

namespace GameMaker.Feature.Shop.Editor
{
    [CustomPropertyDrawer(typeof(ShopDefinition))]
    public class ShopDefinitionPropertyDrawer : BaseDefinitionPropertyDrawer
    {
        private ShopDefinitionHolder _shopDefinitionHolder;
        protected override BaseDefinitionHolder GetBaseDefinitionHolder()
        {
            var asset = UIToolkitLoaderUtils.LoadUXML("ShopDefinitionElement");
            TemplateContainer templateContainer = asset.CloneTree();
            _shopDefinitionHolder= new ShopDefinitionHolder(templateContainer);
            return _shopDefinitionHolder;
        }
    }
}