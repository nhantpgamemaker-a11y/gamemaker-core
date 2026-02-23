using System.Collections.Generic;

namespace GameMaker.Core.Runtime
{
    public interface IDefinitionManager
    {
        public List<IDefinition> GetDefinitions();
    }
}