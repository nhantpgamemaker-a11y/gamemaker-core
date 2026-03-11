using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace GameMaker.Core.Runtime
{
    [System.Serializable]
    public class ItemReceiverProduct : BaseReceiverProduct
    {
        [JsonProperty("Name")]
        private string _name;
        [JsonProperty("ItemDetailDefinitionID")]
        private string _itemDetailDefinitionID;
        [JsonProperty("ItemPropertyDefinitionRefs")]
        private List<ItemPropertyDefinitionRef> _itemPropertyDefinitionRefs = new();
        [JsonIgnore]
        public string Name { get => _name; }
        [JsonIgnore]
        public List<ItemPropertyDefinitionRef> ItemPropertyDefinitionRefs { get => _itemPropertyDefinitionRefs; }
        public ItemReceiverProduct(string id, string name, List<ItemPropertyDefinitionRef> itemStatDefinitionRefs, string itemDetailDefinitionID) : base(id)
        {
            _name = name;
            _itemDetailDefinitionID = itemDetailDefinitionID;
            _itemPropertyDefinitionRefs = itemStatDefinitionRefs;
        }
        public ItemDetailDefinition GetItemDetailDefinition()
        {
            return ItemDetailManager.Instance.GetDefinition(_itemDetailDefinitionID);
        }
        public override void Consume(PlayerDataManager[] playerDataManagers, IExtendData extendData)
        {
            var playerItemDetailManager = playerDataManagers.FirstOrDefault(x => x.GetType() == typeof(PlayerItemDetailManager)) as PlayerItemDetailManager;
            var itemDetailDefinition = ItemDetailManager.Instance.GetDefinition(_itemDetailDefinitionID);
            playerItemDetailManager.AddPlayerItem(new PlayerDetailItem(ID,Name,ItemPropertyDefinitionRefs, itemDetailDefinition));
            RuntimeActionManager.Instance.NotifyAction(ItemActionData.ADD_ITEM_ACTION_DEFINITION, new ItemActionData(itemDetailDefinition.ItemDefinitionId,1,extendData));
            RuntimeActionManager.Instance.NotifyAction(ItemDetailActionData.ADD_ITEM_DETAIL_ACTION_DEFINITION, new ItemDetailActionData(_itemDetailDefinitionID,1,extendData));
        }

        public override object Clone()
        {
            return new ItemReceiverProduct(ID, Name, ItemPropertyDefinitionRefs.Select(x => x.Clone() as ItemPropertyDefinitionRef).ToList(),_itemDetailDefinitionID);
        }
    }
}