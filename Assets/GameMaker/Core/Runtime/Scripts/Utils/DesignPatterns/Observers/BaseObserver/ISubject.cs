using UnityEngine;

namespace GameMaker.Core.Runtime
{
    public interface ISubject<T> where T: IObserverData
    {
        public void AddObserver(IObserver<T> observer);
        public void RemoveObserver(IObserver<T> observer);
        public void NotifyObserver(T observerData);
    }
}