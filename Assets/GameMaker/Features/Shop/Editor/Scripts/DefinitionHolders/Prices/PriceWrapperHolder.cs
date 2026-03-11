using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using GameMaker.Core.Editor;
using GameMaker.Core.Runtime;
using GameMaker.Feature.Shop.Runtime;
using UnityEditor;
using UnityEngine.UIElements;

namespace GameMaker.Feature.Shop.Editor
{
    public class PriceWrapperHolder : BaseHolder
    {
        private Button _editPriceButton;
        private PriceHolderFactory _priceHolderFactory;
        private SerializedProperty _priceElementProperty;
        private BasePriceHolder _currentPriceHolder;
        private string _binding = "_price";
        private TypeDefinitionHolder _typeDefinitionHolder;
        public PriceWrapperHolder(VisualElement root, string binding) : base(root)
        {   
            _priceHolderFactory = new();
            _binding = binding;
            _editPriceButton = Root.Q<Button>("EditPriceButton");
            _editPriceButton.clicked += OnEditPriceClicked;
            var vsElement = new VisualElement();
            Root.Add(vsElement);
            _typeDefinitionHolder = new TypeDefinitionHolder(vsElement);
        }

        public override void Bind(SerializedProperty elementProperty)
        {
            base.Bind(elementProperty);
            _priceElementProperty = elementProperty;
            _typeDefinitionHolder.Bind(elementProperty.FindPropertyRelative(_binding));
        }
        private void OnEditPriceClicked()
        {
            var priceTypes = TypeUtils.GetAllDerivedNonAbstractTypes(typeof(BasePrice));
            var actionDatas = priceTypes.Select(x =>
            {
                return new ActionData(x.Name, () =>
                {
                    _priceElementProperty.FindPropertyRelative(_binding).managedReferenceValue
                    = Activator.CreateInstance(x);
                    _priceElementProperty.serializedObject.ApplyModifiedProperties();
                    
                    if(_currentPriceHolder!=null && _currentPriceHolder.Root != null)
                    {
                        Root.Remove(_currentPriceHolder.Root);
                    }
                    var holderType = _priceHolderFactory.GetHolderType(x);
                    _currentPriceHolder = Activator.CreateInstance(holderType, new VisualElement()) as BasePriceHolder;
                    _currentPriceHolder.Bind(_priceElementProperty.FindPropertyRelative(_binding));
                    Root.Add(_currentPriceHolder.Root);
                    _priceElementProperty.serializedObject.ApplyModifiedProperties();
                });
            }).ToList();
            ButtonActionWindowEditor.ShowWindow("Edit Price", actionDatas);
        }
    }
    public class TypeDefinitionHolder : VisualElementHolder
    {   
        private PriceHolderFactory _priceHolderFactory;
        public TypeDefinitionHolder(VisualElement root) : base(root)
        {
            _priceHolderFactory = new();
        }
        public override void Bind(SerializedProperty elementProperty)
        {
            base.Bind(elementProperty);
            Root.Clear();
            if(elementProperty.managedReferenceValue == null)
            {
                return;
            }
            var type = elementProperty.managedReferenceValue?.GetType();
            var holderType = _priceHolderFactory.GetHolderType(type);
            var holder = Activator.CreateInstance(holderType, Root) as BasePriceHolder;
            holder.Bind(elementProperty);
        }
    }
    public class PriceHolderFactory
        {
            /// <summary>
            /// type_1 is CurrencyDefinitionType
            /// type_2 is CurrencyDefinitionHolderType
            /// </summary>
            private Dictionary<Type, Type> _cache;
            public PriceHolderFactory()
            {
                _cache = new();
                var priceHolderTypes =
                TypeUtils.GetAllConcreteDerivedTypes_Editor(typeof(BasePriceHolder))
                .Where(x =>
                {
                    return x.GetCustomAttribute<TypeContainAttribute>() != null;
                });

                foreach (var itemCurrencyDefinitionHolderType in priceHolderTypes)
                {
                    var type = itemCurrencyDefinitionHolderType
                                .GetCustomAttribute<TypeContainAttribute>().Type;
                    _cache[type] = itemCurrencyDefinitionHolderType;
                }
            }

            public Type GetHolderType(Type type)
            {
                return _cache[type];
            }
        }
}