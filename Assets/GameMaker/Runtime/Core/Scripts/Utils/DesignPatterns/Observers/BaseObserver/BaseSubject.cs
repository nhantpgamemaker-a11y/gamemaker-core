using System.Collections.Generic;
using UnityEngine;

namespace GameMaker.Core.Runtime
{
    [System.Serializable]
    public class BaseSubject<T> : ISubject<T> where T: IObserverData
    {
        [UnityEngine.SerializeField]
        private T _data;
        [UnityEngine.SerializeField]
        private List<IObserver<T>> _observers;
        public BaseSubject(T observerData)
        {
            _observers = new();
            _data = observerData;
        }

        public void AddObserver(IObserver<T> observer)
        {
            _observers.Add(observer);
        }
        
        public void RemoveObserver(IObserver<T> observer)
        {
            _observers.Remove(observer);
        }
        
        public void NotifyObserver(T observerData)
        {
           foreach(var observer in _observers)
            {
                observer.OnNotify(this, _data);
            }
        }
    }
}