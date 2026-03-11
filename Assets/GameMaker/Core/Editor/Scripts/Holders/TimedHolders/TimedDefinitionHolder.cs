using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace GameMaker.Core.Editor
{
    public class TimedDefinitionHolder : BaseDefinitionHolder
    {
        private Foldout _timedFoldout;
        private TextField _nameField;
        private LongField _defaultValueLongField;
        public TimedDefinitionHolder(VisualElement root) : base(root)
        {
            _timedFoldout = root.Q<Foldout>("TimedFoldout");
            _nameField = root.Q<TextField>("NameTextField");
            _defaultValueLongField = root.Q<LongField>("DefaultValueLongField");
        }
        public override void Bind(SerializedProperty elementProperty)
        {
            base.Bind(elementProperty);
            _timedFoldout.name = serializedProperty.FindPropertyRelative("_name").stringValue;
            _nameField.RegisterValueChangedCallback(value =>
            {
                _timedFoldout.text = value.newValue;
            });
            _timedFoldout.text = serializedProperty.FindPropertyRelative("_name").stringValue;
            _defaultValueLongField.BindProperty(serializedProperty.FindPropertyRelative("_defaultValue"));
        }
    }
}
