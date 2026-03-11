
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GameMaker.Core.Runtime
{
    [System.Serializable]
    public class BundleDefinition : BaseDefinition
    {
        [UnityEngine.SerializeField]
        private BaseDefinitionManager<BaseRewardDefinition> _rewardManager;

        public List<BaseRewardDefinition> Rewards => _rewardManager.GetDefinitions();
        public BundleDefinition() : base()
        {
            _rewardManager = new();
        }
        public BundleDefinition(string id,
            string name,
            string title,
            string description,
            Sprite icon,
            BaseMetaData metaData,
            BaseDefinitionManager<BaseRewardDefinition> rewardManager): 
            base(id, name, title, description, icon,metaData)
        {
            _rewardManager = rewardManager;
        }
        public override object Clone()
        {
            return new BundleDefinition(GetID(),
                                         GetName(),
                                         GetTitle(),
                                         GetDescription(),
                                         GetIcon(),
                                         GetMetaData(),
                                         _rewardManager.Clone() as BaseDefinitionManager<BaseRewardDefinition>);
        }
    }
}