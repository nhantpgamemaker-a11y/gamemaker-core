using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using GameMaker.Core.Runtime;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Pool;

namespace GameMaker.Sound.Runtime
{
    [System.Serializable]
    public class SoundDataManager: PlayerDataManager
    {
        public List<PlayerSoundVolume> GetPlayerSoundVolumes()
        {
            return basePlayerDatas.Cast<PlayerSoundVolume>().ToList();
        }
        public PlayerSoundVolume GetPlayerSoundVolume(string id)
        {
            return GetPlayerData(id) as PlayerSoundVolume;
        }

        public void SetVolume(string id, float volume)
        {
            var playerSoundVolume = GetPlayerSoundVolume(id);
            playerSoundVolume.SetVolume(volume);
        }
    }
}