using UnityEngine;

namespace GameMaker.Sound.Runtime
{
    public class SoundRef : MonoBehaviour
    {
        [SerializeField] private SoundID _soundID;
        public void PlayOnShot()
        {
            SoundRuntimeManager.Instance.PlayOneShot(_soundID.GetSoundDefinition());
        }
        public void PlayLoop()
        {
            SoundRuntimeManager.Instance.PlayLoop(_soundID.GetSoundDefinition());
        }
        public void StopLoop()
        {
            SoundRuntimeManager.Instance.StopLoop(_soundID.GetSoundDefinition());
        }
        
    }
}
