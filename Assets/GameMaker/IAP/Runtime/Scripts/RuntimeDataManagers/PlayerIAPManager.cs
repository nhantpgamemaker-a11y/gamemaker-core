using System.Collections.Generic;
using System.Linq;
using GameMaker.Core.Runtime;

namespace GameMaker.IAP.Runtime
{
    public class PlayerIAPManager: PlayerDataManager
    {
        public void AddPlayerIAP(PlayerIAP playerIAP)
        {
            basePlayerDatas.Add(playerIAP);
        }
        public PlayerIAP GetPlayerIAPByTransactionId(string transactionId)
        {
            return GetPlayerDatas(x =>
            {
                var playerIAP = x as PlayerIAP;
                return playerIAP.TransactionId == transactionId;

            }).FirstOrDefault() as PlayerIAP;
        }
        public PlayerIAP GetPlayerIAP(string productId, string transactionId)
        {
            return GetPlayerDatas(x =>
            {
                var playerIAP = x as PlayerIAP;
                return playerIAP.ProductId == productId && playerIAP.TransactionId == transactionId;

            }).FirstOrDefault() as PlayerIAP;
        }
        public List<PlayerIAP> GetPlayerIAPs()
        {
            return basePlayerDatas.Cast<PlayerIAP>().ToList();
        }
    }
}