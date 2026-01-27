using System;
using System.Collections.Generic;
using System.Linq;
using GameMaker.Core.Runtime;
using UnityEngine;

namespace GameMaker.Core.Runtime
{
    public class CurrencyActionDefinition : BaseActionDefinition
    {
        public readonly static string ADD_CURRENCY_ACTION_DEFINITION_ID = "CURRENCY_ADD";
        public CurrencyActionDefinition(string id, string name, string title,string description, Sprite icon, BaseMetaData metaData):base(id, name, title,description, icon, metaData)
        {
        }

        public override object Clone()
        {
            return new CurrencyActionDefinition(GetID(), GetName(), GetTitle(), GetDescription(), GetIcon(),GetMetaData());
        }

        public override List<BaseActionDefinition> GetCoreActionDefinition()
        {
            return new List<BaseActionDefinition>()
            {
                new CurrencyActionDefinition(ADD_CURRENCY_ACTION_DEFINITION_ID, "ADD_CURRENCY_ACTION", "ADD_CURRENCY_ACTION","", null, null)
            };
        }

        public override List<IDefinition> GetDefinitions()
        {
            return CurrencyManager.Instance.GetDefinitions().Cast<IDefinition>().ToList();
        }
    }
}