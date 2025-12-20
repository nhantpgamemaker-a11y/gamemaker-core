using System;
using System.Collections.Generic;
using System.Linq;
using GameMaker.Core.Runtime;
using JetBrains.Annotations;
using UnityEngine;

namespace GameMaker.Item.Runtime
{
    [System.Serializable]
    public class ItemStatDefinitionRef: IDefinition,ICloneable, IEquatable<ItemStatDefinitionRef>
    {
        [SerializeField]
        private string _id;
        [SerializeField]
        private string _name;
        [SerializeField]
        private float _value;

        public float Value { get => _value; set => _value = value; }
        public ItemStatDefinitionRef(string refId,string name, float value)
        {
            _id = refId;
            _value = value;
            _name = name;
        }

        public string GetID()
        {
            return _id;
        }

        public string GetName()
        {
            return _name;
        }

        public bool Equals(ItemStatDefinitionRef other)
        {
            return other.GetID() == _id;
        }

        public object Clone()
        {
            return new ItemStatDefinitionRef(_id,_name, _value);
        }

        public void SetID(string id)
        {
            _id = id;
        }

        public void SetName(string name)
        {
            _name = name;
        }
    }

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
        public ItemDetailDefinition(string id, string name, string title, string itemDefinitionId, List<ItemStatDefinitionRef> itemStatDefinitionRefs) : base(id, name, title)
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
            return new ItemDetailDefinition(id, name, title,_itemDefinitionId,_itemStatDefinitionRefs.Select(i=> i.Clone() as ItemStatDefinitionRef).ToList());
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
            foreach(var stat in itemDefinition.GetItemStatDefinitions())
            {
                var statRef = _itemStatDefinitionRefs.FirstOrDefault(statRef => statRef.GetID() == stat.GetID());
                if (statRef == null)
                {
                    _itemStatDefinitionRefs.Add(new ItemStatDefinitionRef(stat.GetID(),stat.GetName(), stat.DefaultValue));
                }
                else
                {
                    statRef.SetName(stat.GetName());
                }
            }
        }
    }
}