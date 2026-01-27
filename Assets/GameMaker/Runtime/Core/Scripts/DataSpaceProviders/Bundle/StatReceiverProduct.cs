using System.Collections.Generic;

namespace GameMaker.Core.Runtime
{
    public enum ConsumeType
    {
        Add= 0 ,
        Set = 1
    }
    public class ItemReceiverProduct : BaseReceiverProduct
    {
        private string _name;
        private List<ItemStatDefinitionRef> _itemStatDefinitionRefs = new();
        private string _itemDefinitionId;
        public string Name { get => _name; }
        public List<ItemStatDefinitionRef> ItemStatDefinitionRefs { get => _itemStatDefinitionRefs; }
        public string ItemDefinitionId { get => _itemDefinitionId; }
        public ItemReceiverProduct(string id, string name,List<ItemStatDefinitionRef> itemStatDefinitionRefs,string itemDefinitionId) : base(id)
        {
            _name = name;
            _itemStatDefinitionRefs = itemStatDefinitionRefs;
            _itemDefinitionId = itemDefinitionId;
        }
    }
}