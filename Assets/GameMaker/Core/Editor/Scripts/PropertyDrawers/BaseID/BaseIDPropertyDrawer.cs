using System.Collections.Generic;
using System.Linq;
using GameMaker.Core.Editor;
using GameMaker.Core.Runtime;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace GameMaker.Core.Editor
{
    [CustomPropertyDrawer(typeof(BaseID), true)]
    public class BaseIDPropertyDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            var root = new VisualElement();

            var target = fieldInfo.GetValue(property.serializedObject.targetObject) as BaseID;
            if (target == null)
                return root;

            List<IDefinition> definitions = target.GetDefinitions();

            var choices = definitions.Select(d => d.GetName()).ToList();
            choices.Insert(0,"-");
            var targetChoiceID = property.FindPropertyRelative("_id").stringValue;
            var targetChoiceDefinition = definitions.FirstOrDefault(x => x.GetID() == targetChoiceID);
            var targetChoiceName = targetChoiceDefinition != null ? targetChoiceDefinition.GetName() : "-";
            var dropdown = new DropdownField(property.displayName, choices, targetChoiceName);
            dropdown.RegisterValueChangedCallback((v) =>
            {
                var newChoice = v.newValue;
                targetChoiceDefinition = definitions.FirstOrDefault(x => x.GetName() == newChoice);
                if (targetChoiceDefinition==null)
                {
                    property.FindPropertyRelative("_id").stringValue = "";
                }
                else
                {
                    property.FindPropertyRelative("_id").stringValue = targetChoiceDefinition.GetID();
                }
                property.serializedObject.ApplyModifiedProperties();
            });

            root.Add(dropdown);
            return root;
        }
    }
}