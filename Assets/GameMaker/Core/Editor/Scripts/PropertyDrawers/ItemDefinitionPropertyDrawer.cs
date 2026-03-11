using GameMaker.Core.Editor;
using GameMaker.Core.Runtime;
using Mono.Cecil.Cil;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace GameMaker.Core.Editor
{
    [CustomPropertyDrawer(typeof(ItemDefinition))]
    public class ItemDefinitionPropertyDrawer : BaseDefinitionPropertyDrawer
    {
        ItemDefinitionHolder _itemDefinitionHolder;
        protected override BaseDefinitionHolder GetBaseDefinitionHolder()
        {
            var asset = UIToolkitLoaderUtils.LoadUXML("ItemDefinitionElement");
            TemplateContainer templateContainer = asset.CloneTree();
            _itemDefinitionHolder = new ItemDefinitionHolder(templateContainer);
            return _itemDefinitionHolder;
        }
    }
}