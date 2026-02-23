using System.Linq;
using GameMaker.Core.Runtime;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace GameMaker.Core.Editor
{
    [TypeContain(typeof(CurrencyRewardDefinition))]
    public class CurrencyRewardDefinitionHolder : BaseRewardDefinitionHolder
    {
        private DropdownField _currencyDropdownField;
        private FloatField _amountFloatField;
        public CurrencyRewardDefinitionHolder(VisualElement root) : base(root)
        {
        }
        public override void Bind(SerializedProperty elementProperty)
        {
            _currencyDropdownField = Root.Q<DropdownField>("CurrencyDropdownField");
            _currencyDropdownField.choices = CurrencyManager.Instance.GetDefinitions().Select(x => x.GetName()).ToList();
            var data = CurrencyManager.Instance.GetDefinitions().FirstOrDefault(x => x.GetName() == _currencyDropdownField.value);
            if(data == null)
            {
                data = CurrencyManager.Instance.GetDefinitions().First();
            }
            _currencyDropdownField.value = data.GetName();

            
            _currencyDropdownField.RegisterValueChangedCallback(v =>
            {
                var data = CurrencyManager.Instance.GetDefinitions().FirstOrDefault(x => x.GetName() == _currencyDropdownField.value);
                if(data == null)
                {
                    data = CurrencyManager.Instance.GetDefinitions().First();
                }
                elementProperty.FindPropertyRelative("_referenceId").stringValue = data.GetID();
                elementProperty.serializedObject.ApplyModifiedProperties();
                UpdatePropertyFoldout();
            });
            
            _amountFloatField = Root.Q<FloatField>("AmountFloatField");
            _amountFloatField.BindProperty(elementProperty.FindPropertyRelative("_amount"));
            _amountFloatField.RegisterValueChangedCallback(c =>
            {
                UpdatePropertyFoldout();
            });
            base.Bind(elementProperty);
        }

        public override VisualTreeAsset GetVisualTreeAsset()
        {
            return UIToolkitLoaderUtils.LoadUXML("CurrencyRewardDefinitionElement");
        }
        public override string GetNameFoldout()
        {
            var baseName = base.GetNameFoldout();
            return $"<<<CURRENCY>>>:{_currencyDropdownField.value}  {baseName} : {_amountFloatField.value}";
        }
    }
}