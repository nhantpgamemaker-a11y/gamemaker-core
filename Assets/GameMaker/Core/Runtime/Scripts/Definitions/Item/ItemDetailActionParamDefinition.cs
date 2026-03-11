using System.Collections.Generic;

namespace GameMaker.Core.Runtime
{
    [System.Serializable]
    public class ItemDetailActionParamDefinition : BaseDefinitionActionParamDefinition<ItemDetailDefinition>
    {
        public ItemDetailActionParamDefinition():base(){}
        public ItemDetailActionParamDefinition(string  id, string name, string bindingName) : base(id, name, bindingName)
        {
        }
        public override object Clone()
        {
            return new CurrencyActionParamDefinition(GetID(),GetName(), BindingName);
        }

        public override List<ItemDetailDefinition> GetDefinitions()
        {
            return ItemDetailManager.Instance.GetDefinitions();
        }
    }
}