using System;
using System.Linq;
using System.Threading.Tasks;
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
        
        public async UniTask<bool> SetPlayerPropertyAsync(string id, string value, IExtendData extendData)
        {
            bool status = await _propertyDataSpaceProvider.SetAsync(id, value);
            if (status)
            {
                _playerPropertyManager.Set(id, value);
                RuntimeActionManager.Instance.NotifyAction(PropertyActionData.SET_PROPERTY_ACTION_DEFINITION, new PropertyActionData(id, value, extendData));
            }
            return status;
        }
    }
}