
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace GameMaker.Core.Editor
{
    public class ItemDefinitionHolder : BaseDefinitionHolder
    {
        private Foldout _itemFoldout;
        private TextField _nameField;
        private TemplateContainer _dataManagementTemplateContainer;
        private ItemPropertyDataManagerHolder _itemPropertyManagerHolder;
        public ItemDefinitionHolder(VisualElement root) : base(root)
        {
            _itemFoldout = root.Q<Foldout>("ItemFoldout");
            _nameField = root.Q<TextField>("NameTextField");
            _dataManagementTemplateContainer = root.Q<TemplateContainer>("DataManagementTemplateContainer");
            _itemPropertyManagerHolder = new(_dataManagementTemplateContainer);
        }
        public override void Bind(SerializedProperty elementProperty)
        {
            base.Bind(elementProperty);
            _nameField.RegisterValueChangedCallback(value =>
            {
                _itemFoldout.text = value.newValue;
            });
            _itemFoldout.text = serializedProperty.FindPropertyRelative("_name").stringValue;

            _itemPropertyManagerHolder.Bind(serializedProperty.FindPropertyRelative("_itemPropertyManager"));
        }
    }
}
