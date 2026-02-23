using GameMaker.Core.Runtime;
using UnityEngine;

namespace GameMaker.Feature.Shop.Runtime
{
    [System.Serializable]
    public class ItemShopItemDefinition : BaseShopItemDefinition
    {
        [UnityEngine.SerializeField]
        private BaseCreateItemTemplate _createItemTemplate;
        public BaseCreateItemTemplate CreateItemTemplate => _createItemTemplate;
        public ItemShopItemDefinition() : base()
        {

        }
        public ItemShopItemDefinition(string id, string name, string title, string description,Sprite icon, BaseMetaData metaData, string referenceId, Price price,float amount,BaseCreateItemTemplate createItemTemplate)
        :base(id, name, title, description, icon, metaData, referenceId, price, amount)
        {
             _createItemTemplate = createItemTemplate;
        }
        public override object Clone()
        {
            return new ItemShopItemDefinition(GetID(), GetName(), GetTitle(), GetReferenceID(), GetIcon(), GetMetaData(), GetReferenceID(), Price, Amount,CreateItemTemplate);
        }

        public override IDefinition GetDefinition()
        {
            return ItemDetailManager.Instance.GetDefinition(GetReferenceID());
        }
    }
}