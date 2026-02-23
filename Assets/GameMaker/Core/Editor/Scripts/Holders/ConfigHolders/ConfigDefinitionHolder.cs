using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace GameMaker.Core.Editor
{
    public class ConfigDefinitionHolder : DefinitionHolder
    {
        private Foldout _configFoldout;
        private TextField _nameField;
        private TextField _valueTextFiled;
        public ConfigDefinitionHolder(VisualElement root) : base(root)
        {
            _configFoldout = root.Q<Foldout>("ConfigFoldout");
            _nameField = root.Q<TextField>("NameTextField");
            _valueTextFiled = root.Q<TextField>("ValueTextFiled");
        }

        public override void Bind(SerializedProperty elementProperty)
        {
            base.Bind(elementProperty);
            _configFoldout.name = serializedProperty.FindPropertyRelative("_name").stringValue;
            _nameField.RegisterValueChangedCallback(value =>
            {
                _configFoldout.text = value.newValue;
            });
            _configFoldout.text = serializedProperty.FindPropertyRelative("_name").stringValue;
            _valueTextFiled.BindProperty(serializedProperty.FindPropertyRelative("_value")); 
        }
    }
}