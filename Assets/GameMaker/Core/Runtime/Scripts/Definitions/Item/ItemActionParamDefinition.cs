using System.Collections.Generic;

namespace GameMaker.Core.Runtime
{
    [System.Serializable]
    public class ItemActionParamDefinition : BaseDefinitionActionParamDefinition<ItemDefinition>
    {
        public ItemActionParamDefinition():base(){}
        public ItemActionParamDefinition(string  id, string name, string bindingName) : base(id, name, bindingName)
        {
        }
        public override object Clone()
        {
            return new CurrencyActionParamDefinition(GetID(),GetName(), BindingName);
        }

        public override List<ItemDefinition> GetDefinitions()
        {
            return ItemManager.Instance.GetDefinitions();
        }
    }
}