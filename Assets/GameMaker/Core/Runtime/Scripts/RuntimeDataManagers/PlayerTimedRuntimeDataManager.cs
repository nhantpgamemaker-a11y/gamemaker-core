using System;
using System.Linq;
using Cysharp.Threading.Tasks;

namespace GameMaker.Core.Runtime
{
    [System.Serializable]
    [RuntimeDataManager(new Type[] { typeof(BaseTimedDataSpaceProvider) })]
    public class PlayerTimedRuntimeDataManager : BaseRuntimeDataManager
    {
        private string _id = "PlayerTimedRuntimeDataManager";
        private BaseTimedDataSpaceProvider _timedDataSpaceProvider;
        private PlayerTimedManager _playerTimedManager;

        public override async UniTask<bool> InitializeAsync(IDataSpaceProvider[] dataSpaceProviders, PlayerDataManager[] playerDataManagers)
        {
            _playerTimedManager = playerDataManagers.FirstOrDefault(x => x.GetType() == typeof(PlayerTimedManager)) as PlayerTimedManager;
            _timedDataSpaceProvider = dataSpaceProviders.OfType<BaseTimedDataSpaceProvider>().FirstOrDefault();
            var (status, playerTimeds) = await _timedDataSpaceProvider.GetPlayerTimedAsync();
            if (!status) return false;
            _playerTimedManager.Initialize(playerTimeds.Cast<BasePlayerData>().ToList());
            TimedGateway.Initialize(this);
            return true;
        }
        public async UniTask<bool> AddPlayerTimed(string refId, long amount, BaseMetaData metaData)
        {
            var (status, playerTimed) = await _timedDataSpaceProvider.AddTimedAsync(refId, amount);
            if (status)
            {
                _playerTimedManager.CopyFrom(playerTimed);
                // ToDo: Add Runtime Action Notify
            }
            return true;
        }
    }
}