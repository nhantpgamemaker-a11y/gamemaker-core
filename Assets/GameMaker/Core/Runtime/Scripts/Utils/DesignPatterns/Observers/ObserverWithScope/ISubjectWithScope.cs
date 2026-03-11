using UnityEngine;

namespace GameMaker.Core.Runtime
{
    public interface ISubjectWithScope<T, M> where T: IObserverData
    {
        public void AddObserver(IObserverWithScope<T,M> observer, M[] scopes);
        public void RemoveObserver(IObserverWithScope<T,M> observer, M[] scopes);
        public void NotifyObserver(T observerData, M scope);
    }
}