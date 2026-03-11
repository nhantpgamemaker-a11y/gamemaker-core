using System;
using System.Collections.Generic;
using System.Linq;
using GameMaker.Core.Runtime;
using UnityEngine;
using UnityEngine.UI;

namespace GameMaker.Sound.Runtime
{
    [System.Serializable]
    public class SoundGroupID : BaseID
    {
        public override List<IDefinition> GetDefinitions()
        {
            return new();
        }

        public PlayerSoundVolume GetPlayerSoundVolume()
        {
            return SoundGateway.Manager.GetPlayerSoundVolume(ID);
        }
    }
}