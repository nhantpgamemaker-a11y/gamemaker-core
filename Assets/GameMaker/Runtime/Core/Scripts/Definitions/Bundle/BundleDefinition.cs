
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GameMaker.Core.Runtime
{
    [System.Serializable]
    public class BundleDefinition : BaseDefinition
    {
        [UnityEngine.SerializeReference]
        private List<BaseRewardDefinition> _rewards = new();

        public List<BaseRewardDefinition> Rewards => _rewards;
        public BundleDefinition(string id, string name, string title, string description, Sprite icon,BaseMetaData metaData,List<BaseRewardDefinition> rewards): base(id, name, title, description, icon,metaData)
        {
            _rewards = rewards;
        }
        public override object Clone()
        {
            return new BundleDefinition(GetID(), GetName(), GetTitle(), GetDescription(), GetIcon(),GetMetaData(), _rewards.Select(x => x.Clone() as BaseRewardDefinition).ToList());
        }
    }
}