using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using GameMaker.Core.Editor;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace GameMaker.Core.Runtime
{
    [TypeContain(typeof(BigIntCurrencyRewardDefinition))]
    public class BigIntCurrencyRewardDefinitionHolder : BaseCurrencyRewardDefinitionHolder
    {   
        private const string BIG_INT_FILTERED = @"[^0-9\-]";
        private TextField _amountLongField;

        public BigIntCurrencyRewardDefinitionHolder(VisualElement root) : base(root)
        {
           
            
        }
        public override void Bind(SerializedProperty elementProperty)
        {
             _amountLongField = Root.Q<TextField>("AmountTextField");
            base.Bind(elementProperty);
            _amountLongField.RegisterValueChangedCallback(evt =>
            {
                string filtered = Regex.Replace(evt.newValue, BIG_INT_FILTERED, "");

                if (filtered.Count(c => c == '-') > 1)
                    filtered = filtered.Replace("-", "");

                if (filtered != evt.newValue)
                    _amountLongField.SetValueWithoutNotify(filtered);

                serializedProperty.FindPropertyRelative("_amount").stringValue = filtered;
                serializedProperty.serializedObject.ApplyModifiedProperties();
                UpdatePropertyFoldout();
            });
            _amountLongField.value = serializedProperty.FindPropertyRelative("_amount").stringValue;
            _amountLongField.RegisterValueChangedCallback(c =>
            {
                UpdatePropertyFoldout();
            });
            UpdatePropertyFoldout();
        }
        public override string GetNameFoldout()
        {
            var baseName = base.GetNameFoldout();
            return $"<<<BIGINT CURRENCY>>>: {baseName} : {_amountLongField.value}";
        }

        public override List<BaseDefinition> GetRewardDefinitions()
        {
            return CurrencyManager.Instance.GetDefinitions().Where(x=>x.GetType() ==typeof(BigIntCurrencyDefinition)).Cast<BaseDefinition>().ToList();
        }

        public override VisualTreeAsset GetVisualTreeAsset()
        {
            return UIToolkitLoaderUtils.LoadUXML("BigIntCurrencyRewardDefinitionElement");
        }
    }
}