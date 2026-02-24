using GameMaker.Core.Editor;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace GameMaker.Core.Editor
{
    [GameMaker.Core.Runtime.TypeCache]
    public abstract class CurrencyDefinitionHolder : BaseDefinitionHolder
    {
        private Foldout _currencyFoldout;
        private TextField _nameField;
        protected TemplateContainer templateContainer;
        public CurrencyDefinitionHolder(VisualElement root) : base(root)
        {
            var asset = GetVisualTreeAsset();
            templateContainer = asset.CloneTree();
            root.Add(templateContainer);
        }

        public override void Bind(SerializedProperty elementProperty)
        {
            _currencyFoldout = Root.Q<Foldout>("CurrencyFoldout");
            _nameField = Root.Q<TextField>("NameTextField");
            base.Bind(elementProperty);
            _nameField.RegisterValueChangedCallback(value =>
            {
                UpdateCurrencyFoldout();
            });
            UpdateCurrencyFoldout();
        }
        public virtual string GetNameFoldout()
        {
            return $"{serializedProperty.FindPropertyRelative("_name").stringValue}";
        }
        public abstract VisualTreeAsset GetVisualTreeAsset();
        public void UpdateCurrencyFoldout()
        {
            _currencyFoldout.text = GetNameFoldout();
        }
    }
}