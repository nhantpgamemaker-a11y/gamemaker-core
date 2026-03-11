using GameMaker.Sound.Runtime;
using GameMaker.Core.Editor;
using UnityEditor;
using UnityEngine.UIElements;
using System.Collections.Generic;
using System.Linq;

namespace GameMaker.Sound.Editor
{
     [CustomPropertyDrawer(typeof(SoundGroupID), true)]
    public class SoundGroupIDPropertyDrawer : BaseIDPropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {   
            var root = new VisualElement();

            var target = fieldInfo.GetValue(property.serializedObject.targetObject) as SoundGroupID;
            if (target == null)
                return root;

            List<string> definitions = SoundManager.Instance.GetMixerGroupNames();

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