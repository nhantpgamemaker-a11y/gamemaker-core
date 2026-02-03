using System.Collections.Generic;
using GameMaker.Core.Editor;
using GameMaker.Core.Runtime;
using Unity.Collections;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace GameMaker.Item.Editor
{
    public class PriceHolder : BaseHolder
    {
        private DropdownField _currencyDropdownField;
        private FloatField _amountFloatField;
        private List<BaseCurrencyDefinition> _currencyDefinitions;
        public PriceHolder(VisualElement root) : base(root)
        {
            _currencyDropdownField = root.Q<DropdownField>("CurrencyDropdownField");
            _amountFloatField = root.Q<FloatField>("AmountFloatField");
        }
        public override void Bind(SerializedProperty serializedProperty)
        {
            _amountFloatField.BindProperty(serializedProperty.FindPropertyRelative("_amount"));
            _currencyDefinitions = CurrencyManager.Instance.GetDefinitions();
            _currencyDropdownField.choices.Clear();
            foreach (var currencyDefinition in _currencyDefinitions)
            {
                _currencyDropdownField.choices.Add(currencyDefinition.GetName());
            }
            var currencyReferenceId = serializedProperty.FindPropertyRelative("_currencyReferenceId").stringValue;
            var currency = CurrencyManager.Instance.GetDefinition(currencyReferenceId);
            var index = _currencyDefinitions.IndexOf(currency);

            _currencyDropdownField.index = index;

            _currencyDropdownField.RegisterValueChangedCallback((c)=>
            {
                var currencyDefinition = _currencyDefinitions[_currencyDropdownField.index];
                serializedProperty.FindPropertyRelative("_currencyReferenceId").stringValue = currencyDefinition.GetID();
            });

        }
    }
}
