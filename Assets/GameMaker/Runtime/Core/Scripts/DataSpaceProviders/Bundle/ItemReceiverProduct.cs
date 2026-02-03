using System.Collections.Generic;
using System.Linq;

namespace GameMaker.Core.Runtime
{
    public class ItemReceiverProduct : BaseReceiverProduct
    {
        private string _name;
        private string _itemDetailDefinitionID;
        private List<ItemPropertyDefinitionRef> _itemPropertyDefinitionRefs = new();
        public string Name { get => _name; }
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
    }
}