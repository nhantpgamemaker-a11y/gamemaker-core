using System.Net.NetworkInformation;
using GameMaker.Core.Editor;
using GameMaker.Core.Runtime;
using GameMaker.Item.Editor;
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
        private BaseDataManagerHolder<ItemStatDefinition> _itemStatManagerHolder;
        public ItemDefinitionHolder(VisualElement root) : base(root)
        {
            _itemFoldout = root.Q<Foldout>("ItemFoldout");
            _nameField = root.Q<TextField>("NameTextField");
            _dataManagementTemplateContainer = root.Q<TemplateContainer>("DataManagementTemplateContainer");
            _itemStatManagerHolder = new ItemStatDataManagerHolder(_dataManagementTemplateContainer);
        }
        public override void Bind(SerializedProperty elementProperty)
        {
            base.Bind(elementProperty);
            _nameField.RegisterValueChangedCallback(value =>
            {
                _itemFoldout.text = value.newValue;
            });
            _itemFoldout.text = serializedProperty.FindPropertyRelative("_name").stringValue;

            _itemStatManagerHolder.Bind(serializedProperty.FindPropertyRelative("_itemStatManager"));
        }
    }

    public class ItemStatDataManagerHolder : BaseDataManagerHolder<ItemStatDefinition>
    {
        public ItemStatDataManagerHolder(VisualElement root) : base(root)
        {
        }

        protected override DefinitionHolder CreateHolder()
        {
            var asset = Resources.Load<VisualTreeAsset>(
            "ItemStatDefinitionElement");
            var templateContainer = asset.CloneTree();
            templateContainer.style.height = StyleKeyword.Auto;
            templateContainer.style.flexGrow = 1;
            return new ItemStatDefinitionHolder(asset.CloneTree());
        }
    }
}
