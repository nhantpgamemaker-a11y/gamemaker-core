using UnityEngine;

namespace GameMaker.Core.Runtime
{
    public interface IObserver<T> where T: IObserverData
    {
        public void OnNotify(ISubject<T> subject, T data);
    }
}