using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using GameMaker.Core.Editor;
using GameMaker.Core.Runtime;
using GameMaker.Feature.Shop.Runtime;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace GameMaker.Feature.Shop.Editor
{
    [TypeContain(typeof(BigIntPrice))]
    public class BigIntPriceHolder : BasePriceHolder
    {
        private const string BIG_INT_FILTERED = @"[^0-9\-]";
        private TextField _amountTextField;
        public BigIntPriceHolder(VisualElement root) : base(root)
        {
            
            _amountTextField = root.Q<TextField>("AmountTextField");
        }
        public override void Bind(SerializedProperty serializedProperty)
        {
            base.Bind(serializedProperty);
            _amountTextField.RegisterValueChangedCallback(evt =>
            {
                string filtered = Regex.Replace(evt.newValue, BIG_INT_FILTERED, "");

                if (filtered.Count(c => c == '-') > 1)
                    filtered = filtered.Replace("-", "");

                if (filtered != evt.newValue)
                    _amountTextField.SetValueWithoutNotify(filtered);

                serializedProperty.FindPropertyRelative("_amount").stringValue = filtered;
                serializedProperty.serializedObject.ApplyModifiedProperties();
            });

            _amountTextField.SetValueWithoutNotify(serializedProperty.FindPropertyRelative("_amount").stringValue);
        }
        override public List<BaseCurrencyDefinition> GetCurrencyDefinitions()
        {
            return CurrencyManager.Instance.GetDefinitions().Where(x=>x.GetType() ==typeof(BigIntCurrencyDefinition)).ToList();
        }

        public override VisualTreeAsset GetVisualTreeAsset()
        {
            return UIToolkitLoaderUtils.LoadUXML("BigIntPriceElement");
        }
    }
}