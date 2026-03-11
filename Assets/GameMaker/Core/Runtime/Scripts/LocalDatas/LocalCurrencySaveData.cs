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

                var playerCurrencyModelType = _playerCurrencyTypeFactory.GetType(currencyDefinition.GetType());
                var playerCurrency = Activator.CreateInstance(playerCurrencyModelType,
                                                                currencyDefinition.GetID(),
                                                                currencyDefinition.GetName(),
                                                                currencyDefinition.GetDefaultValue()) as BasePlayerCurrencyModel;
                playerCurrency.SetCurrencyDefinition(currencyDefinition);
                _playerCurrencies.Add(playerCurrency);
            }
        }
        protected internal override void OnLoad()
        {
            base.OnLoad();
            var currencyIds = _playerCurrencies.Select(x => x.GetID());
            foreach (var currencyDefinition in CurrencyManager.Instance.GetDefinitions())
            {
                var playerCUrrencyModel = _playerCurrencies.FirstOrDefault(x => x.GetID() == currencyDefinition.GetID());
                if (playerCUrrencyModel != null)
                {
                    playerCUrrencyModel.SetCurrencyDefinition(currencyDefinition);
                    continue;
                }
                else
                {
                    var playerCurrencyModelType = _playerCurrencyTypeFactory.GetType(currencyDefinition.GetType());
                    var playerCurrency = Activator.CreateInstance(playerCurrencyModelType,
                                                                currencyDefinition.GetID(),
                                                                currencyDefinition.GetName(),
                                                                currencyDefinition.GetDefaultValue()) as BasePlayerCurrencyModel;
                    
                    playerCurrency.SetCurrencyDefinition(currencyDefinition);
                    _playerCurrencies.Add(playerCurrency);
                }
            }
        }
        public List<BasePlayerCurrency> GetPlayerCurrencies()
        {
            return _playerCurrencies.Where(x=>x.GetCurrencyDefinition()!=null).Select(x => x.ToPlayerCurrency()).ToList();
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
    [TypeCache]
    public abstract class BasePlayerCurrencyModel : PlayerDataModel
    {
        [JsonIgnore]
        private BaseCurrencyDefinition _currencyDefinition;
        public BasePlayerCurrencyModel(string id, string name, object value) : base(id, name)
        {
            base.id = id;
            base.name = name;
        }
        public abstract void AddValue(object value);
        public abstract object GetValue();

        public abstract BasePlayerCurrency ToPlayerCurrency();

        public BaseCurrencyDefinition GetCurrencyDefinition()
        {
            return _currencyDefinition;
        }
        public void SetCurrencyDefinition(BaseCurrencyDefinition currencyDefinition)
        {
            _currencyDefinition = currencyDefinition;
        }
    }
    [TypeContain(typeof(LongCurrencyDefinition))]
    public class LongPlayerCurrencyModel : BasePlayerCurrencyModel
    {
        [JsonProperty("Value")]
        private long _value;
        public LongPlayerCurrencyModel(string id, string name, object value) : base(id, name, value)
        {
            _value = (long)value;
        }

        public override void AddValue(object value)
        {
            _value += Convert.ToInt64(value);
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
    
    [TypeContain(typeof(BigIntCurrencyDefinition))]
    public class BigIntPlayerCurrencyModel : BasePlayerCurrencyModel
    {
        [JsonProperty("Value")]
        private string _value;
        public BigIntPlayerCurrencyModel(string id, string name, object value) : base(id, name, value)
        {
            _value = value.ToString();
        }

        public override void AddValue(object value)
        {
            _value = (BigInteger.Parse(_value) + (BigInteger)value).ToString();
        }

        public override object GetValue()
        {
            return _value;
        }

        public override BasePlayerCurrency ToPlayerCurrency()
        {
            var currencyDefinition = GetCurrencyDefinition();
            return new BigIntPlayerCurrency(currencyDefinition.GetID(), currencyDefinition, BigInteger.Parse(_value));
        }
    }
}