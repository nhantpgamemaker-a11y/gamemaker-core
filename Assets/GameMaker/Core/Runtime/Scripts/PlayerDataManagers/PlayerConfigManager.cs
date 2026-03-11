using UnityEngine;

namespace GameMaker.Core.Runtime
{
    [System.Serializable]
    public class PlayerConfigManager : PlayerDataManager
    {
        public PlayerConfig GetPlayerConfig(string referenceId)
        {
            return GetPlayerData(referenceId) as PlayerConfig;
        }
        public void SetPlayerConfig(string referenceId, string value)
        {
            var playerConfig = GetPlayerConfig(referenceId);
            playerConfig.SetValue(value);
        }
    }
}
