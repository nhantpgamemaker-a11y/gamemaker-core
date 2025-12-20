using System.Collections.Generic;
using GameMaker.Core.Runtime;
using UnityEngine;

namespace GameMaker.Item.Runtime
{
    [System.Serializable]
    public class ItemDefinition : BaseDefinition
    {
        [SerializeField]
        private BaseDefinitionManager<ItemStatDefinition> _itemStatManager = new();

        public List<ItemStatDefinition> GetItemStatDefinitions()
        {
            return _itemStatManager.GetDefinitions();
        }
        public ItemDefinition() : base()
        {
            
        }
        public ItemDefinition(string id, string name, string title,BaseDefinitionManager<ItemStatDefinition> itemStatManager): base(id, name, title)
        {
            _itemStatManager = itemStatManager;
        }
        public void AddItemStat(ItemStatDefinition itemStatDefinition)
        {
            _itemStatManager.AddDefinition(itemStatDefinition);
        }

        public override object Clone()
        {
            return new ItemDefinition(id, name, title, _itemStatManager.Clone() as BaseDefinitionManager<ItemStatDefinition>);
        }

        public void RemoveItemStat(ItemStatDefinition itemStatDefinition)
        {
            _itemStatManager.RemoveDefinition(itemStatDefinition);
        }
    }
}