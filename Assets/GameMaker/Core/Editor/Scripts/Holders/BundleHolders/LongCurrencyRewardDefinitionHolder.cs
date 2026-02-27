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
        private DropdownField _currencyDropdownField;

        private LongField _amountLongField;

        public LongCurrencyRewardDefinitionHolder(VisualElement root) : base(root)
        {
            _amountLongField = root.Q<LongField>("AmountLongField");
            _currencyDropdownField = Root.Q<DropdownField>("CurrencyDropdownField");
        }
        public override void Bind(SerializedProperty elementProperty)
        {
            base.Bind(elementProperty);
            _amountLongField.BindProperty(serializedProperty.FindPropertyRelative("_amount"));
            _currencyDropdownField.choices = CurrencyManager.Instance.GetDefinitions()
            .Where(x => x.GetType() == typeof(LongCurrencyDefinition)).Select(x => x.GetName()).ToList();
            var data = CurrencyManager.Instance.GetDefinitions().FirstOrDefault(x => x.GetName() == _currencyDropdownField.value);
            if (data == null)
            {
                data = CurrencyManager.Instance.GetDefinitions().FirstOrDefault(x => x.GetType() == typeof(LongCurrencyDefinition));
            }
            _currencyDropdownField.RegisterValueChangedCallback(v =>
            {
                var selectedData = CurrencyManager.Instance.GetDefinitions().FirstOrDefault(x => x.GetName() == _currencyDropdownField.value);
                if (selectedData == null)
                {
                    selectedData = CurrencyManager.Instance.GetDefinitions().FirstOrDefault(x => x.GetType() == typeof(LongCurrencyDefinition));
                }
                elementProperty.FindPropertyRelative("_referenceId").stringValue = selectedData.GetID();
                elementProperty.serializedObject.ApplyModifiedProperties();
                UpdatePropertyFoldout();
            });
        }
        
        public override string GetNameFoldout()
        {
            var baseName = base.GetNameFoldout();
            return $"<<<LONG CURRENCY>>>:{_currencyDropdownField.value}  {baseName} : {_amountLongField.value}";
        }
        public override VisualTreeAsset GetVisualTreeAsset()
        {
            return UIToolkitLoaderUtils.LoadUXML("LongCurrencyRewardDefinitionElement");
        }
    }
}