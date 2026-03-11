using System.Collections.Generic;
using System.Linq;
using GameMaker.Core.Runtime;
using UnityEngine;
using UnityEngine.UI;

namespace GameMaker.Sound.Runtime
{
    [System.Serializable]
    public class SoundID : BaseID
    {
        public override List<IDefinition> GetDefinitions()
        {
            return SoundManager.Instance.GetDefinitions().Cast<IDefinition>().ToList();
        }
        public SoundDefinition GetSoundDefinition()
        {
            return SoundManager.Instance.GetDefinition(ID);
        }
    }
}