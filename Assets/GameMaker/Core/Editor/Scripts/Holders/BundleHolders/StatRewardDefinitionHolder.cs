using System.Collections.Generic;
using System.Linq;
using GameMaker.Core.Runtime;
using Unity.Collections;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace GameMaker.Core.Editor
{
    [TypeContain(typeof(StatRewardDefinition))]
    public class StatRewardDefinitionHolder : BaseRewardDefinitionHolder
    {
        private LongField _amountLongField;
        public StatRewardDefinitionHolder(VisualElement root) : base(root)
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
            return UIToolkitLoaderUtils.LoadUXML("StatRewardDefinitionElement");
        }
        public override string GetNameFoldout()
        {
            var baseName = base.GetNameFoldout();
            return $"<<<STAT>>>: {baseName} : {_amountLongField.value}";
        }

        public override List<BaseDefinition> GetRewardDefinitions()
        {
            return PropertyManager.Instance.GetDefinitions().Cast<BaseDefinition>().ToList();
        }
    }
}