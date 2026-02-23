using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace GameMaker.Core.Editor
{
    public abstract class DefinitionHolder :BaseHolder
    {
        protected Button copyButton;
        protected TextField idTextField;
        protected TextField nameTextField;
        protected Foldout definitionFoldout;
        protected SerializedProperty serializedProperty;

        public DefinitionHolder():base()
        {
            
        }
        protected DefinitionHolder(VisualElement root):base(root)
        {
            
        }

        public override void Bind(SerializedProperty elementProperty)
        {
            copyButton = Root.Q<Button>("CopyIDButton");
            idTextField = Root.Q<TextField>("IDTextField");
            nameTextField = Root.Q<TextField>("NameTextField");
            definitionFoldout = Root.Q<Foldout>("DefinitionFoldout");
            idTextField.isReadOnly = false;
            serializedProperty = elementProperty;
            idTextField.BindProperty(serializedProperty.FindPropertyRelative("_id"));
            nameTextField.BindProperty(serializedProperty.FindPropertyRelative("_name"));
            definitionFoldout.text = $"{serializedProperty.FindPropertyRelative("_id").stringValue}_{serializedProperty.FindPropertyRelative("_name").stringValue}";
            copyButton.clicked += OnClickCopy;


            idTextField.RegisterValueChangedCallback(value =>
            {
                definitionFoldout.text = $"{serializedProperty.FindPropertyRelative("_id").stringValue}_{serializedProperty.FindPropertyRelative("_name").stringValue}";
            });
            nameTextField.RegisterValueChangedCallback(value =>
            {
                definitionFoldout.text = $"{serializedProperty.FindPropertyRelative("_id").stringValue}_{serializedProperty.FindPropertyRelative("_name").stringValue}";
            });

            Root.MarkDirtyRepaint();
        }

        private void OnClickCopy()
        {
            EditorGUIUtility.systemCopyBuffer =
            serializedProperty.FindPropertyRelative("_id")?.stringValue;
        }
    }
}