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
            iconPreviewImage = root.Q<Image>("IconPreviewImage");
            iconField = root.Q<ObjectField>("IconField");
            titleTextField = root.Q<TextField>("TitleTextField");
            descriptionTextField = root.Q<TextField>("DescriptionTextField");
            definitionFoldout = root.Q<Foldout>("DefinitionFoldout");
        }

        public override void Bind(SerializedProperty elementProperty)
        {
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