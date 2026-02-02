using GameMaker.Core.Runtime;
using UnityEngine;

namespace GameMaker.Core.Runtime
{
    [System.Serializable]
    public class ItemDetailActionData : BaseActionData
    {
        private string _itemDetailId;
        public string ItemDetailID => _itemDetailId;
        public  ItemDetailActionData(string itemDetailId, IExtendData extendData) : base(extendData)
        {
            _itemDetailId = itemDetailId;
        }
    }
}