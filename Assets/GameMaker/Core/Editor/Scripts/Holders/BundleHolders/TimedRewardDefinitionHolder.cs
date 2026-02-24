using System.Linq;
using GameMaker.Core.Runtime;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace GameMaker.Core.Editor
{
    [TypeContain(typeof(TimedRewardDefinition))]
    public class TimedRewardDefinitionHolder : BaseRewardDefinitionHolder
    {
        private DropdownField _timedDropdownField;
        private LongField _amountLongField;
        public TimedRewardDefinitionHolder(VisualElement root) : base(root)
        {
        }
        public override void Bind(SerializedProperty elementProperty)
        {
            _timedDropdownField = Root.Q<DropdownField>("TimedDropdownField");
            _timedDropdownField.choices = TimedManager.Instance.GetDefinitions().Select(x => x.GetName()).ToList();
            var data = TimedManager.Instance.GetDefinitions().FirstOrDefault(x => x.GetName() == _timedDropdownField.value);
            if(data == null)
            {
                data = TimedManager.Instance.GetDefinitions().First();
            }
            _timedDropdownField.value = data.GetName();

            
            _timedDropdownField.RegisterValueChangedCallback(v =>
            {
                var data = PropertyManager.Instance.GetStats().FirstOrDefault(x => x.GetName() == _timedDropdownField.value);
                if(data == null)
                {
                    data = PropertyManager.Instance.GetStats().First();
                }
                elementProperty.FindPropertyRelative("_referenceId").stringValue = data.GetID();
                elementProperty.serializedObject.ApplyModifiedProperties();
                UpdatePropertyFoldout();
            });
            
            _amountLongField = Root.Q<LongField>("AmountLongField");
            _amountLongField.BindProperty(elementProperty.FindPropertyRelative("_amount"));
            _amountLongField.RegisterValueChangedCallback(c =>
            {
                UpdatePropertyFoldout();
            });
            base.Bind(elementProperty);
        }

        public override VisualTreeAsset GetVisualTreeAsset()
        {
            return UIToolkitLoaderUtils.LoadUXML("TimedRewardDefinitionElement");
        }
        public override string GetNameFoldout()
        {
            var baseName = base.GetNameFoldout();
            return $"<<<TIMED>>>:{_timedDropdownField.value}  {baseName} : {_amountLongField.value}";
        }
    }
}