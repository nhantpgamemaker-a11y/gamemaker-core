using System.Collections.Generic;
using System.Linq;
using GameMaker.Core.Runtime;
using UnityEngine;

namespace GameMaker.Core.Runtime
{

    public class ItemDetailActionDefinition : BaseActionDefinition
    {
        public readonly static string ADD_ITEM_DETAIL_ACTION_DEFINITION_ID = "ITEM_DETAIL_ADD";
        public readonly static string REMOVE_ITEM_DETAIL_ACTION_DEFINITION_ID = "ITEM_DETAIL_REMOVE";

        public ItemDetailActionDefinition(string id, string name, string title,string description, Sprite icon, BaseMetaData metaData) : base(id, name, title,description, icon,metaData)
        {
        }

        public override object Clone()
        {
            return new ItemDetailActionDefinition(GetID(), GetName(), GetTitle(), GetDescription(), GetIcon(),GetMetaData());
        }

        public override List<BaseActionDefinition> GetCoreActionDefinition()
        {
            return new List<BaseActionDefinition>()
            {
                new ItemActionDefinition(ADD_ITEM_DETAIL_ACTION_DEFINITION_ID, "ADD_ITEM_DETAIL_ACTION", "ADD_ITEM_DETAIL_ACTION","",null,null),
                new ItemActionDefinition(REMOVE_ITEM_DETAIL_ACTION_DEFINITION_ID, "REMOVE_ITEM_DETAIL_ACTION", "REMOVE_ITEM_DETAIL_ACTION","", null,null)
            };
        }

        public override List<IDefinition> GetDefinitions()
        {
            return ItemDetailManager.Instance.GetDefinitions().Cast<IDefinition>().ToList();
        }
    }
}