using GameMaker.Core.Runtime;
using UnityEngine;

namespace GameMaker.Sound.Runtime
{
    public class UISliderSoundVolume : MonoBehaviour, IObserver<BasePlayerData>
    {
        [SerializeField] private SoundGroupID _soundGroupID;
        [SerializeField] private UnityEngine.UI.Slider _slider;
        private PlayerSoundVolume _playerSoundVolume;

        private void OnEnable()
        {
            _slider.onValueChanged.AddListener(OnSliderValueChanged);
            _playerSoundVolume = _soundGroupID.GetPlayerSoundVolume();
            if (_playerSoundVolume == null)
            {
                GameMaker.Core.Runtime.Logger.LogWarning($"[PlayerSoundVolume] not found for SoundID: {_soundGroupID.ID}");
                return;
            }
            _slider.value = _playerSoundVolume.Volume;
            _playerSoundVolume.AddObserver(this);
        }
        
        private void OnDisable()
        {
            _slider.onValueChanged.RemoveListener(OnSliderValueChanged);
            if(_playerSoundVolume != null)
            {
                _playerSoundVolume.RemoveObserver(this);
            }
        }

        private void OnSliderValueChanged(float clamp01)
        {
            SoundGateway.Manager.SetVolume(_soundGroupID.ID, clamp01);
        }

        public void OnNotify(ISubject<BasePlayerData> subject, BasePlayerData data)
        {
        }
    } 
}
