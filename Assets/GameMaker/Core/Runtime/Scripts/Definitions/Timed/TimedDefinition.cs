using UnityEngine;

namespace GameMaker.Core.Runtime
{
    [System.Serializable]
    public class TimedDefinition : BaseDefinition
    {
        [UnityEngine.SerializeField]
        private long _defaultValue;
        public long DefaultValue => _defaultValue;
        public TimedDefinition() : base()
        {

        }
        public TimedDefinition(string id, string name, string title, string description, Sprite icon, BaseMetaData metaData, long defaultValue): 
        base(id, name, title, description, icon, metaData)
        {
            _defaultValue = defaultValue;
        }
        public override object Clone()
        {
            return new TimedDefinition(GetID(), GetName(), GetTitle(), GetDescription(), GetIcon(), GetMetaData(), _defaultValue);
        }
    }
}
