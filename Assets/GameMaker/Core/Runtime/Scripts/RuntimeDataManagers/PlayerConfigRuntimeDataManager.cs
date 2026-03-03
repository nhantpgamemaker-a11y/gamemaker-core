using System;
using System.Linq;
using Cysharp.Threading.Tasks;

namespace GameMaker.Core.Runtime
{
    [System.Serializable]
    [RuntimeDataManagerAttribute(new Type[] { typeof(ConfigDataSpaceProvider) })]
    
    public class PlayerConfigRuntimeDataManager : BaseRuntimeDataManager
    {
        private ConfigDataSpaceProvider _configDataSpaceProvider;
        private PlayerConfigManager _playerConfigManager;
        public PlayerConfigManager PlayerConfigManager => _playerConfigManager;
        public async override UniTask<bool> InitializeAsync(IDataSpaceProvider[] dataSpaceProviders, PlayerDataManager[] playerDataManagers)
        {
            _playerConfigManager = playerDataManagers.FirstOrDefault(x => x.GetType() == typeof(PlayerConfigManager)) as PlayerConfigManager;
            _configDataSpaceProvider = dataSpaceProviders.FirstOrDefault(x => x.GetType() == typeof(ConfigDataSpaceProvider)) as ConfigDataSpaceProvider;
            var playerConfigs = _configDataSpaceProvider.GetPlayerConfigs();
            _playerConfigManager.Initialize(playerConfigs.Cast<BasePlayerData>().ToList());
            ConfigGateway.Initialize(this);
            return true;
        }
        public PlayerConfig GetPlayerConfig(string id)
        {
            return _playerConfigManager.GetPlayerConfig(id);
        }
        public void SetPlayerConfig(string id, string value)
         {
            var playerConfig = _playerConfigManager.GetPlayerConfig(id);
            if (playerConfig != null)
            {
                playerConfig.SetValue(value);
                _configDataSpaceProvider.SetPlayerConfig(id, value);
            }
         }
    } 
}