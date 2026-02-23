using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection;
using GameMaker.Core.Editor;
using GameMaker.Core.Runtime;
using GameMaker.Item.Editor;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace GameMaker.Core.Editor
{
    public class ItemPropertyDataManagerHolder : BaseDataManagerHolder<ItemPropertyDefinition>
    {
        public ItemPropertyDataManagerHolder(VisualElement root) : base(root)
        {
        }
        protected override BaseHolder CreateHolder()
        {
            var visualElement = new VisualElement();
            return new PropertyDefinitionHolder(visualElement);
        }

        protected override void BindItem(VisualElement element, int index)
        {
            var holder = (PropertyDefinitionHolder)element.userData;
            var itemPropertySerializedProperty = items[index];
            element.Clear();
            var propertyDefinitionHolder = new PropertyDefinitionHolder(element);
            propertyDefinitionHolder.Bind(itemPropertySerializedProperty);
        }
        public class PropertyDefinitionHolder : VisualElementHolder
        {
            ItemPropertyDefinitionHolderFactory _itemPropertyDefinitionHolderFactory;
            public PropertyDefinitionHolder(VisualElement root) : base(root)
            {
                _itemPropertyDefinitionHolderFactory = new();
            }
            public override void Bind(SerializedProperty elementProperty)
            {
                Root.Clear();
                var type = elementProperty.managedReferenceValue?.GetType();
                var holderType = _itemPropertyDefinitionHolderFactory.GetHolderType(type);
                var holder = Activator.CreateInstance(holderType, Root) as ItemPropertyDefinitionHolder;
                holder.Bind(elementProperty);
            }
        }
        public class ItemPropertyDefinitionHolderFactory
        {
            /// <summary>
            /// type_1 is ItemPropertyDefinitionType
            /// type_2 is ItemPropertyDefinitionHolderType
            /// </summary>
            private Dictionary<Type, Type> _cache;
            public ItemPropertyDefinitionHolderFactory()
            {
                _cache = new();
                var itemPropertyDefinitionHolderTypes =
                TypeUtils.GetAllConcreteDerivedTypes_Editor(typeof(ItemPropertyDefinitionHolder))
                .Where(x =>
                {
                    return x.GetCustomAttribute<TypeContainAttribute>() != null;
                });

                foreach (var itemPropertyDefinitionHolderType in itemPropertyDefinitionHolderTypes)
                {
                    var type = itemPropertyDefinitionHolderType
                                .GetCustomAttribute<TypeContainAttribute>().Type;
                    _cache[type] = itemPropertyDefinitionHolderType;
                }
            }
            
            public Type GetHolderType(Type type)
            {
                return _cache[type];
            }
        }
    }
}