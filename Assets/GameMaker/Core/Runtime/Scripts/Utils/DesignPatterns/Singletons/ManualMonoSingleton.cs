using UnityEngine;

namespace GameMaker.Core.Runtime
{
    public abstract class ManualMonoSingleton<T> : MonoBehaviour where T :MonoBehaviour
    {
        private static T _instance;
        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = GameObject.FindAnyObjectByType<T>();
                    if (_instance == null) throw new System.Exception($"Not found {typeof(T)} in scene");
                }
                return _instance;
            }
        }
        private void OnDestroy()
        {
            _instance = null;
        }
        void OnApplicationQuit()
        {
            _instance = null;
        }
    }
}
