using System.Collections.Generic;

namespace GameMaker.Core.Runtime
{
    [System.Serializable]
    public class CurrencyActionData : BaseActionData
    {
        public const string ADD_CURRENCY_ACTION_DEFINITION = "ADD_CURRENCY_ACTION_DEFINITION";
        private string _currencyId;
        private float _value;
        public string CurrencyId { get => _currencyId; }
        public float Value { get => _value; }
        public CurrencyActionData(string currencyId, float value, IExtendData extendData) : base(extendData)
        {
            _currencyId = currencyId;
            _value = value;
        }
        public override List<ActionDefinition> GetGenerateActionDefinitions()
        {
            var actionParamManager = new BaseDefinitionManager<BaseActionParamDefinition>();
            actionParamManager.AddDefinition(new StringActionParamDefinition("CurrencyID", "CurrencyID", "_currencyId"));
            actionParamManager.AddDefinition(new FloatActionParamDefinition("Value", "Value", "_value"));
            ActionDefinition actionDefinition = new(ADD_CURRENCY_ACTION_DEFINITION, ADD_CURRENCY_ACTION_DEFINITION, actionParamManager);
            return new List<ActionDefinition>() { actionDefinition };
        }
    }
}