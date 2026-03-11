using GameMaker.Core.Editor;
using GameMaker.Feature.Shop.Runtime;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace GameMaker.Feature.Shop.Editor
{
    [GameMaker.Core.Runtime.TypeCache]
    public class ShopDefinitionHolder : BaseDefinitionHolder
    {
        private Foldout _shopFoldout;
        private TextField _nameField;
        private VisualElement _timeResetConfigElement;
        private TimeResetConfigHolder _timeResetConfigHolder;
        private ShopItemDataManagerHolder _shopItemManagerHolder;
        public ShopDefinitionHolder(VisualElement root) : base(root)
        {
            _shopFoldout = root.Q<Foldout>("ShopFoldout");
            _nameField = root.Q<TextField>("NameTextField");
            _timeResetConfigElement = root.Q<VisualElement>("TimeResetConfigElement");
            _timeResetConfigHolder = new TimeResetConfigHolder(_timeResetConfigElement);
            _shopItemManagerHolder = new ShopItemDataManagerHolder(root.Q<VisualElement>("ShopItemManagerElement"));
        }
        public override void Bind(SerializedProperty elementProperty)
        {
            base.Bind(elementProperty);
            _shopFoldout.name = serializedProperty.FindPropertyRelative("_name").stringValue;
            _nameField.RegisterValueChangedCallback(value =>
            {
                _shopFoldout.text = value.newValue;
            });
            _shopFoldout.text = serializedProperty.FindPropertyRelative("_name").stringValue;
            _timeResetConfigHolder.Bind(serializedProperty.FindPropertyRelative("_timeResetConfig"));
            _shopItemManagerHolder.Bind(serializedProperty.FindPropertyRelative("_shopItemManager"));
        }
    }
}
