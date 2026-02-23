using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection;
using GameMaker.Core.Editor;
using GameMaker.Core.Runtime;
using GameMaker.Item.Editor;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace GameMaker.Core.Editor
{
    public abstract class BaseActionParamDefinitionHolder: DefinitionHolder
    {
        private Foldout _actionParamFoldout;
        private TextField _nameField;
        private TextField _bindingTextField;
        protected TemplateContainer templateContainer; 
        public BaseActionParamDefinitionHolder(VisualElement root) : base(root)
        {
            var asset = GetVisualTreeAsset();
            templateContainer = asset.CloneTree();
            root.Add(templateContainer);
        }
        public abstract VisualTreeAsset GetVisualTreeAsset();

        public override void Bind(SerializedProperty elementProperty)
        {
            _nameField = Root.Q<TextField>("NameTextField");
            _actionParamFoldout = Root.Q<Foldout>("ActionParamFoldout");
            _bindingTextField = Root.Q<TextField>("BindingTextField");
            _bindingTextField.BindProperty(elementProperty.FindPropertyRelative("_bindingName"));
            
            base.Bind(elementProperty);
            _nameField.RegisterValueChangedCallback(value =>
            {
                UpdatePropertyFoldout();
            });
            UpdatePropertyFoldout(); 
        }
        public virtual string GetNameFoldout()
        {
            return $"{serializedProperty.FindPropertyRelative("_name").stringValue}";
        }
        public void UpdatePropertyFoldout()
        {
            _actionParamFoldout.text = GetNameFoldout();
        }
    }
}