using GameMaker.Core.Editor;
using UnityEngine.UIElements;

namespace GameMaker.Feature.Shop.Editor
{
    [FeatureTabContent]
    public class ShopTabContentHolder : BaseFeatureTabContentHolder
    {
        public ShopTabContentHolder(VisualElement root) : base(root)
        {
        }

        public override int GetIndex()
        {
            return 0;
        }

        public override VisualElement GetTabView()
        {
            return new TemplateContainer();
        }

        public override string GetTitle()
        {
            return "SHOP";
        }
    }
}