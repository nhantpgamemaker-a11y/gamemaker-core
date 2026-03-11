using GameMaker.Core.Editor;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace GameMaker.Feature.Shop.Editor
{
    [GameMaker.Core.Runtime.TypeCache]
    public class TimeResetConfigHolder : BaseHolder
    {
        private TextField _cronExpressionTextField;
        private EnumField _resetTimeModeEnumField;
        public TimeResetConfigHolder(VisualElement root) : base(root)
        {
            _cronExpressionTextField = root.Q<TextField>("CronExpressionTextField");
            _resetTimeModeEnumField = root.Q<EnumField>("ResetTimeModeEnumField");
        }
        public override void Bind(SerializedProperty elementProperty)
        {
            base.Bind(elementProperty);
            
            _cronExpressionTextField.BindProperty(elementProperty.FindPropertyRelative("_cronExpression"));
            _resetTimeModeEnumField.BindProperty(elementProperty.FindPropertyRelative("_resetTimeMode"));
        }
    }
}