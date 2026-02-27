using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace GameMaker.Core.Editor
{
    public static class UIToolkitLoaderUtils
    {
        public static VisualTreeAsset LoadUXML(string assetName)
        {
            var asset = Resources.Load<VisualTreeAsset>(assetName);
            
            #if UNITY_EDITOR
            if (asset == null)
            {
                asset =  FindVisualTreeAssetByName(assetName);
            }
            #endif
            
            return asset;
        }

        public static StyleSheet LoadUSS(string assetName)
        {
            var asset = Resources.Load<StyleSheet>(assetName);

#if UNITY_EDITOR
            if (asset == null)
            {
                asset = FindStyleSheetByName(assetName);
            }
#endif

            return asset;
        }
        public static VisualTreeAsset FindVisualTreeAssetByName(string assetName)
        {
            string[] guids = AssetDatabase.FindAssets("t:VisualTreeAsset");
            foreach (string guid in guids)
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);
                VisualTreeAsset asset = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(path);
                if (asset.name == assetName)
                    return asset;
            }
            return null;
        }
        public static StyleSheet FindStyleSheetByName(string assetName)
        {
            string[] guids = AssetDatabase.FindAssets("t:StyleSheet");
            foreach (string guid in guids)
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);
                StyleSheet asset = AssetDatabase.LoadAssetAtPath<StyleSheet>(path);
                if (asset.name == assetName)
                    return asset;
            }
            return null;
        }
    }   

}