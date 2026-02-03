using System.Collections.Generic;

namespace GameMaker.Core.Runtime
{
    [System.Serializable]
    public class CurrencyActionParamDefinition : BaseDefinitionActionParamDefinition<BaseCurrencyDefinition>
    {
        public CurrencyActionParamDefinition():base(){}
        public CurrencyActionParamDefinition(string  id, string name, string bindingName) : base(id, name, bindingName)
        {
        }
        public override object Clone()
        {
            return new CurrencyActionParamDefinition(GetID(),GetName(), BindingName);
        }

        public override List<BaseCurrencyDefinition> GetDefinitions()
        {
            return CurrencyManager.Instance.GetDefinitions();
        }
    }
}