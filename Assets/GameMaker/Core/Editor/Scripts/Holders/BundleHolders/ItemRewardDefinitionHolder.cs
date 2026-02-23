using System.Linq;
using GameMaker.Core.Runtime;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace GameMaker.Core.Editor
{
    [TypeContain(typeof(ItemRewardDefinition))]
    public class ItemRewardDefinitionHolder : BaseRewardDefinitionHolder
    {
        private DropdownField _itemDropdownField;
        private FloatField _amountFloatField;
        private ObjectField _createTemplateObjectField;

        public ItemRewardDefinitionHolder(VisualElement root) : base(root)
        {
        }
        public override void Bind(SerializedProperty elementProperty)
        {
            _itemDropdownField = Root.Q<DropdownField>("ItemDropdownField");
            _amountFloatField = Root.Q<FloatField>("AmountFloatField");
            _createTemplateObjectField = Root.Q<ObjectField>("CreateTemplateObjectField");
            _createTemplateObjectField.BindProperty(elementProperty.FindPropertyRelative("_createItemTemplate"));
            _amountFloatField.BindProperty(elementProperty.FindPropertyRelative("_amount"));
            _itemDropdownField.choices = ItemDetailManager.Instance.GetDefinitions().Select(x => x.GetName()).ToList();
            var data = ItemDetailManager.Instance.GetDefinitions().FirstOrDefault(x => x.GetName() == _itemDropdownField.value);
            if(data == null)
            {
                data = ItemDetailManager.Instance.GetDefinitions().First();
            }
            _itemDropdownField.value = data.GetName();

            
            _itemDropdownField.RegisterValueChangedCallback(v =>
            {
                var data = ItemDetailManager.Instance.GetDefinitions().FirstOrDefault(x => x.GetName() == _itemDropdownField.value);
                if(data == null)
                {
                    data = ItemDetailManager.Instance.GetDefinitions().First();
                }
                elementProperty.FindPropertyRelative("_referenceId").stringValue = data.GetID();
                elementProperty.serializedObject.ApplyModifiedProperties();
                UpdatePropertyFoldout();
            });
            
            _amountFloatField = Root.Q<FloatField>("AmountFloatField");
            _amountFloatField.BindProperty(elementProperty.FindPropertyRelative("_amount"));
            _amountFloatField.RegisterValueChangedCallback(c =>
            {
                UpdatePropertyFoldout();
            });
            base.Bind(elementProperty);
        }

        public override VisualTreeAsset GetVisualTreeAsset()
        {
            return UIToolkitLoaderUtils.LoadUXML("ItemRewardDefinitionElement");
        }
        public override string GetNameFoldout()
        {
            var baseName = base.GetNameFoldout();
            return $"<<<ITEM DETAIL>>>:{_itemDropdownField.value}  {baseName} : {_amountFloatField.value}";
        }
    }
}