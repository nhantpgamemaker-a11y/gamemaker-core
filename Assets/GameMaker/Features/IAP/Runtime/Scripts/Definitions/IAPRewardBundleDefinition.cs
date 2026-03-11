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
    public class IAPRewardBundleDefinition : BaseDefinition
    {
        [UnityEngine.SerializeField]
        private BaseDefinitionManager<BaseIAPRewardDefinition> _rewardManager;
        public List<BaseIAPRewardDefinition> Rewards => _rewardManager.GetDefinitions();
        public IAPRewardBundleDefinition() : base()
        {
            _rewardManager = new();
        }
        public IAPRewardBundleDefinition(string id,
            string name,
            string title,
            string description,
            Sprite icon,
            BaseMetaData metaData,
            BaseDefinitionManager<BaseIAPRewardDefinition> rewardManager): 
            base(id, name, title, description, icon,metaData)
        {
            _rewardManager = rewardManager;
        }
        public override object Clone()
        {
            return new IAPRewardBundleDefinition(GetID(),
                                        GetName(),
                                        GetTitle(),
                                        GetDescription(),
                                        GetIcon(),
                                        GetMetaData(),
                                        _rewardManager.Clone() as BaseDefinitionManager<BaseIAPRewardDefinition>);
        }
        public BundleDefinition ToBundleDefinition()
        {
            return new BundleDefinition(GetID(), GetName(), GetTitle(), GetDescription(), GetIcon(), GetMetaData(),
            new BaseDefinitionManager<BaseRewardDefinition>(Rewards.Select(x => x.Reward).ToList()));
        }
    }
    [System.Serializable]
    public class BaseIAPRewardDefinition : BaseDefinition
    {
        [UnityEngine.SerializeReference]
        private BaseRewardDefinition _reward;
        public BaseRewardDefinition Reward => _reward;
        public BaseIAPRewardDefinition()
        {

        }
        public BaseIAPRewardDefinition(string id,
        string name,
        string title,
        string description,
        Sprite icon,
        BaseMetaData metaData,
        BaseRewardDefinition reward) :
        base(id, name, title, description, icon, metaData)
        {
            _reward = reward;
        }
        public override object Clone()
        {
            return new BaseIAPRewardDefinition(GetID(), GetName(), GetTitle(), GetDescription(), GetIcon(), GetMetaData(),_reward.Clone() as BaseRewardDefinition);
        }
    }
}