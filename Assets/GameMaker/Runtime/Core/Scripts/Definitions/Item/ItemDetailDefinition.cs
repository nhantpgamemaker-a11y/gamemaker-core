using System;
using System.Collections.Generic;
using System.Linq;
using GameMaker.Core.Runtime;
using UnityEngine;

namespace GameMaker.Core.Runtime
{
    [System.Serializable]
    public class ItemDetailDefinition : BaseDefinition
    {
        [SerializeField]
        private string _itemDefinitionId;
        [SerializeField]
        private List<ItemStatDefinitionRef> _itemStatDefinitionRefs = new();
        public IReadOnlyList<ItemStatDefinitionRef> ItemStatDefinitionRefs { get => _itemStatDefinitionRefs; }
        public string ItemDefinitionId => _itemDefinitionId;
        public ItemDetailDefinition() : base() { }
        public ItemDetailDefinition(ItemDefinition itemDefinition) : base()
        {
            _itemDefinitionId = itemDefinition.GetID();
            itemDefinition.GetItemStatDefinitions().ForEach(s =>
            {
                _itemStatDefinitionRefs.Add(new ItemStatDefinitionRef(s.GetID(), s.GetName(), s.DefaultValue));
            });
        }
        public ItemDetailDefinition(string id, string name, string title,string description, Sprite icon,BaseMetaData metaData, string itemDefinitionId, List<ItemStatDefinitionRef> itemStatDefinitionRefs) :base(id, name, title,description, icon,metaData)
        {
            _itemDefinitionId = itemDefinitionId;
            _itemStatDefinitionRefs = itemStatDefinitionRefs;
        }
        
        public void AddItemStatDefinitionRef(ItemStatDefinitionRef itemStatDefinitionRef)
        {
            _itemStatDefinitionRefs.Add(itemStatDefinitionRef);
        }

        public override object Clone()
        {
            return new ItemDetailDefinition(GetID(), GetName(), GetTitle(), GetDescription(), GetIcon(),GetMetaData(),_itemDefinitionId,_itemStatDefinitionRefs.Select(i=> i.Clone() as ItemStatDefinitionRef).ToList());
        }

        public void RemoveItemStatDefinitionRef(ItemStatDefinitionRef itemStatDefinitionRef)
        {
            _itemStatDefinitionRefs.Remove(itemStatDefinitionRef);
        }

        public ItemDefinition GetItemDefinition()
        {
            return ItemManager.Instance.GetDefinition(_itemDefinitionId);
        }

        public void ValidItem(ItemDefinition itemDefinition)
        {
            foreach (var stat in itemDefinition.GetItemStatDefinitions())
            {
                var statRef = _itemStatDefinitionRefs.FirstOrDefault(statRef => statRef.GetID() == stat.GetID());
                if (statRef == null)
                {
                    _itemStatDefinitionRefs.Add(new ItemStatDefinitionRef(stat.GetID(), stat.GetName(), stat.DefaultValue));
                }
                else
                {
                    statRef.SetName(stat.GetName());
                }
            }
        }

        public string GetPrefixID()
        {
            return $"{_itemDefinitionId}_{GetID()}";
        }
    }
}