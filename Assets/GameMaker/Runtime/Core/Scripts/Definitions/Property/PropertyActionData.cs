using GameMaker.Core.Runtime;
using UnityEngine;

namespace GameMaker.Core.Runtime
{
    [System.Serializable]
    public class PropertyActionData : BaseActionData
    {
        private string _propertyId;
        public string PropertyId { get => _propertyId; }
        public PropertyActionData(string propertyId, IExtendData extendData) : base(extendData)
        {
            _propertyId = propertyId;
        }
    }
}