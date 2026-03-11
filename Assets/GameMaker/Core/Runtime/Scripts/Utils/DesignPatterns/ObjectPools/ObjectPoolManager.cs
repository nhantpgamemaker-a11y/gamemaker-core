using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Pool;

namespace GameMaker.Core.Runtime
{
    [System.Serializable]
    public class RegistrationPoolingObjectSetting
    {
        [HideInInspector]
        public string name = string.Empty;
        public GameObject prefab;
        public Transform parent;
        public int defaultCapacity = 5;
        public int maxSize = 100;
        public int preInit = 0;

        public RegistrationPoolingObjectSetting(IObjectPooling prefab, Transform parent)
        {
            this.name = prefab.GetName();
            this.prefab = prefab.GetObject();
            this.parent = parent;
        }
        public void ValidSetting()
        {
            var iObjectPool = this.prefab.GetComponent<IObjectPooling>();
            this.name = iObjectPool.GetName();
        }
    }
    public class ObjectPoolManager : MonoBehaviour
    {
        [SerializeField]
        private List<RegistrationPoolingObjectSetting> registrationPoolingObjectSettings = new();
        [SerializeField]
        private bool initOnAwake = true;
        private bool isInit = false;
        private Dictionary<string, ObjectPool<IObjectPooling>> objectPoolingDict = new();
        private void Awake()
        {
            if (initOnAwake)
            {
                Init();
            }
        }
        private ObjectPool<IObjectPooling> CreateFromSetting(RegistrationPoolingObjectSetting setting)
        {
            setting.ValidSetting();
            var prefab = setting.prefab;
            string name = setting.name;
            ObjectPool<IObjectPooling> objectPool = new ObjectPool<IObjectPooling>(
                            createFunc: () =>
                            {
                                var gameObject = Instantiate(prefab, setting.parent);
                                IObjectPooling objectPooling = gameObject.GetComponent<IObjectPooling>();
                                objectPooling.OnCreateHandler();
                                return objectPooling;
                            },
                            actionOnGet: (objectPooling) =>
                            {
                                objectPooling.GetObject().SetActive(true);
                                objectPooling.OnGetHandler();
                            },
                            actionOnRelease: (objectPooling) =>
                            {
                                objectPooling.GetObject().SetActive(false);
                                objectPooling.GetObject().transform.SetParent(setting.parent);
                                objectPooling.OnReleaseHandler();
                            },
                            actionOnDestroy: (objectPooling) =>
                            {
                                objectPooling.OnDestroyHandler();
                            },
                            defaultCapacity: setting.defaultCapacity,
                            maxSize: setting.maxSize
                        );
            return objectPool;
        }
        public void Init()
        {
            if (!isInit)
            {
                foreach (var setting in registrationPoolingObjectSettings)
                {
                    var prefab = setting.prefab;
                    string name = setting.name;
                    if (!objectPoolingDict.ContainsKey(name))
                    {
                        var pool = CreateFromSetting(setting);
                        objectPoolingDict.Add(name, pool);
                    }
                }
                var datas = new List<IObjectPooling>();
                foreach (var setting in registrationPoolingObjectSettings)
                {
                    if (setting.preInit > 0)
                    {
                        var data = Get<IObjectPooling>(setting.name);
                        datas.Add(data);
                    }
                }
                foreach (var data in datas)
                {
                    Release(data);
                }
                datas.Clear();
            }
        }
        public void ReleasePool()
        {
            foreach (var pool in objectPoolingDict)
            {
                pool.Value.Clear();
            }
            objectPoolingDict.Clear();
            this.isInit = false;
        }
        public T Get<T>(string name)
        {
            if (objectPoolingDict.TryGetValue(name, out var pool))
            {
                return (T)pool.Get();
            }
            return default(T);
        }
        public void Release(IObjectPooling objectPooling)
        {
            if (objectPoolingDict.TryGetValue(objectPooling.GetName(), out var pool))
            {
                pool.Release(objectPooling);
                var setting = registrationPoolingObjectSettings.FirstOrDefault(x => x.name == objectPooling.GetName());
                if (setting == null)
                    return;
                objectPooling.GetObject().transform.SetParent(setting.parent);
            }
        }
        public ObjectPool<IObjectPooling> Register(RegistrationPoolingObjectSetting setting)
        {
            if (!isInit) throw new Exception($"{this.GetType()} is not init");
            string name = setting.name;
            if (objectPoolingDict.TryGetValue(name, out var pool))
            {
                return pool;
            }
            pool = CreateFromSetting(setting);
            objectPoolingDict.Add(name, pool);
            return pool;
        }
        public void UnRegister<T>(string name) where T : IObjectPooling
        {
            if (!isInit) throw new Exception($"{this.GetType()} is not init");
            if (registrationPoolingObjectSettings.FirstOrDefault(x => x.name == name) == null)
            {
                if (objectPoolingDict.TryGetValue(name, out var pool))
                {
                    pool.Dispose();
                    objectPoolingDict.Remove(name);
                }
            }
        }
        void OnValidate()
        {
            ValidSetting();
        }

        [ContextMenu("Valid Setting")]
        public void ValidSetting()
        {
            foreach (var s in registrationPoolingObjectSettings)
            {
                s.ValidSetting();
            }
            #if UNITY_EDITOR
                UnityEditor.EditorUtility.SetDirty(this.gameObject);
            #endif
        }
    }
}
