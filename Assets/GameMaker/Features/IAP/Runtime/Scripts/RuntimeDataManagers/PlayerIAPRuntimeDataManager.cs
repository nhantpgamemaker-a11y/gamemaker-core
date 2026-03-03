using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using GameMaker.Core.Runtime;

namespace GameMaker.IAP.Runtime
{
    [System.Serializable]
    [RuntimeDataManagerAttribute(new Type[] { typeof(BaseIAPDataSpaceProvider) })]
    public class PlayerIAPRuntimeDataManager : BaseRuntimeDataManager
    {
        private string _id = "PlayerIAPRuntimeDataManager";
        private BaseIAPDataSpaceProvider _iapDataSpaceProvider;
        private PlayerIAPManager _playerIAPManager;
        private PlayerDataManager[] _playerDataManager;
        public async override UniTask<bool> InitializeAsync(IDataSpaceProvider[] dataSpaceProviders,
        PlayerDataManager[] playerDataManagers)
        {
            _playerDataManager = playerDataManagers;
            _playerIAPManager = playerDataManagers.FirstOrDefault(x => x.GetType() == typeof(PlayerIAPManager)) as PlayerIAPManager;
            _iapDataSpaceProvider = dataSpaceProviders.OfType<BaseIAPDataSpaceProvider>().FirstOrDefault();
            var (status, playerIAPs) = await _iapDataSpaceProvider.GetPlayerIAPs();
            if (!status) return status;
            _playerIAPManager.Initialize(playerIAPs.Cast<BasePlayerData>().ToList());
            IAPGateway.Initialize(this);
            return true;
        }
        
        public List<PlayerIAP> GetPlayerIAPs()
        {
            return _playerIAPManager.GetPlayerIAPs();
        }

        public async UniTask<bool> RecallPlayerIAPsAsync(List<string> refundTransactionIds)
        {
            var (status, receiverProducts) = await _iapDataSpaceProvider.RecallPlayerIAPsAsync(refundTransactionIds);
            if (!status) return status;
            foreach (var product in receiverProducts)
            {
                product.Consume(_playerDataManager, new IAPPurchaseExtentData("IAP_Recall"));
            }
            refundTransactionIds.ForEach(x =>
            {
                var playerIAP = _playerIAPManager.GetPlayerIAPByTransactionId(x);
                playerIAP.SetActive(false);
            });
            return true;
        }

        public async UniTask<bool> MarkActiveAsync(List<(string productIds, string transactionIds)> confirmedOrders)
        {
            bool status = await _iapDataSpaceProvider.MarkActiveAsync(confirmedOrders);
            return status;
        }

        public async UniTask<(bool, List<BaseReceiverProduct>)> PurchaseAsync(PlayerIAP playerIAP)
        {
            var (status, receiverProducts) = await _iapDataSpaceProvider.PurchaseAsync(playerIAP);
            if (!status) return (status, null);
            playerIAP.SetProducts(receiverProducts);
            foreach (var product in receiverProducts)
            {
                product.Consume(_playerDataManager, new IAPPurchaseExtentData("IAP_Purchase"));
            }
            _playerIAPManager.AddPlayerIAP(playerIAP);
            return (true, receiverProducts);
        }
    }
}