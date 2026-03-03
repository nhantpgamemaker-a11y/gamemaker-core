using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace GameMaker.Core.Runtime
{
    [System.Serializable]
    public class LocalConfigSaveData : BaseLocalData
    {
        [JsonProperty("PlayerConfigs")]
        private List<PlayerConfigModel> _playerConfigs = new();
        protected internal override void OnCreate()
        {
            base.OnCreate();
            foreach (var playerConfig in ConfigManager.Instance.GetDefinitions())
            {
                var playerConfigModel = new PlayerConfigModel(playerConfig.GetID(), playerConfig, playerConfig.Value);
                _playerConfigs.Add(playerConfigModel);
            }
        }
        protected internal override void OnLoad()
        {
            base.OnLoad();
            foreach (var playerConfig in ConfigManager.Instance.GetDefinitions())
            {
                var playerConfigModel = _playerConfigs.FirstOrDefault(x => x.GetID() == playerConfig.GetID());
                if (playerConfigModel != null)
                {
                    playerConfigModel.SetDefinition(playerConfig);
                }
                else
                {
                    playerConfigModel = new PlayerConfigModel(playerConfig.GetID(), playerConfig, playerConfig.Value);
                    _playerConfigs.Add(playerConfigModel);
                }
            }
        }
        public List<PlayerConfig> GetPlayerConfigs()
        {
            return _playerConfigs.Select(x => x.ToPlayerConfig()).ToList();
        }

        public void SetPlayerConfig(string id, string value)
        {
            var playerConfigModel = _playerConfigs.FirstOrDefault(x => x.GetID() == id);
            if (playerConfigModel != null)
            {
                playerConfigModel.SetValue(value);
                Save();
            }
        }
    }
    [System.Serializable]
    public class PlayerConfigModel: PlayerDataModel
    {
        [JsonProperty("Value")]
        private string _value;
        private IDefinition _definition;
        public IDefinition GetDefinition() => _definition;
        public void SetDefinition(IDefinition definition) => _definition = definition;
        public PlayerConfigModel(string id,IDefinition definition, string value) : base(id, id)
        {
            _value = value;
            _definition = definition;
        }
        public void SetValue(string value)
        {
            _value = value;
        }
        public PlayerConfig ToPlayerConfig()
        {
            var playerConfig = new PlayerConfig(GetID(), _definition, _value);
            return playerConfig;
        }

    }
}