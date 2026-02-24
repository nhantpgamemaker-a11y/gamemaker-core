using GameMaker.Core.Editor;
using UnityEngine;
using UnityEngine.UIElements;

namespace GameMaker.Feature.DailyReward.Editor
{
    [FeatureTabContent]
    public class DailyRewardTabContentHolder : BaseFeatureTabContentHolder
    {
        public DailyRewardTabContentHolder(VisualElement root) : base(root)
        {
        }

        public override int GetIndex()
        {
            return 2;
        }

        public override VisualElement GetTabView()
        {
            return new VisualElement();
        }

        public override string GetTitle()
        {
            return "DAILY REWARD";
        }
    }
}
