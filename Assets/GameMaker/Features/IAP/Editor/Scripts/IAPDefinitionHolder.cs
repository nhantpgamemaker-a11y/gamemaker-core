using GameMaker.Core.Editor;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace GameMaker.IAP.Editor
{
    public class IAPDefinitionHolder : BaseDefinitionHolder
    {
        private Foldout _iapFoldout;
        private TextField _nameField;
        private BundleDefinitionHolder _bundleDefinitionHolder;
        private EnumField _productTypeEnumField;
        private TextField _productIdTextField;

        public IAPDefinitionHolder(VisualElement root) : base(root)
        {
            _iapFoldout = root.Q<Foldout>("IAPFoldout");
            _nameField = root.Q<TextField>("NameTextField");
            _bundleDefinitionHolder = new BundleDefinitionHolder(root.Q<VisualElement>("BundleDefinitionElement"));
            _productTypeEnumField = root.Q<EnumField>("ProductEnumField");
            _productIdTextField = root.Q<TextField>("ProductIDTextField");
        }
        public override void Bind(SerializedProperty elementProperty)
        {
            base.Bind(elementProperty);
            _iapFoldout.name = serializedProperty.FindPropertyRelative("_name").stringValue;
            _nameField.RegisterValueChangedCallback(value =>
            {
                _iapFoldout.text = value.newValue;
            });
            _iapFoldout.text = serializedProperty.FindPropertyRelative("_name").stringValue;
            _bundleDefinitionHolder.Bind(serializedProperty.FindPropertyRelative("_reward"));
            _productTypeEnumField.BindProperty(serializedProperty.FindPropertyRelative("_productType"));
            _productIdTextField.BindProperty(serializedProperty.FindPropertyRelative("_productId"));

        }
    }
}
