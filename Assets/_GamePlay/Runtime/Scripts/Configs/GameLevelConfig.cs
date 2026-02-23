using System.Collections.Generic;
using UnityEngine;

namespace CatAdventure.GamePlay
{
    [System.Serializable]
    public class GameLevelConfig
    {
        [UnityEngine.SerializeField]
        public List<GameModelConfig> _gameModelConfigs;
        public GameLevelConfig()
        {
            _gameModelConfigs = new();
        }
    }
}
