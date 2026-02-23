using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace GameMaker.Core.Editor
{
    public class ActionDefinitionHolder :DefinitionHolder
    {
        private Foldout _actionFoldout;
        private TextField _nameField;
        private TemplateContainer _dataManagementTemplateContainer;
        private ActionParamDataManagerHolder _actionParamDataManagerHolder;
        public ActionDefinitionHolder() : base()
        {
            
        }
        public ActionDefinitionHolder(VisualElement root) : base(root)
        {
            _actionFoldout = root.Q<Foldout>("ActionFoldout");
            _nameField = root.Q<TextField>("NameTextField");
            _dataManagementTemplateContainer = root.Q<TemplateContainer>("ActionParamDataManagerElement");
            _actionParamDataManagerHolder = new(_dataManagementTemplateContainer);
        }
        public override void Bind(SerializedProperty elementProperty)
        {
            base.Bind(elementProperty);
            _actionFoldout.name = serializedProperty.FindPropertyRelative("_name").stringValue;
            _nameField.RegisterValueChangedCallback(value =>
            {
                _actionFoldout.text = value.newValue;
            });
            _actionFoldout.text = serializedProperty.FindPropertyRelative("_name").stringValue;
            _actionParamDataManagerHolder.Bind(serializedProperty.FindPropertyRelative("_actionParamManager"));
        }
    }
}
