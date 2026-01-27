using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using GameMaker.Core.Runtime;
using Newtonsoft.Json;
using UnityEngine;
namespace GameMaker.Core.Runtime
{
    [System.Serializable]
    public class LocalPropertySaveData : BaseLocalData
    {
        [JsonProperty("PlayerProperties")]
        [SerializeReference]
        private List<PlayerPropertyModel> playerProperties = new();
        public List<PlayerProperty> GetPlayerProperties()
        {
            return playerProperties.Select(x => x.ToPlayerProperty()).ToList();
        }
        public PlayerProperty GetPlayerProperty(string propertyDefinitionId)
        {
            return playerProperties.FirstOrDefault(x => x.GetID() == propertyDefinitionId)?.ToPlayerProperty();
        }
        public async UniTask AddPlayerStatAsync(string statDefinitionId, long value, bool isSave = true)
        {
            var playerProperty = playerProperties.FirstOrDefault(x => x.GetID() == statDefinitionId);
            if (playerProperty == null)
            {
                var statDefinition = PropertyManager.Instance.GetDefinition(statDefinitionId);
                playerProperties.Add(new PlayerStatModel(statDefinitionId, statDefinition.GetName(), value));
            }
            else
            {
                var playerStat = playerProperty as PlayerStatModel;
                playerStat.AddValue(value);
            }
            if(isSave)
                await SaveAsync();
        }
        public async UniTask SetPlayerAttributeAsync(string attributeDefinitionId, string value, bool isSave = true)
        {
            var playerProperty = playerProperties.FirstOrDefault(x => x.GetID() == attributeDefinitionId);
            if (playerProperty == null)
            {
                var attributeDefinition = PropertyManager.Instance.GetDefinition(attributeDefinitionId);
                playerProperties.Add(new PlayerAttributeModel(attributeDefinitionId, attributeDefinition.GetName(), value));
            }
            else
            {
                var playerAttribute = playerProperty as PlayerAttributeModel;
                playerAttribute.SetValue(value);
            }
            if(isSave)
                await SaveAsync();
        }

        public async UniTask SetPlayerStatAsync(string id, long value, bool isSave = true)
        {
             var playerProperty = playerProperties.FirstOrDefault(x => x.GetID() == id);
            if (playerProperty == null)
            {
                var definition = PropertyManager.Instance.GetDefinition(id);
                playerProperties.Add(new PlayerStatModel(id, definition.GetName(), value));
            }
            else
            {
                var playerStat = playerProperty as PlayerStatModel;
                playerStat.SetValue(value);
            }
            if(isSave)
                await SaveAsync();
        }
    }
    [System.Serializable]
    public abstract class PlayerPropertyModel : PlayerDataModel
    {
        public PlayerPropertyModel(string id, string name) : base(id, name)
        {
        }
        public abstract PlayerProperty ToPlayerProperty();
    }
    [System.Serializable]
    public class PlayerStatModel : PlayerPropertyModel
    {
        [JsonProperty("Value")]
        private long _value;
        public PlayerStatModel(string id, string name, long value) : base(id, name)
        {
            _value = value;
        }
        public void AddValue(long value)
        {
            _value += value;
        }
        public float GetValue()
        {
            return _value;
        }

        public override PlayerProperty ToPlayerProperty()
        {
            var statDefinition = PropertyManager.Instance.GetDefinition(id);
            return new PlayerStat(statDefinition, _value);
        }

        internal void SetValue(long value)
        {
            _value = value;
        }
    }
    
    [System.Serializable]
    public class PlayerAttributeModel : PlayerPropertyModel
    {
        [JsonProperty("Value")]
        private string _value;
        public PlayerAttributeModel(string id, string name, string value) : base(id, name)
        {
            _value = value;
        }
        public void SetValue(string value)
        {
            _value  =  value;
        }
        public string GetValue()
        {
            return _value;
        }

        public override PlayerProperty ToPlayerProperty()
        {
            var attributeDefinition = PropertyManager.Instance.GetDefinition(id);
            return new PlayerAttribute(attributeDefinition, _value);
        }
    }
}