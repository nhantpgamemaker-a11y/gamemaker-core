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


        protected DefinitionHolder(VisualElement root):base(root)
        {
            copyButton = root.Q<Button>("CopyIDButton");
            idTextField = root.Q<TextField>("IDTextField");
            nameTextField = root.Q<TextField>("NameTextField");
            definitionFoldout = root.Q<Foldout>("DefinitionFoldout");
            idTextField.isReadOnly = false;
        }

        public override void Bind(SerializedProperty elementProperty)
        {
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