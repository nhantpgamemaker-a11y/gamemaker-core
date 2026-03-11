using System.Threading;
using GameMaker.Core.Runtime;
using UnityEngine;

namespace GameMaker.Core.Runtime
{
    [System.Serializable]
    public class LongCurrencyDefinition : BaseCurrencyDefinition
    {
        [UnityEngine.SerializeField]
        private long _defaultValue = 0;
        [UnityEngine.SerializeField]
        private long _maxValue = long.MaxValue;
        public LongCurrencyDefinition() : base()
        {
        }

        public LongCurrencyDefinition(string id, string name, string title, string description, Sprite icon, BaseMetaData metaData
        , long defaultValue, long maxValue)
         : base(id, name, title, description, icon, metaData)
        {
            _defaultValue = defaultValue;
            _maxValue = maxValue;
        }

        public override object Clone()
        {
            return new LongCurrencyDefinition(GetID(), GetName(), GetTitle(), GetDescription(), GetIcon(), GetMetaData(), _defaultValue, _maxValue);
        }

        public override object GetDefaultValue()
        {
            return _defaultValue;
        }
    }
}