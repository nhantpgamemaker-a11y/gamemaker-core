using UnityEngine;
using System.Linq;

namespace GameMaker.Core.Runtime
{
    public abstract class ScriptableObjectSingleton<T>: ScriptableObject where T : ScriptableObject
    {
        private static T _instance;
        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
#if UNITY_EDITOR
                    string[] guids = UnityEditor.AssetDatabase.FindAssets($"t:{typeof(T).Name}");
                    if (guids.Length > 0)
                    {
                        string path = UnityEditor.AssetDatabase.GUIDToAssetPath(guids[0]);
                        _instance = UnityEditor.AssetDatabase.LoadAssetAtPath<T>(path);
                    }
                    else
                    {
                        _instance = ScriptableObject.CreateInstance<T>();
                        var attribute = typeof(T).GetCustomAttributes(typeof(ScriptableObjectSingletonPathAttribute), true)
                                    .FirstOrDefault() as ScriptableObjectSingletonPathAttribute;
                        string resourcesPath = $"{attribute.Path}";
                        if (!UnityEditor.AssetDatabase.IsValidFolder(resourcesPath))
                        {
                            UnityEditor.AssetDatabase.CreateFolder("Assets", "Resources");
                        }
                        string assetPath = $"{resourcesPath}/{typeof(T).Name}.asset";
                        UnityEditor.AssetDatabase.CreateAsset(_instance, assetPath);
                        UnityEditor.AssetDatabase.SaveAssets();
                        UnityEditor.AssetDatabase.Refresh();
                        Debug.Log($"✅ Created new {typeof(T).Name} at {assetPath}");
                    }
#else
                    _instance = Resources.Load<T>(typeof(T).Name);
                    _instance.OnLoad();
#endif
                }
                return _instance;
            }
        }

        protected virtual void OnLoad()
        {
            
        }
    }
}
