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
    [TypeContain(typeof(LongPrice))]
    public class LongPriceHolder : BasePriceHolder
    {
        private LongField _amountLongField;
        public LongPriceHolder(VisualElement root) : base(root)
        {

            _amountLongField = root.Q<LongField>("AmountLongField");
        }
        public override void Bind(SerializedProperty elementProperty)
        {
            base.Bind(elementProperty);
            _amountLongField.BindProperty(elementProperty.FindPropertyRelative("_amount"));
        }
        override public List<BaseCurrencyDefinition> GetCurrencyDefinitions()
        {
            return CurrencyManager.Instance.GetDefinitions().Where(x => x.GetType() == typeof(LongCurrencyDefinition)).ToList();
        }

        public override VisualTreeAsset GetVisualTreeAsset()
        {
            return UIToolkitLoaderUtils.LoadUXML("LongPriceElement");
        }
    }
}