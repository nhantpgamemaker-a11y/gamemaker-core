using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
namespace GameMaker.Core.Runtime
{
    [RuntimeDataManager(new Type[] { typeof(BaseItemDataSpaceProvider) })]
    [System.Serializable]
    public class PlayerItemDetailRuntimeDataManager : BaseRuntimeDataManager
    {
        private string _id = "PlayerItemDetailRuntimeDataManager";
        [UnityEngine.SerializeField]
        private PlayerItemDetailManager _playerItemDetailManager;
        public PlayerItemDetailManager PlayerItemDetailManager => _playerItemDetailManager;

        private BaseItemDataSpaceProvider _itemDataSpaceProvider;
        public override async UniTask<bool> InitializeAsync(IDataSpaceProvider[] dataSpaceProviders, PlayerDataManager[] playerDataManagers)
        {
            _playerItemDetailManager = playerDataManagers.FirstOrDefault(x => x.GetType() == typeof(PlayerItemDetailManager)) as PlayerItemDetailManager;
            _itemDataSpaceProvider = dataSpaceProviders.OfType<BaseItemDataSpaceProvider>().FirstOrDefault();
            var (status, playerDetailItems) = await _itemDataSpaceProvider.GetPlayerItemDetails();
            if (!status) return status;
            _playerItemDetailManager.Initialize(playerDetailItems.Cast<BasePlayerData>().ToList());
            return true;
        }
        public List<PlayerDetailItem> GetPlayerDetailItems()
        {
            return _playerItemDetailManager.GetPlayerDetailItems();
        }
        public PlayerDetailItem GetPlayerDetailItem(string id)
        {
            return _playerItemDetailManager.GetPlayerDetailItem(id);
        }
        public async UniTask<bool> AddPlayerItemDetailAsync(PlayerDetailItem playerDetailItem, IExtendData extendData)
        {
            bool status = await _itemDataSpaceProvider.AddPlayerItemDetailAsync(playerDetailItem);
            if (status)
            {
                _playerItemDetailManager.AddPlayerItem(playerDetailItem);
                var itemDetailDefinition = playerDetailItem.GetDefinition() as ItemDetailDefinition;
                var itemDefinition = itemDetailDefinition.GetItemDefinition();

                RuntimeActionManager.Instance.NotifyAction(ItemActionData.ADD_ITEM_ACTION_DEFINITION, new ItemActionData(itemDefinition.GetID(),1,extendData));
                RuntimeActionManager.Instance.NotifyAction(ItemDetailActionData.ADD_ITEM_DETAIL_ACTION_DEFINITION, new ItemDetailActionData(itemDetailDefinition.GetID(),1,extendData));
    
            }
            return status;
        }
        public async UniTask<bool> UpdatePlayerDetailItemAsync(string id, PlayerDetailItem playerDetailItem, IExtendData extendData)
        {
            bool status = await _itemDataSpaceProvider.UpdatePlayerDetailItemAsync(id, playerDetailItem);
            if (status)
            {
                _playerItemDetailManager.UpdatePlayerDetailItem(id, playerDetailItem, extendData);
            }
            return status;
        }
        public async UniTask<bool> RemovePlayerDetailItemAsync(PlayerDetailItem playerDetailItem, IExtendData extendData)
        {
            bool status = await _itemDataSpaceProvider.RemovePlayerItemDetailAsync(playerDetailItem);
            if (status)
            {
                _playerItemDetailManager.RemovePlayerItem(playerDetailItem, extendData);

                var itemDetailDefinition = playerDetailItem.GetDefinition() as ItemDetailDefinition;
                RuntimeActionManager.Instance.NotifyAction(ItemActionData.REMOVE_ITEM_ACTION_DEFINITION, new ItemActionData(itemDetailDefinition.ItemDefinitionId,1,extendData));
                RuntimeActionManager.Instance.NotifyAction(ItemDetailActionData.REMOVE_ITEM_DETAIL_ACTION_DEFINITION, new ItemDetailActionData(itemDetailDefinition.GetID(),1,extendData));
            }
            return status;
        }
    }
}