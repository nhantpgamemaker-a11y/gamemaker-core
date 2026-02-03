using GameMaker.Core.Runtime;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace GameMaker.Core.Editor
{
    [TypeContain(typeof(StatDefinition))]
    public class StatDefinitionHolder : PropertyDefinitionHolder
    {
        private FloatField _defaultValueFloatField;

        public StatDefinitionHolder(VisualElement root) : base(root)
        {

        }
        public override void Bind(SerializedProperty elementProperty)
        {
            _defaultValueFloatField = Root.Q<FloatField>("DefaultValueFloatField");
            _defaultValueFloatField.BindProperty(elementProperty.FindPropertyRelative("_defaultValue"));
             _defaultValueFloatField.RegisterValueChangedCallback(c =>
            {
                UpdatePropertyFoldout();
            });
            base.Bind(elementProperty);
        }

        public override VisualTreeAsset GetVisualTreeAsset()
        {
            return UIToolkitLoaderUtils.LoadUXML("StatDefinitionElement");
        }
        public override string GetNameFoldout()
        {
            var baseName = base.GetNameFoldout();
            return $"<<<STAT>>> {baseName} : {_defaultValueFloatField.value}";
        }
    }
}