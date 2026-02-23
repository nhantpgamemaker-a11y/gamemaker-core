using System;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine.UIElements;

namespace GameMaker.Core.Runtime
{
    [RuntimeDataManager(new Type[] 
    { typeof(BaseBundleDataSpaceProvider) })]
    [System.Serializable]
    public class BundleRuntimeDataManager : BaseRuntimeDataManager
    {
        private string _id = "BundleRuntimeDataManager";
        private BaseBundleDataSpaceProvider _bundleDataSpaceProvider;
        private PlayerDataManager[] _playerDataManager;
        public async override UniTask<bool> InitializeAsync(IDataSpaceProvider[] dataSpaceProviders, PlayerDataManager[] playerDataManagers)
        {
            _playerDataManager = playerDataManagers;
            _bundleDataSpaceProvider = dataSpaceProviders.OfType<BaseBundleDataSpaceProvider>().FirstOrDefault();
            BundleGateway.Initialize(this);
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