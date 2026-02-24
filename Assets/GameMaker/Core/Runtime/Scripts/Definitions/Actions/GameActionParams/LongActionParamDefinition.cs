namespace GameMaker.Core.Runtime
{
    [System.Serializable]
    public class LongActionParamDefinition : BaseActionParamDefinition
    {
        public LongActionParamDefinition()
        {
            
        }
        public LongActionParamDefinition(string id ,string name, string bindingName) : base(id, name, bindingName)
        {
        }

        public override object Clone()
        {
            return new LongActionParamDefinition(GetID(), GetName() ,BindingName);
        }
    }
}