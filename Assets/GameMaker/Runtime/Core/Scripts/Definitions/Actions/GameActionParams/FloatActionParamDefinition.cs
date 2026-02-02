namespace GameMaker.Core.Runtime
{
    [System.Serializable]
    public class FloatActionParamDefinition : BaseActionParamDefinition
    {
        public FloatActionParamDefinition()
        {
            
        }
        public FloatActionParamDefinition(string id ,string name, string bindingName) : base(id, name, bindingName)
        {
        }

        public override object Clone()
        {
            return new FloatActionParamDefinition(GetID(), GetName() ,BindingName);
        }
    }
}