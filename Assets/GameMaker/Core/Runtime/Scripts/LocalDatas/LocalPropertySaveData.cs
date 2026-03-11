using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;
namespace GameMaker.Core.Runtime
{
    [System.Serializable]
    public class LocalPropertySaveData : BaseLocalData
    {
        [JsonProperty("PlayerProperties")]
        [SerializeReference]
        private List<PlayerPropertyModel> _playerProperties = new();
        [JsonIgnore]
        private PlayerPropertyTypeFactory _playerPropertyTypeFactory = new();
        protected internal override void OnCreate()
        {
            foreach (var propertyDefinition in PropertyManager.Instance.GetDefinitions())
            {
                var playerPropertyModelType = _playerPropertyTypeFactory.GetType(propertyDefinition.GetType());
                var playerProperty = Activator.CreateInstance(playerPropertyModelType,
                                                                propertyDefinition.GetID(),
                                                                propertyDefinition.GetName(),
                                                                propertyDefinition) as PlayerPropertyModel;
                playerProperty.Set(propertyDefinition.GetStringValue());
                _playerProperties.Add(playerProperty);
            }
        }
        protected internal override void OnLoad()
        {
            foreach (var propertyDefinition in PropertyManager.Instance.GetDefinitions())
            {
                var playerPropertyModel = _playerProperties.FirstOrDefault(x => x.GetID() == propertyDefinition.GetID());
                if (playerPropertyModel != null)
                {
                    playerPropertyModel.SetPropertyDefinition(propertyDefinition);
                    continue;
                }
                else
                {
                    var playerPropertyModelType = _playerPropertyTypeFactory.GetType(propertyDefinition.GetType());
                    var playerProperty = Activator.CreateInstance(playerPropertyModelType,
                                                                    propertyDefinition.GetID(),
                                                                    propertyDefinition.GetName(),
                                                                    propertyDefinition) as PlayerPropertyModel;
                    playerProperty.Set(propertyDefinition.GetStringValue());
                    _playerProperties.Add(playerProperty);
                }
            }
        }
        public List<PlayerProperty> GetPlayerProperties()
        {
            return _playerProperties.Where(x=>x.GetPropertyDefinition()!=null).Select(x => x.ToPlayerProperty()).ToList();
        }
        public PlayerProperty GetPlayerProperty(string propertyDefinitionId)
        {
            return _playerProperties.FirstOrDefault(x => x.GetID() == propertyDefinitionId)?.ToPlayerProperty();
        }
        public async UniTask AddPlayerPropertyAsync(string propertyDefinitionID, string value, bool isSave = true)
        {
            var playerProperty = _playerProperties.FirstOrDefault(x => x.GetID() == propertyDefinitionID);
            if (playerProperty == null)
            {
                var propertyDefinition = PropertyManager.Instance.GetDefinition(propertyDefinitionID);
                var playerPropertyModelType = _playerPropertyTypeFactory.GetType(propertyDefinition.GetType());
                var playerPropertyModel = Activator.CreateInstance(playerPropertyModelType,
                                                                propertyDefinition.GetID(),
                                                                propertyDefinition.GetName(),
                                                                propertyDefinition) as PlayerPropertyModel;
                playerPropertyModel.Add(value);
                _playerProperties.Add(playerPropertyModel);
            }
            else
            {
                playerProperty.Add(value);
            }
            if (isSave)
                await SaveAsync();
        }
        public async UniTask SetPlayerPropertyAsync(string propertyDefinitionID, string value, bool isSave = true)
        {
            var playerProperty = _playerProperties.FirstOrDefault(x => x.GetID() == propertyDefinitionID);
            if (playerProperty == null)
            {
                var propertyDefinition = PropertyManager.Instance.GetDefinition(propertyDefinitionID);
                var playerPropertyModelType = _playerPropertyTypeFactory.GetType(propertyDefinition.GetType());
                var playerPropertyModel = Activator.CreateInstance(playerPropertyModelType,
                                                                propertyDefinition.GetID(),
                                                                propertyDefinition.GetName(),
                                                                propertyDefinition) as PlayerPropertyModel;
                playerPropertyModel.Set(value);
                _playerProperties.Add(playerPropertyModel);
            }
            else
            {
                playerProperty.Set(value);
            }
            if (isSave)
                await SaveAsync();
        }
    }
    public class PlayerPropertyTypeFactory
    {
        private Dictionary<Type, Type> _caches;
        public PlayerPropertyTypeFactory()
        {
            _caches = TypeUtils.GetAllDerivedNonAbstractTypes(typeof(PlayerPropertyModel))
            .Where(x => x.GetCustomAttribute<GameMaker.Core.Runtime.TypeContainAttribute>()!=null)
            .ToDictionary(x => x.GetCustomAttribute<GameMaker.Core.Runtime.TypeContainAttribute>().Type, x => x);
        }
        public Type GetType(Type playerPropertyDefinitionType)
        {
            return _caches[playerPropertyDefinitionType];
        }
    }
    [System.Serializable]
    [TypeCache]
    public abstract class PlayerPropertyModel : PlayerDataModel
    {
        [JsonIgnore]
        protected PropertyDefinition _propertyDefinition;
        public PlayerPropertyModel() : base()
        {
            
        }
        public PlayerPropertyModel(string id, string name, PropertyDefinition propertyDefinition) : base(id, name)
        {
            _propertyDefinition = propertyDefinition;
        }
        public abstract void Add(string value);
        public abstract void Set(string value);
        public abstract string Get();
        public PropertyDefinition GetPropertyDefinition()
        {
            return _propertyDefinition;
        }
        public void SetPropertyDefinition(PropertyDefinition propertyDefinition)
        {
            _propertyDefinition = propertyDefinition;
        }
        public abstract PlayerProperty ToPlayerProperty();
    }
    [System.Serializable]
    [TypeContain(typeof(StatDefinition))]
    public class PlayerStatModel : PlayerPropertyModel
    {
        [JsonProperty("Value")]
        private float _value;
        public PlayerStatModel(){}
        public PlayerStatModel(string id, string name, PropertyDefinition propertyDefinition) : base(id, name,propertyDefinition)
        {
            _value = (propertyDefinition as StatDefinition).DefaultValue;
        }

        public override void Add(string value)
        {
            _value += float.Parse(value);
        }

        public override string Get()
        {
            return _value.ToString();
        }

        public override void Set(string value)
        {
            _value = float.Parse(value);
        }

        public override PlayerProperty ToPlayerProperty()
        {
            var statDefinition = PropertyManager.Instance.GetDefinition(id);
            return new PlayerStat(statDefinition.GetID(), statDefinition, _value);
        }
    }

    [System.Serializable]
    [TypeContain(typeof(AttributeDefinition))]
    public class PlayerAttributeModel : PlayerPropertyModel
    {
        [JsonProperty("Value")]
        private string _value;
        public PlayerAttributeModel(){}
        public PlayerAttributeModel(string id, string name,PropertyDefinition propertyDefinition) : base(id, name,propertyDefinition)
        {
        }

        public override PlayerProperty ToPlayerProperty()
        {
            var attributeDefinition = PropertyManager.Instance.GetDefinition(id);
            return new PlayerAttribute(attributeDefinition.GetID(), attributeDefinition, _value);
        }

        public override void Add(string value)
        {
           
        }

        public override void Set(string value)
        {
            _value = value;
        }

        public override string Get()
        {
            return _value;
        }
    }
}