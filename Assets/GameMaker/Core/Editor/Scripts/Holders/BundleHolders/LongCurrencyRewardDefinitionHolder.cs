using System.Collections.Generic;
using System.Linq;
using GameMaker.Core.Editor;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace GameMaker.Core.Runtime
{
    [TypeContain(typeof(LongCurrencyRewardDefinition))]
    public class LongCurrencyRewardDefinitionHolder : BaseCurrencyRewardDefinitionHolder
    {
        private LongField _amountLongField;

        public LongCurrencyRewardDefinitionHolder(VisualElement root) : base(root)
        {
            _amountLongField = root.Q<LongField>("AmountLongField");
        }
        public override void Bind(SerializedProperty elementProperty)
        {
            base.Bind(elementProperty);
            _amountLongField.BindProperty(serializedProperty.FindPropertyRelative("_amount"));
            _amountLongField.RegisterValueChangedCallback(c =>
            {
                UpdatePropertyFoldout();
            });
            UpdatePropertyFoldout();
        }
        
        public override string GetNameFoldout()
        {
            var baseName = base.GetNameFoldout();
            return $"<<<LONG CURRENCY>>>: {baseName} : {_amountLongField.value}";
        }

        public override List<BaseDefinition> GetRewardDefinitions()
        {
            return CurrencyManager.Instance.GetDefinitions().Where(x=>x.GetType() ==typeof(LongCurrencyDefinition)).Cast<BaseDefinition>().ToList();
        }

        public override VisualTreeAsset GetVisualTreeAsset()
        {
            return UIToolkitLoaderUtils.LoadUXML("LongCurrencyRewardDefinitionElement");
        }
    }
}