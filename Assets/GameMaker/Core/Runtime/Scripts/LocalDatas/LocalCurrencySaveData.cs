using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;

namespace GameMaker.Core.Runtime
{
    [System.Serializable]
    public class LocalCurrencySaveData : BaseLocalData
    {
        [JsonProperty("PlayerCurrencies")]
        [UnityEngine.SerializeReference]
        private List<BasePlayerCurrencyModel> _playerCurrencies = new();
        private PlayerCurrencyTypeFactory _playerCurrencyTypeFactory = new();
        protected internal override void OnCreate()
        {
            base.OnCreate();
            foreach(var currencyDefinition in CurrencyManager.Instance.GetDefinitions())
            {
                throw new System.NotImplementedException();
                //_playerCurrencies.Add(new BasePlayerCurrencyModel(currencyDefinition.GetID(), currencyDefinition.GetName(), currencyDefinition.DefaultValue));
            }
        }
        protected internal override void OnLoad()
        {
            base.OnLoad();
            var currencyIds = _playerCurrencies.Select(x => x.GetID());
            foreach(var currencyDefinition in CurrencyManager.Instance.GetDefinitions())
            {
                if (!currencyIds.Contains(currencyDefinition.GetID()))
                {
                    throw new System.NotImplementedException();
                    //_playerCurrencies.Add(new BasePlayerCurrencyModel(currencyDefinition.GetID(), currencyDefinition.GetName(), currencyDefinition.DefaultValue));
                }
            }
        }
        public List<BasePlayerCurrency> GetPlayerCurrencies()
        {
            return _playerCurrencies.Select(x => x.ToPlayerCurrency()).ToList();
        }
        public BasePlayerCurrency GetPlayerCurrency(string currencyId)
        {
            return _playerCurrencies.FirstOrDefault(x => x.GetID() == currencyId)?.ToPlayerCurrency();
        }
        public async UniTask AddPlayerCurrency(string currencyDefinitionId, object value, bool isSave = true)
        {
            var playerCurrency = _playerCurrencies.FirstOrDefault(x => x.GetID() == currencyDefinitionId);
            if (playerCurrency == null)
            {
                var currencyDefinition = CurrencyManager.Instance.GetDefinition(currencyDefinitionId);
                var playerCurrencyType = _playerCurrencyTypeFactory.GetType(currencyDefinition.GetType());
                _playerCurrencies.Add((BasePlayerCurrencyModel)Activator.CreateInstance(playerCurrencyType, currencyDefinitionId, currencyDefinition.GetName(), value));
            }
            else
            {
                playerCurrency.AddValue(value);
            }
            if(isSave)
                await SaveAsync();
        }
    }
    public class PlayerCurrencyTypeFactory
    {
        private Dictionary<Type, Type> _caches;
        public PlayerCurrencyTypeFactory()
        {
            _caches = TypeUtils.GetAllDerivedNonAbstractTypes(typeof(BasePlayerCurrencyModel))
            .Where(x => x.GetCustomAttribute<GameMaker.Core.Runtime.TypeContainAttribute>()!=null)
            .ToDictionary(x => x.GetCustomAttribute<GameMaker.Core.Runtime.TypeContainAttribute>().Type, x => x);
        }
        public Type GetType(Type playerCurrencyDefinitionType)
        {
            return _caches[playerCurrencyDefinitionType];
        }
    }
    [System.Serializable]
    public abstract class BasePlayerCurrencyModel : PlayerDataModel
    {
        public BasePlayerCurrencyModel(string id, string name) : base(id, name)
        {
            base.id = id;
            base.name = name;
        }
        public abstract void AddValue(object value);
        public abstract object GetValue();

        public abstract BasePlayerCurrency ToPlayerCurrency();
    }
    public class LongPlayerCurrencyModel : BasePlayerCurrencyModel
    {
        private long _value;
        public LongPlayerCurrencyModel(string id, string name, long value) : base(id, name)
        {
            _value = value;
        }

        public override void AddValue(object value)
        {
            _value += (long)value;
        }

        public override object GetValue()
        {
            return _value;
        }

        public override BasePlayerCurrency ToPlayerCurrency()
        {
            var currencyDefinition = CurrencyManager.Instance.GetDefinition(id);
            return new LongPlayerCurrency(currencyDefinition.GetID(), currencyDefinition, _value);
        }
    }
    public class BigIntPlayerCurrencyModel : BasePlayerCurrencyModel
    {
        private BigInteger _value;
        public BigIntPlayerCurrencyModel(string id, string name, BigInteger value) : base(id, name)
        {
            _value = value;
        }

        public override void AddValue(object value)
        {
            _value += (BigInteger)value;
        }

        public override object GetValue()
        {
            return _value;
        }

        public override BasePlayerCurrency ToPlayerCurrency()
        {
            var currencyDefinition = CurrencyManager.Instance.GetDefinition(id);
            return new BigIntPlayerCurrency(currencyDefinition.GetID(), currencyDefinition, _value);
        }
    }
}