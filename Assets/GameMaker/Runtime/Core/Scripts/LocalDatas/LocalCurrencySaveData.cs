using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using GameMaker.Core.Runtime;
using Newtonsoft.Json;
using UnityEngine;

namespace GameMaker.Core.Runtime
{
    [System.Serializable]
    public class LocalCurrencySaveData : BaseLocalData
    {
        [JsonProperty("PlayerCurrencies")]
        [SerializeField]
        private List<PlayerCurrencyModel> _playerCurrencies = new();

        internal override void OnCreate()
        {
            base.OnCreate();
            foreach(var currencyDefinition in CurrencyManager.Instance.GetDefinitions())
            {
                _playerCurrencies.Add(new PlayerCurrencyModel(currencyDefinition.GetID(), currencyDefinition.GetName(), currencyDefinition.DefaultValue));
            }
        }
        internal override void OnLoad()
        {
            base.OnLoad();
            var currencyIds = _playerCurrencies.Select(x => x.GetID());
            foreach(var currencyDefinition in CurrencyManager.Instance.GetDefinitions())
            {
                if (!currencyIds.Contains(currencyDefinition.GetID()))
                {
                    _playerCurrencies.Add(new PlayerCurrencyModel(currencyDefinition.GetID(), currencyDefinition.GetName(), currencyDefinition.DefaultValue));
                }
            }
        }

        public List<PlayerCurrency> GetPlayerCurrencies()
        {
            return _playerCurrencies.Select(x => x.ToPlayerCurrency()).ToList();
        }
        public PlayerCurrency GetPlayerCurrency(string currencyId)
        {
            return _playerCurrencies.FirstOrDefault(x => x.GetID() == currencyId)?.ToPlayerCurrency();
        }

        public async UniTask AddPlayerCurrency(string currencyDefinitionId, float value, bool isSave = true)
        {
            var playerCurrency = _playerCurrencies.FirstOrDefault(x => x.GetID() == currencyDefinitionId);
            if (playerCurrency == null)
            {
                var currencyDefinition = CurrencyManager.Instance.GetDefinition(currencyDefinitionId);
                _playerCurrencies.Add(new PlayerCurrencyModel(currencyDefinitionId, currencyDefinition.GetName(), value));
            }
            else
            {
                playerCurrency.AddValue(value);
            }
            if(isSave)
                await SaveAsync();
        }
    }
    
    [System.Serializable]
    public class PlayerCurrencyModel: PlayerDataModel
    {
        [JsonProperty("Value")]
        private float _value;
        public PlayerCurrencyModel(string id, string name, float value):base(id, name)
        {
            base.id = id;
            base.name = name;
            _value = value;
        }
        public void AddValue(float value)
        {
            _value += value;
        }
        public float GetValue()
        {
            return _value;
        }

        public PlayerCurrency ToPlayerCurrency()
        {
            var currencyDefinition =  CurrencyManager.Instance.GetDefinition(id);
            return new PlayerCurrency(currencyDefinition,_value);
        }
    }
}