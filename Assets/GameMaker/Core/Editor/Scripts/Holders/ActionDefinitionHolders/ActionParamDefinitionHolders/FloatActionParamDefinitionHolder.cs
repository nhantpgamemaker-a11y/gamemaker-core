using GameMaker.Core.Runtime;
using UnityEngine;
using UnityEngine.UIElements;

namespace GameMaker.Core.Editor
{
    [TypeContain(typeof(FloatActionParamDefinition))]
    public class FloatActionParamDefinitionHolder : BaseActionParamDefinitionHolder
    {
        
        public FloatActionParamDefinitionHolder(VisualElement root) : base(root)
        {
        }

        public override string GetNameFoldout()
        {
            var baseName = base.GetNameFoldout();
            return $"<<<Float>>>  {baseName}";
        }

        public override VisualTreeAsset GetVisualTreeAsset()
        {
            return UIToolkitLoaderUtils.LoadUXML("BaseActionParamDefinitionElement");
        }
    }
}