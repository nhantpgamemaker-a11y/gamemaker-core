using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using GameMaker.Core.Runtime;
using UnityEngine;

namespace GameMaker.Core.Runtime
{
    [System.Serializable]
    public class ItemDetailActionData : BaseActionData
    {
        public const string ADD_ITEM_DETAIL_ACTION_DEFINITION = "ADD_ITEM_DETAIL_ACTION_DEFINITION";
        public const string REMOVE_ITEM_DETAIL_ACTION_DEFINITION = "REMOVE_ITEM_DETAIL_ACTION_DEFINITION";
        private string _itemDetailId;
        private int _amount;
        public string ItemDetailID => _itemDetailId;
        public int Amount => _amount;
        public ItemDetailActionData(): base(){}
        public ItemDetailActionData(string itemDetailId,int amount, IExtendData extendData) : base(extendData)
        {
            _itemDetailId = itemDetailId;
            _amount = amount;
        }
        public override List<ActionDefinition> GetGenerateActionDefinitions()
        {
            List<ActionDefinition> actionDefinitions = new();
            var actionParamManager = new BaseDefinitionManager<BaseActionParamDefinition>();
            actionParamManager.AddDefinition(new ItemActionParamDefinition("ItemDetailID", "ItemDetailID", "_itemDetailId"));
            actionParamManager.AddDefinition(new FloatActionParamDefinition("Amount", "Amount", "_amount"));
            ActionDefinition actionDefinition = new(ADD_ITEM_DETAIL_ACTION_DEFINITION, ADD_ITEM_DETAIL_ACTION_DEFINITION, actionParamManager);
            actionDefinitions.Add(actionDefinition);

            actionParamManager = new BaseDefinitionManager<BaseActionParamDefinition>();
            actionParamManager.AddDefinition(new ItemActionParamDefinition("ItemDetailID", "ItemDetailID", "_itemDetailId"));
            actionParamManager.AddDefinition(new FloatActionParamDefinition("Amount", "Amount", "_amount"));
            actionDefinition = new(REMOVE_ITEM_DETAIL_ACTION_DEFINITION, REMOVE_ITEM_DETAIL_ACTION_DEFINITION, actionParamManager);
            actionDefinitions.Add(actionDefinition);

            return actionDefinitions;
        }

        public override bool Equals(BaseActionData other)
        {
            if (other == null)
                return false;

            if (GetType() != other.GetType())
                return false;

            if (other is not ItemDetailActionData o)
                return false;

            return _itemDetailId == o._itemDetailId;
        }
    }
}