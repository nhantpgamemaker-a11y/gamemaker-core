using System.Collections.Generic;

namespace GameMaker.Core.Runtime
{
    [System.Serializable]
    public class TimedActionParamDefinition : BaseDefinitionActionParamDefinition<TimedDefinition>
    {
        public TimedActionParamDefinition():base(){}
        public TimedActionParamDefinition(string  id, string name, string bindingName) : base(id, name, bindingName)
        {
        }
        public override object Clone()
        {
            return new TimedActionParamDefinition(GetID(), GetName(), BindingName);
        }

        public override List<TimedDefinition> GetDefinitions()
        {
            return TimedManager.Instance.GetDefinitions();
        }
    }
}