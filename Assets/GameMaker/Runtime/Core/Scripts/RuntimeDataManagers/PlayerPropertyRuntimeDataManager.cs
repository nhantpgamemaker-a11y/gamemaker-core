using System;
using System.Globalization;
using System.Linq;
using System.Reflection;
using Cysharp.Threading.Tasks;
using GameMaker.Core.Runtime;

namespace GameMaker.Core.Runtime
{
    [System.Serializable]
    [RuntimeDataManager(new Type[] { typeof(BasePropertyDataSpaceProvider)}, new Type[] { typeof(PlayerPropertyManager)})]
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
                _playerPropertyManager.AddStat(id, amount, extendData);
            }
            return status;
        }
        public async UniTask<bool> SetAttributeAsync(string id, string value, IExtendData extendData)
        {
            bool status = await _propertyDataSpaceProvider.SetAttributeAsync(id, value);
            if (status)
            {
                _playerPropertyManager.SetAttribute(id, value, extendData);
            }
            return status;
        } 
        public async UniTask<bool> SetStatAsync(string id, long value, IExtendData extendData)
        {
            bool status = await _propertyDataSpaceProvider.SetStatAsync(id, value);
            if (status)
            {
                _playerPropertyManager.SetStat(id, value, extendData);
            }
            return status;
        }
    }
}