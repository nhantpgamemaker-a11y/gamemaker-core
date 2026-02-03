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
    [CustomPropertyDrawer(typeof(BaseDefinitionManager<PropertyDefinition>))]
    public class PropertyDataManagerPropertyHolder : Core.Editor.BaseDataManagerPropertyDrawer<PropertyDefinition>
    {
        protected override BaseDataManagerHolder<PropertyDefinition> CreateBaseDataManagerHolder()
        {
            var asset = UIToolkitLoaderUtils.LoadUXML("DataManagerElement");
            var templateContainer = asset.CloneTree();
            templateContainer.style.height = StyleKeyword.Auto;
            templateContainer.style.flexGrow = 1;
            return new PropertyDataManagerHolder(templateContainer);
        }
    }
    public class PropertyDataManagerHolder : BaseDataManagerHolder<PropertyDefinition>
    {
        public PropertyDataManagerHolder(VisualElement root) : base(root)
        {

        }
        protected override string GetTitle()
        {
            return "Property";
        }

        protected override BaseHolder CreateHolder()
        {
            var visualElement = new VisualElement();
            return new TypeDefinitionHolder(visualElement);
        }
        public class TypeDefinitionHolder : VisualElementHolder
        {
            PropertyDefinitionHolderFactory _propertyDefinitionHolderFactory;
            public TypeDefinitionHolder(VisualElement root) : base(root)
            {
                _propertyDefinitionHolderFactory = new();
            }
            public override void Bind(SerializedProperty elementProperty)
            {
                Root.Clear();
                var type = elementProperty.managedReferenceValue?.GetType();
                var holderType = _propertyDefinitionHolderFactory.GetHolderType(type);
                var holder = Activator.CreateInstance(holderType, Root) as PropertyDefinitionHolder;
                holder.Bind(elementProperty);
            }
        }
        public class PropertyDefinitionHolderFactory
        {
            /// <summary>
            /// type_1 is PropertyDefinitionType
            /// type_2 is PropertyDefinitionHolderType
            /// </summary>
            private Dictionary<Type, Type> _cache;
            public PropertyDefinitionHolderFactory()
            {
                _cache = new();
                var itemPropertyDefinitionHolderTypes =
                TypeUtils.GetAllConcreteDerivedTypes(typeof(PropertyDefinitionHolder))
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
