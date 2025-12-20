using System.Collections.Generic;
using GameMaker.Core.Editor;
using GameMaker.Item.Runtime;
using NUnit.Framework;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace GameMaker.Item.Editor
{
    public class ItemDetailDefinitionHolder : BaseDefinitionHolder
    {
        private MultiColumnListView _statRefMultiColumnListView;
        private BaseDataManagerHolder<ItemDetailDefinition> _itemDetailDefinitionManagerHolder;
        private Foldout _itemDetailFoldout;
        private TextField _nameField;

        private DropdownField _itemReferenceDropdown;
        private SerializedProperty _itemStatDefinitionRefs;
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
            _itemDetailFoldout.name = serializedProperty.FindPropertyRelative("name").stringValue;
            _nameField.RegisterValueChangedCallback(value =>
            {
                _itemDetailFoldout.text = value.newValue;
            });
            _itemDetailFoldout.text = serializedProperty.FindPropertyRelative("name").stringValue;
            _itemStatDefinitionRefs = serializedProperty.FindPropertyRelative("_itemStatDefinitionRefs");
            var itemReferenceDefinitionId = serializedProperty.FindPropertyRelative("_itemDefinitionId").stringValue;
            var itemDefinition = ItemManager.Instance.GetDefinition(itemReferenceDefinitionId);
            _itemReferenceDropdown.SetValueWithoutNotify(itemDefinition.GetName());
            _statRefMultiColumnListView.columns.Clear();
            for (int i = 0; i < _itemStatDefinitionRefs.arraySize; i++)
            {
                var item = _itemStatDefinitionRefs.GetArrayElementAtIndex(i);
                var column = new Column()
                {
                    title = item.FindPropertyRelative("_name").stringValue,
                    makeCell = () =>
                    {
                        var floatField = new FloatField();
                        floatField.style.marginLeft = 3;
                        floatField.style.marginRight = 3;
                        return floatField;
                    },
                    bindCell = (element, index) =>
                    {
                        var floatField = element as FloatField;
                        var valueProp = item.FindPropertyRelative("_value");
                        floatField.BindProperty(valueProp);
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
