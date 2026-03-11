using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

namespace GameMaker.Core.Runtime
{
    public interface IStateMachine<T>
    {
        public Dictionary<T, IState<T>> GetStatesLookup();
        public T GetCurrentStateType();
        public IState<T> GetCurrentState();
        public void ChangeState(T stateType, BaseStateData baseStateData = null);
        public void OnInit(List<IState<T>> states);
    }
}
