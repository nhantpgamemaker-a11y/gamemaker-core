using System.Collections.Generic;
using GameMaker.Core.Editor;
using GameMaker.Core.Runtime;
using NUnit.Framework;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace GameMaker.Core.Editor
{
    public class ItemDetailDefinitionHolder : BaseDefinitionHolder
    {
        private MultiColumnListView _statRefMultiColumnListView;
        private BaseDataManagerHolder<ItemDetailDefinition> _itemDetailDefinitionManagerHolder;
        private Foldout _itemDetailFoldout;
        private TextField _nameField;

        private DropdownField _itemReferenceDropdown;
        private SerializedProperty _itemPropertyDefinitionRefs;
        public ItemDetailDefinitionHolder(VisualElement root) : base(root)
        {
            _itemDetailFoldout = root.Q<Foldout>("ItemDetailFoldout");
            _nameField = root.Q<TextField>("NameTextField");
            _statRefMultiColumnListView = root.Q<MultiColumnListView>("StatRefMultiColumnListView");
            _itemReferenceDropdown = root.Q<DropdownField>("ItemReferenceDropdown");
        }
        public override void Bind(SerializedProperty elementProperty)
        {
            base.Bind(elementProperty);
            _itemDetailFoldout.name = serializedProperty.FindPropertyRelative("_name").stringValue;
            _nameField.RegisterValueChangedCallback(value =>
            {
                _itemDetailFoldout.text = value.newValue;
            });
            _itemDetailFoldout.text = serializedProperty.FindPropertyRelative("_name").stringValue;
            _itemPropertyDefinitionRefs = serializedProperty.FindPropertyRelative("_itemPropertyDefinitionRefs");
            var itemReferenceDefinitionId = serializedProperty.FindPropertyRelative("_itemDefinitionId").stringValue;
            var itemDefinition = ItemManager.Instance.GetDefinition(itemReferenceDefinitionId);
            _itemReferenceDropdown.SetValueWithoutNotify(itemDefinition.GetName());
            _statRefMultiColumnListView.columns.Clear();
            for (int i = 0; i < _itemPropertyDefinitionRefs.arraySize; i++)
            {
                var item = _itemPropertyDefinitionRefs.GetArrayElementAtIndex(i);
                var valueProp = item.FindPropertyRelative("_value");
                var column = new Column()
                {
                    title = item.FindPropertyRelative("_name").stringValue,
                    makeCell = () =>
                    {
                        switch (valueProp.propertyType)
                        {
                            case SerializedPropertyType.String:
                                var textField = new TextField();
                                textField.style.marginLeft = 3;
                                textField.style.marginRight = 3;
                                return textField;
                            case SerializedPropertyType.Float:
                                var floatField = new FloatField();
                                floatField.style.marginLeft = 3;
                                floatField.style.marginRight = 3;
                                return floatField;
                            default:
                                var label = new Label();
                                label.style.marginLeft = 3;
                                label.style.marginRight = 3;
                                return label;
                        }
                        
                    },
                    bindCell = (element, index) =>
                    {
                        if (element is FloatField floatField)
                            floatField.BindProperty(valueProp);
                        if (element is TextField textField)
                            textField.BindProperty(valueProp);
                        if(element is Label label)
                            label.text = "Not Support";
                    },
                    stretchable = true,
                };
                _statRefMultiColumnListView.columns.Add(column);
            }
            _statRefMultiColumnListView.itemsSource = new List<string>() { "_" };
            _statRefMultiColumnListView.Rebuild();
            _statRefMultiColumnListView.RefreshItems();
        }
    }
}
