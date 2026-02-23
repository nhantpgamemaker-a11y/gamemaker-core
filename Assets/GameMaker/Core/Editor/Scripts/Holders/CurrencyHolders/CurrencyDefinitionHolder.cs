using GameMaker.Core.Editor;
using UnityEditor;
using UnityEngine.UIElements;

namespace GameMaker.Core.Editor
{
    public class CurrencyDefinitionHolder : BaseDefinitionHolder
    {
        private Foldout _currencyFoldout;
        private TextField _nameField;
        public CurrencyDefinitionHolder(VisualElement root) : base(root)
        {
            _currencyFoldout = root.Q<Foldout>("CurrencyFoldout");
            _nameField = root.Q<TextField>("NameTextField");
        }

        public override void Bind(SerializedProperty elementProperty)
        {
            base.Bind(elementProperty);
            _currencyFoldout.name = serializedProperty.FindPropertyRelative("_name").stringValue;
            _nameField.RegisterValueChangedCallback(value =>
            {
                _currencyFoldout.text = value.newValue;
            });
            _currencyFoldout.text = serializedProperty.FindPropertyRelative("_name").stringValue;
        }
    }
}