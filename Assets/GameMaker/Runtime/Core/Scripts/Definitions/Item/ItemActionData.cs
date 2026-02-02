using GameMaker.Core.Runtime;
using UnityEngine;

namespace GameMaker.Core.Runtime
{
    [System.Serializable]
    public class ItemActionData : BaseActionData
    {
        private string _itemId;
        public string ItemID => _itemId;
        public  ItemActionData(string itemId, IExtendData extendData) : base(extendData)
        {
            _itemId = itemId;
        }
    }
}