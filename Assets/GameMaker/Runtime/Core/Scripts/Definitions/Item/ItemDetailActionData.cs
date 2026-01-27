using GameMaker.Core.Runtime;
using UnityEngine;

namespace GameMaker.Core.Runtime
{
    [System.Serializable]
    public class ItemDetailActionData : BaseActionData
    {
        private string _itemDetailId;
        public  ItemDetailActionData(string itemId, object data = null): base(data)
        {
            _itemDetailId = itemId;
        }

        public override IDefinition GetDefinition()
        {
            return ItemDetailManager.Instance.GetDefinition(_itemDetailId);
        }
    }
}