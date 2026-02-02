using GameMaker.Core.Runtime;
using UnityEngine;

namespace GameMaker.Core.Runtime
{
    [System.Serializable]
    public class StatActionData : PropertyActionData
    {
        private float _value;
        public float Value { get => _value; }
        public StatActionData(string propertyId,float value, IExtendData extendData) : base(propertyId,extendData)
        {
            _value = value;
        }
    }
}