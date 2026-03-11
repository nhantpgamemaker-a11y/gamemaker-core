using System;
using UnityEngine;
using UnityEngine.UI;

namespace GameMaker.Sound.Runtime
{
    [RequireComponent(typeof(Button))]
    public class UIClickableSoundEffect : MonoBehaviour
    {
        [UnityEngine.SerializeField]
        private Button _button;
        [UnityEngine.SerializeField]
        private SoundID _soundID;
        public Button Button { get => _button; }
        public SoundID SoundID { get => _soundID; }

        protected virtual void Awake()
        {
            if (_button == null) _button = GetComponent<Button>();
            _button.onClick.AddListener(OnClick);
        }
        protected virtual  void OnDestroy()
        {
            _button.onClick.RemoveListener(OnClick);
        }

        protected virtual void OnClick()
        {
            SoundRuntimeManager.Instance.PlayOneShot(_soundID.GetSoundDefinition());
        }
    }
}
