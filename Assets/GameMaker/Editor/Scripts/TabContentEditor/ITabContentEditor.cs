using System;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UIElements;

namespace GameMaker.Core.Editor
{
    public interface ITabContentEditor
    {
        public bool IsSelected { get; set; }
        public abstract string GetTitle();
        public abstract string GetTooltip();
        public abstract void OnGUIDrawer();
        public virtual void OnSelected()
        {
            IsSelected = true;
        }
        public virtual void OnDeselected()
        {
            IsSelected = false;
        }
    }
}
