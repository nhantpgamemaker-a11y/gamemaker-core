using GameMaker.Core.Runtime;
using UnityEngine;
using UnityEngine.UIElements;

namespace GameMaker.Core.Editor
{
    [TypeContain(typeof(CurrencyActionParamDefinition))]
    public class CurrencyDefinitionActionParamDefinitionHolder : BaseActionParamDefinitionHolder
    {
        public CurrencyDefinitionActionParamDefinitionHolder(VisualElement root) : base(root)
        {
        }

        public override string GetNameFoldout()
        {
            var baseName = base.GetNameFoldout();
            return $"<<<Currency>>>  {baseName}";
        }

        public override VisualTreeAsset GetVisualTreeAsset()
        {
            return UIToolkitLoaderUtils.LoadUXML("BaseActionParamDefinitionElement");
        }
    }

    [TypeContain(typeof(StatActionParamDefinition))]
    public class StatDefinitionActionParamDefinitionHolder : BaseActionParamDefinitionHolder
    {
        public StatDefinitionActionParamDefinitionHolder(VisualElement root) : base(root)
        {
        }

        public override string GetNameFoldout()
        {
            var baseName = base.GetNameFoldout();
            return $"<<<Stat>>>  {baseName}";
        }

        public override VisualTreeAsset GetVisualTreeAsset()
        {
            return UIToolkitLoaderUtils.LoadUXML("BaseActionParamDefinitionElement");
        }
    }

    [TypeContain(typeof(ItemActionParamDefinition))]
    public class ItemDefinitionActionParamDefinitionHolder : BaseActionParamDefinitionHolder
    {
        public ItemDefinitionActionParamDefinitionHolder(VisualElement root) : base(root)
        {
        }

        public override string GetNameFoldout()
        {
            var baseName = base.GetNameFoldout();
            return $"<<<Item>>>  {baseName}";
        }

        public override VisualTreeAsset GetVisualTreeAsset()
        {
            return UIToolkitLoaderUtils.LoadUXML("BaseActionParamDefinitionElement");
        }
    }

    [TypeContain(typeof(ItemDetailActionParamDefinition))]
    public class ItemDetailDefinitionActionParamDefinitionHolder : BaseActionParamDefinitionHolder
    {
        public ItemDetailDefinitionActionParamDefinitionHolder(VisualElement root) : base(root)
        {
        }

        public override string GetNameFoldout()
        {
            var baseName = base.GetNameFoldout();
            return $"<<<Item Detail>>>  {baseName}";
        }

        public override VisualTreeAsset GetVisualTreeAsset()
        {
            return UIToolkitLoaderUtils.LoadUXML("BaseActionParamDefinitionElement");
        }
    }
}