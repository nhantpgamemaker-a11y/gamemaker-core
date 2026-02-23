using GameMaker.Core.Runtime;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using System;

namespace GameMaker.Core.Editor
{
    [CustomPropertyDrawer(typeof(BaseDefinition))]
    public abstract class BaseDefinitionPropertyDrawer : PropertyDrawer
    {
        protected abstract BaseDefinitionHolder GetBaseDefinitionHolder();
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            var baseDefinitionHolder = GetBaseDefinitionHolder();
            baseDefinitionHolder.Bind(property);
            return baseDefinitionHolder.Root;
        }
    } 
}