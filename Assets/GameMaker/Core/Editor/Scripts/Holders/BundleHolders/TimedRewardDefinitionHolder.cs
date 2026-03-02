using System.Collections.Generic;
using System.Linq;
using GameMaker.Core.Runtime;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace GameMaker.Core.Editor
{
    [TypeContain(typeof(TimedRewardDefinition))]
    public class TimedRewardDefinitionHolder : BaseRewardDefinitionHolder
    {
        private LongField _amountLongField;
        public TimedRewardDefinitionHolder(VisualElement root) : base(root)
        {
        }
        public override void Bind(SerializedProperty elementProperty)
        {
            _amountLongField = Root.Q<LongField>("AmountLongField");
            _amountLongField.BindProperty(elementProperty.FindPropertyRelative("_amount"));
            _amountLongField.RegisterValueChangedCallback(c =>
            {
                UpdatePropertyFoldout();
            });
            base.Bind(elementProperty);
        }

        public override VisualTreeAsset GetVisualTreeAsset()
        {
            return UIToolkitLoaderUtils.LoadUXML("TimedRewardDefinitionElement");
        }
        public override string GetNameFoldout()
        {
            var baseName = base.GetNameFoldout();
            return $"<<<TIMED>>>:  {baseName} : {_amountLongField.value}";
        }

        public override List<BaseDefinition> GetRewardDefinitions()
        {
            return TimedManager.Instance.GetDefinitions().Cast<BaseDefinition>().ToList();
        }
    }
}