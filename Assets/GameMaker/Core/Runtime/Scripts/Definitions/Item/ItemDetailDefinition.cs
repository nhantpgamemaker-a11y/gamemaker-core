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
        [SerializeReference]
        private List<ItemPropertyDefinitionRef> _itemPropertyDefinitionRefs = new();
        public IReadOnlyList<ItemPropertyDefinitionRef> ItemPropertyDefinitionRefs { get => _itemPropertyDefinitionRefs; }
        public string ItemDefinitionId => _itemDefinitionId;
        public ItemDetailDefinition() : base() { }
        public ItemDetailDefinition(ItemDefinition itemDefinition) : base()
        {
            _itemDefinitionId = itemDefinition.GetID();
            itemDefinition.GetItemPropertyDefinitions().ForEach(s =>
            {
                _itemPropertyDefinitionRefs.Add(s.GetPropertyDefinitionRef());
            });
        }
        public ItemDetailDefinition(string id, string name, string title,string description, Sprite icon,BaseMetaData metaData, string itemDefinitionId, List<ItemPropertyDefinitionRef> itemPropertyDefinitionRefs) :base(id, name, title,description, icon,metaData)
        {
            _itemDefinitionId = itemDefinitionId;
            _itemPropertyDefinitionRefs = itemPropertyDefinitionRefs;
        }
        
        public void AddItemStatDefinitionRef(ItemStatDefinitionRef itemStatDefinitionRef)
        {
            _itemPropertyDefinitionRefs.Add(itemStatDefinitionRef);
        }

        public override object Clone()
        {
            return new ItemDetailDefinition(GetID(), GetName(), GetTitle(), GetDescription(), GetIcon(),GetMetaData(),_itemDefinitionId,_itemPropertyDefinitionRefs.Select(i=> i.Clone() as ItemPropertyDefinitionRef).ToList());
        }

        public void RemoveItemStatDefinitionRef(ItemStatDefinitionRef itemStatDefinitionRef)
        {
            _itemPropertyDefinitionRefs.Remove(itemStatDefinitionRef);
        }

        public ItemDefinition GetItemDefinition()
        {
            return ItemManager.Instance.GetDefinition(_itemDefinitionId);
        }

        public void ValidItem(ItemDefinition itemDefinition)
        {
            foreach (var p in itemDefinition.GetItemPropertyDefinitions())
            {
                var statRef = _itemPropertyDefinitionRefs.FirstOrDefault(statRef => statRef.GetID() == p.GetID());
                if (statRef == null)
                {
                    _itemPropertyDefinitionRefs.Add(p.GetPropertyDefinitionRef());
                }
                else
                {
                    statRef.SetName(p.GetName());
                }
            }
        }

        public string GetPrefixID()
        {
            return $"{_itemDefinitionId}_{GetID()}";
        }
    }
}