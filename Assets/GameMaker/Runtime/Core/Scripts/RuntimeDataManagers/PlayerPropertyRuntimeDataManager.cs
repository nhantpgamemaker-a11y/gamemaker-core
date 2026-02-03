using System;
using System.Linq;
using Cysharp.Threading.Tasks;

namespace GameMaker.Core.Runtime
{
    [System.Serializable]
    [RuntimeDataManager(new Type[] { typeof(BasePropertyDataSpaceProvider)})]
    public class PlayerPropertyRuntimeDataManager: BaseRuntimeDataManager
    {
        private string _id = "PlayerPropertyRuntimeDataManager";
        [UnityEngine.SerializeField]
        private PlayerPropertyManager _playerPropertyManager;
        public PlayerPropertyManager PlayerPropertyManager => _playerPropertyManager;
        private BasePropertyDataSpaceProvider _propertyDataSpaceProvider;
        public override async UniTask<bool> InitializeAsync(IDataSpaceProvider[] dataSpaceProviders, PlayerDataManager[] playerDataManagers)
        {
            _playerPropertyManager = playerDataManagers.FirstOrDefault(x => x.GetType() == typeof(PlayerPropertyManager)) as PlayerPropertyManager;
            _propertyDataSpaceProvider = dataSpaceProviders.OfType<BasePropertyDataSpaceProvider>().FirstOrDefault();
            var (status, playerProperties) = await _propertyDataSpaceProvider.GetPlayerPropertiesAsync();
            if (!status) return status;
            _playerPropertyManager.Initialize(playerProperties.Cast<BasePlayerData>().ToList());
            PropertyGateway.Initialize(this);
            return true;
        }
        public PlayerProperty GetPlayerProperty(string id)
        {
            return _playerPropertyManager.GetProperty(id);
        }
        public PlayerStat GetPlayerStat(string id)
        {
            return _playerPropertyManager.GetProperty(id) as PlayerStat;
        }
        public PlayerAttribute GetPlayerAttribute(string id)
        {
            return _playerPropertyManager.GetProperty(id) as PlayerAttribute;
        }
        public async UniTask<bool> AddPlayerStatAsync(string id, long amount, IExtendData extendData)
        {
            bool status = await _propertyDataSpaceProvider.AddStatAsync(id, amount);
            if (status)
            {
                _playerPropertyManager.AddStat(id, amount);
                RuntimeActionManager.Instance.NotifyAction(StatActionData.ADD_STAT_ACTION_DEFINITION, new StatActionData(id, amount, extendData));
            }
            return status;
        }
        public async UniTask<bool> SetAttributeAsync(string id, string value, IExtendData extendData)
        {
            bool status = await _propertyDataSpaceProvider.SetAttributeAsync(id, value);
            if (status)
            {
                _playerPropertyManager.SetAttribute(id, value);
                RuntimeActionManager.Instance.NotifyAction(AttributeActionData.SET_ATTRIBUTE_ACTION_DEFINITION, new AttributeActionData(id, value, extendData));
            }
            return status;
        } 
        public async UniTask<bool> SetStatAsync(string id, long value, IExtendData extendData)
        {
            bool status = await _propertyDataSpaceProvider.SetStatAsync(id, value);
            if (status)
            {
                _playerPropertyManager.SetStat(id, value);
                RuntimeActionManager.Instance.NotifyAction(StatActionData.SET_STAT_ACTION_DEFINITION, new StatActionData(id, value, extendData));
            }
            return status;
        }
    }
}