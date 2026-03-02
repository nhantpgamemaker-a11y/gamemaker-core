using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using GameMaker.Core.Editor;
using GameMaker.Core.Runtime;
using GameMaker.Feature.Shop.Runtime;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace GameMaker.Feature.Shop.Editor
{
    public class ShopItemDataManagerHolder : BaseDataManagerHolder<BaseShopItemDefinition>
    {
        public ShopItemDataManagerHolder(VisualElement root) : base(root)
        {
        }

        protected override BaseHolder CreateHolder()
        {
            var visualElement = new VisualElement();
            return new ShopItemDefinitionHolder(visualElement);
        }
        protected override void BindItem(VisualElement element, int index)
        {
            var holder = (ShopItemDefinitionHolder)element.userData;
            var shopItemSerializedProperty = items[index];
            var shopItemDefinitionHolder = holder;
            shopItemDefinitionHolder.Bind(shopItemSerializedProperty);
        }
    }
    public class ShopItemDefinitionHolder : VisualElementHolder
    {
        ShopItemDefinitionHolderFactory _shopItemDefinitionHolderFactory;
        public ShopItemDefinitionHolder(VisualElement root) : base(root)
        {
            _shopItemDefinitionHolderFactory = new();
        }
        public override void Bind(SerializedProperty elementProperty)
        {
            Root.Clear();
            var type = elementProperty.managedReferenceValue?.GetType();
            var holderType = _shopItemDefinitionHolderFactory.GetHolderType(type);
            var holder = Activator.CreateInstance(holderType, Root) as BaseShopItemDefinitionHolder;
            holder.Bind(elementProperty);
        }
    }
    public class ShopItemDefinitionHolderFactory
        {
            /// <summary>
            /// type_1 is ItemPropertyDefinitionType
            /// type_2 is ItemPropertyDefinitionHolderType
            /// </summary>
            private Dictionary<Type, Type> _cache;
            public ShopItemDefinitionHolderFactory()
            {
                _cache = new();
                var itemPropertyDefinitionHolderTypes =
                TypeUtils.GetAllConcreteAssignableTypes(typeof(BaseShopItemDefinitionHolder))
                .Where(x =>
                {
                    return x.GetCustomAttribute<TypeContainAttribute>() != null;
                });

                foreach (var shopItemDefinitionHolderType in itemPropertyDefinitionHolderTypes)
                {
                    var type = shopItemDefinitionHolderType
                                .GetCustomAttribute<TypeContainAttribute>().Type;
                    _cache[type] = shopItemDefinitionHolderType;
                }
            }
            
            public Type GetHolderType(Type type)
            {
                return _cache[type];
            }
        }
}