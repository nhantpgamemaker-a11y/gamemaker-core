using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace GameMaker.Core.Editor
{
    public abstract class BaseDefinitionHolder :DefinitionHolder
    {
        protected Image iconPreviewImage;
        protected ObjectField iconField;
        protected TextField titleTextField;
        protected TextField descriptionTextField;
        

        protected BaseDefinitionHolder(VisualElement root):base(root)
        {
            
        }

        public override void Bind(SerializedProperty elementProperty)
        {
            iconPreviewImage = Root.Q<Image>("IconPreviewImage");
            iconField = Root.Q<ObjectField>("IconField");
            titleTextField = Root.Q<TextField>("TitleTextField");
            descriptionTextField = Root.Q<TextField>("DescriptionTextField");
            definitionFoldout = Root.Q<Foldout>("DefinitionFoldout");
            
            base.Bind(elementProperty);
            titleTextField.BindProperty(serializedProperty.FindPropertyRelative("_title"));
            iconField.BindProperty(serializedProperty.FindPropertyRelative("_icon"));
            descriptionTextField.BindProperty(serializedProperty.FindPropertyRelative("_description"));

            iconPreviewImage.sprite = serializedProperty.FindPropertyRelative("_icon")?.objectReferenceValue as Sprite;
            iconField.RegisterValueChangedCallback(value =>
            {
                iconPreviewImage.sprite = value.newValue as Sprite;
            });
            iconPreviewImage.sprite = serializedProperty.FindPropertyRelative("_icon")?.objectReferenceValue as Sprite;
            Root.MarkDirtyRepaint();
        }
    }
}