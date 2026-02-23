using System.Linq;

namespace GameMaker.Core.Runtime
{
    [System.Serializable]
    public class StringActionParamDefinition : BaseActionParamDefinition
    {
        public StringActionParamDefinition(string id,string name, string bindingName) : base(id, name, bindingName)
        {
        }

        public override object Clone()
        {
            return new StringActionParamDefinition(GetID(), GetName(), BindingName);
        }
    }
}