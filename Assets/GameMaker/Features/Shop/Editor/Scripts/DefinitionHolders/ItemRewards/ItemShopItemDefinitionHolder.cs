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
    [TypeContain(typeof(ItemShopItemDefinition))]
    public class ItemShopItemDefinitionHolder : BaseShopItemDefinitionHolder
    {
        private IntegerField _amountIntField;
        private ObjectField _createItemTemplateObjectField;
        public ItemShopItemDefinitionHolder(VisualElement root) : base(root)
        {
            _amountIntField = root.Q<IntegerField>("AmountIntField");
            _createItemTemplateObjectField = root.Q<ObjectField>("CreateItemTemplateObjectField");
        }
        public override void Bind(SerializedProperty elementProperty)
        {
            base.Bind(elementProperty);
            _amountIntField.BindProperty(elementProperty.FindPropertyRelative("_amount"));
            _amountIntField.RegisterValueChangedCallback(value =>
            {
                UpdateShopItemFoldout();
            });
            _createItemTemplateObjectField.BindProperty(elementProperty.FindPropertyRelative("_createItemTemplate"));
        }

        public override List<BaseDefinition> GetDefinitions()
        {
            return ItemDetailManager.Instance.GetDefinitions().Cast<BaseDefinition>().ToList();
        }

        public override VisualTreeAsset GetVisualTreeAsset()
        {
            return UIToolkitLoaderUtils.LoadUXML("ItemShopItemDefinitionElement");
        }
        override public string GetNameFoldout()
        {
            var baseName = base.GetNameFoldout();
            return $"<<<ITEM>>> {baseName} : {_amountIntField.value}";
        } 
    }
}