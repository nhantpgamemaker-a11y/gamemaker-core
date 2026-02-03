using GameMaker.Core.Editor;
using GameMaker.Core.Runtime;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace GameMaker.Core.Editor
{
    [CustomPropertyDrawer(typeof(BaseCurrencyDefinition))]
    public class CurrencyDefinitionPropertyDrawer : BaseDefinitionPropertyDrawer
    {
        protected override BaseDefinitionHolder GetBaseDefinitionHolder()
        {
            var asset = UIToolkitLoaderUtils.LoadUXML("CurrencyDefinitionElement");
            TemplateContainer templateContainer = asset.CloneTree();
            return new CurrencyDefinitionHolder(templateContainer);
        }
    }
}