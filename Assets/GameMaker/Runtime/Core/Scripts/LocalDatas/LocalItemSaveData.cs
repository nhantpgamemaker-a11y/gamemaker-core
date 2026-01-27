using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using GameMaker.Core.Runtime;
using Newtonsoft.Json;
using UnityEngine;

namespace GameMaker.Core.Runtime
{
    [System.Serializable]
    public class LocalItemSaveData : BaseLocalData
    {
        [JsonProperty("PlayerDetailItems")]
        [SerializeReference]
        private List<ItemDetailModel> _playerDetailItems = new();

        public List<PlayerDetailItem> GetPlayerDetailItems()
        {
            return _playerDetailItems.Select(x => x.ToPlayerItemDetail()).ToList();
        }

        public async UniTask UpdateItemDetailAsync(string playerItemDetailReferenceId,

        ItemStatDefinitionRefModel[] itemStatDefinitionRefModels,bool isSave = true)
        {
            var playerDetailItem = _playerDetailItems.FirstOrDefault(x => x.GetID() == playerItemDetailReferenceId);
            foreach (var stat in itemStatDefinitionRefModels)
            {
                playerDetailItem.UpdateItemStatDefinitionRefModel(stat);
            }
            if (isSave) await SaveAsync();
        }

        public async UniTask AddPlayerItemDetailAsync(PlayerDetailItem playerDetailItem,bool isSave = true)
        {
            _playerDetailItems.Add(new ItemDetailModel(
                playerDetailItem.GetID(),
                playerDetailItem.GetName(),
                playerDetailItem.ItemStatDefinitionRefs.Select(x => new ItemStatDefinitionRefModel(x.GetID(), x.GetName(), x.Value)).ToList()
            ));
            if (isSave) await SaveAsync();
        }
        
        public void RemovePlayerItemDetail(string playerItemDetailReferenceId)
        {
            var playerDetailItem = _playerDetailItems.FirstOrDefault(x => x.GetID() == playerItemDetailReferenceId);
            _playerDetailItems.Remove(playerDetailItem);
        }
    }
    [System.Serializable]
    public class ItemDetailModel : PlayerDataModel
    {
        [SerializeField]
        [JsonProperty("ItemStatDefinitionRefs")]
        private List<ItemStatDefinitionRefModel> _itemStatDefinitionRefs = new();
        public ItemDetailModel(string id, string name, List<ItemStatDefinitionRefModel> itemStatDefinitionRefs) : base(id, name)
        {
            _itemStatDefinitionRefs = itemStatDefinitionRefs;
        }

        public PlayerDetailItem ToPlayerItemDetail()
        {
            var itemDefinition = ItemDetailManager.Instance.GetDefinition(id);
            return new PlayerDetailItem(id, name, _itemStatDefinitionRefs.Select(x => x.ToItemStatDefinitionRef()).Cast<ItemStatDefinitionRef>().ToList(), itemDefinition);
        }
        public ItemStatDefinitionRefModel GetItemStatDefinitionRefModel(string referenceId)
        {
            return _itemStatDefinitionRefs.FirstOrDefault(x => x.GetID() == referenceId);
        }
        public void UpdateItemStatDefinitionRefModel(ItemStatDefinitionRefModel itemStatDefinitionRefModel)
        {
            var statRef = GetItemStatDefinitionRefModel(itemStatDefinitionRefModel.GetID());
            if(statRef == null)
            {
                _itemStatDefinitionRefs.Add(statRef);
            }
            else
            {
                statRef.Value = itemStatDefinitionRefModel.Value;
                statRef.SetName(itemStatDefinitionRefModel.GetName().ToString());
            }
        }
    }

    [System.Serializable]
    public class ItemStatDefinitionRefModel: IDefinition
    {
        [SerializeField]
        [JsonProperty("Id")]
        private string _id;

        [SerializeField]
        [JsonProperty("Name")]
        private string _name;

        [SerializeField]
        [JsonProperty("Value")]
        private float _value;
    
        public float Value { get => _value; set => _value = value; }
        public ItemStatDefinitionRefModel(string refId, string name, float value)
        {
            _id = refId;
            _value = value;
            _name = name;
        }
        public ItemStatDefinitionRef ToItemStatDefinitionRef()
        {
            return new ItemStatDefinitionRef(_id,_name, _value);
        }

        public string GetID()
        {
            return _id;
        }

        public string GetName()
        {
            return _name;
        }

        public void SetID(string id)
        {
            _id = id;
        }

        public void SetName(string name)
        {
            _name = name;
        }
    }
}