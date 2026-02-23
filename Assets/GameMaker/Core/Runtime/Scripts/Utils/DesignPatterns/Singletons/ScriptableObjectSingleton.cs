using UnityEngine;
using System.Linq;
using System.Reflection;
using System;

namespace GameMaker.Core.Runtime
{
    public abstract class ScriptableObjectSingleton<T>: ScriptableObject where T : ScriptableObjectSingleton<T>
    {
        private static T _instance;
        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    var bestType = typeof(T);
#if UNITY_EDITOR
                    Logger.Log($"[Resolver] Selected type: {bestType.FullName}");
#endif
                    _instance = Resources.Load<T>(bestType.Name);

                    if (_instance == null)
                    {
#if UNITY_EDITOR
                        Logger.LogWarning(
                            $"[Resolver] Resource not found for {bestType.FullName}. Creating one in Editor...");

                        _instance = CreateAndSaveAsset(bestType);

                        if (_instance == null)
                        {
                            Logger.LogError(
                                $"[Resolver] Failed to create ScriptableObject for {bestType.FullName}");
                            return null;
                        }
#else
                        Logger.LogError(
                            $"[Resolver] Cannot load resource for type {bestType.FullName}");
                        return null;
#endif
                    }
                    _instance.OnLoad();
                }

                return _instance;
            }
        }

#if UNITY_EDITOR
        private static T CreateAndSaveAsset(Type type)
        {
            // 1. Resolve path từ attribute
            var attr = typeof(T)
                .GetCustomAttribute<ScriptableObjectSingletonPathAttribute>();

            string rootPath = attr?.Path ?? "Assets/Resources";

            if (!rootPath.Contains("/Resources"))
            {
                Logger.LogError(
                    $"[ScriptableObjectSingleton] Path must be under a Resources folder: {rootPath}");
                return null;
            }

            EnsureFolderExists(rootPath);
            string assetPath = $"{rootPath}/{typeof(T).Name}.asset";

            var existing = UnityEditor.AssetDatabase.LoadAssetAtPath<T>(assetPath);
            if (existing != null)
            {
                return existing;
            }

            var instance = (T)CreateInstance(type);
            UnityEditor.AssetDatabase.CreateAsset(instance, assetPath);
            UnityEditor.AssetDatabase.SaveAssets();
            UnityEditor.AssetDatabase.Refresh();

            Logger.Log($"✅ Created ScriptableObject singleton: {assetPath}");
            return instance;
        }

        private static void EnsureFolderExists(string fullPath)
        {
            var parts = fullPath.Split('/');
            string current = parts[0];

            for (int i = 1; i < parts.Length; i++)
            {
                string next = $"{current}/{parts[i]}";
                if (!UnityEditor.AssetDatabase.IsValidFolder(next))
                {
                    UnityEditor.AssetDatabase.CreateFolder(current, parts[i]);
                }
                current = next;
            }
        }
#endif

        protected virtual void OnLoad()
        {

        }
        
    }
}
