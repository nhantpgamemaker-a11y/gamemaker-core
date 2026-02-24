using GameMaker.Core.Editor;
using GameMaker.Core.Runtime;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace GameMaker.Core.Editor
{
    [TypeContain(typeof(LongCurrencyDefinition))]
    public class LongCurrencyDefinitionHolder : CurrencyDefinitionHolder
    {
        private LongField _defaultValueLongField;
         private LongField _maxValueLongField;
        public LongCurrencyDefinitionHolder(VisualElement root) : base(root)
        {
            _defaultValueLongField = root.Q<LongField>("DefaultValueLongField"); 
            _maxValueLongField = root.Q<LongField>("MaxValueLongField");
        }

        public override void Bind(SerializedProperty elementProperty)
        {
            base.Bind(elementProperty);
            _defaultValueLongField.BindProperty(serializedProperty.FindPropertyRelative("_defaultValue"));
            _maxValueLongField.BindProperty(serializedProperty.FindPropertyRelative("_maxValue"));
            _defaultValueLongField.RegisterValueChangedCallback(evt =>
            {
                UpdateCurrencyFoldout();
            });
            _maxValueLongField.RegisterValueChangedCallback(evt =>
            {
                UpdateCurrencyFoldout();
            });
            UpdateCurrencyFoldout();
        }
        override public string GetNameFoldout()
        {
            var baseName = base.GetNameFoldout();
            return $"<<<LONG CURRENCY>>> {baseName} : {_defaultValueLongField.value} : {_maxValueLongField.value}";
        }

        public override VisualTreeAsset GetVisualTreeAsset()
        {
            return UIToolkitLoaderUtils.LoadUXML("LongCurrencyDefinitionElement");
        }
    }
}