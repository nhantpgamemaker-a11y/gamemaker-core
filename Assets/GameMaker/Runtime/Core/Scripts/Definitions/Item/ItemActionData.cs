using GameMaker.Core.Runtime;
using UnityEngine;

namespace GameMaker.Core.Runtime
{
    [System.Serializable]
    public class ItemActionData : BaseActionData
    {
        private string _itemId;
        public  ItemActionData(string itemId, object data = null): base(data)
        {
            _itemId = itemId;
        }

        public override IDefinition GetDefinition()
        {
            return ItemManager.Instance.GetDefinition(_itemId);
        }
    }
}