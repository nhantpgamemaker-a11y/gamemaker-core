using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GameMaker.Core.Runtime;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace GameMaker.Core.Editor
{
    public abstract class BaseDataManagerPropertyDrawer<M> : PropertyDrawer where M : IDefinition
    {
        private BaseDataManagerHolder<M> baseDataManagerHolder;
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            baseDataManagerHolder = CreateBaseDataManagerHolder();
            baseDataManagerHolder.Bind(property);
            return baseDataManagerHolder.Root;
        }
        protected abstract BaseDataManagerHolder<M> CreateBaseDataManagerHolder();
    }
}
