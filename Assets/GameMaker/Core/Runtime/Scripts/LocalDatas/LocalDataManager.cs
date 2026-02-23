using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;
using Cysharp.Threading.Tasks;

namespace GameMaker.Core.Runtime
{
    public class LocalDataManager 
    {
        private bool _isInit = false;
        private List<BaseLocalData> _localDataList = new();
        private Dictionary<Type, BaseLocalData> _localDataDict = new();

        public bool IsInit => _isInit;

        public T Get<T>() where T : BaseLocalData
        {
            var type = typeof(T);
            return Get(type) as T;
        }
        
        public BaseLocalData Get(Type type)
        {
            if (!_isInit) throw new Exception("LocalData is not really initialize"); 
            if (!_localDataDict.TryGetValue(type, out BaseLocalData baseLocalData))
            {
                baseLocalData = _localDataList.FirstOrDefault(x => x.GetType() == type);
                _localDataDict[type] = baseLocalData;
            }

            return baseLocalData;
        }

        public async UniTask InitAsync()
        {
            await LoadInternalAsync();
            _isInit = true;
            GameMaker.Core.Runtime.Logger.Log("Load LocalData successfully");
        }

        public async UniTask SaveAsync<T>() where T : BaseLocalData
        {
            var localData = Get<T>();
            await SaveInternalAsync(localData);
        }
        
        public async UniTask SaveAsync(Type type)
        {
            var localData = Get(type);
            await SaveInternalAsync(localData);
        }

        public async UniTask SaveAll()
        {
            List<UniTask> saveTasks = new();
            foreach (var data in _localDataList)
            {
                var saveTask = SaveAsync(data.GetType());
                saveTasks.Add(saveTask);
            }
            await UniTask.WhenAll(saveTasks);
        }
        
        private async UniTask SaveInternalAsync(BaseLocalData baseLocalData)
        {
            var path = Application.persistentDataPath;
            path += $"/{baseLocalData.GetType().Name}.json";
            var jsonSerializerSettings = new JsonSerializerSettings()
            {
                Formatting = Formatting.Indented,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                TypeNameHandling = TypeNameHandling.Auto
            };
            string dataString = JsonConvert.SerializeObject(baseLocalData, jsonSerializerSettings);
            await UniTask.RunOnThreadPool(async () => await File.WriteAllTextAsync(path, dataString));
            await UniTask.SwitchToMainThread();
        }

        private async UniTask LoadInternalAsync()
        {
            var path = Application.persistentDataPath;
            string[] files = Directory.GetFiles(path, "*.json");
            var allType = TypeUtils.GetAllDerivedNonAbstractTypes(typeof(BaseLocalData));
            var loadTaskList = new List<UniTask<BaseLocalData>>();
            foreach (var type in allType)
            {
                var dataPath = $"{path}/{type.Name}.json";
                if (File.Exists(dataPath))
                {
                    var loadTask = LoadDataFromLocalAsync(dataPath, type);
                    loadTaskList.Add(loadTask);
                }
                else
                {
                    var createDataTask = CreateDataAsync(type);
                    loadTaskList.Add(createDataTask);
                }
            }
            var baseLocalDataList = await UniTask.RunOnThreadPool(async () =>
            {
                return await UniTask.WhenAll(loadTaskList);
            });
            await UniTask.SwitchToMainThread();
            for (int i = 0; i < baseLocalDataList.Count(); i++)
            {
                baseLocalDataList[i].SetManager(this);
                baseLocalDataList[i].OnLoad();
            }
            _localDataList.AddRange(baseLocalDataList);
        }

        private async UniTask<BaseLocalData> LoadDataFromLocalAsync(string path, Type type)
        {
            string dataString = await File.ReadAllTextAsync(path).AsUniTask();
            var jsonSerializerSettings = new JsonSerializerSettings()
            {
                Formatting = Formatting.Indented,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                TypeNameHandling = TypeNameHandling.Auto
            };
            var data = JsonConvert.DeserializeObject(dataString, type,jsonSerializerSettings);
            return (BaseLocalData)data;
        }
        
        private async UniTask<BaseLocalData> CreateDataAsync(Type type)
        {
            var data = (BaseLocalData)Activator.CreateInstance(type);
            data.SetManager(this);
            data.OnCreate();
            return data;
        }
    }
}
