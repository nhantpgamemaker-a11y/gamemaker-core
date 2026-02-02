using System.Collections.Generic;

namespace GameMaker.Core.Runtime
{
    public class RuntimeActionManager : AutomaticMonoSingleton<RuntimeActionManager>, ISubjectWithScope<BaseActionData, string>
    {
        protected Dictionary<string, List<IObserverWithScope<BaseActionData, string>>> _observerWithScopeDict = new();
        public void AddObserver(IObserverWithScope<BaseActionData, string> observer, string[] scopes)
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

        public void NotifyObserver(BaseActionData observerData, string scope)
        {
            if(_observerWithScopeDict.TryGetValue(scope, out var observerWithScopes))
            {
                foreach(var observer in observerWithScopes)
                {
                    observer.OnNotify(this, observerData, scope);
                }
            }
        }

        public void RemoveObserver(IObserverWithScope<BaseActionData, string> observer, string[] scopes)
        {
            foreach (var scope in scopes)
            {
                if (_observerWithScopeDict.TryGetValue(scope, out var observerWithScopes))
                {
                    observerWithScopes.Remove(observer);
                    if (observerWithScopes.Count == 0)
                    {
                        _observerWithScopeDict.Remove(scope);
                    }
                }
            }
        }

        public void NotifyAction(string actionDefinitionID, BaseActionData baseActionData)
        {
            NotifyObserver(baseActionData, actionDefinitionID);
        }
    }
}