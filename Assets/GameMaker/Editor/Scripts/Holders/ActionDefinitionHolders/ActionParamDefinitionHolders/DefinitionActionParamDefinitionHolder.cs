using GameMaker.Core.Runtime;
using UnityEngine;
using UnityEngine.UIElements;

namespace GameMaker.Core.Editor
{
    [TypeHolder(typeof(CurrencyActionParamDefinition))]
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
            return Resources.Load<VisualTreeAsset>("BaseActionParamDefinitionElement");
        }
    }

    [TypeHolder(typeof(StatActionParamDefinition))]
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
            return Resources.Load<VisualTreeAsset>("BaseActionParamDefinitionElement");
        }
    }

    [TypeHolder(typeof(BaseDefinitionActionParamDefinition<ItemDefinition>))]
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
            return Resources.Load<VisualTreeAsset>("BaseActionParamDefinitionElement");
        }
    }

    [TypeHolder(typeof(BaseDefinitionActionParamDefinition<ItemDetailDefinition>))]
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
            return Resources.Load<VisualTreeAsset>("BaseActionParamDefinitionElement");
        }
    }
}