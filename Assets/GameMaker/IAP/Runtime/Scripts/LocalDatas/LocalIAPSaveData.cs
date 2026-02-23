using System;
using GameMaker.Core.Runtime;

namespace GameMaker.IAP.Runtime
{
    [System.Serializable]
    public class LocalIAPSaveData : BaseLocalData
    {
        public PlayerIAP GetPlayerIAPByTransactionId(string transactionId)
        {
            throw new NotImplementedException();
        }
    }
}