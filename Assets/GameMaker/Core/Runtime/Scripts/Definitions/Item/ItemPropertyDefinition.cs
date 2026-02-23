
namespace GameMaker.Core.Runtime
{
    [System.Serializable]
    public abstract class ItemPropertyDefinition : Definition
    {
        public ItemPropertyDefinition():base()
        {
            
        }
        public ItemPropertyDefinition(string id, string name) : base(id, name)
        {

        }
        public abstract ItemPropertyDefinitionRef GetPropertyDefinitionRef();
    }
}