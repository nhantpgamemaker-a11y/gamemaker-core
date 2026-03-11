using System.Collections.Generic;
using System.Linq;
using GameMaker.Core.Runtime;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace GameMaker.Core.Editor
{
    [TypeContain(typeof(ItemRewardDefinition))]
    public class ItemRewardDefinitionHolder : BaseRewardDefinitionHolder
    {
        private FloatField _amountFloatField;
        private ObjectField _createTemplateObjectField;

        public ItemRewardDefinitionHolder(VisualElement root) : base(root)
        {
        }
        public override void Bind(SerializedProperty elementProperty)
        {
            _amountFloatField = Root.Q<FloatField>("AmountFloatField");
            _createTemplateObjectField = Root.Q<ObjectField>("CreateTemplateObjectField");
            _createTemplateObjectField.BindProperty(elementProperty.FindPropertyRelative("_createItemTemplate"));
            _amountFloatField.BindProperty(elementProperty.FindPropertyRelative("_amount"));
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
            return UIToolkitLoaderUtils.LoadUXML("ItemRewardDefinitionElement");
        }
        public override string GetNameFoldout()
        {
            var baseName = base.GetNameFoldout();
            return $"<<<ITEM DETAIL>>>: {baseName} : {_amountFloatField.value}";
        }

        public override List<BaseDefinition> GetRewardDefinitions()
        {
            return ItemDetailManager.Instance.GetDefinitions().Cast<BaseDefinition>().ToList();
        }
    }
}