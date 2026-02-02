using GameMaker.Core.Runtime;
using UnityEngine;
using UnityEngine.UIElements;

namespace GameMaker.Core.Editor
{
    [TypeHolder(typeof(StringActionParamDefinition))]
    public class StringActionParamDefinitionHolder : BaseActionParamDefinitionHolder
    {
        public StringActionParamDefinitionHolder(VisualElement root) : base(root)
        {
        }

        public override string GetNameFoldout()
        {
            var baseName = base.GetNameFoldout();
            return $"<<<String>>>  {baseName}";
        }

        public override VisualTreeAsset GetVisualTreeAsset()
        {
            return Resources.Load<VisualTreeAsset>("BaseActionParamDefinitionElement");
        }
    }
}