using GameMaker.Core.Editor;
using GameMaker.Core.Runtime;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace GameMaker.Item.Editor
{
    [TypeContain(typeof(ItemAttributeDefinition))]
    public class ItemAttributeDefinitionHolder : ItemPropertyDefinitionHolder
    {
         private TextField _defaultValueTextField;
        public ItemAttributeDefinitionHolder(VisualElement root) : base(root)
        {
            
        }
        public override void Bind(SerializedProperty elementProperty)
        {
            
            _defaultValueTextField = templateContainer.Q<TextField>("DefaultValueTextField");
            _defaultValueTextField.BindProperty(elementProperty.FindPropertyRelative("_defaultValue"));
            _defaultValueTextField.RegisterValueChangedCallback(c =>
            {
                UpdatePropertyFoldout();
            });
            base.Bind(elementProperty);
        }
        public override VisualTreeAsset GetVisualTreeAsset()
        {
            return UIToolkitLoaderUtils.LoadUXML("ItemAttributeDefinitionElement");
        }
        public override string GetNameFoldout()
        {
            var baseName = base.GetNameFoldout();
            return $"<<<ATTRIBUTE>>>  {baseName} : {_defaultValueTextField.value}";
        }
    }
}
