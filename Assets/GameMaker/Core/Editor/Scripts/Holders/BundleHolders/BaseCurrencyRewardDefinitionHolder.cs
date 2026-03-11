using System.Linq;
using GameMaker.Core.Runtime;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace GameMaker.Core.Editor
{
    // [TypeContain(typeof(BaseCurrencyRewardDefinition))]
    [TypeCache]
    public abstract class BaseCurrencyRewardDefinitionHolder : BaseRewardDefinitionHolder
    {
       
        public BaseCurrencyRewardDefinitionHolder(VisualElement root) : base(root)
        {
        }
        public override void Bind(SerializedProperty elementProperty)
        {
            base.Bind(elementProperty);
        }
        public override VisualTreeAsset GetVisualTreeAsset()
        {
            return UIToolkitLoaderUtils.LoadUXML("BaseCurrencyRewardDefinitionElement");
        }
    }
}