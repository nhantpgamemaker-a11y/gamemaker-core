using System.Collections.Generic;
using GameMaker.Core.Editor;
using GameMaker.Core.Runtime;
using Unity.Collections;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace GameMaker.Core.Editor
{
    public class VisualElementHolder : BaseHolder
    {
        public VisualElementHolder(VisualElement root) : base(root)
        {
        }
    }
}