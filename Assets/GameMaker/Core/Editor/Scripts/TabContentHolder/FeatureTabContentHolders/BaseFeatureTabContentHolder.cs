using GameMaker.Core.Runtime;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace GameMaker.Core.Editor
{
    public abstract class BaseFeatureTabContentHolder : BaseTabContentHolder
    {
        protected BaseFeatureTabContentHolder(VisualElement root) : base(root)
        {
        }

        public abstract override int GetIndex();

        public abstract override VisualElement GetTabView();

        public abstract override string GetTitle();
    }
}