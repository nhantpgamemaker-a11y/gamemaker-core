using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using GameMaker.Core.Editor;
using GameMaker.Core.Runtime;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace GameMaker.Core.Editor
{
    [CustomPropertyDrawer(typeof(BaseDefinitionManager<BaseCurrencyDefinition>))]
    public class CurrencyDataManagerPropertyDrawer : Core.Editor.BaseDataManagerPropertyDrawer<BaseCurrencyDefinition>
    {
        protected override BaseDataManagerHolder<BaseCurrencyDefinition> CreateBaseDataManagerHolder()
        {
            var asset = UIToolkitLoaderUtils.LoadUXML("DataManagerElement");
            var templateContainer = asset.CloneTree();
            templateContainer.style.height = StyleKeyword.Auto;
            templateContainer.style.flexGrow = 1;
            return new CurrencyDataManagerHolder(templateContainer);
        }
    }
    public class CurrencyDataManagerHolder : BaseDataManagerHolder<BaseCurrencyDefinition>
    {
        public CurrencyDataManagerHolder(VisualElement root) : base(root)
        {
        }
        protected override string GetTitle()
        {
            return "Currency";
        }

        protected override BaseHolder CreateHolder()
        {
            var visualElement = new VisualElement();
            return new TypeDefinitionHolder(visualElement);
        }
        public class TypeDefinitionHolder : VisualElementHolder
        {
            CurrencyDefinitionHolderFactory _propertyDefinitionHolderFactory;
            public TypeDefinitionHolder(VisualElement root) : base(root)
            {
                _propertyDefinitionHolderFactory = new();
            }
            public override void Bind(SerializedProperty elementProperty)
            {
                Root.Clear();
                var type = elementProperty.managedReferenceValue?.GetType();
                var holderType = _propertyDefinitionHolderFactory.GetHolderType(type);
                var holder = Activator.CreateInstance(holderType, Root) as CurrencyDefinitionHolder;
                holder.Bind(elementProperty);
            }
        }
        public class CurrencyDefinitionHolderFactory
        {
            /// <summary>
            /// type_1 is CurrencyDefinitionType
            /// type_2 is CurrencyDefinitionHolderType
            /// </summary>
            private Dictionary<Type, Type> _cache;
            public CurrencyDefinitionHolderFactory()
            {
                _cache = new();
                var itemCurrencyDefinitionHolderTypes =
                TypeUtils.GetAllConcreteDerivedTypes_Editor(typeof(CurrencyDefinitionHolder))
                .Where(x =>
                {
                    return x.GetCustomAttribute<TypeContainAttribute>() != null;
                });

                foreach (var itemCurrencyDefinitionHolderType in itemCurrencyDefinitionHolderTypes)
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
}