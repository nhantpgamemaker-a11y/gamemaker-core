using UnityEngine;
using System.Linq;

namespace GameMaker.Core.Runtime
{
    public abstract class ScriptableObjectSingleton<T>: ScriptableObject where T : ScriptableObjectSingleton<T>
    {
        private static T _instance;
        private bool _loaded;
        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
#if UNITY_EDITOR
                    string[] guids =
                        UnityEditor.AssetDatabase.FindAssets($"t:{typeof(T).FullName}");

                    if (guids.Length > 1)
                    {
                        Debug.LogError(
                            $"Multiple {typeof(T).Name} assets found!");
                    }

                    if (guids.Length > 0)
                    {
                        string path =
                            UnityEditor.AssetDatabase.GUIDToAssetPath(guids[0]);
                        _instance =
                            UnityEditor.AssetDatabase.LoadAssetAtPath<T>(path);
                    }
                    else
                    {
                        _instance = CreateAndSaveAsset();
                    }
#else
                    _instance = Resources.Load<T>(typeof(T).Name);
#endif
                }

            if (_instance != null && !_instance._loaded)
            {
                _instance._loaded = true;
                _instance.OnLoad();
            }

            return _instance;
        }
    }

        
#if UNITY_EDITOR
    private static T CreateAndSaveAsset()
    {
        var instance = CreateInstance<T>();

        var attr = typeof(T)
            .GetCustomAttributes(typeof(ScriptableObjectSingletonPathAttribute), true)
            .FirstOrDefault() as ScriptableObjectSingletonPathAttribute;

        string path = attr?.Path ?? "Assets/Resources";
        if (!UnityEditor.AssetDatabase.IsValidFolder(path))
        {
            string parent = System.IO.Path.GetDirectoryName(path);
            string folder = System.IO.Path.GetFileName(path);
            UnityEditor.AssetDatabase.CreateFolder(parent, folder);
        }

        string assetPath = $"{path}/{typeof(T).Name}.asset";
        UnityEditor.AssetDatabase.CreateAsset(instance, assetPath);
        UnityEditor.AssetDatabase.SaveAssets();
        UnityEditor.AssetDatabase.Refresh();

        Logger.Log($"✅ Created {typeof(T).Name} at {assetPath}");
        return instance;
    }
#endif
        protected virtual void OnLoad()
        {

        }
        
    }
}
