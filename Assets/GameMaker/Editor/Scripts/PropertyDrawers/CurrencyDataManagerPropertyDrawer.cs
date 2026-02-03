using GameMaker.Core.Editor;
using GameMaker.Core.Runtime;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace GameMaker.Core.Editor
{
    [CustomPropertyDrawer(typeof(BaseDefinitionManager<BaseCurrencyDefinition>))]
    public class CurrencyDataManagerPropertyDrawer : Core.Editor.BaseDataManagerPropertyDrawer<BaseCurrencyDefinition>
    {
        protected override BaseDataManagerHolder<BaseCurrencyDefinition> CreateBaseDataManagerHolder()
        {
            var asset = UIToolkitLoaderUtils.LoadUXML("DataManagerElement");
            var templateContainer = asset.CloneTree();
            templateContainer.style.height = StyleKeyword.Auto;
            templateContainer.style.flexGrow = 1;
            templateContainer.Add(new Button());
            return new CurrencyDataManagerHolder(templateContainer);
        }
    }
    public class CurrencyDataManagerHolder : BaseDataManagerHolder<BaseCurrencyDefinition>
    {
        public CurrencyDataManagerHolder(VisualElement root) : base(root)
        {
        }
        protected override string GetTitle()
        {
            return "Currency";
        }

        protected override BaseHolder CreateHolder()
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