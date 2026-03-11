namespace GameMaker.Core.Runtime
{
    [System.Serializable]
    public class ObjectActionParamDefinition : BaseActionParamDefinition
    {
        public ObjectActionParamDefinition()
        {
            
        }
        public ObjectActionParamDefinition(string id ,string name, string bindingName) : base(id, name, bindingName)
        {
        }

        public override object Clone()
        {
            return new ObjectActionParamDefinition(GetID(), GetName() ,BindingName);
        }
    }
}