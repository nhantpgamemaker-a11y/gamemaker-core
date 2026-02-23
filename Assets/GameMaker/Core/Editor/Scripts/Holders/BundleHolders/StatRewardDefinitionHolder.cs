using System.Linq;
using GameMaker.Core.Runtime;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace GameMaker.Core.Editor
{
    [TypeContain(typeof(StatRewardDefinition))]
    public class StatRewardDefinitionHolder : BaseRewardDefinitionHolder
    {
        private DropdownField _statDropdownField;
        private LongField _amountLongField;
        private EnumField _updateTypeEnumField;
        public StatRewardDefinitionHolder(VisualElement root) : base(root)
        {
        }
        public override void Bind(SerializedProperty elementProperty)
        {
            _statDropdownField = Root.Q<DropdownField>("StatDropdownField");
            _updateTypeEnumField = Root.Q<EnumField>("UpdateTypeEnumField");
            _updateTypeEnumField.BindProperty(elementProperty.FindPropertyRelative("_updateType"));
            _statDropdownField.choices = PropertyManager.Instance.GetStats().Select(x => x.GetName()).ToList();
            var data = PropertyManager.Instance.GetStats().FirstOrDefault(x => x.GetName() == _statDropdownField.value);
            if(data == null)
            {
                data = PropertyManager.Instance.GetStats().First();
            }
            _statDropdownField.value = data.GetName();

            
            _statDropdownField.RegisterValueChangedCallback(v =>
            {
                var data = PropertyManager.Instance.GetStats().FirstOrDefault(x => x.GetName() == _statDropdownField.value);
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
            return UIToolkitLoaderUtils.LoadUXML("StatRewardDefinitionElement");
        }
        public override string GetNameFoldout()
        {
            var baseName = base.GetNameFoldout();
            return $"<<<STAT>>>:{_statDropdownField.value}  {baseName} : {_amountLongField.value}";
        }
    }
}