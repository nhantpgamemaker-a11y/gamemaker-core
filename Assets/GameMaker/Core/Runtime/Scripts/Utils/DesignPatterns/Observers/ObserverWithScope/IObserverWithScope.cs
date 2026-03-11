using UnityEngine;

namespace GameMaker.Core.Runtime
{
    public interface IObserverWithScope<T,M> where T: IObserverData
    {
        public void OnNotify(ISubjectWithScope<T,M> subject, T data, M scope);
    }
}