using System.Collections.Generic;
using UnityEngine;

namespace GameMaker.Core.Runtime
{
    [System.Serializable]
    public abstract class BaseActionDefinition : BaseDefinition
    {
        public BaseActionDefinition(string id, string name, string title, string description, Sprite icon, BaseMetaData metaData):base(id, name, title,description, icon, metaData)
        {
            
        }
        public abstract List<IDefinition> GetDefinitions();
        public abstract List<BaseActionDefinition> GetCoreActionDefinition();
    }
}