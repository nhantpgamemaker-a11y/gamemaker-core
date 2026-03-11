using System.Collections.Generic;
using System.Linq;
using GameMaker.Core.Editor;
using GameMaker.Core.Runtime;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace GameMaker.Core.Editor
{
    [InitializeOnLoad]
    public static class TypeCacheReloadHook
    {
        static TypeCacheReloadHook()
        {
            EditorApplication.delayCall += OnAfterAssemblyReload;
        }
        private static void OnAfterAssemblyReload()
        {
            GameMaker.Core.Runtime.TypeCache.Instance.RebuildCache();
        }
    }
}