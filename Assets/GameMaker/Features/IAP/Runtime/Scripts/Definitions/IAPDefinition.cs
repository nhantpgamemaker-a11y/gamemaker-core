
using System.Linq;
using GameMaker.Core.Runtime;
using UnityEngine;
using UnityEngine.Purchasing;

namespace GameMaker.IAP.Runtime
{
    [System.Serializable]
    public class IAPDefinition : BaseDefinition
    {
        [UnityEngine.SerializeField]
        private BundleDefinition _reward = new();
        [UnityEngine.SerializeField]
        private BundleDefinition _restoreReward = new();

        [UnityEngine.SerializeField]
        private ProductType _productType;
        [UnityEngine.SerializeField]
        private string _productId;
        [UnityEngine.SerializeField]
        private string _groupId;
        public BundleDefinition Reward => _reward;
        public BundleDefinition RestoreReward => _restoreReward;
        public ProductType ProductType => _productType;
        public string ProductID => _productId;
        public string GroupID => _groupId;
        public IAPDefinition() : base()
        {
        }
        public IAPDefinition(string id, string name, string title, string description, Sprite icon, BaseMetaData metaData
        , BundleDefinition reward,ProductType productType, string productId, string groupId) :
        base(id, name, title, description,icon, metaData)
        {
            _reward = reward;
            _productType = productType;
            _productId = productId;
            _groupId = groupId;
        }
        public override object Clone()
        {
            return new IAPDefinition(GetID(),
            GetName(),
            GetTitle(),
            GetDescription(),
            GetIcon(),
            GetMetaData(),
            _reward.Clone() as BundleDefinition,
            _productType,
            _productId,
            _groupId);
        }
    }
}