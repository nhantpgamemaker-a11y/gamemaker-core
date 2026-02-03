using System;
using System.Linq;
using Cysharp.Threading.Tasks;

namespace GameMaker.Core.Runtime
{
    [RuntimeDataManager(new Type[] 
    { typeof(BaseBundleDataSpaceProvider) })]
    [System.Serializable]
    public class BundleRuntimeDataManager : BaseRuntimeDataManager
    {
        private string _id = "BundleRuntimeDataManager";
        private BaseBundleDataSpaceProvider _bundleDataSpaceProvider;
        private PlayerCurrencyManager _playerCurrencyManager;
        private PlayerPropertyManager _playerPropertyManager;
        private PlayerItemDetailManager _playerItemDetailManager;
        private PlayerDataManager[] _playerDataManager;
        public async override UniTask<bool> InitializeAsync(IDataSpaceProvider[] dataSpaceProviders, PlayerDataManager[] playerDataManagers)
        {
            _playerDataManager = playerDataManagers;
            _playerCurrencyManager = playerDataManagers.FirstOrDefault(x => x.GetType() == typeof(PlayerCurrencyManager)) as PlayerCurrencyManager;
            _playerPropertyManager = playerDataManagers.FirstOrDefault(x => x.GetType() == typeof(PlayerPropertyManager)) as PlayerPropertyManager;
            _playerItemDetailManager = playerDataManagers.FirstOrDefault(x => x.GetType() == typeof(PlayerItemDetailManager)) as PlayerItemDetailManager;
            _bundleDataSpaceProvider = dataSpaceProviders.OfType<BaseBundleDataSpaceProvider>().FirstOrDefault();
            return true;
        }

        public async UniTask<bool> ConsumeAsync(BundleDefinition bundleDefinition, IExtendData extendData)
        {
            var (status, baseReceiverProducts) = await _bundleDataSpaceProvider.ConsumeAsync(bundleDefinition);
            if (!status) return status;
            foreach (var product in baseReceiverProducts)
            {
                product.Consume(_playerDataManager, extendData);
            } 
            return true;
        }
    }
}