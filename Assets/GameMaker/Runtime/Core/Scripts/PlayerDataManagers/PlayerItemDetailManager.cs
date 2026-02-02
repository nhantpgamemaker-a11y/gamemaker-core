using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using Cysharp.Threading.Tasks;
using GameMaker.Core.Runtime;
using UnityEngine;

namespace GameMaker.Core.Runtime
{
    [System.Serializable]
    public class PlayerItemDetailManager : PlayerDataManager
    {
        private ObservableCollection<PlayerDetailItem> _playerDetailItems;
        private List<Action<object, NotifyCollectionChangedEventArgs>> _actions = new();
        public PlayerItemDetailManager()
        {
            _playerDetailItems = new();
        }
        ~PlayerItemDetailManager()
        {
            _playerDetailItems.CollectionChanged -= OnCollectionChanged;
        }
        public void AddObserver(IObserverWithScope<PlayerDetailItem, string> observer, string[] scopes)
        {
            AddObserver((IObserverWithScope<BasePlayerData, string>)observer, scopes);
        }
        public void RemoveObserver(IObserverWithScope<PlayerDetailItem, string> observer, string[] scopes)
        {
            RemoveObserver((IObserverWithScope<BasePlayerData, string>)observer, scopes);
        }
        public List<PlayerDetailItem> GetPlayerDetailItems()
        {
            return basePlayerDatas.Cast<PlayerDetailItem>().ToList();
        }
        public PlayerDetailItem GetPlayerDetailItem(string referenceId)
        {
            return basePlayerDatas.FirstOrDefault(x => (x as PlayerDetailItem).GetID() == referenceId) as PlayerDetailItem;
        }
        public void UpdatePlayerDetailItem(string id, PlayerDetailItem playerDetailItem,IExtendData extendData)
        {
            var playerItem = GetPlayerDetailItem(id);
            playerItem.Update(playerDetailItem);
        }
        public void AddPlayerItem(PlayerDetailItem playerDetailItem,IExtendData extendData)
        {
            basePlayerDatas.Add(playerDetailItem);
            _playerDetailItems.Add(playerDetailItem);
            var itemDetailDefinition = playerDetailItem.GetDefinition() as ItemDetailDefinition;
            var itemDefinition = itemDetailDefinition.GetItemDefinition();
            //RuntimeActionManager.Instance.NotifyAction(ItemActionDefinition.ADD_ITEM_ACTION_DEFINITION_ID, new ItemActionData(itemDefinition.GetID(),extendData));
            //RuntimeActionManager.Instance.NotifyAction(ItemDetailActionDefinition.ADD_ITEM_DETAIL_ACTION_DEFINITION_ID, new ItemDetailActionData(itemDetailDefinition.GetID(),extendData));
        }
        public void RemovePlayerItem(PlayerDetailItem playerDetailItem,IExtendData extendData)
        {
            basePlayerDatas.Remove(playerDetailItem);
            _playerDetailItems.Remove(playerDetailItem);
            var itemDetailDefinition = playerDetailItem.GetDefinition() as ItemDetailDefinition;
            var itemDefinition = itemDetailDefinition.GetItemDefinition();
            //RuntimeActionManager.Instance.NotifyAction(ItemActionDefinition.REMOVE_ITEM_ACTION_DEFINITION_ID, new ItemActionData(itemDefinition.GetID(),extendData));
            //RuntimeActionManager.Instance.NotifyAction(ItemDetailActionDefinition.REMOVE_ITEM_DETAIL_ACTION_DEFINITION_ID, new ItemDetailActionData(itemDetailDefinition.GetID(),extendData));
        }
        private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
        {
            _actions.ForEach(a => a.Invoke(sender, notifyCollectionChangedEventArgs));
        }
    }
}