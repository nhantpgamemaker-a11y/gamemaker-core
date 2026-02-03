using UnityEngine.UIElements;

namespace GameMaker.Core.Editor
{
    [CoreTabContext]
    public class BundleTabContentHolder : BaseTabContentHolder
    {
        public BundleTabContentHolder(VisualElement root) : base(root)
        {
        }

        public override int GetIndex()
        {
            return int.MaxValue - 10;
        }

        public override VisualElement GetTabView()
        {
            return new VisualElement();
        }

        public override string GetTitle()
        {
            return "BUNDLE";
        }
    }
}