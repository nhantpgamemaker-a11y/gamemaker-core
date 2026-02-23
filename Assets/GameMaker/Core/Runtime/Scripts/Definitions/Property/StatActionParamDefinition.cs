using System.Collections.Generic;
using System.Linq;

namespace GameMaker.Core.Runtime
{
    [System.Serializable]
    public class StatActionParamDefinition : BaseDefinitionActionParamDefinition<StatDefinition>
    {
        public StatActionParamDefinition():base(){}
        public StatActionParamDefinition(string  id, string name, string bindingName) : base(id, name, bindingName)
        {
        }
        public override object Clone()
        {
            return new StatActionParamDefinition(GetID(),GetName(), BindingName);
        }

        public override List<StatDefinition> GetDefinitions()
        {
            return PropertyManager.Instance.GetDefinitions().Where(x=>x.GetType()  == typeof(StatDefinition)).Cast<StatDefinition>().ToList();
        }
    }
}