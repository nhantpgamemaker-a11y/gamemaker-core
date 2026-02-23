using UnityEditor;
using UnityEngine.UIElements;

namespace GameMaker.Core.Editor
{
    public abstract class ItemPropertyDefinitionHolder : DefinitionHolder
    {
        private Foldout _itemPropertyFoldout;
        private TextField _nameField;
        protected TemplateContainer templateContainer;
        public ItemPropertyDefinitionHolder(VisualElement root) : base(root)
        {
            var asset = GetVisualTreeAsset();
            templateContainer = asset.CloneTree();
            root.Add(templateContainer);
        }
        public abstract VisualTreeAsset GetVisualTreeAsset();

        public override void Bind(SerializedProperty elementProperty)
        {
            _nameField = Root.Q<TextField>("NameTextField");
            _itemPropertyFoldout = Root.Q<Foldout>("ItemPropertyFoldout");
            
            base.Bind(elementProperty);
            _nameField.RegisterValueChangedCallback(value =>
            {
                UpdatePropertyFoldout();
            });
            UpdatePropertyFoldout(); 
        }
        public virtual string GetNameFoldout()
        {
            return $"{serializedProperty.FindPropertyRelative("_name").stringValue}";
        }
        public void UpdatePropertyFoldout()
        {
            _itemPropertyFoldout.text = GetNameFoldout();
        }
    }
}
