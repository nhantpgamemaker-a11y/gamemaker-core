using System.Linq;
using System.Text.RegularExpressions;
using GameMaker.Core.Editor;
using GameMaker.Core.Runtime;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace GameMaker.Core.Editor
{
    [TypeContain(typeof(BigIntCurrencyDefinition))]
    public class BigIntCurrencyDefinitionHolder : CurrencyDefinitionHolder
    {
        private const string BIG_INT_FILTERED = @"[^0-9\-]";
        private TextField _defaultValueTextField;
         private TextField _maxValueTextField;
        public BigIntCurrencyDefinitionHolder(VisualElement root) : base(root)
        {
            _defaultValueTextField = root.Q<TextField>("DefaultValueTextField");
            _maxValueTextField = root.Q<TextField>("MaxValueTextField");
        }

        public override void Bind(SerializedProperty elementProperty)
        {
            base.Bind(elementProperty);
            _defaultValueTextField.RegisterValueChangedCallback(evt =>
            {
                string filtered = Regex.Replace(evt.newValue, BIG_INT_FILTERED, "");

                if (filtered.Count(c => c == '-') > 1)
                    filtered = filtered.Replace("-", "");

                if (filtered != evt.newValue)
                    _defaultValueTextField.SetValueWithoutNotify(filtered);

                serializedProperty.FindPropertyRelative("_defaultValue").stringValue = filtered;
                serializedProperty.serializedObject.ApplyModifiedProperties();
            });

            _maxValueTextField.RegisterValueChangedCallback(evt =>
            {
                string filtered = Regex.Replace(evt.newValue, BIG_INT_FILTERED, "");

                if (filtered.Count(c => c == '-') > 1)
                    filtered = filtered.Replace("-", "");

                if (filtered != evt.newValue)
                    _maxValueTextField.SetValueWithoutNotify(filtered);

                serializedProperty.FindPropertyRelative("_maxValue").stringValue = filtered;
                serializedProperty.serializedObject.ApplyModifiedProperties();
            });
            _defaultValueTextField.SetValueWithoutNotify(serializedProperty.FindPropertyRelative("_defaultValue").stringValue);
            _maxValueTextField.SetValueWithoutNotify(serializedProperty.FindPropertyRelative("_maxValue").stringValue);

            _defaultValueTextField.RegisterValueChangedCallback(evt =>
            {
                UpdateCurrencyFoldout();
            });
            _maxValueTextField.RegisterValueChangedCallback(evt =>
            {
                UpdateCurrencyFoldout();
            });
            UpdateCurrencyFoldout();
            
        }
        override public string GetNameFoldout()
        {
            var baseName = base.GetNameFoldout();
            return $"<<<BIG INT CURRENCY>>> {baseName} : {_defaultValueTextField.value} : {_maxValueTextField.value}";
        }

        public override VisualTreeAsset GetVisualTreeAsset()
        {
            return UIToolkitLoaderUtils.LoadUXML("BigIntCurrencyDefinitionElement");
        }
    }
}