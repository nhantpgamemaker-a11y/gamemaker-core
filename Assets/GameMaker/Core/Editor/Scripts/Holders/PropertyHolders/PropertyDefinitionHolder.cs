using System;
using GameMaker.Core.Runtime;
using UnityEditor;
using UnityEngine.UIElements;

namespace GameMaker.Core.Editor
{
    [TypeCache]
    public abstract class PropertyDefinitionHolder : BaseDefinitionHolder
    {
        private Foldout _propertyFoldout;
        private TextField _nameField;
        protected TemplateContainer templateContainer;
        public PropertyDefinitionHolder(VisualElement root) : base(root)
        {
            var asset = GetVisualTreeAsset();
            templateContainer = asset.CloneTree();
            root.Add(templateContainer);
        }
        public override void Bind(SerializedProperty elementProperty)
        {
            _nameField = Root.Q<TextField>("NameTextField");
            _propertyFoldout = Root.Q<Foldout>("PropertyFoldout");
            base.Bind(elementProperty);
            _nameField.RegisterValueChangedCallback(value =>
            {
                UpdatePropertyFoldout();
            });
            UpdatePropertyFoldout();
            
        }
        public abstract VisualTreeAsset GetVisualTreeAsset();

        public virtual string GetNameFoldout()
        {
            return $"{serializedProperty.FindPropertyRelative("_name").stringValue}";
        }
        public void UpdatePropertyFoldout()
        {
            _propertyFoldout.text = GetNameFoldout();
        }
    }
}