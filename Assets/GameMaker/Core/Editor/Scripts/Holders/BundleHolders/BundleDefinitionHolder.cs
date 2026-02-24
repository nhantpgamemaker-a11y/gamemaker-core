using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace GameMaker.Core.Editor
{
    public class BundleDefinitionHolder : BaseDefinitionHolder
    {
        private Foldout _bundleFoldout;
        private TextField _nameField;
        private TemplateContainer _dataManagementTemplateContainer;
        private RewardDefinitionDataManagerHolder _rewardDefinitionDataManagerHolder;
        public BundleDefinitionHolder(VisualElement root) : base(root)
        {
            _bundleFoldout = root.Q<Foldout>("BundleFoldout");
            _nameField = root.Q<TextField>("NameTextField");
            _dataManagementTemplateContainer = root.Q<TemplateContainer>("DataManagementTemplateContainer");
            _rewardDefinitionDataManagerHolder = new(_dataManagementTemplateContainer);
        }
        public override void Bind(SerializedProperty elementProperty)
        {
            base.Bind(elementProperty);
            _nameField.RegisterValueChangedCallback(value =>
            {
                _bundleFoldout.text = value.newValue;
            });
            _bundleFoldout.text = serializedProperty.FindPropertyRelative("_name").stringValue;

            _rewardDefinitionDataManagerHolder.Bind(serializedProperty.FindPropertyRelative("_rewardManager"));
        }
    }
}
