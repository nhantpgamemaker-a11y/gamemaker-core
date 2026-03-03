using System.Collections.Generic;
using System.Linq;
using GameMaker.Core.Editor;
using GameMaker.Core.Runtime;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace GameMaker.Feature.Shop.Editor
{
    [GameMaker.Core.Runtime.TypeCache]
    public abstract class BaseShopItemDefinitionHolder : BaseDefinitionHolder
    {
         private Foldout _shopItemFoldout;
        private DropdownField _definitionDropdown;
        private PriceWrapperHolder _priceWrapperHolder;
         private TextField _nameField;
        protected BaseShopItemDefinitionHolder(VisualElement root) : base(root)
        {
            var asset = GetVisualTreeAsset();
            var templateContainer = asset.CloneTree();
            root.Add(templateContainer);
            _definitionDropdown = root.Q<DropdownField>("DefinitionDropdown");
            _priceWrapperHolder = new PriceWrapperHolder(Root.Q<VisualElement>("PriceWrapperElement"), "_price");
            _nameField = Root.Q<TextField>("NameTextField");
            _shopItemFoldout = Root.Q<Foldout>("ShopItemFoldout");
        }
        public override void Bind(SerializedProperty elementProperty)
        {
            base.Bind(elementProperty);
            _priceWrapperHolder.Bind(elementProperty);
            _definitionDropdown.choices = GetDefinitions().Select(x => x.GetName()).ToList();
            var definitionReferenceId = elementProperty.FindPropertyRelative("_referenceId").stringValue;
            var definition = GetDefinitions().FirstOrDefault(x => x.GetID() == definitionReferenceId);
            if (definition != null)
            {
                _definitionDropdown.value = definition.GetName();
            }
            else if (GetDefinitions().Count > 0)
            {
                _definitionDropdown.value = GetDefinitions().First().GetName();
                elementProperty.FindPropertyRelative("_referenceId").stringValue = GetDefinitions().First().GetID();
                elementProperty.serializedObject.ApplyModifiedProperties();
            }
            _definitionDropdown.RegisterValueChangedCallback(value =>
            {
                var definition = GetDefinitions().FirstOrDefault(x => x.GetName() == value.newValue);
                if (definition != null)
                {
                    elementProperty.FindPropertyRelative("_referenceId").stringValue = definition.GetID();
                    elementProperty.serializedObject.ApplyModifiedProperties();
                }
            });
            _nameField.RegisterValueChangedCallback(value =>
            {
                UpdateShopItemFoldout();
            });
            UpdateShopItemFoldout();
        }
        public abstract List<BaseDefinition> GetDefinitions();
        public abstract VisualTreeAsset GetVisualTreeAsset();
        public virtual string GetNameFoldout()
        {
            return $"{serializedProperty.FindPropertyRelative("_name").stringValue}";
        }
        public void UpdateShopItemFoldout()
        {
            _shopItemFoldout.text = GetNameFoldout();
        }
    }
}