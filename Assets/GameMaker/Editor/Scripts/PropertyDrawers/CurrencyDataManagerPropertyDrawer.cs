using GameMaker.Core.Editor;
using GameMaker.Core.Runtime;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace GameMaker.Core.Editor
{
    [CustomPropertyDrawer(typeof(BaseDefinitionManager<CurrencyDefinition>))]
    public class CurrencyDataManagerPropertyDrawer : Core.Editor.BaseDataManagerPropertyDrawer<CurrencyDefinition>
    {
        protected override BaseDataManagerHolder<CurrencyDefinition> CreateBaseDataManagerHolder()
        {
            var asset = Resources.Load<VisualTreeAsset>("DataManagerElement");
            var templateContainer = asset.CloneTree();
            templateContainer.style.height = StyleKeyword.Auto;
            templateContainer.style.flexGrow = 1;
            return new CurrencyDataManagerHolder(templateContainer);
        }
    }
    public class CurrencyDataManagerHolder : BaseDataManagerHolder<CurrencyDefinition>
    {
        public CurrencyDataManagerHolder(VisualElement root) : base(root)
        {

        }
        protected override string GetTitle()
        {
            return "Currency";
        }

        protected override DefinitionHolder CreateHolder()
        {
            var asset = Resources.Load<VisualTreeAsset>(
            "CurrencyDefinitionElement");
            var templateContainer = asset.CloneTree();
            templateContainer.style.height = StyleKeyword.Auto;
            templateContainer.style.flexGrow = 1;
            return new CurrencyDefinitionHolder(asset.CloneTree());
        }
    }
}