using System.Collections.Generic;
using System.Linq;
using GameMaker.Core.Runtime;
using UnityEngine;

namespace GameMaker.Core.Runtime
{
    public class ItemActionDefinition : BaseActionDefinition
    {
        public readonly static string ADD_ITEM_ACTION_DEFINITION_ID = "ITEM_ADD";
        public readonly static string REMOVE_ITEM_ACTION_DEFINITION_ID = "ITEM_REMOVE";
        public ItemActionDefinition(string id, string name, string title,string description, Sprite icon, BaseMetaData metaData) : base(id, name, title,description, icon,metaData)
        {
            
        }

        public override object Clone()
        {
            return new ItemActionDefinition(GetID(), GetName(), GetTitle(), GetDescription(), GetIcon(),GetMetaData());
        }

        public override List<BaseActionDefinition> GetCoreActionDefinition()
        {
            return new List<BaseActionDefinition>()
            {
                new ItemActionDefinition(ADD_ITEM_ACTION_DEFINITION_ID, "ADD_ITEM_ACTION", "ADD_ITEM_ACTION","", null,null),
                new ItemActionDefinition(REMOVE_ITEM_ACTION_DEFINITION_ID, "REMOVE_ITEM_ACTION", "REMOVE_ITEM_ACTION","", null,null)
            };
        }

        public override List<IDefinition> GetDefinitions()
        {
            return ItemManager.Instance.GetDefinitions().Cast<IDefinition>().ToList();
        }
    }
}