using System;
using System.Collections.Generic;
using GameMaker.Core.Runtime;
using UnityEngine;
using UnityEngine.Serialization;

namespace GameMaker.Core.Runtime
{
    [System.Serializable]
    public class ItemDefinition : BaseDefinition
    {
        [UnityEngine.SerializeField]
        private BaseDefinitionManager<ItemPropertyDefinition> _itemPropertyManager = new();

        public List<ItemPropertyDefinition> GetItemPropertyDefinitions()
        {
            return _itemPropertyManager.GetDefinitions();
        }
        public ItemDefinition() : base()
        {
            
        }
        public ItemDefinition(string id, string name, string title,string description, Sprite icon,BaseMetaData metaData,BaseDefinitionManager<ItemPropertyDefinition> itemPropertyManager): base(id, name, title,description, icon,metaData)
        {
            _itemPropertyManager = itemPropertyManager;
        }
        public void AddItemStat(ItemStatDefinition itemStatDefinition)
        {
            _itemPropertyManager.AddDefinition(itemStatDefinition);
        }

        public override object Clone()
        {
            return new ItemDefinition(GetID(), GetName(), GetTitle(), GetDescription(), GetIcon(),GetMetaData(), _itemPropertyManager.Clone() as BaseDefinitionManager<ItemPropertyDefinition>);
        }

        public void RemoveItemStat(ItemStatDefinition itemStatDefinition)
        {
            _itemPropertyManager.RemoveDefinition(itemStatDefinition);
        }

        
    }
}