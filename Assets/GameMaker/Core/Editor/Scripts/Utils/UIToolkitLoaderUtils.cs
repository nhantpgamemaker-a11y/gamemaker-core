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
                string[] guids = AssetDatabase.FindAssets($"{assetName} t:VisualTreeAsset");
                if (guids.Length > 0)
                {
                    string path = AssetDatabase.GUIDToAssetPath(guids[0]);
                    asset = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(path);
                }
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
                string[] guids = AssetDatabase.FindAssets($"{assetName} t:StyleSheet");
                if (guids.Length > 0)
                {
                    string path = AssetDatabase.GUIDToAssetPath(guids[0]);
                    asset = AssetDatabase.LoadAssetAtPath<StyleSheet>(path);
                }
            }
            #endif
            
            return asset;
        }
    }   

}