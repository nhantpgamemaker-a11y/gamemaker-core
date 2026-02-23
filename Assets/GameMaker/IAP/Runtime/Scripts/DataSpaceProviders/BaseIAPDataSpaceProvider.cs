using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using GameMaker.Core.Runtime;

namespace GameMaker.IAP.Runtime
{
    public abstract class BaseIAPDataSpaceProvider : IDataSpaceProvider
    {
        public virtual void Dispose() { }

        public abstract UniTask<bool> InitAsync(BaseDataSpaceSetting baseDataSpaceSetting);
        public abstract UniTask<(bool status, List<PlayerIAP>)> GetPlayerIAPs();
        public abstract UniTask<(bool, List<BaseReceiverProduct>)> RecallPlayerIAPsAsync(List<string> transactionIds);
        public abstract UniTask<bool> MarkActiveAsync(List<(string productIds, string transactionIds)> confirmedOrders);
        public abstract UniTask<(bool,List<BaseReceiverProduct>)> PurchaseAsync(PlayerIAP playerIAP);
    }
}