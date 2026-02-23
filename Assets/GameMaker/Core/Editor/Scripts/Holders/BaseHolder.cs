using UnityEngine;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace GameMaker.Core.Editor
{
    public abstract class BaseHolder
    {
        public VisualElement Root { get; }
        public BaseHolder()
        {
            
        }
        public BaseHolder(VisualElement root)
        {
            Root = root;
        }
        public virtual void Bind(SerializedProperty elementProperty)
        {
            Root.Bind(elementProperty.serializedObject);
        }
    }
}
