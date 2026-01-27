using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GameMaker.Core.Runtime
{   
    [System.Serializable]
    public abstract class PlayerDataManager: ISubjectWithScope<BasePlayerData,string>, IObserver<BasePlayerData>
    {
        [UnityEngine.SerializeReference]
        protected List<BasePlayerData> basePlayerDatas;
        protected Dictionary<string, List<IObserverWithScope<BasePlayerData, string>>> _observerWithScopeDict;
        public PlayerDataManager()
        {
            
        }
        public void Initialize(List<BasePlayerData> basePlayerDatas)
        {
            this.basePlayerDatas = basePlayerDatas;
            _observerWithScopeDict = new();
            foreach(var basePlayerData in basePlayerDatas)
            {
                basePlayerData.AddObserver(this);
            }
        }

        public void AddObserver(IObserverWithScope<BasePlayerData,string> observer, string[] scopes)
        {
            foreach(var scope in scopes)
            {
                if(_observerWithScopeDict.TryGetValue(scope, out var observerWithScopes))
                {
                    observerWithScopes.Add(observer);
                }
                else
                {
                    observerWithScopes = new() { observer };
                    _observerWithScopeDict[scope] = observerWithScopes;
                }
            }
        }

        public void RemoveObserver(IObserverWithScope<BasePlayerData,string>  observer, string[] scopes)
        {
            foreach(var scope in scopes)
            {
                if(_observerWithScopeDict.TryGetValue(scope, out var observerWithScopes))
                {
                    observerWithScopes.Remove(observer);
                    if(observerWithScopes.Count == 0){
                        _observerWithScopeDict.Remove(scope);
                    }
                }
            }
        }

        public void NotifyObserver(BasePlayerData observerData, string scope)
        {
            if(_observerWithScopeDict.TryGetValue(scope, out var observerWithScopes))
            {
                foreach(var observer in observerWithScopes)
                {
                    observer.OnNotify(this, observerData, scope);
                }
            }
        }

        protected BasePlayerData GetPlayerData(string referenceId)
        {
            return basePlayerDatas.FirstOrDefault(x => x.GetDefinition().GetID() == referenceId);
        }

        public void OnNotify(ISubject<BasePlayerData> subject, BasePlayerData data)
        {
            var referenceId = data.GetReferenceID();
            NotifyObserver(data, referenceId);
        }
    }
}