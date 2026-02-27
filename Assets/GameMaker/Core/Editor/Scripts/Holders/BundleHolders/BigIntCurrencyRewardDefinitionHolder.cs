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
        private DropdownField _currencyDropdownField;
        private const string BIG_INT_FILTERED = @"[^0-9\-]";
        private TextField _amountLongField;

        public BigIntCurrencyRewardDefinitionHolder(VisualElement root) : base(root)
        {
           
            
        }
        public override void Bind(SerializedProperty elementProperty)
        {
             _amountLongField = Root.Q<TextField>("AmountTextField");
            _currencyDropdownField = Root.Q<DropdownField>("CurrencyDropdownField");
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
            });
            _amountLongField.SetValueWithoutNotify(serializedProperty.FindPropertyRelative("_amount").stringValue);

            _currencyDropdownField.choices = CurrencyManager.Instance.GetDefinitions()
            .Where(x => x.GetType() == typeof(BigIntCurrencyDefinition)).Select(x => x.GetName()).ToList();
            var data = CurrencyManager.Instance.GetDefinitions().FirstOrDefault(x => x.GetName() == _currencyDropdownField.value);
            if (data == null)
            {
                data = CurrencyManager.Instance.GetDefinitions().FirstOrDefault(x => x.GetType() == typeof(BigIntCurrencyDefinition));
            }
            _currencyDropdownField.RegisterValueChangedCallback(v =>
            {
                var selectedData = CurrencyManager.Instance.GetDefinitions().FirstOrDefault(x => x.GetName() == _currencyDropdownField.value);
                if (selectedData == null)
                {
                    selectedData = CurrencyManager.Instance.GetDefinitions().FirstOrDefault(x => x.GetType() == typeof(BigIntCurrencyDefinition));
                }
                elementProperty.FindPropertyRelative("_referenceId").stringValue = selectedData.GetID();
                elementProperty.serializedObject.ApplyModifiedProperties();
                UpdatePropertyFoldout();
            });
        }
        public override string GetNameFoldout()
        {
            var baseName = base.GetNameFoldout();
            return $"<<<BIGINT CURRENCY>>>:{_currencyDropdownField.value}  {baseName} : {_amountLongField.value}";
        }
        public override VisualTreeAsset GetVisualTreeAsset()
        {
            return UIToolkitLoaderUtils.LoadUXML("BigIntCurrencyRewardDefinitionElement");
        }
    }
}