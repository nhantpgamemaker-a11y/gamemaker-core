using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
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

        ItemPropertyDefinitionRef[] itemPropertyDefinitionRefModels,bool isSave = true)
        {
            var playerDetailItem = _playerDetailItems.FirstOrDefault(x => x.GetID() == playerItemDetailReferenceId);
            foreach (var stat in itemPropertyDefinitionRefModels)
            {
                playerDetailItem.UpdateItemPropertyDefinitionRefModel(stat);
            }
            if (isSave) await SaveAsync();
        }

        public async UniTask AddPlayerItemDetailAsync(PlayerDetailItem playerDetailItem, bool isSave = true)
        {
            _playerDetailItems.Add(new ItemDetailModel(
                playerDetailItem.GetID(),
                playerDetailItem.GetName(),
                playerDetailItem.ItemStatDefinitionRefs.Select(x => x.ToItemPropertyDefinitionRefModel()).ToList()
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
        [UnityEngine.SerializeReference]
        [JsonProperty("ItemStatDefinitionRefs")]
        private List<ItemPropertyDefinitionRefModel> _itemPropertyDefinitionRefs = new();
        public ItemDetailModel(string id, string name, List<ItemPropertyDefinitionRefModel> itemStatDefinitionRefs) : base(id, name)
        {
            _itemPropertyDefinitionRefs = itemStatDefinitionRefs;
        }

        public PlayerDetailItem ToPlayerItemDetail()
        {
            var itemDefinition = ItemDetailManager.Instance.GetDefinition(id);
            return new PlayerDetailItem(id, name, _itemPropertyDefinitionRefs.Select(x => x.ToItemPropertyDefinitionRef()).ToList(), itemDefinition);
        }
        public ItemPropertyDefinitionRefModel GetItemPropertyDefinitionRefModel(string referenceId)
        {
            return _itemPropertyDefinitionRefs.FirstOrDefault(x => x.GetID() == referenceId);
        }
        public void UpdateItemPropertyDefinitionRefModel(ItemPropertyDefinitionRef itemPropertyDefinitionRefModel)
        {
            var statRef = GetItemPropertyDefinitionRefModel(itemPropertyDefinitionRefModel.GetID());
            if (statRef == null)
            {
                _itemPropertyDefinitionRefs.Add(statRef);
            }
            else
            {
                statRef.UpdateValue(itemPropertyDefinitionRefModel);
                statRef.SetName(itemPropertyDefinitionRefModel.GetName().ToString());
            }
        }
    }

    [System.Serializable]

    public  abstract class ItemPropertyDefinitionRefModel: IDefinition
    {
        [SerializeField]
        [JsonProperty("Id")]
        private string _id;

        [SerializeField]
        [JsonProperty("Name")]
        private string _name;

        public ItemPropertyDefinitionRefModel(string id, string name)
        {
            _id = id;
            _name = name;
        }

        public bool Equals(IDefinition other)
        {
            if (other == null)
                return false;

            if (GetType() != other.GetType())
                return false;

            return GetID() == other.GetID();
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
        
        public abstract ItemPropertyDefinitionRef ToItemPropertyDefinitionRef();

        public abstract void UpdateValue(ItemPropertyDefinitionRef itemPropertyDefinitionRefModel);
    }

    [System.Serializable]
    public class ItemStatDefinitionRefModel : ItemPropertyDefinitionRefModel
    {
        [SerializeField]
        [JsonProperty("Value")]
        private float _value;
        [JsonIgnore]
        public float Value { get => _value; set => _value = value; }
        public ItemStatDefinitionRefModel(string refId, string name, float value) : base(refId, name)
        {
            _value = value;
        }

        public override ItemPropertyDefinitionRef ToItemPropertyDefinitionRef()
        {
            return new ItemStatDefinitionRef(GetID(), GetName(), _value);
        }

        public override void UpdateValue(ItemPropertyDefinitionRef itemPropertyDefinitionRefModel)
        {
            _value = (itemPropertyDefinitionRefModel as ItemStatDefinitionRef).Value;
        }
    }
    [System.Serializable]
    public class ItemAttributeDefinitionRefModel: ItemPropertyDefinitionRefModel
    {
        [SerializeField]
        [JsonProperty("Value")]
        private string _value;
    
        [JsonIgnore]
        public string Value { get => _value; set => _value = value; }
        public ItemAttributeDefinitionRefModel(string refId, string name, string value):base(refId, name)
        {
            _value = value;
        }

        public override ItemPropertyDefinitionRef ToItemPropertyDefinitionRef()
        {
            return new ItemAttributeDefinitionRef(GetID(),GetName(), _value);
        }

        public override void UpdateValue(ItemPropertyDefinitionRef itemPropertyDefinitionRefModel)
        {
            _value = (itemPropertyDefinitionRefModel as ItemAttributeDefinitionRef).Value;
        }
    }
}