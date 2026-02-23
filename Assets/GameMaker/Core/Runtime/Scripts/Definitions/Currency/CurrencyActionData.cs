using System.Collections.Generic;
using UnityEngine;

namespace GameMaker.Core.Runtime
{
    [System.Serializable]
    public class CurrencyActionData : BaseActionData
    {
        public const string ADD_CURRENCY_ACTION_DEFINITION = "ADD_CURRENCY_ACTION_DEFINITION";
        private string _currencyId;
        private string _value;
        public string CurrencyId { get => _currencyId; }
        public string Value { get => _value; }
        public CurrencyActionData():base(){}
        public CurrencyActionData(string currencyId, string value, IExtendData extendData) : base(extendData)
        {
            _currencyId = currencyId;
            _value = value;
        }
        public override List<ActionDefinition> GetGenerateActionDefinitions()
        {
            var actionParamManager = new BaseDefinitionManager<BaseActionParamDefinition>();
            actionParamManager.AddDefinition(new CurrencyActionParamDefinition("CurrencyID", "CurrencyID", "_currencyId"));
            actionParamManager.AddDefinition(new FloatActionParamDefinition("Value", "Value", "_value"));
            ActionDefinition actionDefinition = new(ADD_CURRENCY_ACTION_DEFINITION, ADD_CURRENCY_ACTION_DEFINITION, actionParamManager);
            return new List<ActionDefinition>() { actionDefinition };
        }

        public override bool Equals(BaseActionData other)
        {
            if (other is not CurrencyActionData o)
                return false;

            return _currencyId == o._currencyId
                && _value == o._value;
        }
    }
}