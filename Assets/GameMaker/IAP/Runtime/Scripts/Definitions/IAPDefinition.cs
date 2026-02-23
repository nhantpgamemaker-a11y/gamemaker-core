using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using GameMaker.Core.Runtime;
using Unity.Android.Gradle.Manifest;
using UnityEngine;
using UnityEngine.Purchasing;

namespace GameMaker.IAP.Runtime
{
    [System.Serializable]
    public class IAPDefinition : BaseDefinition
    {
        [UnityEngine.SerializeField]
        private List<string> _storeIds;

        [UnityEngine.SerializeField]
        private BundleDefinition _reward = new();
        [UnityEngine.SerializeField]
        private ProductType _productType;
        [UnityEngine.SerializeField]
        private string _productId;
        public List<string> StoreIDs => _storeIds;
        public BundleDefinition Reward => _reward;
        public ProductType ProductType => _productType;
        public string ProductID => _productId;
        public IAPDefinition() : base()
        {
            _storeIds = new List<string>();
        }
        public IAPDefinition(string id, string name, string title, string description, Sprite icon, BaseMetaData metaData,
        List<string> storeIds, BundleDefinition reward,ProductType productType, string productId) :
        base(id, name, title, description,icon, metaData)
        {
            _storeIds = storeIds;
            _reward = reward;
            _productType = productType;
            _productId = productId;
        }
        public override object Clone()
        {
            return new IAPDefinition(GetID(),
            GetName(),
            GetTitle(),
            GetDescription(),
            GetIcon(),
            GetMetaData(),
            _storeIds.ToList(),
            _reward.Clone() as BundleDefinition,
            _productType,
            _productId);
        }
    }
}