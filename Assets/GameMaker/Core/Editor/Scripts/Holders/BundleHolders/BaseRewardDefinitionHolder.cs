using System.Collections.Generic;
using System.Linq;
using GameMaker.Core.Runtime;
using UnityEditor;
using UnityEngine.UIElements;

namespace GameMaker.Core.Editor
{
    [GameMaker.Core.Runtime.TypeCache]
    public abstract class BaseRewardDefinitionHolder : BaseDefinitionHolder
    {
         private DropdownField _definitionDropdownField;
        private Foldout _rewardFoldout;
        private TextField _nameField;
        protected TemplateContainer templateContainer;
        protected DropdownField definitionDropdownField => _definitionDropdownField;
        public BaseRewardDefinitionHolder(VisualElement root) : base(root)
        {
            var asset = GetVisualTreeAsset();
            templateContainer = asset.CloneTree();
            root.Add(templateContainer);
            _definitionDropdownField = Root.Q<DropdownField>("DefinitionDropdownField");
        }
        public abstract VisualTreeAsset GetVisualTreeAsset();

        public override void Bind(SerializedProperty elementProperty)
        {
            _nameField = Root.Q<TextField>("NameTextField");
            _rewardFoldout = Root.Q<Foldout>("RewardFoldout");
            
            base.Bind(elementProperty);
            _nameField.RegisterValueChangedCallback(value =>
            {
                UpdatePropertyFoldout();
            });
            UpdatePropertyFoldout();
            _definitionDropdownField.choices = GetRewardDefinitions().Select(x => x.GetName()).ToList();
            var data = GetRewardDefinitions().FirstOrDefault(x => x.GetID() == elementProperty.FindPropertyRelative("_referenceId").stringValue);
            if (data != null)
            {
                _definitionDropdownField.value = data.GetName();
            }
            else
            {
                if (GetRewardDefinitions().Count > 0)
                {
                    _definitionDropdownField.value = GetRewardDefinitions()[0].GetName();
                    elementProperty.FindPropertyRelative("_referenceId").stringValue = GetRewardDefinitions()[0].GetID();
                    elementProperty.serializedObject.ApplyModifiedProperties();
                }
                UpdatePropertyFoldout();
            }
            _definitionDropdownField.RegisterValueChangedCallback(v =>
            {
                var data = GetRewardDefinitions().FirstOrDefault(x => x.GetName() == _definitionDropdownField.value);
                if (data != null)
                {
                    elementProperty.FindPropertyRelative("_referenceId").stringValue = data.GetID();
                    elementProperty.serializedObject.ApplyModifiedProperties();
                }
                UpdatePropertyFoldout();
            });
            UpdatePropertyFoldout();
        }
        public virtual string GetNameFoldout()
        {
            return $"{serializedProperty.FindPropertyRelative("_name").stringValue} : {_definitionDropdownField.value}";
        }
        public void UpdatePropertyFoldout()
        {
            _rewardFoldout.text = GetNameFoldout();
        }
        public abstract List<BaseDefinition> GetRewardDefinitions();
    }
}
