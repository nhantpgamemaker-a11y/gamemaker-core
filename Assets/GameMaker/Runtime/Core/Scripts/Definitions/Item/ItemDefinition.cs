using System.Collections.Generic;
using GameMaker.Core.Runtime;
using UnityEngine;

namespace GameMaker.Core.Runtime
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
        public ItemDefinition(string id, string name, string title,string description, Sprite icon,BaseMetaData metaData,BaseDefinitionManager<ItemStatDefinition> itemStatManager): base(id, name, title,description, icon,metaData)
        {
            _itemStatManager = itemStatManager;
        }
        public void AddItemStat(ItemStatDefinition itemStatDefinition)
        {
            _itemStatManager.AddDefinition(itemStatDefinition);
        }

        public override object Clone()
        {
            return new ItemDefinition(GetID(), GetName(), GetTitle(), GetDescription(), GetIcon(),GetMetaData(), _itemStatManager.Clone() as BaseDefinitionManager<ItemStatDefinition>);
        }

        public void RemoveItemStat(ItemStatDefinition itemStatDefinition)
        {
            _itemStatManager.RemoveDefinition(itemStatDefinition);
        }
    }
}