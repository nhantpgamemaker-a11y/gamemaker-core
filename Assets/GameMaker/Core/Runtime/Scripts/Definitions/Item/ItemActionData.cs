using System.Collections.Generic;
using GameMaker.Core.Runtime;
using UnityEngine;

namespace GameMaker.Core.Runtime
{
    [System.Serializable]
    public class ItemActionData : BaseActionData
    {
        public const string ADD_ITEM_ACTION_DEFINITION = "ADD_ITEM_ACTION_DEFINITION";
        public const string REMOVE_ITEM_ACTION_DEFINITION = "REMOVE_ITEM_ACTION_DEFINITION";
        private string _itemId;
        private int _amount;
        public string ItemID => _itemId;
        public int Amount => _amount;
        public ItemActionData():base(){}
        public ItemActionData(string itemId, int amount, IExtendData extendData) : base(extendData)
        {
            _itemId = itemId;
            _amount = amount;
        }
        public override List<ActionDefinition> GetGenerateActionDefinitions()
        {
            List<ActionDefinition> actionDefinitions = new();
            var actionParamManager = new BaseDefinitionManager<BaseActionParamDefinition>();
            actionParamManager.AddDefinition(new ItemActionParamDefinition("ItemID", "ItemID", "_itemId"));
            actionParamManager.AddDefinition(new FloatActionParamDefinition("Amount", "Amount", "_amount"));
            ActionDefinition actionDefinition = new(ADD_ITEM_ACTION_DEFINITION, ADD_ITEM_ACTION_DEFINITION, actionParamManager);
            actionDefinitions.Add(actionDefinition);

            actionParamManager = new BaseDefinitionManager<BaseActionParamDefinition>();
            actionParamManager.AddDefinition(new ItemActionParamDefinition("ItemID", "ItemID", "_itemId"));
            actionParamManager.AddDefinition(new FloatActionParamDefinition("Amount", "Amount", "_amount"));
            actionDefinition = new(REMOVE_ITEM_ACTION_DEFINITION, REMOVE_ITEM_ACTION_DEFINITION, actionParamManager);
            actionDefinitions.Add(actionDefinition);

            return actionDefinitions;
        }

        public override bool Equals(BaseActionData other)
        {
            if (other == null)
                return false;

            if (GetType() != other.GetType())
                return false;

            if (other is not ItemActionData o)
                return false;

            return _itemId == o._itemId;
        }
    }
}