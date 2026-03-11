using UnityEngine;

namespace GameMaker.Core.Runtime
{
    public abstract class AutomaticMonoSingleton<T> : MonoBehaviour where T : AutomaticMonoSingleton<T>
    {
        private static T _instance;
        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = GameObject.FindAnyObjectByType<T>();
                    if (_instance == null)
                    {
                        GameObject go = new GameObject(typeof(T).FullName);
                        _instance = go.AddComponent<T>();
                    }
                    _instance.OnLoad();
                }
                return _instance;
            }
        }
        public virtual void OnLoad()
        {
            
        }
        void Awake()
        {
            DontDestroyOnLoad(this.gameObject);
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
