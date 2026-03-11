using System.Collections.Generic;
using System.Linq;
using GameMaker.Core.Editor;
using GameMaker.Core.Runtime;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace GameMaker.Feature.Shop.Editor
{
    [TypeCache]
    public abstract class BasePriceHolder : BaseHolder
    {       
        private DropdownField _currencyDropdownField;
        protected BasePriceHolder(VisualElement root) : base(root)
        {
            var asset = GetVisualTreeAsset();
            var templateContainer = asset.CloneTree();
            root.Add(templateContainer);
            _currencyDropdownField = root.Q<DropdownField>("CurrencyDropdownField");
        }
        public override void Bind(SerializedProperty elementProperty)
        {
            base.Bind(elementProperty);
            _currencyDropdownField.choices = GetCurrencyDefinitions().Select(x => x.GetName()).ToList();
            var currencyReferenceId = elementProperty.FindPropertyRelative("_currencyReferenceId").stringValue;
            var currencyDefinition = GetCurrencyDefinitions().FirstOrDefault(x => x.GetID() == currencyReferenceId);
            if (currencyDefinition != null)
            {
                _currencyDropdownField.value = currencyDefinition.GetName();
            }
            else if (GetCurrencyDefinitions().Count > 0)
            {
                _currencyDropdownField.value = GetCurrencyDefinitions().First().GetName();
                elementProperty.FindPropertyRelative("_currencyReferenceId").stringValue = GetCurrencyDefinitions().First().GetID();
                elementProperty.serializedObject.ApplyModifiedProperties();
            }
            _currencyDropdownField.RegisterValueChangedCallback(value =>
            {
                var currencyDefinition = GetCurrencyDefinitions().FirstOrDefault(x => x.GetName() == value.newValue);
                if (currencyDefinition != null)
                {
                    elementProperty.FindPropertyRelative("_currencyReferenceId").stringValue = currencyDefinition.GetID();
                    elementProperty.serializedObject.ApplyModifiedProperties();
                }
            });
        }
        public virtual List<BaseCurrencyDefinition> GetCurrencyDefinitions()
        {
            return CurrencyManager.Instance.GetDefinitions().ToList();
        }
        public abstract VisualTreeAsset GetVisualTreeAsset();
    }
}