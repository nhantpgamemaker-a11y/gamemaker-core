using System;
using System.Collections.Generic;
using GameMaker.Core.Runtime;
using UnityEngine;

namespace GameMaker.Core.Runtime
{
    [System.Serializable]
    public class PlayerDetailItem : BasePlayerData, IDefinition
    {
        private string _name;
        private List<ItemPropertyDefinitionRef> _itemPropertyDefinitionRefs = new();
        public PlayerDetailItem(string id, string name,List<ItemPropertyDefinitionRef> itemPropertyDefinitionRefs , IDefinition definition) : base(id,definition)
        {
            _name = name;
            _itemPropertyDefinitionRefs = itemPropertyDefinitionRefs;
        }

        public List<ItemPropertyDefinitionRef> ItemStatDefinitionRefs { get => _itemPropertyDefinitionRefs; }

        public override object Clone()
        {
            return new PlayerDetailItem(GetID(), _name,_itemPropertyDefinitionRefs,definition);
        }

        public override void CopyFrom(BasePlayerData basePlayerData)
        {
            _itemPropertyDefinitionRefs = (basePlayerData as PlayerDetailItem).ItemStatDefinitionRefs;
            NotifyObserver(this);
        }

        public bool Equals(IDefinition other)
        {
            if (other == null)
                return false;

            if (GetType() != other.GetType())
                return false;

            return GetID() == other.GetID();
        }

        public string GetName()
        {
            return _name;
        }

        public void Update(PlayerDetailItem playerDetailItem)
        {
            _itemPropertyDefinitionRefs = playerDetailItem._itemPropertyDefinitionRefs;
            NotifyObserver(this);
        }
        public void Update(params ItemStatDefinitionRef[] itemStatDefinitionRefs)
        {
            foreach (var param in itemStatDefinitionRefs)
            {
                _itemPropertyDefinitionRefs.Remove(param);
                _itemPropertyDefinitionRefs.Add(param);
            }
            NotifyObserver(this);
        }
    }
}