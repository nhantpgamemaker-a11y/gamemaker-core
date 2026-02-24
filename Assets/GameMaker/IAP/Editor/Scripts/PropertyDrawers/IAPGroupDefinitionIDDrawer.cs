namespace GameMaker.IAP.Editor
{
    using System.Collections.Generic;
    using System.Linq;
    using GameMaker.Core.Editor;
    using GameMaker.IAP.Runtime;
    using UnityEditor;
    using UnityEngine;
    using UnityEngine.UIElements;

    [CustomPropertyDrawer(typeof(IAPGroupDefinitionID))]
    public class IAPGroupDefinitionIDDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            var root = new VisualElement();

            var target = fieldInfo.GetValue(property.serializedObject.targetObject) as IAPGroupDefinitionID;
            if (target == null)
                return root;

            List<string> definitions = IAPManager.Instance.IAPGroups.ToList();

            var choices = definitions.Select(d=>d).ToList();
            choices.Insert(0,"-");
            var targetChoiceID = property.FindPropertyRelative("_id").stringValue;
            var targetChoiceDefinition = definitions.FirstOrDefault(x => x == targetChoiceID);
            var targetChoiceName = targetChoiceDefinition != null ? targetChoiceDefinition : "-";
            var dropdown = new DropdownField(property.displayName, choices, targetChoiceName);
            dropdown.RegisterValueChangedCallback((v) =>
            {
                var newChoice = v.newValue;
                targetChoiceDefinition = definitions.FirstOrDefault(x => x == newChoice);
                if (targetChoiceDefinition==null)
                {
                    property.FindPropertyRelative("_id").stringValue = "";
                }
                else
                {
                    property.FindPropertyRelative("_id").stringValue = targetChoiceDefinition;
                }
                property.serializedObject.ApplyModifiedProperties();
            });

            root.Add(dropdown);
            return root;
        }
    }
}