using GameMaker.Core.Runtime;
using UnityEngine;

namespace GameMaker.Core.Runtime
{
    [System.Serializable]
    [TypeCache]
    public abstract class PropertyDefinition :  BaseDefinition
    {
        public PropertyDefinition() : base()
        {
            
        }
        public PropertyDefinition(string id, string name, string title, string description, Sprite icon, BaseMetaData metaData) : base(id, name, title, description, icon, metaData)
        {

        }
        public abstract string GetStringValue();

#if UNITY_EDITOR
        public abstract string GetGenClassCode();
#endif
    }
}