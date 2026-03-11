using System.Numerics;
using System.Threading;
using GameMaker.Core.Runtime;
using UnityEngine;

namespace GameMaker.Core.Runtime
{
    [System.Serializable]
    public class BigIntCurrencyDefinition : BaseCurrencyDefinition
    {
        [UnityEngine.SerializeField]
        private string _defaultValue = "0";
        [UnityEngine.SerializeField]
        private string _maxValue = "0";
        public BigIntCurrencyDefinition() : base()
        {
        }

        public BigIntCurrencyDefinition(string id, string name, string title, string description, Sprite icon, BaseMetaData metaData
        , string defaultValue, string maxValue)
         : base(id, name, title, description, icon, metaData)
        {
            _defaultValue = defaultValue;
            _maxValue = maxValue;
        }

        public override object Clone()
        {
            return new BigIntCurrencyDefinition(GetID(), GetName(), GetTitle(), GetDescription(), GetIcon(), GetMetaData(), _defaultValue, _maxValue);
        }

        public override object GetDefaultValue()
        {
            return BigInteger.Parse(_defaultValue);
        }
    }
}