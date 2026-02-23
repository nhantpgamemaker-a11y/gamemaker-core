using System.Linq;
using GameMaker.Core.Editor;
using GameMaker.Core.Runtime;
using GameMaker.Sound.Runtime;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace GameMaker.Sound.Editor
{
    [TypeContain(typeof(SoundDefinition))]
    public class SoundDefinitionHolder : BaseDefinitionHolder
    {
        protected ObjectField _clipObjectField;
        protected DropdownField _mixerGroupDropdownField;
        protected FloatField _volumeFloatField;
        public SoundDefinitionHolder(VisualElement root) : base(root)
        {
        }
        public override void Bind(SerializedProperty elementProperty)
        {
            _clipObjectField = Root.Q<ObjectField>("ClipObjectField");
            _clipObjectField.BindProperty(elementProperty.FindPropertyRelative("_clip"));

            _mixerGroupDropdownField = Root.Q<DropdownField>("MixerGroupDropdownField");
            _mixerGroupDropdownField.choices = SoundManager.Instance.GetMixerGroupNames().ToList();
            var data = SoundManager.Instance.GetMixerGroupNames().FirstOrDefault(x => x == _mixerGroupDropdownField.value);
            _mixerGroupDropdownField.value = elementProperty.FindPropertyRelative("_mixerGroup").stringValue;
            _mixerGroupDropdownField.RegisterValueChangedCallback(c =>
            {
                var data = SoundManager.Instance.GetMixerGroupNames().FirstOrDefault(x => x == _mixerGroupDropdownField.value);
                _mixerGroupDropdownField.value = data;
                elementProperty.FindPropertyRelative("_mixerGroup").stringValue = data;
                elementProperty.serializedObject.ApplyModifiedProperties();
            });
            _volumeFloatField = Root.Q<FloatField>("VolumeFloatField");
            _volumeFloatField.BindProperty(elementProperty.FindPropertyRelative("_volumeScale"));
            base.Bind(elementProperty);
        }
    }
}