using UnityEngine.UIElements;

namespace GameMaker.Core.Editor
{
    public abstract class BaseTabContentHolder : BaseHolder
    {
        public abstract int GetIndex();
        public abstract string GetTitle();
        public abstract VisualElement GetTabView();
        public BaseTabContentHolder(VisualElement root) : base(root)
        {

        }
    }
}
