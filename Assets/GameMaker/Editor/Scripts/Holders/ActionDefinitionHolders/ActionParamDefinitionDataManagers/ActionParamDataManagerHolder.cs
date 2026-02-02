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
    public class ActionParamDataManagerHolder : BaseDataManagerHolder<BaseActionParamDefinition>
    {
        public ActionParamDataManagerHolder(VisualElement root) : base(root)
        {
        }

        protected override BaseHolder CreateHolder()
        {
            var visualElement = new VisualElement();
            return new ActionParamDefinitionHolder(visualElement);
        }
        public class ActionParamDefinitionHolder : VisualElementHolder
        {
            ActionParamDefinitionHolderFactory _actionParamDefinitionHolderFactory;
            public ActionParamDefinitionHolder(VisualElement root) : base(root)
            {
                _actionParamDefinitionHolderFactory = new();
            }
            public override void Bind(SerializedProperty elementProperty)
            {
                Root.Clear();
                var type = elementProperty.managedReferenceValue?.GetType();
                var holderType = _actionParamDefinitionHolderFactory.GetHolderType(type);
                var holder = Activator.CreateInstance(holderType, Root) as BaseActionParamDefinitionHolder;
                holder.Bind(elementProperty);
            }
        }
        public class ActionParamDefinitionHolderFactory
        {
            /// <summary>
            /// type_1 is ActionParamDefinitionType
            /// type_2 is ActionParamDefinitionHolderType
            /// </summary>
            private Dictionary<Type, Type> _cache;
            public ActionParamDefinitionHolderFactory()
            {
                _cache = new();
                var itemPropertyDefinitionHolderTypes =
                TypeUtils.GetAllLeafDerivedTypes(typeof(BaseActionParamDefinitionHolder))
                .Where(x =>
                {
                    return x.GetCustomAttribute<TypeHolderAttribute>() != null;
                });

                foreach (var itemPropertyDefinitionHolderType in itemPropertyDefinitionHolderTypes)
                {
                    var type = itemPropertyDefinitionHolderType
                                .GetCustomAttribute<TypeHolderAttribute>().Type;
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