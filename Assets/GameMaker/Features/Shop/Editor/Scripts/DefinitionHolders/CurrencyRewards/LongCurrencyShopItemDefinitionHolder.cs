using System.Collections.Generic;
using System.Linq;
using GameMaker.Core.Editor;
using GameMaker.Core.Runtime;
using GameMaker.Feature.Shop.Runtime;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace GameMaker.Feature.Shop.Editor
{
    [TypeContain(typeof(LongCurrencyShopItemDefinition))]
    public class LongCurrencyShopItemDefinitionHolder : BaseCurrencyShopItemDefinitionHolder
    {
        private LongField _amountLongField;
        public LongCurrencyShopItemDefinitionHolder(VisualElement root) : base(root)
        {
            _amountLongField = root.Q<LongField>("AmountLongField");
        }
        public override void Bind(SerializedProperty elementProperty)
        {
            base.Bind(elementProperty);
            _amountLongField.BindProperty(elementProperty.FindPropertyRelative("_amount"));
                _amountLongField.RegisterValueChangedCallback(value =>
                {
                    UpdateShopItemFoldout();
                });
                _amountLongField.SetValueWithoutNotify(serializedProperty.FindPropertyRelative("_amount").longValue);
        }

        public override List<BaseDefinition> GetDefinitions()
        {
            return CurrencyManager.Instance.GetDefinitions().Where(x=>x.GetType() ==typeof(LongCurrencyDefinition)).Cast<BaseDefinition>().ToList();
        }

        public override VisualTreeAsset GetVisualTreeAsset()
        {
            return UIToolkitLoaderUtils.LoadUXML("LongCurrencyShopItemDefinitionElement");
        }
        override public string GetNameFoldout()
        {
            var baseName = base.GetNameFoldout();
            return $"<<<LONG CURRENCY>>> {baseName} : {_amountLongField.value}";
        }
    }
}