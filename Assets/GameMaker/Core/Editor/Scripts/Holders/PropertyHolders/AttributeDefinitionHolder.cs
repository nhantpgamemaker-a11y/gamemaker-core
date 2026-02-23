using GameMaker.Core.Runtime;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace GameMaker.Core.Editor
{
    [TypeContain(typeof(AttributeDefinition))]
    public class AttributeDefinitionHolder : PropertyDefinitionHolder
    {
        private TextField _defaultValueTextField;

        public AttributeDefinitionHolder(VisualElement root) : base(root)
        {

        }
        public override void Bind(SerializedProperty elementProperty)
        {
            _defaultValueTextField = Root.Q<TextField>("DefaultValueTextField");
            _defaultValueTextField.BindProperty(elementProperty.FindPropertyRelative("_defaultValue"));
             _defaultValueTextField.RegisterValueChangedCallback(c =>
            {
                UpdatePropertyFoldout();
            });
            base.Bind(elementProperty);
        }

        public override VisualTreeAsset GetVisualTreeAsset()
        {
            return UIToolkitLoaderUtils.LoadUXML("AttributeDefinitionElement");
        }
        public override string GetNameFoldout()
        {
            var baseName = base.GetNameFoldout();
            return $"<<<ATTRIBUTE>>> {baseName} : {_defaultValueTextField.value}";
        }
    }
}