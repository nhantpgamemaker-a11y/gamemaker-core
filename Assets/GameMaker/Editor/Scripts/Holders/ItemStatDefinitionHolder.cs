using GameMaker.Core.Editor;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace GameMaker.Core.Editor
{
    public class ItemStatDefinitionHolder : DefinitionHolder
    {
        private FloatField _defaultValueFloatField;
        private Foldout _itemStatFoldout;
        private TextField _nameField;
        public ItemStatDefinitionHolder(VisualElement root) : base(root)
        {
            _defaultValueFloatField = root.Q<FloatField>("DefaultValueFloatField");
            _itemStatFoldout = root.Q<Foldout>("ItemStatFoldout");
            _nameField = root.Q<TextField>("NameTextField");
        }
        public override void Bind(SerializedProperty elementProperty)
        {
            base.Bind(elementProperty);
            _defaultValueFloatField.BindProperty(serializedProperty.FindPropertyRelative("_defaultValue"));
            _nameField.RegisterValueChangedCallback(value =>
            {
                _itemStatFoldout.text = GetNameFoldout();
            });

            _defaultValueFloatField.RegisterValueChangedCallback(value =>
            {
                _itemStatFoldout.text = GetNameFoldout();
            });
            _itemStatFoldout.text = GetNameFoldout();
        }

        public string GetNameFoldout()
        {
            return $"{serializedProperty.FindPropertyRelative("_name").stringValue} : {_defaultValueFloatField.value}";
        }
    }
}
